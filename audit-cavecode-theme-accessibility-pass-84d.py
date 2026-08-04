#!/usr/bin/env python3
from __future__ import annotations

from collections import Counter
from pathlib import Path
import json
import re
import sys
from datetime import datetime, timezone


REPORT_DIR = Path("Archive/Reports")
TEXT_REPORT = REPORT_DIR / "theme-accessibility-pass-84d.txt"
JSON_REPORT = REPORT_DIR / "theme-accessibility-pass-84d.json"

EXCLUDED_PARTS = {
    ".git",
    "bin",
    "obj",
    "node_modules",
    "Archive",
}

TEXT_EXTENSIONS = {
    ".razor",
    ".css",
    ".html",
    ".js",
}

HEX_COLOR = re.compile(r"#[0-9a-fA-F]{3,8}\b")
RGB_COLOR = re.compile(r"\brgba?\s*\(", re.IGNORECASE)
HSL_COLOR = re.compile(r"\bhsla?\s*\(", re.IGNORECASE)
VAR_COLOR = re.compile(r"var\(\s*--[A-Za-z0-9_-]+")
TRANSITION = re.compile(r"\btransition(?:-property)?\s*:", re.IGNORECASE)
ANIMATION = re.compile(r"\banimation(?:-name)?\s*:", re.IGNORECASE)
REDUCED_MOTION = re.compile(
    r"prefers-reduced-motion|reduced-motion|ReducedMotion",
    re.IGNORECASE,
)
FIXED_FONT_PX = re.compile(
    r"font-size\s*:\s*(\d+(?:\.\d+)?)px",
    re.IGNORECASE,
)
OVERFLOW_HIDDEN = re.compile(r"overflow\s*:\s*hidden", re.IGNORECASE)
WHITE_SPACE_NOWRAP = re.compile(r"white-space\s*:\s*nowrap", re.IGNORECASE)
FIXED_HEIGHT = re.compile(
    r"(?:height|min-height|max-height)\s*:\s*(\d+(?:\.\d+)?)px",
    re.IGNORECASE,
)


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


def candidate_files(root: Path) -> list[Path]:
    return sorted(
        path
        for path in root.rglob("*")
        if path.is_file()
        and path.suffix.lower() in TEXT_EXTENSIONS
        and not is_excluded(path, root)
    )


def read_text(path: Path) -> str:
    return path.read_text(encoding="utf-8")


def line_number(text: str, index: int) -> int:
    return text.count("\n", 0, index) + 1


def matched_lines(
    text: str,
    pattern: re.Pattern[str],
    limit: int = 20,
) -> list[dict[str, object]]:
    results: list[dict[str, object]] = []

    for match in pattern.finditer(text):
        line = line_number(text, match.start())
        source_line = text.splitlines()[line - 1].strip()

        results.append({
            "line": line,
            "text": source_line[:220],
        })

        if len(results) >= limit:
            break

    return results


def analyze_file(path: Path, root: Path) -> dict[str, object]:
    text = read_text(path)

    hardcoded = len(HEX_COLOR.findall(text))
    rgb = len(RGB_COLOR.findall(text))
    hsl = len(HSL_COLOR.findall(text))
    theme_vars = len(VAR_COLOR.findall(text))
    transitions = len(TRANSITION.findall(text))
    animations = len(ANIMATION.findall(text))
    reduced_motion = bool(REDUCED_MOTION.search(text))
    nowrap = len(WHITE_SPACE_NOWRAP.findall(text))
    overflow_hidden = len(OVERFLOW_HIDDEN.findall(text))
    fixed_heights = [
        float(match.group(1))
        for match in FIXED_HEIGHT.finditer(text)
    ]
    small_fonts = [
        float(match.group(1))
        for match in FIXED_FONT_PX.finditer(text)
        if float(match.group(1)) < 11
    ]

    risks: list[str] = []

    if hardcoded + rgb + hsl >= 8 and theme_vars < 3:
        risks.append(
            "Heavy use of hard-coded colors with little theme-variable usage."
        )

    if (transitions or animations) and not reduced_motion:
        risks.append(
            "Contains motion but no reduced-motion handling was detected."
        )

    if nowrap and overflow_hidden:
        risks.append(
            "Uses both nowrap and overflow:hidden; text clipping is possible."
        )

    if any(value <= 48 for value in fixed_heights) and small_fonts:
        risks.append(
            "Small fixed-height controls combined with sub-11px text may clip under text scaling."
        )

    if len(small_fonts) >= 5:
        risks.append(
            "Contains many font sizes below 11px; readability may suffer."
        )

    return {
        "file": str(path.relative_to(root)),
        "hardcoded_hex_colors": hardcoded,
        "rgb_colors": rgb,
        "hsl_colors": hsl,
        "theme_variable_references": theme_vars,
        "transitions": transitions,
        "animations": animations,
        "reduced_motion_detected": reduced_motion,
        "nowrap_rules": nowrap,
        "overflow_hidden_rules": overflow_hidden,
        "fixed_height_count": len(fixed_heights),
        "small_font_count": len(small_fonts),
        "risks": risks,
        "hardcoded_color_examples": matched_lines(text, HEX_COLOR, limit=10),
        "small_font_examples": matched_lines(text, FIXED_FONT_PX, limit=10),
        "nowrap_examples": matched_lines(text, WHITE_SPACE_NOWRAP, limit=10),
    }


