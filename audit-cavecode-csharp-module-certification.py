#!/usr/bin/env python3
from __future__ import annotations

from pathlib import Path
import argparse
import json
import re
import subprocess
import sys
from datetime import datetime, timezone


SOURCE = Path("Pages/CSharp.razor")
REPORT_DIR = Path("Archive/Reports")

FIELDS = [
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


def extract_lesson_blocks(text: str) -> list[str]:
    marker = "private static readonly List<Lesson> Lessons"
    start = text.find(marker)

    if start < 0:
        raise RuntimeError("Could not locate the C# Lessons list.")

    section = text[start:]
    blocks: list[str] = []
    cursor = 0

    while True:
        found = section.find("new Lesson(", cursor)

        if found < 0:
            break

        pos = found + len("new Lesson(")
        depth = 1
        in_string = False
        escaped = False
        end = pos

        while end < len(section) and depth:
            char = section[end]

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

            end += 1

        if depth != 0:
            raise RuntimeError("Unbalanced Lesson constructor found.")

        blocks.append(section[pos:end - 1])
        cursor = end

    return blocks


def split_arguments(block: str) -> list[str]:
    values: list[str] = []
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
            values.append(block[start:index].strip())
            start = index + 1

    values.append(block[start:].strip())
    return values


def strip_leading_comments(value: str) -> str:
    cleaned = value.strip()

    while True:
        block = re.match(r"^/\*.*?\*/\s*", cleaned, flags=re.DOTALL)
        if block:
            cleaned = cleaned[block.end():].lstrip()
            continue

        line = re.match(r"^//[^\n]*(?:\n|$)\s*", cleaned)
        if line:
            cleaned = cleaned[line.end():].lstrip()
            continue

        break

    return cleaned


def decode_string(value: str) -> str:
    value = strip_leading_comments(value)

    match = re.search(r'"(?:\\.|[^"\\])*"', value, flags=re.DOTALL)
    if not match:
        return value

    raw = match.group(0)
    body = raw[1:-1]
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


def parse_options(raw: str) -> list[str]:
    return [
        decode_string(match)
        for match in re.findall(r'"(?:\\.|[^"\\])*"', raw)
    ]


def parse_module(text: str, module_number: int) -> dict[str, object]:
    blocks = extract_lesson_blocks(text)

    if not 1 <= module_number <= len(blocks):
        raise RuntimeError(
            f"Module number must be between 1 and {len(blocks)}."
        )

    arguments = split_arguments(blocks[module_number - 1])

    if len(arguments) != len(FIELDS):
        raise RuntimeError(
            f"Module {module_number}: expected {len(FIELDS)} fields; "
            f"found {len(arguments)}."
        )

    module: dict[str, object] = {}

    for field, raw in zip(FIELDS, arguments):
        if field == "PredictionOptions":
            module[field] = parse_options(raw)
        elif field == "PredictionCorrect":
            cleaned = strip_leading_comments(raw)
            module[field] = int(cleaned)
        else:
            module[field] = decode_string(raw)

    return module


def normalize_code(code: str) -> str:
    return re.sub(r"\s+", "", code)


def code_identifiers(code: str) -> set[str]:
    keywords = {
        "int", "string", "bool", "double", "float", "char", "var",
        "true", "false", "public", "private", "static", "void",
        "class", "new", "return", "if", "else", "for", "while",
    }

    return {
        token
        for token in re.findall(r"\b[A-Za-z_]\w*\b", code)
        if token not in keywords
    }


def code_literals(code: str) -> set[str]:
    values = set(re.findall(r'"([^"]*)"', code))
    values.update(re.findall(r"\b(?:true|false)\b", code))
    values.update(
        re.findall(r"(?<![\w.])-?\d+(?:\.\d+)?(?![\w.])", code)
    )
    return {value for value in values if value != ""}


def prompt_mentions(prompt: str, value: str) -> bool:
    return value.lower() in prompt.lower()


def add_check(
    checks: list[dict[str, object]],
    name: str,
    passed: bool,
    details: str,
    severity: str = "HIGH",
) -> None:
    checks.append({
        "name": name,
        "passed": passed,
        "severity": severity if not passed else "PASS",
        "details": details,
    })


def run_build(root: Path) -> dict[str, object]:
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
        "tail": output.splitlines()[-30:],
    }


