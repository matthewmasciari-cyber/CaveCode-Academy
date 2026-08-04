#!/usr/bin/env python3
from __future__ import annotations

from collections import Counter, defaultdict
from pathlib import Path
import json
import sys
from datetime import datetime, timezone


REPORT_DIR = Path("Archive/Reports")
INPUTS = {
    "build_health": REPORT_DIR / "build-health-pass-84b.json",
    "component_usage": REPORT_DIR / "component-usage-pass-84c.json",
    "theme_accessibility": REPORT_DIR / "theme-accessibility-pass-84d.json",
    "duplicate_assets": REPORT_DIR / "duplicate-assets-pass-84e.json",
}
TEXT_REPORT = REPORT_DIR / "project-health-summary-pass-84f.txt"
JSON_REPORT = REPORT_DIR / "project-health-summary-pass-84f.json"


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


def load_json(root: Path, relative: Path) -> dict:
    path = root / relative

    if not path.is_file():
        raise RuntimeError(
            f"Missing required report: {relative}. "
            "Run Passes 84B through 84E first."
        )

    return json.loads(path.read_text(encoding="utf-8"))


def risk_score_file(
    file_name: str,
    build_health: dict,
    component_usage: dict,
    theme_accessibility: dict,
    duplicate_assets: dict,
) -> tuple[int, list[str]]:
    score = 0
    reasons: list[str] = []

    largest = {
        item["file"]: item
        for item in build_health.get("largest_files", [])
    }

    if file_name in largest:
        item = largest[file_name]
        lines = int(item.get("lines", 0))
        size = int(item.get("bytes", 0))

        if lines >= 2500:
            score += 8
            reasons.append(f"very large file ({lines} lines)")
        elif lines >= 1200:
            score += 5
            reasons.append(f"large file ({lines} lines)")
        elif lines >= 700:
            score += 2
            reasons.append(f"moderately large file ({lines} lines)")

        if size >= 150_000:
            score += 3
            reasons.append(f"large file size ({size} bytes)")

    for item in component_usage.get("large_pages", []):
        if item.get("file") == file_name:
            score += 5
            reasons.append("page flagged for component extraction")

    for item in theme_accessibility.get("files", []):
        if item.get("file") != file_name:
            continue

        risks = item.get("risks", [])
        score += len(risks) * 2

        for risk in risks:
            reasons.append(str(risk))

        small = int(item.get("small_font_count", 0))
        nowrap = int(item.get("nowrap_rules", 0))
        hardcoded = int(item.get("hardcoded_hex_colors", 0))

        if small >= 10:
            score += 3
            reasons.append(f"{small} small-font rules")

        if nowrap >= 5:
            score += 2
            reasons.append(f"{nowrap} nowrap rules")

        if hardcoded >= 20:
            score += 2
            reasons.append(f"{hardcoded} hard-coded colors")

    css_stats = {
        item["file"]: item
        for item in duplicate_assets.get("css", {}).get("file_stats", [])
    }

    if file_name in css_stats:
        selectors = int(css_stats[file_name].get("selector_count", 0))

        if selectors >= 250:
            score += 4
            reasons.append(f"{selectors} CSS selectors")
        elif selectors >= 120:
            score += 2
            reasons.append(f"{selectors} CSS selectors")

    js_stats = {
        item["file"]: item
        for item in duplicate_assets.get("javascript", {}).get("file_stats", [])
    }

    if file_name in js_stats:
        lines = int(js_stats[file_name].get("lines", 0))

        if lines >= 1500:
            score += 5
            reasons.append(f"very large JavaScript file ({lines} lines)")
        elif lines >= 800:
            score += 3
            reasons.append(f"large JavaScript file ({lines} lines)")

    return score, reasons


def build_priorities(
    build_health: dict,
    component_usage: dict,
    theme_accessibility: dict,
    duplicate_assets: dict,
) -> list[dict]:
    files: set[str] = set()

    for item in build_health.get("largest_files", []):
        files.add(item["file"])

    for item in theme_accessibility.get("files", []):
        if item.get("risks"):
            files.add(item["file"])

    for item in duplicate_assets.get("css", {}).get("file_stats", []):
        files.add(item["file"])

    for item in duplicate_assets.get("javascript", {}).get("file_stats", []):
        files.add(item["file"])

    results: list[dict] = []

    for file_name in sorted(files):
        score, reasons = risk_score_file(
            file_name,
            build_health,
            component_usage,
            theme_accessibility,
            duplicate_assets,
        )

        if score <= 0:
            continue

        results.append({
            "file": file_name,
            "score": score,
            "reasons": reasons,
        })

    return sorted(
        results,
        key=lambda item: (-item["score"], item["file"]),
    )


def severity(score: int) -> str:
    if score >= 15:
        return "HIGH"
    if score >= 8:
        return "MEDIUM"
    return "LOW"


