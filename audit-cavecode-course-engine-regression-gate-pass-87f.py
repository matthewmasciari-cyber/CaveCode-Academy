#!/usr/bin/env python3

from __future__ import annotations

import json
import re
import subprocess
import sys
from pathlib import Path
from typing import Any

REPORT_DIR = Path("Archive/Reports")
TEXT_REPORT = REPORT_DIR / "course-engine-regression-gate-pass-87f.txt"
JSON_REPORT = REPORT_DIR / "course-engine-regression-gate-pass-87f.json"

FILES = {
    "Python page": Path("Pages/Python.razor"),
    "CSharp page": Path("Pages/CSharp.razor"),
    "CourseSession": Path("CourseEngine/CourseSession.cs"),
    "CourseProgressPanel": Path(
        "Components/CourseEngine/CourseProgressPanel.razor"
    ),
    "CourseStageTabs": Path(
        "Components/CourseEngine/CourseStageTabs.razor"
    ),
    "CourseCodeEditor": Path(
        "Components/CourseEngine/CourseCodeEditor.razor"
    ),
    "CourseExerciseActions": Path(
        "Components/CourseEngine/CourseExerciseActions.razor"
    ),
    "_Imports": Path("_Imports.razor"),
}

results: list[dict[str, Any]] = []


def add_result(name: str, passed: bool, detail: str = "OK") -> None:
    results.append(
        {
            "name": name,
            "passed": passed,
            "detail": detail,
        }
    )


def check_required_files() -> dict[str, str]:
    texts: dict[str, str] = {}

    for name, path in FILES.items():
        if not path.is_file():
            add_result(
                f"Required file: {name}",
                False,
                f"Missing: {path}",
            )
            continue

        text = path.read_text(encoding="utf-8")
        texts[name] = text

        add_result(
            f"Required file: {name}",
            bool(text.strip()),
            str(path) if text.strip() else f"Empty file: {path}",
        )

    return texts


def check_page(page_name: str, text: str) -> None:
    shared_components = [
        "CourseProgressPanel",
        "CourseStageTabs",
        "CourseCodeEditor",
        "CourseExerciseActions",
    ]

    missing_components = [
        component
        for component in shared_components
        if component not in text
    ]

    add_result(
        f"{page_name}: shared UI components",
        not missing_components,
        (
            "OK"
            if not missing_components
            else "Missing: " + ", ".join(missing_components)
        ),
    )

    session_markers = [
        "CourseSession",
        "Session.",
        "Session.TrySelectLesson(",
        "Session.TrySelectStage(",
    ]

    missing_session = [
        marker for marker in session_markers if marker not in text
    ]

    add_result(
        f"{page_name}: shared CourseSession integration",
        not missing_session,
        (
            "OK"
            if not missing_session
            else "Missing: " + ", ".join(missing_session)
        ),
    )

    persistence_markers = [
        "private sealed class CourseProgressSnapshot",
        'JsonPropertyName("currentModuleIndex")',
        'JsonPropertyName("currentStage")',
        'JsonPropertyName("highestCompletedStage")',
        'JsonPropertyName("moduleCompleted")',
        "JS.InvokeAsync<CourseProgressSnapshot?>",
        "SaveProgressAsync()",
        "CourseProgressSnapshot snapshot = new()",
    ]

    missing_persistence = [
        marker
        for marker in persistence_markers
        if marker not in text
    ]

    add_result(
        f"{page_name}: progress save and restore contract",
        not missing_persistence,
        (
            "OK"
            if not missing_persistence
            else "Missing: " + ", ".join(missing_persistence)
        ),
    )

    opening_nav = len(
        re.findall(
            r"<nav(?:\s|>)",
            text,
            flags=re.IGNORECASE,
        )
    )

    closing_nav = len(
        re.findall(
            r"</nav\s*>",
            text,
            flags=re.IGNORECASE,
        )
    )

    add_result(
        f"{page_name}: Razor nav-tag balance",
        opening_nav == closing_nav,
        f"<nav>: {opening_nav}, </nav>: {closing_nav}",
    )


def check_course_session(text: str) -> None:
    state_markers = [
        "public int CurrentModuleIndex",
        "public int CurrentStageIndex",
        "public int[] HighestCompletedStage",
        "public bool[] ModuleCompleted",
        "public int CompletedModuleCount",
    ]

    missing_state = [
        marker for marker in state_markers if marker not in text
    ]

    add_result(
        "CourseSession: shared state",
        not missing_state,
        (
            "OK"
            if not missing_state
            else "Missing: " + ", ".join(missing_state)
        ),
    )

    methods = [
        "Initialize",
        "ProgressPercent",
        "ModuleMastery",
        "IsLessonUnlocked",
        "IsStageUnlocked",
        "IsStageComplete",
        "TrySelectLesson",
        "TrySelectStage",
        "MarkCurrentStageComplete",
        "CompleteModule",
        "MoveToNextModule",
    ]

    missing_methods: list[str] = []

    for method in methods:
        pattern = re.compile(
            rf"public\s+(?:bool|void|int)\s+"
            rf"{re.escape(method)}\s*\("
        )

        if not pattern.search(text):
            missing_methods.append(method)

    add_result(
        "CourseSession: current lifecycle API",
        not missing_methods,
        (
            "OK"
            if not missing_methods
            else "Missing: " + ", ".join(missing_methods)
        ),
    )

    safety_markers = [
        "ValidateCurrentModule()",
        "ValidateModuleIndex(",
        "throw new ArgumentOutOfRangeException",
        "Math.Clamp(",
    ]

    missing_safety = [
        marker for marker in safety_markers if marker not in text
    ]

    add_result(
        "CourseSession: bounds and validation",
        not missing_safety,
        (
            "OK"
            if not missing_safety
            else "Missing: " + ", ".join(missing_safety)
        ),
    )