def main() -> None:
    root = find_root()
    files = candidate_files(root)
    analyses = [analyze_file(path, root) for path in files]

    risk_files = [
        item
        for item in analyses
        if item["risks"]
    ]

    totals = {
        "files_scanned": len(analyses),
        "files_with_risks": len(risk_files),
        "hardcoded_hex_colors": sum(
            int(item["hardcoded_hex_colors"])
            for item in analyses
        ),
        "theme_variable_references": sum(
            int(item["theme_variable_references"])
            for item in analyses
        ),
        "transitions": sum(
            int(item["transitions"])
            for item in analyses
        ),
        "animations": sum(
            int(item["animations"])
            for item in analyses
        ),
        "files_with_reduced_motion_support": sum(
            1
            for item in analyses
            if item["reduced_motion_detected"]
        ),
        "nowrap_rules": sum(
            int(item["nowrap_rules"])
            for item in analyses
        ),
        "overflow_hidden_rules": sum(
            int(item["overflow_hidden_rules"])
            for item in analyses
        ),
        "small_font_rules": sum(
            int(item["small_font_count"])
            for item in analyses
        ),
    }

    risk_counter = Counter(
        risk
        for item in risk_files
        for risk in item["risks"]
    )

    report = {
        "pass": "84D",
        "created_utc": datetime.now(timezone.utc).isoformat(),
        "totals": totals,
        "risk_summary": dict(risk_counter),
        "files": analyses,
        "notes": [
            "This is a static audit and does not measure actual rendered contrast ratios.",
            "Hard-coded colors may be intentional for logos, code editors, or preview swatches.",
            "Motion support may be provided globally and not detected in the same file.",
            "Small text findings are candidates for review, not automatic failures.",
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
        "CAVECODE THEME AND ACCESSIBILITY COVERAGE — PASS 84D",
        "=" * 100,
        "",
        "TOTALS",
        "------",
    ]

    for key, value in totals.items():
        lines.append(f"{key}: {value}")

    lines += [
        "",
        "RISK SUMMARY",
        "------------",
    ]

    if risk_counter:
        for risk, count in risk_counter.most_common():
            lines.append(f"{count:>4} | {risk}")
    else:
        lines.append("No static risk patterns detected.")

    lines += [
        "",
        "FILES REQUIRING REVIEW",
        "----------------------",
    ]

    if risk_files:
        for item in risk_files:
            lines.append(item["file"])

            for risk in item["risks"]:
                lines.append(f"  - {risk}")

            lines.append(
                "  counts: "
                f"hex={item['hardcoded_hex_colors']}, "
                f"vars={item['theme_variable_references']}, "
                f"motion={item['transitions'] + item['animations']}, "
                f"small-font={item['small_font_count']}, "
                f"nowrap={item['nowrap_rules']}, "
                f"overflow-hidden={item['overflow_hidden_rules']}"
            )
    else:
        lines.append("None found.")

    lines += [
        "",
        "TOP HARDCODED-COLOR FILES",
        "-------------------------",
    ]

    for item in sorted(
        analyses,
        key=lambda value: (
            -int(value["hardcoded_hex_colors"]),
            value["file"],
        ),
    )[:20]:
        lines.append(
            f"{item['hardcoded_hex_colors']:>4} hex colors | "
            f"{item['theme_variable_references']:>4} theme vars | "
            f"{item['file']}"
        )

    lines += [
        "",
        "TOP SMALL-TEXT FILES",
        "--------------------",
    ]

    for item in sorted(
        analyses,
        key=lambda value: (
            -int(value["small_font_count"]),
            value["file"],
        ),
    )[:20]:
        lines.append(
            f"{item['small_font_count']:>4} sub-11px rules | "
            f"{item['file']}"
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

    print("CaveCode Theme and Accessibility Audit Pass 84D completed.")
    print()
    print(f"Text report: {TEXT_REPORT}")
    print(f"JSON report: {JSON_REPORT}")
    print()
    print("No application files were modified.")
    print()
    print("Run:")
    print(f"  cat {TEXT_REPORT}")
    print()
    print("Paste the report back into ChatGPT for Pass 84E.")


if __name__ == "__main__":
    try:
        main()
    except Exception as error:
        print(f"ERROR: {error}", file=sys.stderr)
        raise SystemExit(1)