def main() -> None:
    root = find_root()

    reports = {
        key: load_json(root, path)
        for key, path in INPUTS.items()
    }

    build_health = reports["build_health"]
    component_usage = reports["component_usage"]
    theme_accessibility = reports["theme_accessibility"]
    duplicate_assets = reports["duplicate_assets"]

    priorities = build_priorities(
        build_health,
        component_usage,
        theme_accessibility,
        duplicate_assets,
    )

    totals = {
        "project_files":
            build_health.get("inventory", {}).get("total_files", 0),
        "total_lines":
            build_health.get("inventory", {}).get("total_lines", 0),
        "routes":
            len(component_usage.get("routes", [])),
        "components":
            len(component_usage.get("components", [])),
        "unused_component_candidates":
            len(component_usage.get("unused_components", [])),
        "large_page_candidates":
            len(component_usage.get("large_pages", [])),
        "theme_accessibility_risk_files":
            theme_accessibility.get("totals", {}).get("files_with_risks", 0),
        "duplicate_css_selectors":
            duplicate_assets.get("totals", {}).get("duplicate_css_selectors", 0),
        "duplicate_js_function_names":
            duplicate_assets.get("totals", {}).get("duplicate_js_function_names", 0),
        "identical_asset_groups":
            duplicate_assets.get("totals", {}).get("identical_asset_groups", 0),
        "likely_orphan_assets":
            len(build_health.get("likely_orphan_assets", [])),
    }

    recommended_order = []

    if component_usage.get("duplicate_routes"):
        recommended_order.append(
            "Resolve duplicate routes before adding new navigation."
        )

    if priorities:
        recommended_order.append(
            "Refactor the highest-scoring files one at a time, starting with the top three."
        )

    if component_usage.get("unused_components"):
        recommended_order.append(
            "Review unused component candidates manually before deleting anything."
        )

    if duplicate_assets.get("assets", {}).get("identical_assets"):
        recommended_order.append(
            "Consolidate byte-identical assets after confirming references."
        )

    if theme_accessibility.get("totals", {}).get("files_with_risks", 0):
        recommended_order.append(
            "Address text clipping, small fonts, and reduced-motion gaps before cosmetic polish."
        )

    if build_health.get("likely_orphan_assets"):
        recommended_order.append(
            "Review likely orphan assets last because dynamic references may be missed."
        )

    summary = {
        "pass": "84F",
        "created_utc": datetime.now(timezone.utc).isoformat(),
        "totals": totals,
        "priority_files": priorities,
        "recommended_order": recommended_order,
        "source_reports": {
            key: str(path)
            for key, path in INPUTS.items()
        },
        "notes": [
            "Scores are heuristic and intended to prioritize review, not prove defects.",
            "No files are modified by this pass.",
            "Deletion should never be automated from these reports alone.",
        ],
    }

    report_dir = root / REPORT_DIR
    report_dir.mkdir(parents=True, exist_ok=True)

    (root / JSON_REPORT).write_text(
        json.dumps(summary, indent=2),
        encoding="utf-8",
        newline="\n",
    )

    lines = [
        "=" * 100,
        "CAVECODE CONSOLIDATED PROJECT HEALTH SUMMARY — PASS 84F",
        "=" * 100,
        "",
        "PROJECT TOTALS",
        "--------------",
    ]

    for key, value in totals.items():
        lines.append(f"{key}: {value}")

    lines += [
        "",
        "PRIORITY FILES",
        "--------------",
    ]

    if priorities:
        for item in priorities[:30]:
            lines.append(
                f"[{severity(int(item['score']))}] "
                f"score {item['score']:>2} | {item['file']}"
            )

            for reason in item["reasons"]:
                lines.append(f"  - {reason}")
    else:
        lines.append("No prioritized risk files were produced.")

    lines += [
        "",
        "RECOMMENDED CLEANUP ORDER",
        "-------------------------",
    ]

    if recommended_order:
        for index, item in enumerate(recommended_order, start=1):
            lines.append(f"{index}. {item}")
    else:
        lines.append("No cleanup recommendations generated.")

    lines += [
        "",
        "TOP UNUSED COMPONENT CANDIDATES",
        "-------------------------------",
    ]

    unused = component_usage.get("unused_components", [])

    if unused:
        for item in unused[:30]:
            lines.append(item)
    else:
        lines.append("None found.")

    lines += [
        "",
        "DUPLICATE ROUTES",
        "----------------",
    ]

    duplicate_routes = component_usage.get("duplicate_routes", {})

    if duplicate_routes:
        for route, files in duplicate_routes.items():
            lines.append(route)
            for file_name in files:
                lines.append(f"  - {file_name}")
    else:
        lines.append("None found.")

    lines += [
        "",
        "OUTPUT",
        "------",
        f"Text report: {TEXT_REPORT}",
        f"JSON report: {JSON_REPORT}",
        "",
        "This pass changed no production files.",
        "",
    ]

    (root / TEXT_REPORT).write_text(
        "\n".join(lines),
        encoding="utf-8",
        newline="\n",
    )

    print("CaveCode Consolidated Project Health Summary Pass 84F completed.")
    print()
    print(f"Text report: {TEXT_REPORT}")
    print(f"JSON report: {JSON_REPORT}")
    print()
    print("No application files were modified.")
    print()
    print("Run:")
    print(f"  cat {TEXT_REPORT}")
    print()
    print("Paste the report back into ChatGPT for the first targeted cleanup pass.")


if __name__ == "__main__":
    try:
        main()
    except Exception as error:
        print(f"ERROR: {error}", file=sys.stderr)
        raise SystemExit(1)
