#!/usr/bin/env python3

from __future__ import annotations

import ast
import json
import re
import subprocess
import sys
from dataclasses import dataclass
from pathlib import Path
from typing import Any

ROOT = Path.cwd()
LESSON_FILE = ROOT / "CourseEngine/HtmlCssChapterOneLessons.cs"
PAGE_FILE = ROOT / "Pages/HtmlCss.razor"
VALIDATOR_FILE = ROOT / "CourseEngine/HtmlCourseCodeValidator.cs"
REGRESSION_SCRIPT = ROOT / "audit-cavecode-course-engine-regression-gate-pass-87f.py"

REPORT_DIR = ROOT / "Archive/Reports"
TEXT_REPORT = REPORT_DIR / "htmlcss-baseline-audit.txt"
JSON_REPORT = REPORT_DIR / "htmlcss-baseline-audit.json"

FIELD_NAMES = [
    "Chapter",
    "Topic",
    "Title",
    "Teaching",
    "ExampleCode",
    "TargetCode",
    "FillStarter",
    "PredictionQuestion",
    "PredictionOptions",
    "PredictionCorrect",
    "PredictionExplanation",
    "BrokenCode",
    "DebugPrompt",
    "RecallPrompt",
    "TransferPrompt",
    "TransferCode",
    "PreviewMessage",
]


@dataclass
class Check:
    name: str
    passed: bool
    detail: str


checks: list[Check] = []
module_results: list[dict[str, Any]] = []


def add_check(name: str, passed: bool, detail: str = "OK") -> None:
    checks.append(Check(name, passed, detail))


def decode_csharp_string(value: str) -> str:
    value = value.strip()
    if not value.startswith('"'):
        raise ValueError(f"Not a regular C# string literal: {value[:40]}")
    return ast.literal_eval(value)


def extract_regular_strings(text: str) -> list[str]:
    values: list[str] = []
    for match in re.finditer(r'"(?:\\.|[^"\\])*"', text, flags=re.DOTALL):
        values.append(decode_csharp_string(match.group(0)))
    return values


def find_matching(
    text: str,
    start: int,
    opening: str,
    closing: str,
) -> int:
    depth = 0
    in_string = False
    escaped = False

    for index in range(start, len(text)):
        char = text[index]

        if in_string:
            if escaped:
                escaped = False
            elif char == "\\":
                escaped = True
            elif char == '"':
                in_string = False
            continue

        if char == '"':
            in_string = True
            continue

        if char == opening:
            depth += 1
        elif char == closing:
            depth -= 1
            if depth == 0:
                return index

    raise ValueError(f"Unmatched {opening}{closing} starting at {start}")


def split_top_level_arguments(text: str) -> list[str]:
    arguments: list[str] = []
    start = 0
    paren = brace = bracket = 0
    in_string = False
    escaped = False

    for index, char in enumerate(text):
        if in_string:
            if escaped:
                escaped = False
            elif char == "\\":
                escaped = True
            elif char == '"':
                in_string = False
            continue

        if char == '"':
            in_string = True
            continue

        if char == "(":
            paren += 1
        elif char == ")":
            paren -= 1
        elif char == "{":
            brace += 1
        elif char == "}":
            brace -= 1
        elif char == "[":
            bracket += 1
        elif char == "]":
            bracket -= 1
        elif char == "," and paren == brace == bracket == 0:
            arguments.append(text[start:index].strip())
            start = index + 1

    final = text[start:].strip()
    if final:
        arguments.append(final)

    return arguments


def extract_lesson_blocks(source: str) -> list[tuple[str, str]]:
    marker = "new CourseLesson("
    blocks: list[tuple[str, str]] = []
    cursor = 0

    while True:
        start = source.find(marker, cursor)
        if start < 0:
            break

        open_paren = start + len("new CourseLesson")
        close_paren = find_matching(source, open_paren, "(", ")")
        constructor = source[open_paren + 1 : close_paren]

        tail = close_paren + 1
        while tail < len(source) and source[tail].isspace():
            tail += 1

        initializer = ""
        if tail < len(source) and source[tail] == "{":
            close_brace = find_matching(source, tail, "{", "}")
            initializer = source[tail + 1 : close_brace]
            cursor = close_brace + 1
        else:
            cursor = close_paren + 1

        blocks.append((constructor, initializer))

    return blocks


