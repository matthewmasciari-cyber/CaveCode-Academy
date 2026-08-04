#!/usr/bin/env python3
from __future__ import annotations

from collections import defaultdict
from pathlib import Path
import json
import re
import sys
from datetime import datetime, timezone


TARGET = Path("Pages/Python.razor")
REPORT_DIR = Path("Archive/Reports")
TEXT_REPORT = REPORT_DIR / "python-page-decomposition-pass-85a.txt"
JSON_REPORT = REPORT_DIR / "python-page-decomposition-pass-85a.json"


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


def line_number(text: str, index: int) -> int:
    return text.count("\n", 0, index) + 1


def find_tag_sections(text: str) -> list[dict[str, object]]:
    patterns = [
        ("section", re.compile(r"<section\b[^>]*>", re.IGNORECASE)),
        ("article", re.compile(r"<article\b[^>]*>", re.IGNORECASE)),
        ("header", re.compile(r"<header\b[^>]*>", re.IGNORECASE)),
        ("aside", re.compile(r"<aside\b[^>]*>", re.IGNORECASE)),
        ("main", re.compile(r"<main\b[^>]*>", re.IGNORECASE)),
        ("nav", re.compile(r"<nav\b[^>]*>", re.IGNORECASE)),
    ]

    results: list[dict[str, object]] = []

    for tag, pattern in patterns:
        for match in pattern.finditer(text):
            markup = match.group(0)
            class_match = re.search(r'class\s*=\s*"([^"]+)"', markup)
            id_match = re.search(r'id\s*=\s*"([^"]+)"', markup)

            results.append({
                "tag": tag,
                "line": line_number(text, match.start()),
                "class": class_match.group(1) if class_match else "",
                "id": id_match.group(1) if id_match else "",
                "markup": markup[:220],
            })

    return sorted(results, key=lambda item: item["line"])


def find_components(text: str) -> list[dict[str, object]]:
    native = {
        "div", "span", "section", "article", "header", "footer", "main",
        "nav", "aside", "button", "input", "textarea", "select", "option",
        "label", "form", "p", "h1", "h2", "h3", "h4", "h5", "h6",
        "small", "strong", "em", "code", "pre", "ul", "ol", "li", "table",
        "thead", "tbody", "tr", "th", "td", "img", "svg", "path", "circle",
        "line", "polyline", "polygon", "rect", "a", "style", "link", "meta",
    }

    pattern = re.compile(r"<([A-Z][A-Za-z0-9_.]*)\b")
    grouped: dict[str, list[int]] = defaultdict(list)

    for match in pattern.finditer(text):
        name = match.group(1)

        if name.lower() in native:
            continue

        grouped[name].append(line_number(text, match.start()))

    return [
        {
            "component": name,
            "count": len(lines),
            "lines": lines[:20],
        }
        for name, lines in sorted(grouped.items())
    ]


def find_code_block(text: str) -> tuple[int, str]:
    match = re.search(r"(?m)^@code\s*\{", text)

    if not match:
        return 0, ""

    return line_number(text, match.start()), text[match.end():]


def find_methods(code: str, code_start_line: int) -> list[dict[str, object]]:
    method_pattern = re.compile(
        r"(?m)^[ \t]*(?:private|protected|public|internal)\s+"
        r"(?:async\s+)?"
        r"(?:Task|ValueTask|void|bool|int|string|double|decimal|"
        r"[A-Z][A-Za-z0-9_<>,?\[\]. ]+)\s+"
        r"([A-Za-z_][A-Za-z0-9_]*)\s*\(",
    )

    methods = []

    for match in method_pattern.finditer(code):
        local_line = line_number(code, match.start())
        methods.append({
            "name": match.group(1),
            "line": code_start_line + local_line,
            "signature": match.group(0).strip()[:220],
        })

    return methods


def find_fields(code: str, code_start_line: int) -> list[dict[str, object]]:
    field_pattern = re.compile(
        r"(?m)^[ \t]*private\s+(?:static\s+)?(?:readonly\s+)?"
        r"([A-Za-z_][A-Za-z0-9_<>,?\[\]. ]+)\s+"
        r"([A-Za-z_][A-Za-z0-9_]*)\s*(?:=|;|=>)",
    )

    fields = []

    for match in field_pattern.finditer(code):
        local_line = line_number(code, match.start())
        fields.append({
            "type": re.sub(r"\s+", " ", match.group(1)).strip(),
            "name": match.group(2),
            "line": code_start_line + local_line,
        })

    return fields


