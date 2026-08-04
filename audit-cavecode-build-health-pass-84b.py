#!/usr/bin/env python3
from __future__ import annotations

from collections import Counter, defaultdict
from pathlib import Path
import hashlib
import json
import re
import sys
from datetime import datetime, timezone


REPORT_DIR = Path("Archive/Reports")
TEXT_REPORT = REPORT_DIR / "build-health-pass-84b.txt"
JSON_REPORT = REPORT_DIR / "build-health-pass-84b.json"

EXCLUDED_PARTS = {
    ".git",
    "bin",
    "obj",
    "node_modules",
    "Archive",
}

SOURCE_EXTENSIONS = {
    ".razor",
    ".cs",
    ".js",
    ".css",
    ".html",
    ".json",
    ".md",
    ".sql",
    ".svg",
}

ASSET_EXTENSIONS = {
    ".png",
    ".jpg",
    ".jpeg",
    ".webp",
    ".gif",
    ".svg",
    ".ico",
    ".woff",
    ".woff2",
    ".ttf",
    ".otf",
}


def find_root() -> Path:
    root = Path.cwd().resolve()

    while root.parent != root and not (root / "CaveCode.csproj").is_file():
        root = root.parent

    if not (root / "CaveCode.csproj").is_file():
        raise RuntimeError(
            "Could not find CaveCode.csproj. "
            "Run this from /workspaces/CaveCode-Academy."
        )

    return root


def is_excluded(path: Path, root: Path) -> bool:
    relative = path.relative_to(root)
    return any(part in EXCLUDED_PARTS for part in relative.parts)


def project_files(root: Path) -> list[Path]:
    return sorted(
        path
        for path in root.rglob("*")
        if path.is_file() and not is_excluded(path, root)
    )


def line_count(path: Path) -> int:
    try:
        return len(path.read_text(encoding="utf-8").splitlines())
    except (UnicodeDecodeError, OSError):
        return 0


def sha256(path: Path) -> str:
    digest = hashlib.sha256()

    with path.open("rb") as file:
        for chunk in iter(lambda: file.read(65536), b""):
            digest.update(chunk)

    return digest.hexdigest()


def collect_routes(files: list[Path], root: Path) -> list[dict[str, str]]:
    routes: list[dict[str, str]] = []
    pattern = re.compile(r'@page\s+"([^"]+)"', re.IGNORECASE)

    for path in files:
        if path.suffix.lower() != ".razor":
            continue

        text = path.read_text(encoding="utf-8")

        for match in pattern.finditer(text):
            routes.append({
                "route": match.group(1),
                "file": str(path.relative_to(root)),
            })

    return sorted(routes, key=lambda item: item["route"])


def duplicate_filenames(files: list[Path], root: Path) -> dict[str, list[str]]:
    grouped: dict[str, list[str]] = defaultdict(list)

    for path in files:
        grouped[path.name].append(str(path.relative_to(root)))

    return {
        name: locations
        for name, locations in sorted(grouped.items())
        if len(locations) > 1
    }


def duplicate_content(files: list[Path], root: Path) -> list[dict[str, object]]:
    grouped: dict[tuple[int, str], list[Path]] = defaultdict(list)

    for path in files:
        if path.stat().st_size == 0:
            continue

        if path.suffix.lower() not in SOURCE_EXTENSIONS | ASSET_EXTENSIONS:
            continue

        key = (path.stat().st_size, sha256(path))
        grouped[key].append(path)

    duplicates: list[dict[str, object]] = []

    for (size, digest), paths in grouped.items():
        if len(paths) < 2:
            continue

        duplicates.append({
            "bytes": size,
            "sha256": digest,
            "files": [
                str(path.relative_to(root))
                for path in sorted(paths)
            ],
        })

    return sorted(
        duplicates,
        key=lambda item: (-item["bytes"], item["files"][0]),
    )


def empty_directories(root: Path) -> list[str]:
    output: list[str] = []

    for path in sorted(root.rglob("*")):
        if not path.is_dir() or is_excluded(path, root):
            continue

        try:
            if not any(path.iterdir()):
                output.append(str(path.relative_to(root)))
        except OSError:
            continue

    return output


def referenced_assets(files: list[Path], root: Path) -> set[str]:
    references: set[str] = set()

    patterns = [
        re.compile(
            r'(?:src|href)\s*=\s*["\']([^"\']+)["\']',
            re.IGNORECASE,
        ),
        re.compile(
            r'url\(\s*["\']?([^)"\']+)["\']?\s*\)',
            re.IGNORECASE,
        ),
    ]

    for path in files:
        if path.suffix.lower() not in {
            ".razor",
            ".html",
            ".css",
            ".js",
            ".cs",
        }:
            continue

        try:
            text = path.read_text(encoding="utf-8")
        except UnicodeDecodeError:
            continue

        for pattern in patterns:
            for match in pattern.finditer(text):
                value = match.group(1).split("?")[0].split("#")[0]
                value = value.lstrip("~/")
                references.add(value)

    return references


def likely_orphan_assets(
    files: list[Path],
    root: Path,
) -> list[str]:
    refs = referenced_assets(files, root)
    assets_root = root / "wwwroot"

    if not assets_root.exists():
        return []

    assets = [
        path
        for path in assets_root.rglob("*")
        if path.is_file()
        and path.suffix.lower() in ASSET_EXTENSIONS
    ]

    orphans: list[str] = []

    for path in assets:
        relative_to_wwwroot = str(
            path.relative_to(assets_root)
        ).replace("\\", "/")

        basename = path.name

        if (
            relative_to_wwwroot not in refs
            and basename not in refs
            and not any(
                reference.endswith("/" + basename)
                for reference in refs
            )
        ):
            orphans.append(
                str(path.relative_to(root))
            )

    return sorted(orphans)


