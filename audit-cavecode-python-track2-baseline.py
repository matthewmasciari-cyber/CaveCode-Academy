#!/usr/bin/env python3
from __future__ import annotations

from pathlib import Path
import json
import re
import subprocess
import sys
from datetime import datetime, timezone


CANDIDATE_SOURCES = [
    Path("Pages/Python.razor"),
    Path("Pages/PythonCourse.razor"),
    Path("Pages/PythonPath.razor"),
]

REPORT_DIR = Path("Archive/Reports")
TEXT_REPORT = REPORT_DIR / "python-track2-baseline-audit.txt"
JSON_REPORT = REPORT_DIR / "python-track2-baseline-audit.json"


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


def find_python_source(root: Path) -> Path:
    for candidate in CANDIDATE_SOURCES:
        if (root / candidate).is_file():
            return candidate

    matches = sorted((root / "Pages").glob("*Python*.razor"))

    if len(matches) == 1:
        return matches[0].relative_to(root)

    if not matches:
        raise RuntimeError(
            "Could not find a Python Razor page under Pages/."
        )

    raise RuntimeError(
        "Multiple Python Razor pages were found: "
        + ", ".join(str(path.relative_to(root)) for path in matches)
    )


def find_lesson_constructor(text: str) -> tuple[str, int]:
    candidates = [
        "new Lesson(",
        "new PythonLesson(",
        "new CourseLesson(",
        "new ModuleLesson(",
    ]

    counts = {
        candidate: text.count(candidate)
        for candidate in candidates
    }

    constructor = max(counts, key=counts.get)

    if counts[constructor] == 0:
        raise RuntimeError(
            "No recognized lesson constructor was found in the Python page."
        )

    return constructor, counts[constructor]


def extract_constructor_blocks(
    text: str,
    constructor: str,
) -> list[str]:
    blocks: list[str] = []
    cursor = 0

    while True:
        start = text.find(constructor, cursor)

        if start < 0:
            break

        content_start = start + len(constructor)
        position = content_start
        depth = 1
        in_string = False
        escaped = False

        while position < len(text) and depth:
            char = text[position]

            if in_string:
                if escaped:
                    escaped = False
                elif char == "\\":
                    escaped = True
                elif char == '"':
                    in_string = False
            else:
                if char == '"':
                    in_string = True
                elif char == "(":
                    depth += 1
                elif char == ")":
                    depth -= 1

            position += 1

        if depth != 0:
            raise RuntimeError("Unbalanced Python lesson constructor found.")

        blocks.append(text[content_start:position - 1])
        cursor = position

    return blocks


def split_arguments(block: str) -> list[str]:
    arguments: list[str] = []
    start = 0
    parens = brackets = braces = 0
    in_string = False
    escaped = False

    for index, char in enumerate(block):
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
        elif char == "(":
            parens += 1
        elif char == ")":
            parens -= 1
        elif char == "[":
            brackets += 1
        elif char == "]":
            brackets -= 1
        elif char == "{":
            braces += 1
        elif char == "}":
            braces -= 1
        elif char == "," and parens == brackets == braces == 0:
            arguments.append(block[start:index].strip())
            start = index + 1

    arguments.append(block[start:].strip())
    return arguments


def decode_first_string(raw: str) -> str | None:
    match = re.search(r'"(?:\\.|[^"\\])*"', raw, flags=re.DOTALL)

    if not match:
        return None

    literal = match.group(0)
    body = literal[1:-1]
    result = ""
    index = 0

    while index < len(body):
        if body[index] == "\\" and index + 1 < len(body):
            next_char = body[index + 1]
            replacements = {
                "\\": "\\",
                '"': '"',
                "n": "\n",
                "r": "\r",
                "t": "\t",
            }
            result += replacements.get(next_char, next_char)
            index += 2
        else:
            result += body[index]
            index += 1

    return result


def summarize_module(number: int, arguments: list[str]) -> dict:
    string_values = [
        value
        for value in (decode_first_string(argument) for argument in arguments)
        if value is not None
    ]

    title_guess = None

    for value in string_values[:5]:
        if 3 <= len(value) <= 100 and "\n" not in value:
            title_guess = value
            break

    fields = {
        "argument_count": len(arguments),
        "string_field_count": len(string_values),
        "contains_fill_blank": any("___" in value for value in string_values),
        "contains_recall_word": any(
            "recall" in value.lower() for value in string_values
        ),
        "contains_transfer_word": any(
            "transfer" in value.lower() for value in string_values
        ),
        "contains_debug_word": any(
            "debug" in value.lower() for value in string_values
        ),
        "title_guess": title_guess,
    }

    return {
        "module_number": number,
        **fields,
    }