def find_css(text: str) -> dict[str, object]:
    style_match = re.search(
        r"<style[^>]*>(.*?)</style>",
        text,
        flags=re.IGNORECASE | re.DOTALL,
    )

    if not style_match:
        return {
            "start_line": 0,
            "line_count": 0,
            "selectors": [],
        }

    css = style_match.group(1)
    start_line = line_number(text, style_match.start())

    selector_pattern = re.compile(
        r"(?m)([^{}@][^{}]*?)\s*\{",
    )

    selectors = []

    for match in selector_pattern.finditer(css):
        selector = re.sub(r"\s+", " ", match.group(1)).strip()

        if not selector:
            continue

        selectors.append({
            "selector": selector[:220],
            "line": start_line + line_number(css, match.start()),
        })

    return {
        "start_line": start_line,
        "line_count": len(css.splitlines()),
        "selectors": selectors,
    }


def infer_boundaries(
    sections: list[dict[str, object]],
    methods: list[dict[str, object]],
    fields: list[dict[str, object]],
    components: list[dict[str, object]],
    css: dict[str, object],
) -> list[dict[str, object]]:
    boundaries: list[dict[str, object]] = []

    keyword_groups = {
        "CourseHeader": ("header", "hero", "course-header", "academy-header"),
        "ModuleNavigation": ("module", "chapter", "lesson", "navigation"),
        "StageProgressPanel": ("stage", "progress", "mastery"),
        "LearningTerminal": ("terminal", "console", "output"),
        "CodeEditorPanel": ("editor", "code", "input"),
        "ReferencePanel": ("reference", "hint", "example"),
        "GamePreviewPanel": ("preview", "simulation", "world", "control-room"),
        "RewardPanel": ("reward", "xp", "crystal", "achievement"),
    }

    searchable = [
        {
            "line": item["line"],
            "text": " ".join([
                str(item.get("class", "")),
                str(item.get("id", "")),
                str(item.get("markup", "")),
            ]).lower(),
        }
        for item in sections
    ]

    for suggested, keywords in keyword_groups.items():
        matching = [
            item["line"]
            for item in searchable
            if any(keyword in item["text"] for keyword in keywords)
        ]

        if matching:
            boundaries.append({
                "suggested_component": suggested,
                "evidence_lines": sorted(set(matching))[:20],
                "reason": "Matching structural class/id names were detected.",
            })

    if css.get("line_count", 0) >= 500:
        boundaries.append({
            "suggested_component": "PythonCourseStyles.css",
            "evidence_lines": [css.get("start_line", 0)],
            "reason": (
                f"Embedded style block contains {css.get('line_count', 0)} lines "
                "and should likely move to a dedicated stylesheet."
            ),
        })

    if len(methods) >= 30:
        boundaries.append({
            "suggested_component": "PythonCoursePageState",
            "evidence_lines": [methods[0]["line"] if methods else 0],
            "reason": (
                f"The page contains {len(methods)} detected methods; "
                "state and orchestration may benefit from a page-state class."
            ),
        })

    if len(fields) >= 20:
        boundaries.append({
            "suggested_component": "PythonCourseViewModel",
            "evidence_lines": [fields[0]["line"] if fields else 0],
            "reason": (
                f"The page contains {len(fields)} detected private fields/properties."
            ),
        })

    return boundaries