def module_check(
    module_number: int,
    title: str,
    name: str,
    passed: bool,
    detail: str = "OK",
) -> dict[str, Any]:
    return {
        "module": module_number,
        "title": title,
        "name": name,
        "passed": passed,
        "detail": detail,
    }


def run_command(command: list[str]) -> tuple[int, str]:
    completed = subprocess.run(
        command,
        cwd=ROOT,
        text=True,
        capture_output=True,
        check=False,
    )
    return completed.returncode, completed.stdout + completed.stderr


def main() -> int:
    REPORT_DIR.mkdir(parents=True, exist_ok=True)

    required_files = [
        LESSON_FILE,
        PAGE_FILE,
        VALIDATOR_FILE,
        REGRESSION_SCRIPT,
    ]

    missing_files = [str(path.relative_to(ROOT)) for path in required_files if not path.is_file()]
    add_check(
        "Required HTML/CSS certification files",
        not missing_files,
        "OK" if not missing_files else "Missing: " + ", ".join(missing_files),
    )

    if missing_files:
        return finish("", "", 1)

    lesson_source = LESSON_FILE.read_text(encoding="utf-8")
    page_source = PAGE_FILE.read_text(encoding="utf-8")
    validator_source = VALIDATOR_FILE.read_text(encoding="utf-8")

    blocks = extract_lesson_blocks(lesson_source)

    count_match = re.search(
        r"PlayableModuleCount\s*=\s*(\d+)",
        lesson_source,
    )
    declared_count = int(count_match.group(1)) if count_match else -1

    add_check(
        "HTML/CSS playable module count",
        declared_count == 8 and len(blocks) == 8,
        f"Declared: {declared_count}; Parsed: {len(blocks)}",
    )

    parsed_titles: list[str] = []
    parsed_topics: list[str] = []

    for module_number, (constructor, initializer) in enumerate(blocks, start=1):
        args = split_top_level_arguments(constructor)
        title = f"Module {module_number}"

        if len(args) >= 3:
            try:
                title = decode_csharp_string(args[2])
            except Exception:
                pass

        local_results: list[dict[str, Any]] = []

        local_results.append(
            module_check(
                module_number,
                title,
                "Constructor field count",
                len(args) == len(FIELD_NAMES),
                f"Expected {len(FIELD_NAMES)}; found {len(args)}",
            )
        )

        if len(args) != len(FIELD_NAMES):
            module_results.extend(local_results)
            continue

        values: dict[str, Any] = {}

        for index, field in enumerate(FIELD_NAMES):
            raw = args[index]
            try:
                if field == "PredictionOptions":
                    values[field] = extract_regular_strings(raw)
                elif field == "PredictionCorrect":
                    values[field] = int(raw.strip())
                else:
                    values[field] = decode_csharp_string(raw)
            except Exception as exc:
                values[field] = None
                local_results.append(
                    module_check(
                        module_number,
                        title,
                        f"Parse {field}",
                        False,
                        str(exc),
                    )
                )

        parsed_titles.append(values.get("Title") or title)
        parsed_topics.append(values.get("Topic") or "")

        required_text_fields = [
            field
            for field in FIELD_NAMES
            if field not in {"PredictionOptions", "PredictionCorrect"}
        ]
        empty_fields = [
            field
            for field in required_text_fields
            if not isinstance(values.get(field), str)
            or not values[field].strip()
        ]
        local_results.append(
            module_check(
                module_number,
                title,
                "Required lesson text",
                not empty_fields,
                "OK" if not empty_fields else "Empty: " + ", ".join(empty_fields),
            )
        )

        options = values.get("PredictionOptions")
        correct = values.get("PredictionCorrect")
        prediction_valid = (
            isinstance(options, list)
            and len(options) == 4
            and len(set(options)) == 4
            and isinstance(correct, int)
            and 0 <= correct < len(options)
        )
        local_results.append(
            module_check(
                module_number,
                title,
                "Prediction question contract",
                prediction_valid,
                (
                    f"Options: {len(options) if isinstance(options, list) else 'invalid'}; "
                    f"correct index: {correct}"
                ),
            )
        )

        concept_match = re.search(
            r"ConceptPoints\s*=\s*new\[\]\s*\{(?P<body>.*?)\}",
            initializer,
            flags=re.DOTALL,
        )
        concept_points = (
            extract_regular_strings(concept_match.group("body"))
            if concept_match
            else []
        )
        local_results.append(
            module_check(
                module_number,
                title,
                "Concept-point support",
                len(concept_points) >= 3
                and all(point.strip() for point in concept_points),
                f"Concept points: {len(concept_points)}",
            )
        )

        file_match = re.search(
            r'EditorFileNameOverride\s*=\s*("(?:\\.|[^"\\])*")',
            initializer,
            flags=re.DOTALL,
        )
        editor_file = (
            decode_csharp_string(file_match.group(1))
            if file_match
            else None
        )
        local_results.append(
            module_check(
                module_number,
                title,
                "Editor filename",
                editor_file == "index.html",
                f"Editor file: {editor_file!r}",
            )
        )

        target = values.get("TargetCode") or ""
        fill = values.get("FillStarter") or ""
        broken = values.get("BrokenCode") or ""
        transfer = values.get("TransferCode") or ""

        code_contract = (
            "<" in target
            and ">" in target
            and "___" not in target
            and "___" in fill
            and broken != target
            and transfer != target
            and "___" not in transfer
        )
        local_results.append(
            module_check(
                module_number,
                title,
                "Guided/fill/debug/transfer code contract",
                code_contract,
                (
                    f"Target chars: {len(target)}; fill placeholders: {fill.count('___')}; "
                    f"broken differs: {broken != target}; transfer differs: {transfer != target}"
                ),
            )
        )

        prompt_contract = all(
            isinstance(values.get(field), str)
            and len(values[field].strip()) >= 20
            for field in [
                "Teaching",
                "DebugPrompt",
                "RecallPrompt",
                "TransferPrompt",
                "PredictionExplanation",
            ]
        )
        local_results.append(
            module_check(
                module_number,
                title,
                "Instructional prompt depth",
                prompt_contract,
                "All major instructional fields contain at least 20 characters.",
            )
        )

        module_results.extend(local_results)

    add_check(
        "Unique module titles",
        len(parsed_titles) == len(set(parsed_titles)) == 8,
        f"Unique titles: {len(set(parsed_titles))} / {len(parsed_titles)}",
    )

    add_check(
        "Distinct module topics",
        len(parsed_topics) == len(set(parsed_topics)) == 8,
        f"Unique topics: {len(set(parsed_topics))} / {len(parsed_topics)}",
    )

    page_markers = [
        "HtmlCssChapterOneLessons.All",
        "HtmlCssChapterOneLessons.PlayableModuleCount",
        "<CourseProgressPanel",
        "<CourseStageTabs",
        "<CourseCodeEditor",
        "CourseEngine.ValidateCode(",
        "CourseTrainingStage.Guided => CurrentLesson.TargetCode",
        "CourseTrainingStage.Fill => CurrentLesson.TargetCode",
        "CourseTrainingStage.Debug => CurrentLesson.TargetCode",
        "CourseTrainingStage.Recall => CurrentLesson.TargetCode",
        "CourseTrainingStage.Transfer => CurrentLesson.TransferCode",
        "SaveProgressAsync()",
    ]
    missing_page_markers = [marker for marker in page_markers if marker not in page_source]
    add_check(
        "HTML/CSS page course-engine integration",
        not missing_page_markers,
        "OK" if not missing_page_markers else "Missing: " + ", ".join(missing_page_markers),
    )

    validator_markers = [
        "public CourseCodeValidationResult Validate(",
        "NormalizeLineEndings",
        "ExpectedCode",
    ]
    missing_validator_markers = [
        marker for marker in validator_markers if marker not in validator_source
    ]
    add_check(
        "HTML validator integration",
        not missing_validator_markers,
        "OK" if not missing_validator_markers else "Missing: " + ", ".join(missing_validator_markers),
    )

    build_code, build_output = run_command(["dotnet", "build", "--nologo"])
    build_warning_match = re.search(
        r"(\d+)\s+Warning\(s\)",
        build_output,
    )
    build_error_match = re.search(
        r"(\d+)\s+Error\(s\)",
        build_output,
    )
    warnings = int(build_warning_match.group(1)) if build_warning_match else 0
    errors = int(build_error_match.group(1)) if build_error_match else (
        0 if build_code == 0 else 1
    )
    add_check(
        "dotnet build",
        build_code == 0 and warnings == 0 and errors == 0,
        f"Exit: {build_code}; warnings: {warnings}; errors: {errors}",
    )

    regression_code, regression_output = run_command(
        ["python3", str(REGRESSION_SCRIPT)]
    )
    add_check(
        "Course-engine regression gate",
        regression_code == 0 and "Overall: PASS" in regression_output,
        (
            "PASS"
            if regression_code == 0 and "Overall: PASS" in regression_output
            else f"Exit: {regression_code}"
        ),
    )

    return finish(build_output, regression_output, 0)


