#!/usr/bin/env python3
from __future__ import annotations

from pathlib import Path
import argparse
import json
import re
import sys
from datetime import datetime, timezone


SOURCE = Path("Pages/Python.razor")
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
        raise RuntimeError("Could not locate the Python Lessons list.")

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
            raise RuntimeError("Unbalanced Python Lesson constructor found.")

        blocks.append(section[pos:end - 1])
        cursor = end

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


def strip_comments(value: str) -> str:
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
    value = strip_comments(value)
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


def parse_modules(text: str) -> list[dict[str, object]]:
    modules = []

    for number, block in enumerate(extract_lesson_blocks(text), start=1):
        arguments = split_arguments(block)

        if len(arguments) != len(FIELDS):
            raise RuntimeError(
                f"Module {number}: expected {len(FIELDS)} fields; "
                f"found {len(arguments)}."
            )

        module: dict[str, object] = {"ModuleNumber": number}

        for field, raw in zip(FIELDS, arguments):
            if field == "PredictionOptions":
                module[field] = parse_options(raw)
            elif field == "PredictionCorrect":
                module[field] = int(strip_comments(raw))
            else:
                module[field] = decode_string(raw)

        modules.append(module)

    return modules


def python_identifiers(code: str) -> set[str]:
    code_without_strings = re.sub(
        r'("""[\s\S]*?"""|\'\'\'[\s\S]*?\'\'\'|"(?:\\.|[^"\\])*"|\'(?:\\.|[^\'\\])*\')',
        "",
        code,
    )

    keywords = {
        "and", "as", "assert", "async", "await", "break", "class",
        "continue", "def", "del", "elif", "else", "except", "False",
        "finally", "for", "from", "global", "if", "import", "in", "is",
        "lambda", "None", "nonlocal", "not", "or", "pass", "raise",
        "return", "True", "try", "while", "with", "yield",
    }

    return {
        token
        for token in re.findall(r"\b[A-Za-z_]\w*\b", code_without_strings)
        if token not in keywords
    }


def python_literals(code: str) -> set[str]:
    values = set()

    for match in re.findall(
        r'"((?:\\.|[^"\\])*)"|\'((?:\\.|[^\'\\])*)\'',
        code,
    ):
        value = match[0] or match[1]
        if value:
            values.add(value)

    values.update(re.findall(r"\b\d+(?:\.\d+)?\b", code))

    for literal in ("True", "False", "None"):
        if re.search(rf"\b{literal}\b", code):
            values.add(literal)

    return values


def contains_token(prompt: str, token: str) -> bool:
    if re.fullmatch(r"[A-Za-z_]\w*", token):
        return re.search(rf"\b{re.escape(token)}\b", prompt) is not None

    return token in prompt


def make_check(
    name: str,
    passed: bool,
    severity: str,
    details: str,
) -> dict[str, object]:
    return {
        "name": name,
        "passed": passed,
        "severity": severity,
        "details": details,
    }