def audit(module: dict[str, object], build: dict[str, object]) -> list[dict[str, object]]:
    checks: list[dict[str, object]] = []

    target = str(module["TargetCode"])
    fill = str(module["FillStarter"])
    options = list(module["PredictionOptions"])
    correct = int(module["PredictionCorrect"])
    broken = str(module["BrokenCode"])
    recall_prompt = str(module["RecallPrompt"])
    transfer_prompt = str(module["TransferPrompt"])
    transfer_code = str(module["TransferCode"])

    add_check(
        checks,
        "Module identity",
        all(
            bool(str(module[field]).strip())
            for field in ("Chapter", "Topic", "Title")
        ),
        "Chapter, topic, and title must all be present.",
        "BLOCKER",
    )

    add_check(
        checks,
        "Teaching content",
        len(str(module["Teaching"]).strip()) >= 40,
        "Teaching explanation should be complete enough for a beginner.",
    )

    add_check(
        checks,
        "Example and target code",
        bool(str(module["ExampleCode"]).strip()) and bool(target.strip()),
        "ExampleCode and TargetCode must both contain code.",
        "BLOCKER",
    )

    add_check(
        checks,
        "Fill stage contains blank",
        "___" in fill,
        "FillStarter must contain at least one ___ placeholder.",
        "BLOCKER",
    )

    add_check(
        checks,
        "Prediction option count",
        len(options) == 4,
        f"Found {len(options)} prediction options.",
        "MEDIUM",
    )

    add_check(
        checks,
        "Prediction index",
        0 <= correct < len(options),
        f"Correct index is {correct}.",
        "BLOCKER",
    )

    add_check(
        checks,
        "Prediction options unique",
        len({option.strip().lower() for option in options}) == len(options),
        "Every prediction answer must be meaningfully different.",
    )

    add_check(
        checks,
        "Prediction explanation",
        bool(str(module["PredictionQuestion"]).strip())
        and bool(str(module["PredictionExplanation"]).strip()),
        "Prediction question and explanation must both be present.",
    )

    add_check(
        checks,
        "Broken code differs from target",
        normalize_code(broken) != normalize_code(target),
        "Debug code must contain a real defect.",
        "BLOCKER",
    )

    add_check(
        checks,
        "Debug prompt",
        bool(str(module["DebugPrompt"]).strip()),
        "DebugPrompt must explain what the learner should inspect.",
    )

    missing_recall_names = sorted(
        identifier
        for identifier in code_identifiers(target)
        if not prompt_mentions(recall_prompt, identifier)
    )

    add_check(
        checks,
        "Recall names are stated",
        not missing_recall_names,
        (
            "All exact-match identifiers appear in RecallPrompt."
            if not missing_recall_names
            else "RecallPrompt omits: " + ", ".join(missing_recall_names)
        ),
    )

    missing_recall_values = sorted(
        literal
        for literal in code_literals(target)
        if not prompt_mentions(recall_prompt, literal)
    )

    add_check(
        checks,
        "Recall values are fair",
        not missing_recall_values,
        (
            "All required values appear in RecallPrompt."
            if not missing_recall_values
            else "RecallPrompt omits exact required values: "
            + ", ".join(missing_recall_values)
        ),
    )

    missing_transfer_names = sorted(
        identifier
        for identifier in code_identifiers(transfer_code)
        if not prompt_mentions(transfer_prompt, identifier)
    )

    add_check(
        checks,
        "Transfer names are stated",
        not missing_transfer_names,
        (
            "All exact-match identifiers appear in TransferPrompt."
            if not missing_transfer_names
            else "TransferPrompt omits: "
            + ", ".join(missing_transfer_names)
        ),
        "BLOCKER",
    )

    missing_transfer_values = sorted(
        literal
        for literal in code_literals(transfer_code)
        if not prompt_mentions(transfer_prompt, literal)
    )

    add_check(
        checks,
        "Transfer values are stated",
        not missing_transfer_values,
        (
            "All required values appear in TransferPrompt."
            if not missing_transfer_values
            else "TransferPrompt omits exact required values: "
            + ", ".join(missing_transfer_values)
        ),
        "BLOCKER",
    )

    vague_words = [
        word
        for word in (
            "sensible",
            "appropriate",
            "reasonable",
            "your choice",
            "any value",
            "some value",
        )
        if word in transfer_prompt.lower()
    ]

    add_check(
        checks,
        "Transfer wording matches exact validator",
        not vague_words,
        (
            "Transfer wording is exact."
            if not vague_words
            else "Open-ended wording found: "
            + ", ".join(vague_words)
        ),
        "BLOCKER",
    )

    add_check(
        checks,
        "Preview message",
        bool(str(module["PreviewMessage"]).strip()),
        "PreviewMessage must describe the visible outcome.",
        "MEDIUM",
    )

    add_check(
        checks,
        "Project build",
        bool(build["passed"]),
        (
            "dotnet build succeeded."
            if build["passed"]
            else f"dotnet build failed with {len(build['errors'])} errors."
        ),
        "BLOCKER",
    )

    return checks


