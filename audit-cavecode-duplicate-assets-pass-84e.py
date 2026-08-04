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
TEXT_REPORT = REPORT_DIR / "duplicate-assets-pass-84e.txt"
JSON_REPORT = REPORT_DIR / "duplicate-assets-pass-84e.json"

EXCLUDED_PARTS = {
    ".git",
    "bin",
    "obj",
    "node_modules",
    "Archive",
}

CSS_SELECTOR = re.compile(
    r"(?P<selector>[^{}@][^{}]*?)\s*\{(?P<body>[^{}]*)\}",
    re.MULTILINE,
)

JS_FUNCTION_PATTERNS = [
    re.compile(
        r"\bfunction\s+([A-Za-z_$][\w$]*)\s*\(",
        re.MULTILINE,
    ),
    re.compile(
        r"\b(?:const|let|var)\s+([A-Za-z_$][\w$]*)\s*=\s*"
        r"(?:async\s*)?\([^)]*\)\s*=>",
        re.MULTILINE,
    ),
    re.compile(
        r"\b([A-Za-z_$][\w$]*)\s*:\s*(?:async\s*)?function\s*\(",
        re.MULTILINE,
    ),
]


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


def files_with_suffix(root: Path, suffix: str) -> list[Path]:
    return sorted(
        path
        for path in root.rglob(f"*{suffix}")
        if path.is_file() and not is_excluded(path, root)
    )


def normalize_css_body(body: str) -> str:
    declarations = []

    for raw in body.split(";"):
        item = re.sub(r"\s+", " ", raw).strip()

        if item:
            declarations.append(item)

    return ";".join(sorted(declarations))


def normalize_selector(selector: str) -> str:
    return re.sub(r"\s+", " ", selector).strip()


def css_audit(files: list[Path], root: Path) -> dict[str, object]:
    selector_locations: dict[str, list[dict[str, object]]] = defaultdict(list)
    body_locations: dict[str, list[dict[str, object]]] = defaultdict(list)
    file_stats: list[dict[str, object]] = []

    for path in files:
        text = path.read_text(encoding="utf-8")
        selector_count = 0

        for match in CSS_SELECTOR.finditer(text):
            selector = normalize_selector(match.group("selector"))
            body = normalize_css_body(match.group("body"))

            if not selector or not body:
                continue

            selector_count += 1
            line = text.count("\n", 0, match.start()) + 1
            location = {
                "file": str(path.relative_to(root)),
                "line": line,
            }

            selector_locations[selector].append(location)
            body_locations[body].append({
                **location,
                "selector": selector,
            })

        file_stats.append({
            "file": str(path.relative_to(root)),
            "bytes": path.stat().st_size,
            "lines": len(text.splitlines()),
            "selector_count": selector_count,
        })

    duplicate_selectors = {
        selector: locations
        for selector, locations in selector_locations.items()
        if len(locations) > 1
    }

    duplicate_bodies = [
        {
            "body": body,
            "locations": locations,
        }
        for body, locations in body_locations.items()
        if len(locations) > 1
    ]

    duplicate_bodies.sort(
        key=lambda item: (
            -len(item["locations"]),
            item["locations"][0]["file"],
        )
    )

    return {
        "file_stats": sorted(
            file_stats,
            key=lambda item: (-item["bytes"], item["file"]),
        ),
        "duplicate_selectors": dict(
            sorted(duplicate_selectors.items())
        ),
        "duplicate_declaration_blocks": duplicate_bodies,
    }


def js_audit(files: list[Path], root: Path) -> dict[str, object]:
    names: dict[str, list[dict[str, object]]] = defaultdict(list)
    file_stats: list[dict[str, object]] = []

    for path in files:
        text = path.read_text(encoding="utf-8")
        found = 0

        for pattern in JS_FUNCTION_PATTERNS:
            for match in pattern.finditer(text):
                name = match.group(1)
                line = text.count("\n", 0, match.start()) + 1

                names[name].append({
                    "file": str(path.relative_to(root)),
                    "line": line,
                })

                found += 1

        file_stats.append({
            "file": str(path.relative_to(root)),
            "bytes": path.stat().st_size,
            "lines": len(text.splitlines()),
            "named_function_count": found,
        })

    duplicate_names = {
        name: locations
        for name, locations in sorted(names.items())
        if len(locations) > 1
    }

    return {
        "file_stats": sorted(
            file_stats,
            key=lambda item: (-item["bytes"], item["file"]),
        ),
        "duplicate_function_names": duplicate_names,
    }


def hash_file(path: Path) -> str:
    digest = hashlib.sha256()

    with path.open("rb") as file:
        for chunk in iter(lambda: file.read(65536), b""):
            digest.update(chunk)

    return digest.hexdigest()