def audit_module(module: dict[str, object]) -> list[dict[str, object]]:
    checks: list[dict[str, object]] = []

    text_fields = [
        str(module[field])
        for field in FIELDS
        if field not in {"PredictionOptions", "PredictionCorrect"}
    ]

    checks.append(make_check(
        "All lesson fields are populated",
        all(value.strip() for value in text_fields),
        "BLOCKER",
        "Every required lesson field must contain content.",
    ))

    fill = str(module["FillStarter"])
    checks.append(make_check(
        "Fill stage contains blank",
        "___" in fill,
        "BLOCKER",
        "FillStarter must contain at least one ___ placeholder.",
    ))

    options = list(module["PredictionOptions"])
    checks.append(make_check(
        "Prediction has four options",
        len(options) == 4,
        "BLOCKER",
        f"PredictionOptions contains {len(options)} option(s).",
    ))

    checks.append(make_check(
        "Prediction options are unique",
        len(options) == len(set(options)),
        "HIGH",
        "Prediction answers must not contain duplicates.",
    ))

    correct_index = int(module["PredictionCorrect"])
    checks.append(make_check(
        "Prediction correct index is valid",
        0 <= correct_index < len(options),
        "BLOCKER",
        f"PredictionCorrect is {correct_index}.",
    ))

    checks.append(make_check(
        "Prediction explanation exists",
        bool(str(module["PredictionExplanation"]).strip()),
        "HIGH",
        "PredictionExplanation must explain the correct answer.",
    ))

    target = str(module["TargetCode"]).strip()
    broken = str(module["BrokenCode"]).strip()

    checks.append(make_check(
        "Debug code differs from target",
        broken != target,
        "BLOCKER",
        "BrokenCode must differ from TargetCode.",
    ))

    checks.append(make_check(
        "Debug prompt explains the task",
        len(str(module["DebugPrompt"]).strip()) >= 12,
        "HIGH",
        "DebugPrompt should clearly describe what needs fixing.",
    ))

    recall = str(module["RecallPrompt"])
    target_ids = python_identifiers(target)
    missing_recall_ids = sorted(
        identifier
        for identifier in target_ids
        if not contains_token(recall, identifier)
    )

    checks.append(make_check(
        "Recall names are stated",
        not missing_recall_ids,
        "HIGH",
        (
            "RecallPrompt omits: " + ", ".join(missing_recall_ids)
            if missing_recall_ids
            else "All required identifiers are stated."
        ),
    ))

    target_values = python_literals(target)
    missing_recall_values = sorted(
        value
        for value in target_values
        if value not in recall
    )

    checks.append(make_check(
        "Recall values are fair",
        not missing_recall_values,
        "HIGH",
        (
            "RecallPrompt omits exact required values: "
            + ", ".join(missing_recall_values)
            if missing_recall_values
            else "All exact required values are stated."
        ),
    ))

    transfer = str(module["TransferPrompt"])
    transfer_code = str(module["TransferCode"]).strip()
    transfer_ids = python_identifiers(transfer_code)
    missing_transfer_ids = sorted(
        identifier
        for identifier in transfer_ids
        if not contains_token(transfer, identifier)
    )

    checks.append(make_check(
        "Transfer names are stated",
        not missing_transfer_ids,
        "BLOCKER",
        (
            "TransferPrompt omits: " + ", ".join(missing_transfer_ids)
            if missing_transfer_ids
            else "All required identifiers are stated."
        ),
    ))

    transfer_values = python_literals(transfer_code)
    missing_transfer_values = sorted(
        value
        for value in transfer_values
        if value not in transfer
    )

    checks.append(make_check(
        "Transfer values are stated",
        not missing_transfer_values,
        "HIGH",
        (
            "TransferPrompt omits exact required values: "
            + ", ".join(missing_transfer_values)
            if missing_transfer_values
            else "All exact required values are stated."
        ),
    ))

    vague_terms = [
        "sensible", "reasonable", "appropriate",
        "your choice", "any value", "some value",
    ]
    vague_found = [
        term for term in vague_terms if term in transfer.lower()
    ]

    checks.append(make_check(
        "Transfer wording matches exact validator",
        not vague_found,
        "HIGH",
        (
            "TransferPrompt uses vague wording: "
            + ", ".join(vague_found)
            if vague_found
            else "Transfer wording is deterministic."
        ),
    ))

    checks.append(make_check(
        "Target code exists",
        bool(target),
        "BLOCKER",
        "TargetCode must not be empty.",
    ))

    checks.append(make_check(
        "Transfer code exists",
        bool(transfer_code),
        "BLOCKER",
        "TransferCode must not be empty.",
    ))

    checks.append(make_check(
        "Preview message exists",
        bool(str(module["PreviewMessage"]).strip()),
        "MEDIUM",
        "PreviewMessage must not be empty.",
    ))

    checks.append(make_check(
        "Teaching includes Python content",
        bool(re.search(
            r"[A-Za-z_]\w*\s*=|print\s*\(|def\s+\w+\s*\(|with\s+open\s*\(|"
            r"\bif\b|\bfor\b|\bwhile\b|\blist\b|\bdict\b",
            str(module["Teaching"]) + "\n" + str(module["ExampleCode"]),
        )),
        "MEDIUM",
        "Teaching or ExampleCode should demonstrate Python syntax.",
    ))

    return checks