def main() -> None:
    root = find_root()
    target = root / TARGET

    if not target.is_file():
        raise RuntimeError(f"Missing {TARGET}")

    text = target.read_text(encoding="utf-8")
    total_lines = len(text.splitlines())

    sections = find_tag_sections(text)
    components = find_components(text)
    code_start_line, code = find_code_block(text)
    methods = find_methods(code, code_start_line)
    fields = find_fields(code, code_start_line)
    css = find_css(text)

    boundaries = infer_boundaries(
        sections,
        methods,
        fields,
        components,
        css,
    )

    report = {
        "pass": "85A",
        "created_utc": datetime.now(timezone.utc).isoformat(),
        "target": str(TARGET),
        "total_lines": total_lines,
        "structural_sections": sections,
        "existing_components": components,
        "code_start_line": code_start_line,
        "methods": methods,
        "fields": fields,
        "embedded_css": css,
        "suggested_boundaries": boundaries,
        "safety_rules_for_85b": [
            "Extract markup-only components first.",
            "Keep ProgressionService, validation, and resume orchestration in Python.razor initially.",
            "Do not move mutable page state during the first extraction.",
            "Pass values into child components using parameters.",
            "Pass actions back using EventCallback.",
            "Build and test after each individual component extraction.",
            "Do not change routes, storage keys, XP awards, or lesson identifiers.",
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
        "CAVECODE PYTHON PAGE DECOMPOSITION MAP — PASS 85A",
        "=" * 100,
        "",
        f"Target: {TARGET}",
        f"Total lines: {total_lines}",
        f"@code begins near line: {code_start_line}",
        f"Detected methods: {len(methods)}",
        f"Detected fields/properties: {len(fields)}",
        f"Embedded CSS lines: {css.get('line_count', 0)}",
        "",
        "EXISTING CHILD COMPONENTS",
        "-------------------------",
    ]

    if components:
        for item in components:
            line_text = ", ".join(str(line) for line in item["lines"])
            lines.append(
                f"{item['component']}: {item['count']} use(s) "
                f"at lines {line_text}"
            )
    else:
        lines.append("No child components detected.")

    lines += [
        "",
        "STRUCTURAL SECTIONS",
        "-------------------",
    ]

    for item in sections:
        label = item["class"] or item["id"] or item["markup"]
        lines.append(
            f"{item['line']:>5} | <{item['tag']}> | {label}"
        )

    lines += [
        "",
        "METHOD INVENTORY",
        "----------------",
    ]

    for item in methods:
        lines.append(
            f"{item['line']:>5} | {item['name']} | {item['signature']}"
        )

    lines += [
        "",
        "STATE FIELD INVENTORY",
        "---------------------",
    ]

    for item in fields:
        lines.append(
            f"{item['line']:>5} | {item['type']} {item['name']}"
        )

    lines += [
        "",
        "SUGGESTED COMPONENT BOUNDARIES",
        "------------------------------",
    ]

    if boundaries:
        for item in boundaries:
            evidence = ", ".join(
                str(line) for line in item["evidence_lines"]
            )
            lines.append(item["suggested_component"])
            lines.append(f"  evidence lines: {evidence}")
            lines.append(f"  reason: {item['reason']}")
    else:
        lines.append(
            "No safe automatic boundaries were inferred. "
            "Review the structural section list manually."
        )

    lines += [
        "",
        "SAFE EXTRACTION ORDER FOR PASS 85B",
        "----------------------------------",
        "1. Extract the most self-contained display-only panel.",
        "2. Keep all mutable state and services in Python.razor.",
        "3. Pass display values through [Parameter].",
        "4. Pass button actions through EventCallback only when needed.",
        "5. Build and test before extracting a second panel.",
        "6. Leave course IDs, storage keys, scoring, mastery, and resume logic unchanged.",
        "",
        "OUTPUT",
        "------",
        f"Text report: {TEXT_REPORT}",
        f"JSON report: {JSON_REPORT}",
        "",
        "No production files were modified.",
        "",
    ]

    (root / TEXT_REPORT).write_text(
        "\n".join(lines),
        encoding="utf-8",
        newline="\n",
    )

    print("CaveCode Python Page Decomposition Pass 85A completed.")
    print()
    print(f"Text report: {TEXT_REPORT}")
    print(f"JSON report: {JSON_REPORT}")
    print()
    print("No production files were modified.")
    print()
    print("Run:")
    print(f"  cat {TEXT_REPORT}")
    print()
    print("Paste the report back into ChatGPT for the first safe extraction in Pass 85B.")


if __name__ == "__main__":
    try:
        main()
    except Exception as error:
        print(f"ERROR: {error}", file=sys.stderr)
        raise SystemExit(1)