def largest_files(files: list[Path], root: Path) -> list[dict[str, object]]:
    records = []

    for path in files:
        records.append({
            "file": str(path.relative_to(root)),
            "bytes": path.stat().st_size,
            "lines": line_count(path),
        })

    return sorted(
        records,
        key=lambda item: (-item["bytes"], item["file"]),
    )[:30]


def build_inventory(files: list[Path], root: Path) -> dict[str, object]:
    suffix_counts = Counter(
        path.suffix.lower() or "[no extension]"
        for path in files
    )

    folder_counts = Counter(
        path.relative_to(root).parts[0]
        for path in files
    )

    return {
        "total_files": len(files),
        "total_lines": sum(line_count(path) for path in files),
        "file_types": dict(sorted(suffix_counts.items())),
        "top_level_file_counts": dict(sorted(folder_counts.items())),
        "razor_pages": sum(
            1
            for path in files
            if path.suffix.lower() == ".razor"
            and "@page" in path.read_text(encoding="utf-8")
        ),
        "razor_components": sum(
            1
            for path in files
            if path.suffix.lower() == ".razor"
            and "@page" not in path.read_text(encoding="utf-8")
        ),
        "services": sum(
            1
            for path in files
            if path.suffix.lower() == ".cs"
            and "Service" in path.name
        ),
        "javascript_files": sum(
            1 for path in files if path.suffix.lower() == ".js"
        ),
        "css_files": sum(
            1 for path in files if path.suffix.lower() == ".css"
        ),
        "svg_files": sum(
            1 for path in files if path.suffix.lower() == ".svg"
        ),
    }


def main() -> None:
    root = find_root()
    files = project_files(root)

    inventory = build_inventory(files, root)
    routes = collect_routes(files, root)
    filename_duplicates = duplicate_filenames(files, root)
    content_duplicates = duplicate_content(files, root)
    empty_dirs = empty_directories(root)
    orphan_assets = likely_orphan_assets(files, root)
    largest = largest_files(files, root)

    report = {
        "pass": "84B",
        "created_utc": datetime.now(timezone.utc).isoformat(),
        "inventory": inventory,
        "routes": routes,
        "duplicate_filenames": filename_duplicates,
        "duplicate_content": content_duplicates,
        "empty_directories": empty_dirs,
        "likely_orphan_assets": orphan_assets,
        "largest_files": largest,
        "notes": [
            "Orphan asset detection is conservative and may include files loaded dynamically.",
            "Duplicate content does not automatically mean a file should be deleted.",
            "Archive, bin, obj, node_modules, and .git are excluded.",
        ],
    }

    report_dir = root / REPORT_DIR
    report_dir.mkdir(parents=True, exist_ok=True)

    (root / JSON_REPORT).write_text(
        json.dumps(report, indent=2),
        encoding="utf-8",
        newline="\n",
    )

    lines = [
        "=" * 100,
        "CAVECODE BUILD HEALTH INVENTORY — PASS 84B",
        "=" * 100,
        "",
        "INVENTORY",
        "---------",
    ]

    for key, value in inventory.items():
        lines.append(f"{key}: {value}")

    lines += [
        "",
        "ROUTES",
        "------",
    ]

    for item in routes:
        lines.append(f"{item['route']:<40} {item['file']}")

    lines += [
        "",
        "LARGEST FILES",
        "-------------",
    ]

    for item in largest:
        lines.append(
            f"{item['bytes']:>10} bytes | "
            f"{item['lines']:>7} lines | "
            f"{item['file']}"
        )

    lines += [
        "",
        "DUPLICATE FILENAMES",
        "-------------------",
    ]

    if filename_duplicates:
        for name, locations in filename_duplicates.items():
            lines.append(name)
            for location in locations:
                lines.append(f"  - {location}")
    else:
        lines.append("None found.")

    lines += [
        "",
        "IDENTICAL FILE CONTENT",
        "----------------------",
    ]

    if content_duplicates:
        for group in content_duplicates:
            lines.append(
                f"{group['bytes']} bytes | {group['sha256'][:16]}..."
            )
            for location in group["files"]:
                lines.append(f"  - {location}")
    else:
        lines.append("None found.")

    lines += [
        "",
        "EMPTY DIRECTORIES",
        "-----------------",
    ]

    lines.extend(
        empty_dirs
        or ["None found."]
    )

    lines += [
        "",
        "LIKELY ORPHAN ASSETS",
        "--------------------",
        "Review before deleting. Dynamic references may not be detected.",
    ]

    lines.extend(
        orphan_assets
        or ["None found."]
    )

    lines += [
        "",
        "OUTPUT",
        "------",
        f"Text report: {TEXT_REPORT}",
        f"JSON report: {JSON_REPORT}",
        "",
    ]

    (root / TEXT_REPORT).write_text(
        "\n".join(lines),
        encoding="utf-8",
        newline="\n",
    )

    print("CaveCode Build Health Inventory Pass 84B completed.")
    print()
    print(f"Text report: {TEXT_REPORT}")
    print(f"JSON report: {JSON_REPORT}")
    print()
    print("No application files were modified.")
    print()
    print("Run:")
    print(f"  cat {TEXT_REPORT}")
    print()
    print("Paste the report back into ChatGPT for Pass 84C.")


if __name__ == "__main__":
    try:
        main()
    except Exception as error:
        print(f"ERROR: {error}", file=sys.stderr)
        raise SystemExit(1)