def finish(
    build_output: str,
    regression_output: str,
    forced_exit: int,
) -> int:
    passed_module_checks = sum(
        1 for result in module_results if result["passed"]
    )
    total_module_checks = len(module_results)

    passed_global_checks = sum(1 for check in checks if check.passed)
    total_global_checks = len(checks)

    all_passed = (
        forced_exit == 0
        and passed_module_checks == total_module_checks
        and passed_global_checks == total_global_checks
    )

    lines = [
        "=" * 100,
        "CAVECODE HTML/CSS TRACK BASELINE AUDIT",
        "=" * 100,
        "",
        "GLOBAL CHECKS",
        "-------------",
    ]

    for check in checks:
        status = "PASS" if check.passed else "FAIL"
        lines.append(f"[{status}] {check.name}")
        lines.append(f"       {check.detail}")

    lines.extend(["", "MODULE CHECKS", "-------------"])

    current_module = None
    for result in module_results:
        if result["module"] != current_module:
            current_module = result["module"]
            lines.extend(
                [
                    "",
                    f"Module {result['module']}: {result['title']}",
                    "-" * 80,
                ]
            )

        status = "PASS" if result["passed"] else "FAIL"
        lines.append(f"[{status}] {result['name']}")
        lines.append(f"       {result['detail']}")

    lines.extend(
        [
            "",
            "SUMMARY",
            "-------",
            f"Overall: {'PASS' if all_passed else 'FAIL'}",
            f"Global checks: {passed_global_checks} / {total_global_checks}",
            f"Module checks: {passed_module_checks} / {total_module_checks}",
            f"Modules parsed: {len({r['module'] for r in module_results})}",
            "",
            f"Text report: {TEXT_REPORT.relative_to(ROOT)}",
            f"JSON report: {JSON_REPORT.relative_to(ROOT)}",
        ]
    )

    TEXT_REPORT.write_text("\n".join(lines) + "\n", encoding="utf-8")

    JSON_REPORT.write_text(
        json.dumps(
            {
                "overall": "PASS" if all_passed else "FAIL",
                "global_checks_passed": passed_global_checks,
                "global_checks_total": total_global_checks,
                "module_checks_passed": passed_module_checks,
                "module_checks_total": total_module_checks,
                "global_checks": [check.__dict__ for check in checks],
                "module_checks": module_results,
                "build_output": build_output,
                "regression_output": regression_output,
            },
            indent=2,
        )
        + "\n",
        encoding="utf-8",
    )

    print("HTML/CSS baseline audit completed.")
    print()
    print(f"Overall: {'PASS' if all_passed else 'FAIL'}")
    print(f"Global checks: {passed_global_checks} / {total_global_checks}")
    print(f"Module checks: {passed_module_checks} / {total_module_checks}")
    print(f"Text report: {TEXT_REPORT.relative_to(ROOT)}")
    print(f"JSON report: {JSON_REPORT.relative_to(ROOT)}")

    return 0 if all_passed else 1


if __name__ == "__main__":
    sys.exit(main())