def main() -> None:
    parser = argparse.ArgumentParser(
        description="Audit one CaveCode Python curriculum module."
    )
    parser.add_argument("module", type=int)
    args = parser.parse_args()

    root = find_root()
    source_path = root / SOURCE

    if not source_path.is_file():
        raise RuntimeError(f"Missing source file: {SOURCE}")

    modules = parse_modules(
        source_path.read_text(encoding="utf-8-sig")
    )

    if not 1 <= args.module <= len(modules):
        raise RuntimeError(
            f"Module must be between 1 and {len(modules)}."
        )

    module = modules[args.module - 1]
    checks = audit_module(module)

    passed = sum(check["passed"] for check in checks)
    failed = len(checks) - passed

    severity_counts = {
        severity: sum(
            not check["passed"] and check["severity"] == severity
            for check in checks
        )
        for severity in ("BLOCKER", "HIGH", "MEDIUM")
    }

    summary = {
        "total_checks": len(checks),
        "passed_checks": passed,
        "failed_checks": failed,
        "blockers": severity_counts["BLOCKER"],
        "high": severity_counts["HIGH"],
        "medium": severity_counts["MEDIUM"],
    }

    report = {
        "pass": "Python Module Certification",
        "created_utc": datetime.now(timezone.utc).isoformat(),
        "module": module,
        "checks": checks,
        "summary": summary,
    }

    report_dir = root / REPORT_DIR
    report_dir.mkdir(parents=True, exist_ok=True)

    text_path = (
        report_dir
        / f"python-module-{args.module}-certification.txt"
    )
    json_path = (
        report_dir
        / f"python-module-{args.module}-certification.json"
    )

    json_path.write_text(
        json.dumps(report, indent=2),
        encoding="utf-8",
        newline="\n",
    )

    lines = [
        f"Python Module {args.module} Certification",
        "",
        f"Module: {module['Title']}",
        f"Checks passed: {passed} / {len(checks)}",
        f"Failed checks: {failed}",
        f"Blockers: {summary['blockers']}",
        f"High: {summary['high']}",
        f"Medium: {summary['medium']}",
        "",
    ]

    for check in checks:
        if check["passed"]:
            continue

        lines += [
            f"[FAIL] {check['name']}",
            f"Severity: {check['severity']}",
            f"Details: {check['details']}",
            "",
        ]

    lines += [
        f"Text report: {text_path.relative_to(root)}",
        f"JSON report: {json_path.relative_to(root)}",
        "",
        "No production code was modified.",
    ]

    text_path.write_text(
        "\n".join(lines),
        encoding="utf-8",
        newline="\n",
    )

    print(f"Python Module {args.module} Certification completed.")
    print()
    print(f"Module: {module['Title']}")
    print(f"Checks passed: {passed} / {len(checks)}")
    print(f"Failed checks: {failed}")
    print(f"Blockers: {summary['blockers']}")
    print(f"High: {summary['high']}")
    print(f"Medium: {summary['medium']}")
    print()
    print(f"Text report: {text_path.relative_to(root)}")
    print(f"JSON report: {json_path.relative_to(root)}")
    print()
    print("No production code was modified.")


if __name__ == "__main__":
    try:
        main()
    except Exception as error:
        print(f"ERROR: {error}", file=sys.stderr)
        raise SystemExit(1)