def main() -> None:
    parser = argparse.ArgumentParser(
        description="Audit one CaveCode C# curriculum module."
    )
    parser.add_argument(
        "module",
        type=int,
        help="One-based module number, for example 4.",
    )
    args = parser.parse_args()

    root = find_root()
    source_path = root / SOURCE

    if not source_path.is_file():
        raise RuntimeError(f"Missing source file: {SOURCE}")

    module = parse_module(
        source_path.read_text(encoding="utf-8-sig"),
        args.module,
    )
    build = run_build(root)
    checks = audit(module, build)
    failures = [check for check in checks if not check["passed"]]

    summary = {
        "total_checks": len(checks),
        "passed_checks": len(checks) - len(failures),
        "failed_checks": len(failures),
        "blockers": sum(
            check["severity"] == "BLOCKER" for check in failures
        ),
        "high": sum(
            check["severity"] == "HIGH" for check in failures
        ),
        "medium": sum(
            check["severity"] == "MEDIUM" for check in failures
        ),
    }

    text_report = (
        REPORT_DIR
        / f"csharp-module-{args.module}-certification.txt"
    )
    json_report = (
        REPORT_DIR
        / f"csharp-module-{args.module}-certification.json"
    )

    data = {
        "pass": f"C# Module {args.module} certification",
        "created_utc": datetime.now(timezone.utc).isoformat(),
        "source": str(SOURCE),
        "module_number": args.module,
        "module": module,
        "checks": checks,
        "build": build,
        "summary": summary,
    }

    report_dir = root / REPORT_DIR
    report_dir.mkdir(parents=True, exist_ok=True)

    (root / json_report).write_text(
        json.dumps(data, indent=2),
        encoding="utf-8",
        newline="\n",
    )

    lines = [
        "=" * 100,
        f"CAVECODE C# MODULE {args.module} CERTIFICATION",
        "=" * 100,
        "",
        f"Chapter: {module['Chapter']}",
        f"Topic: {module['Topic']}",
        f"Title: {module['Title']}",
        "",
        f"COMPLETE MODULE {args.module} CONTENT",
        "-" * 30,
    ]

    for field in FIELDS:
        lines += ["", f"{field}:", str(module[field])]

    lines += ["", "CERTIFICATION CHECKS", "--------------------"]

    for check in checks:
        status = "PASS" if check["passed"] else "FAIL"
        lines += [
            "",
            f"[{status}] {check['name']}",
            f"Severity: {check['severity']}",
            f"Details: {check['details']}",
        ]

    lines += [
        "",
        "SUMMARY",
        "-------",
        f"Checks passed: {summary['passed_checks']} / {summary['total_checks']}",
        f"Failed checks: {summary['failed_checks']}",
        f"Blockers: {summary['blockers']}",
        f"High: {summary['high']}",
        f"Medium: {summary['medium']}",
        "",
        f"Text report: {text_report}",
        f"JSON report: {json_report}",
        "",
    ]

    (root / text_report).write_text(
        "\n".join(lines),
        encoding="utf-8",
        newline="\n",
    )

    print(f"C# Module {args.module} Certification completed.")
    print()
    print(f"Module: {module['Title']}")
    print(
        f"Checks passed: "
        f"{summary['passed_checks']} / {summary['total_checks']}"
    )
    print(f"Failed checks: {summary['failed_checks']}")
    print(f"Blockers: {summary['blockers']}")
    print(f"High: {summary['high']}")
    print(f"Medium: {summary['medium']}")
    print()
    print(f"Text report: {text_report}")
    print(f"JSON report: {json_report}")
    print()
    print("No production code was modified.")


if __name__ == "__main__":
    try:
        main()
    except Exception as error:
        print(f"ERROR: {error}", file=sys.stderr)
        raise SystemExit(1)
