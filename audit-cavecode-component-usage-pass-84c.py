#!/usr/bin/env python3
from __future__ import annotations

from collections import defaultdict
from pathlib import Path
import json
import re
import sys
from datetime import datetime, timezone


REPORT_DIR = Path("Archive/Reports")
TEXT_REPORT = REPORT_DIR / "component-usage-pass-84c.txt"
JSON_REPORT = REPORT_DIR / "component-usage-pass-84c.json"

EXCLUDED_PARTS = {
    ".git",
    "bin",
    "obj",
    "node_modules",
    "Archive",
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


def razor_files(root: Path) -> list[Path]:
    return sorted(
        path
        for path in root.rglob("*.razor")
        if path.is_file() and not is_excluded(path, root)
    )


def read_text(path: Path) -> str:
    return path.read_text(encoding="utf-8")


def component_name(path: Path) -> str:
    return path.stem


def collect_routes(files: list[Path], root: Path) -> list[dict[str, str]]:
    route_pattern = re.compile(r'@page\s+"([^"]+)"', re.IGNORECASE)
    routes: list[dict[str, str]] = []

    for path in files:
        text = read_text(path)

        for match in route_pattern.finditer(text):
            routes.append({
                "route": match.group(1),
                "file": str(path.relative_to(root)),
            })

    return sorted(routes, key=lambda item: (item["route"], item["file"]))


def route_duplicates(routes: list[dict[str, str]]) -> dict[str, list[str]]:
    grouped: dict[str, list[str]] = defaultdict(list)

    for item in routes:
        grouped[item["route"]].append(item["file"])

    return {
        route: files
        for route, files in sorted(grouped.items())
        if len(files) > 1
    }


def component_usage(
    files: list[Path],
    root: Path,
) -> dict[str, object]:
    components = {
        component_name(path): path
        for path in files
        if not re.search(
            r'@page\s+"',
            read_text(path),
            flags=re.IGNORECASE,
        )
    }

    usages: dict[str, list[str]] = defaultdict(list)

    for source in files:
        text = read_text(source)

        for name, component_path in components.items():
            if source == component_path:
                continue

            tag_pattern = re.compile(
                rf"<\s*{re.escape(name)}(?:\s|/|>)",
                re.IGNORECASE,
            )

            if tag_pattern.search(text):
                usages[name].append(str(source.relative_to(root)))

    records: list[dict[str, object]] = []
    unused: list[str] = []

    for name, path in sorted(components.items()):
        used_by = sorted(set(usages.get(name, [])))

        record = {
            "component": name,
            "file": str(path.relative_to(root)),
            "used_by": used_by,
            "usage_count": len(used_by),
        }

        records.append(record)

        if not used_by:
            unused.append(str(path.relative_to(root)))

    return {
        "records": records,
        "unused_components": unused,
    }


def page_component_usage(
    files: list[Path],
    root: Path,
    component_records: list[dict[str, object]],
) -> list[dict[str, object]]:
    component_names = [
        record["component"]
        for record in component_records
    ]

    pages: list[dict[str, object]] = []

    for path in files:
        text = read_text(path)

        if "@page" not in text:
            continue

        used_components: list[str] = []

        for name in component_names:
            if re.search(
                rf"<\s*{re.escape(str(name))}(?:\s|/|>)",
                text,
                flags=re.IGNORECASE,
            ):
                used_components.append(str(name))

        routes = re.findall(
            r'@page\s+"([^"]+)"',
            text,
            flags=re.IGNORECASE,
        )

        pages.append({
            "file": str(path.relative_to(root)),
            "routes": routes,
            "components": sorted(used_components),
            "component_count": len(used_components),
            "lines": len(text.splitlines()),
        })

    return sorted(pages, key=lambda item: item["file"])


def suspicious_large_pages(
    pages: list[dict[str, object]],
) -> list[dict[str, object]]:
    return [
        page
        for page in pages
        if int(page["lines"]) >= 1200
    ]


def main() -> None:
    root = find_root()
    files = razor_files(root)

    routes = collect_routes(files, root)
    duplicates = route_duplicates(routes)
    usage = component_usage(files, root)
    pages = page_component_usage(
        files,
        root,
        usage["records"],
    )
    large_pages = suspicious_large_pages(pages)

    report = {
        "pass": "84C",
        "created_utc": datetime.now(timezone.utc).isoformat(),
        "routes": routes,
        "duplicate_routes": duplicates,
        "components": usage["records"],
        "unused_components": usage["unused_components"],
        "pages": pages,
        "large_pages": large_pages,
        "notes": [
            "Component usage is based on direct Razor tag references.",
            "Dynamic rendering and reflection-based usage may not be detected.",
            "Unused does not automatically mean safe to delete.",
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
        "CAVECODE COMPONENT AND ROUTE USAGE — PASS 84C",
        "=" * 100,
        "",
        "ROUTES",
        "------",
    ]

    for item in routes:
        lines.append(f"{item['route']:<42} {item['file']}")

    lines += [
        "",
        "DUPLICATE ROUTES",
        "----------------",
    ]

    if duplicates:
        for route, locations in duplicates.items():
            lines.append(route)
            for location in locations:
                lines.append(f"  - {location}")
    else:
        lines.append("None found.")

    lines += [
        "",
        "COMPONENT USAGE",
        "---------------",
    ]

    for record in usage["records"]:
        lines.append(
            f"{record['component']} "
            f"({record['usage_count']} references)"
        )
        lines.append(f"  file: {record['file']}")

        if record["used_by"]:
            for location in record["used_by"]:
                lines.append(f"  used by: {location}")
        else:
            lines.append("  used by: none detected")

    lines += [
        "",
        "UNUSED COMPONENT CANDIDATES",
        "---------------------------",
    ]

    lines.extend(
        usage["unused_components"]
        or ["None found."]
    )

    lines += [
        "",
        "PAGE INVENTORY",
        "--------------",
    ]

    for page in pages:
        route_text = ", ".join(page["routes"]) or "[no route]"
        component_text = ", ".join(page["components"]) or "[none]"

        lines.append(page["file"])
        lines.append(f"  routes: {route_text}")
        lines.append(f"  lines: {page['lines']}")
        lines.append(f"  direct components: {component_text}")

    lines += [
        "",
        "LARGE PAGE CANDIDATES",
        "---------------------",
        "Pages at or above 1,200 lines may benefit from component extraction.",
    ]

    if large_pages:
        for page in large_pages:
            lines.append(
                f"{page['lines']:>6} lines | {page['file']}"
            )
    else:
        lines.append("None found.")

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

    print("CaveCode Component and Route Usage Pass 84C completed.")
    print()
    print(f"Text report: {TEXT_REPORT}")
    print(f"JSON report: {JSON_REPORT}")
    print()
    print("No application files were modified.")
    print()
    print("Run:")
    print(f"  cat {TEXT_REPORT}")
    print()
    print("Paste the report back into ChatGPT for Pass 84D.")


if __name__ == "__main__":
    try:
        main()
    except Exception as error:
        print(f"ERROR: {error}", file=sys.stderr)
        raise SystemExit(1)