def check_imports(text: str) -> None:
    passed = "CaveCode.CourseEngine" in text

    add_result(
        "_Imports: CourseEngine namespace",
        passed,
        "OK" if passed else "Missing: CaveCode.CourseEngine",
    )


def run_build() -> tuple[str, int, int]:
    completed = subprocess.run(
        ["dotnet", "build", "--nologo"],
        text=True,
        capture_output=True,
        check=False,
    )

    output = completed.stdout + completed.stderr

    warning_matches = re.findall(
        r"^\s*(\d+)\s+Warning\(s\)\s*$",
        output,
        flags=re.MULTILINE,
    )

    error_matches = re.findall(
        r"^\s*(\d+)\s+Error\(s\)\s*$",
        output,
        flags=re.MULTILINE,
    )

    warning_count = (
        int(warning_matches[-1])
        if warning_matches
        else 0
    )

    error_count = (
        int(error_matches[-1])
        if error_matches
        else (0 if completed.returncode == 0 else 1)
    )

    passed = completed.returncode == 0 and error_count == 0

    add_result(
        "dotnet build",
        passed,
        (
            f"Build succeeded with {warning_count} warning(s)."
            if passed
            else f"Build failed with {error_count} error(s)."
        ),
    )

    return output, warning_count, error_count


def write_reports(
    build_output: str,
    warning_count: int,
    error_count: int,
) -> bool:
    REPORT_DIR.mkdir(parents=True, exist_ok=True)

    passed_count = sum(
        1 for result in results if result["passed"]
    )
    total_count = len(results)
    overall_passed = passed_count == total_count

    lines = [
        "=" * 100,
        "CAVECODE COURSE ENGINE REGRESSION GATE — PASS 87F",
        "=" * 100,
        "",
    ]

    for result in results:
        status = "PASS" if result["passed"] else "FAIL"
        lines.append(f"[{status}] {result['name']}")
        lines.append(f"       {result['detail']}")

    lines.extend(
        [
            "",
            "SUMMARY",
            "-------",
            f"Overall: {'PASS' if overall_passed else 'FAIL'}",
            f"Checks passed: {passed_count} / {total_count}",
            f"Build warnings: {warning_count}",
            f"Build errors: {error_count}",
            "",
            f"Text report: {TEXT_REPORT}",
            f"JSON report: {JSON_REPORT}",
            "",
            "BUILD OUTPUT",
            "------------",
            build_output.rstrip(),
            "",
        ]
    )

    TEXT_REPORT.write_text(
        "\n".join(lines),
        encoding="utf-8",
    )

    JSON_REPORT.write_text(
        json.dumps(
            {
                "overall": (
                    "PASS" if overall_passed else "FAIL"
                ),
                "checks_passed": passed_count,
                "checks_total": total_count,
                "build_warnings": warning_count,
                "build_errors": error_count,
                "results": results,
            },
            indent=2,
        )
        + "\n",
        encoding="utf-8",
    )

    print("Course Engine Regression Gate Pass 87F completed.")
    print()
    print(
        f"Overall: {'PASS' if overall_passed else 'FAIL'}"
    )
    print(
        f"Checks passed: {passed_count} / {total_count}"
    )
    print(f"Build warnings: {warning_count}")
    print(f"Build errors: {error_count}")
    print()
    print(f"Text report: {TEXT_REPORT}")
    print(f"JSON report: {JSON_REPORT}")

    return overall_passed


def main() -> int:
    texts = check_required_files()

    python_text = texts.get("Python page")
    csharp_text = texts.get("CSharp page")
    session_text = texts.get("CourseSession")
    imports_text = texts.get("_Imports")

    if python_text is not None:
        check_page("Python", python_text)

    if csharp_text is not None:
        check_page("CSharp", csharp_text)

    if session_text is not None:
        check_course_session(session_text)

    if imports_text is not None:
        check_imports(imports_text)

    build_output, warnings, errors = run_build()

    overall_passed = write_reports(
        build_output,
        warnings,
        errors,
    )

    return 0 if overall_passed else 1


if __name__ == "__main__":
    sys.exit(main())