def detect_consistency(modules: list[dict]) -> dict:
    argument_counts = sorted({
        module["argument_count"]
        for module in modules
    })

    string_counts = sorted({
        module["string_field_count"]
        for module in modules
    })

    return {
        "consistent_argument_count": len(argument_counts) == 1,
        "argument_counts_found": argument_counts,
        "consistent_string_field_count": len(string_counts) == 1,
        "string_field_counts_found": string_counts,
        "all_modules_have_fill_blank": all(
            module["contains_fill_blank"] for module in modules
        ),
        "modules_missing_fill_blank": [
            module["module_number"]
            for module in modules
            if not module["contains_fill_blank"]
        ],
    }


def run_build(root: Path) -> dict:
    process = subprocess.run(
        ["dotnet", "build", "--nologo"],
        cwd=root,
        text=True,
        capture_output=True,
    )

    output = (process.stdout + "\n" + process.stderr).strip()

    return {
        "passed": process.returncode == 0,
        "exit_code": process.returncode,
        "warnings": [
            line
            for line in output.splitlines()
            if ": warning " in line.lower()
        ],
        "errors": [
            line
            for line in output.splitlines()
            if ": error " in line.lower()
        ],
    }


def main() -> None:
    root = find_root()
    source = find_python_source(root)
    source_path = root / source
    text = source_path.read_text(encoding="utf-8-sig")

    constructor, estimated_count = find_lesson_constructor(text)
    blocks = extract_constructor_blocks(text, constructor)

    modules = [
        summarize_module(index, split_arguments(block))
        for index, block in enumerate(blocks, start=1)
    ]

    consistency = detect_consistency(modules)
    build = run_build(root)

    reusable = (
        consistency["consistent_argument_count"]
        and consistency["argument_counts_found"] == [17]
    )

    summary = {
        "source": str(source),
        "constructor": constructor,
        "module_count": len(modules),
        "estimated_constructor_count": estimated_count,
        "csharp_17_field_model_reusable": reusable,
        "build_passed": build["passed"],
    }

    data = {
        "pass": "Python Track 2 Baseline",
        "created_utc": datetime.now(timezone.utc).isoformat(),
        "summary": summary,
        "consistency": consistency,
        "build": build,
        "modules": modules,
    }

    report_dir = root / REPORT_DIR
    report_dir.mkdir(parents=True, exist_ok=True)

    (root / JSON_REPORT).write_text(
        json.dumps(data, indent=2),
        encoding="utf-8",
        newline="\n",
    )

    lines = [
        "=" * 100,
        "CAVECODE PYTHON TRACK 2 — BASELINE AUDIT",
        "=" * 100,
        "",
        f"Source file: {summary['source']}",
        f"Lesson constructor: {summary['constructor']}",
        f"Modules discovered: {summary['module_count']}",
        f"Argument counts found: {consistency['argument_counts_found']}",
        f"String-field counts found: {consistency['string_field_counts_found']}",
        (
            "C# 17-field certification model reusable: "
            + ("YES" if reusable else "NO")
        ),
        f"Build passed: {'YES' if build['passed'] else 'NO'}",
        "",
        "MODULE SNAPSHOT",
        "---------------",
    ]

    for module in modules:
        lines.append(
            f"Module {module['module_number']:>2}: "
            f"args={module['argument_count']}, "
            f"strings={module['string_field_count']}, "
            f"fill={'YES' if module['contains_fill_blank'] else 'NO'}, "
            f"title={module['title_guess']!r}"
        )

    lines += [
        "",
        "CONSISTENCY",
        "-----------",
        (
            "Consistent argument count: "
            + ("YES" if consistency["consistent_argument_count"] else "NO")
        ),
        (
            "Consistent string field count: "
            + ("YES" if consistency["consistent_string_field_count"] else "NO")
        ),
        (
            "Modules missing a ___ fill blank: "
            + (
                ", ".join(
                    str(number)
                    for number in consistency["modules_missing_fill_blank"]
                )
                or "None"
            )
        ),
        "",
        "NEXT DECISION",
        "-------------",
    ]

    if reusable:
        lines += [
            "The Python lesson structure matches the C# 17-field model.",
            "Next: create a Python-specific 17-check certification auditor.",
        ]
    else:
        lines += [
            "The Python lesson structure does not exactly match the C# model.",
            "Next: inspect the argument layout before creating certification rules.",
        ]

    lines += [
        "",
        f"Text report: {TEXT_REPORT}",
        f"JSON report: {JSON_REPORT}",
        "",
        "No production code was modified.",
    ]

    (root / TEXT_REPORT).write_text(
        "\n".join(lines),
        encoding="utf-8",
        newline="\n",
    )

    print("Python Track 2 Baseline Audit completed.")
    print()
    print(f"Source file: {summary['source']}")
    print(f"Modules discovered: {summary['module_count']}")
    print(
        "C# 17-field certification model reusable: "
        + ("YES" if reusable else "NO")
    )
    print(f"Build passed: {'YES' if build['passed'] else 'NO'}")
    print()
    print(f"Text report: {TEXT_REPORT}")
    print(f"JSON report: {JSON_REPORT}")
    print()
    print("No production code was modified.")


if __name__ == "__main__":
    try:
        main()
    except Exception as error:
        print(f"ERROR: {error}", file=sys.stderr)
        raise SystemExit(1)
