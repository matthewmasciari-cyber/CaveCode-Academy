#!/usr/bin/env python3
from pathlib import Path
import re
import subprocess
import sys
from datetime import datetime, timezone


MODULE_COUNT = 40
AUDITOR = Path("audit-cavecode-csharp-module-certification.py")
REPORT_DIR = Path("Archive/Reports/csharp-module-batch")
SUMMARY_PATH = Path("Archive/Reports/csharp-module-certification-summary.txt")


def main() -> None:
    if not AUDITOR.is_file():
        raise RuntimeError(f"Missing auditor: {AUDITOR}")

    REPORT_DIR.mkdir(parents=True, exist_ok=True)

    module_results = []

    for module_number in range(1, MODULE_COUNT + 1):
        print()
        print("=" * 80)
        print(f"AUDITING C# MODULE {module_number}")
        print("=" * 80)

        result = subprocess.run(
            [
                sys.executable,
                str(AUDITOR),
                str(module_number),
            ],
            text=True,
            capture_output=True,
        )

        output = result.stdout + result.stderr
        print(output, end="")

        log_path = REPORT_DIR / f"module-{module_number}.txt"
        log_path.write_text(
            output,
            encoding="utf-8",
            newline="\n",
        )

        title_match = re.search(
            r"^Module:\s*(.+)$",
            output,
            flags=re.MULTILINE,
        )

        checks_match = re.search(
            r"^Checks passed:\s*(\d+)\s*/\s*(\d+)$",
            output,
            flags=re.MULTILINE,
        )

        failed_match = re.search(
            r"^Failed checks:\s*(\d+)$",
            output,
            flags=re.MULTILINE,
        )

        title = (
            title_match.group(1).strip()
            if title_match
            else "Unknown module"
        )

        passed = (
            int(checks_match.group(1))
            if checks_match
            else 0
        )

        total = (
            int(checks_match.group(2))
            if checks_match
            else 0
        )

        failed = (
            int(failed_match.group(1))
            if failed_match
            else max(total - passed, 1)
        )

        module_results.append({
            "number": module_number,
            "title": title,
            "passed": passed,
            "total": total,
            "failed": failed,
            "exit_code": result.returncode,
            "log": str(log_path),
        })

    certified = sum(
        item["failed"] == 0 and item["exit_code"] == 0
        for item in module_results
    )

    passed_checks = sum(
        item["passed"]
        for item in module_results
    )

    total_checks = sum(
        item["total"]
        for item in module_results
    )

    lines = [
        "CAVECODE C# — 40-MODULE BATCH CERTIFICATION",
        "",
        f"Created UTC: {datetime.now(timezone.utc).isoformat()}",
        f"Modules certified: {certified} / {MODULE_COUNT}",
        f"Checks passed: {passed_checks} / {total_checks}",
        "",
        "MODULE RESULTS",
        "--------------",
    ]

    for item in module_results:
        status = (
            "CERTIFIED"
            if item["failed"] == 0 and item["exit_code"] == 0
            else "NEEDS REPAIR"
        )

        lines.append(
            f"Module {item['number']:>2}: "
            f"{item['title']} — "
            f"{item['passed']} / {item['total']} — "
            f"{status}"
        )

    failed_modules = [
        item
        for item in module_results
        if item["failed"] != 0 or item["exit_code"] != 0
    ]

    lines += [
        "",
        "FAILED MODULES",
        "--------------",
    ]

    if failed_modules:
        for item in failed_modules:
            lines.append(
                f"Module {item['number']}: "
                f"{item['title']} — "
                f"log: {item['log']}"
            )
    else:
        lines.append("None")

    SUMMARY_PATH.write_text(
        "\n".join(lines) + "\n",
        encoding="utf-8",
        newline="\n",
    )

    print()
    print("=" * 80)
    print("\n".join(lines))
    print()
    print(f"Summary: {SUMMARY_PATH}")


if __name__ == "__main__":
    try:
        main()
    except Exception as error:
        print(f"ERROR: {error}", file=sys.stderr)
        raise SystemExit(1)