def asset_audit(root: Path) -> dict[str, object]:
    extensions = {
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

    assets = sorted(
        path
        for path in (root / "wwwroot").rglob("*")
        if path.is_file()
        and path.suffix.lower() in extensions
        and not is_excluded(path, root)
    )

    grouped: dict[tuple[int, str], list[Path]] = defaultdict(list)
    file_stats = []

    for path in assets:
        size = path.stat().st_size
        digest = hash_file(path)
        grouped[(size, digest)].append(path)

        file_stats.append({
            "file": str(path.relative_to(root)),
            "bytes": size,
            "sha256": digest,
        })

    duplicates = []

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

    duplicates.sort(
        key=lambda item: (-item["bytes"], item["files"][0])
    )

    return {
        "file_stats": sorted(
            file_stats,
            key=lambda item: (-item["bytes"], item["file"]),
        ),
        "identical_assets": duplicates,
    }


def main() -> None:
    root = find_root()

    css_files = files_with_suffix(root, ".css")
    js_files = files_with_suffix(root, ".js")

    css = css_audit(css_files, root)
    js = js_audit(js_files, root)
    assets = asset_audit(root)

    totals = {
        "css_files": len(css_files),
        "js_files": len(js_files),
        "asset_files": len(assets["file_stats"]),
        "duplicate_css_selectors": len(css["duplicate_selectors"]),
        "duplicate_css_declaration_blocks":
            len(css["duplicate_declaration_blocks"]),
        "duplicate_js_function_names":
            len(js["duplicate_function_names"]),
        "identical_asset_groups":
            len(assets["identical_assets"]),
    }

    report = {
        "pass": "84E",
        "created_utc": datetime.now(timezone.utc).isoformat(),
        "totals": totals,
        "css": css,
        "javascript": js,
        "assets": assets,
        "notes": [
            "Duplicate selector names can be intentional when later rules override earlier ones.",
            "Duplicate JavaScript function names may be scoped to separate IIFEs or modules.",
            "Identical assets are byte-for-byte matches and are strong consolidation candidates.",
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
        "CAVECODE DUPLICATE CSS, JAVASCRIPT, AND ASSET AUDIT — PASS 84E",
        "=" * 100,
        "",
        "TOTALS",
        "------",
    ]

    for key, value in totals.items():
        lines.append(f"{key}: {value}")

    lines += [
        "",
        "LARGEST CSS FILES",
        "-----------------",
    ]

    for item in css["file_stats"][:20]:
        lines.append(
            f"{item['bytes']:>10} bytes | "
            f"{item['lines']:>6} lines | "
            f"{item['selector_count']:>5} selectors | "
            f"{item['file']}"
        )

    lines += [
        "",
        "DUPLICATE CSS SELECTORS",
        "-----------------------",
    ]

    if css["duplicate_selectors"]:
        for selector, locations in css["duplicate_selectors"].items():
            lines.append(selector)

            for location in locations:
                lines.append(
                    f"  - {location['file']}:{location['line']}"
                )
    else:
        lines.append("None found.")

    lines += [
        "",
        "REPEATED CSS DECLARATION BLOCKS",
        "-------------------------------",
    ]

    if css["duplicate_declaration_blocks"]:
        for group in css["duplicate_declaration_blocks"][:40]:
            lines.append(
                f"{len(group['locations'])} matching blocks"
            )

            for location in group["locations"]:
                lines.append(
                    f"  - {location['file']}:{location['line']} "
                    f"[{location['selector']}]"
                )
    else:
        lines.append("None found.")

    lines += [
        "",
        "LARGEST JAVASCRIPT FILES",
        "------------------------",
    ]

    for item in js["file_stats"][:20]:
        lines.append(
            f"{item['bytes']:>10} bytes | "
            f"{item['lines']:>6} lines | "
            f"{item['named_function_count']:>5} named functions | "
            f"{item['file']}"
        )

    lines += [
        "",
        "DUPLICATE JAVASCRIPT FUNCTION NAMES",
        "-----------------------------------",
    ]

    if js["duplicate_function_names"]:
        for name, locations in js["duplicate_function_names"].items():
            lines.append(name)

            for location in locations:
                lines.append(
                    f"  - {location['file']}:{location['line']}"
                )
    else:
        lines.append("None found.")

    lines += [
        "",
        "IDENTICAL ASSET GROUPS",
        "----------------------",
    ]

    if assets["identical_assets"]:
        for group in assets["identical_assets"]:
            lines.append(
                f"{group['bytes']} bytes | "
                f"{group['sha256'][:16]}..."
            )

            for file in group["files"]:
                lines.append(f"  - {file}")
    else:
        lines.append("None found.")

    lines += [
        "",
        "LARGEST ASSETS",
        "--------------",
    ]

    for item in assets["file_stats"][:25]:
        lines.append(
            f"{item['bytes']:>10} bytes | {item['file']}"
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

    print("CaveCode Duplicate Asset Audit Pass 84E completed.")
    print()
    print(f"Text report: {TEXT_REPORT}")
    print(f"JSON report: {JSON_REPORT}")
    print()
    print("No application files were modified.")
    print()
    print("Run:")
    print(f"  cat {TEXT_REPORT}")
    print()
    print("Paste the report back into ChatGPT for Pass 84F.")


if __name__ == "__main__":
    try:
        main()
    except Exception as error:
        print(f"ERROR: {error}", file=sys.stderr)
        raise SystemExit(1)
