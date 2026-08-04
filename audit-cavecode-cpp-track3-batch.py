#!/usr/bin/env python3
from pathlib import Path
import json
import subprocess
import sys
from datetime import datetime, timezone


MODULE_COUNT = 8
AUDITOR = Path("audit-cavecode-cpp-module-certification.py")
REPORT_DIR = Path("Archive/Reports")
SUMMARY_TEXT = REPORT_DIR / "cpp-track3-certification-summary.txt"
SUMMARY_JSON = REPORT_DIR / "cpp-track3-certification-summary.json"


def main() -> None:
    if not AUDITOR.is_file():
        raise RuntimeError(f"Missing auditor: {AUDITOR}")

    REPORT_DIR.mkdir(parents=True, exist_ok=True)

    modules = []

    for module_number in range(1, MODULE_COUNT + 1):
        print("=" * 80)
        print(f"Auditing C++ Module {module_number}")
        print("=" * 80)

        result = subprocess.run(
            [
                sys.executable,
                str(AUDITOR),
                str(module_number),
            ],
            text=True,
        )

        if result.returncode != 0:
            raise RuntimeError(
                f"C++ Module {module_number} auditor failed to run."
            )

        report_path = (
            REPORT_DIR
            / f"cpp-module-{module_number}-certification.json"
        )

        if not report_path.is_file():
            raise RuntimeError(
                f"Missing certification report: {report_path}"
            )

        report = json.loads(
            report_path.read_text(encoding="utf-8")
        )

        modules.append({
            "module_number": module_number,
            "title": report["module"]["Title"],
            "summary": report["summary"],
        })

    total_checks = sum(
        module["summary"]["total_checks"]
        for module in modules
    )

    passed_checks = sum(
        module["summary"]["passed_checks"]
        for module in modules
    )

    failed_checks = sum(
        module["summary"]["failed_checks"]
        for module in modules
    )

    blockers = sum(
        module["summary"]["blockers"]
        for module in modules
    )

    high = sum(
        module["summary"]["high"]
        for module in modules
    )

    medium = sum(
        module["summary"]["medium"]
        for module in modules
    )

    certified_modules = sum(
        module["summary"]["failed_checks"] == 0
        for module in modules
    )

    summary = {
        "pass": "C++ Track 3 Batch Certification",
        "created_utc": datetime.now(timezone.utc).isoformat(),
        "module_count": MODULE_COUNT,
        "certified_modules": certified_modules,
        "total_checks": total_checks,
        "passed_checks": passed_checks,
        "failed_checks": failed_checks,
        "blockers": blockers,
        "high": high,
        "medium": medium,
        "modules": modules,
    }

    SUMMARY_JSON.write_text(
        json.dumps(summary, indent=2),
        encoding="utf-8",
        newline="\n",
    )

    lines = [
        "CAVECODE C++ TRACK 3 — BATCH CERTIFICATION",
        "",
        f"Modules certified: {certified_modules} / {MODULE_COUNT}",
        f"Checks passed: {passed_checks} / {total_checks}",
        f"Failed checks: {failed_checks}",
        f"Blockers: {blockers}",
        f"High: {high}",
        f"Medium: {medium}",
        "",
        "MODULE RESULTS",
        "--------------",
    ]

    for module in modules:
        module_summary = module["summary"]
        status = (
            "CERTIFIED"
            if module_summary["failed_checks"] == 0
            else "NEEDS REPAIR"
        )

        lines.append(
            f"Module {module['module_number']}: "
            f"{module['title']} — "
            f"{module_summary['passed_checks']} / "
            f"{module_summary['total_checks']} — "
            f"{status}"
        )

    lines += [
        "",
        f"Text report: {SUMMARY_TEXT}",
        f"JSON report: {SUMMARY_JSON}",
    ]

    SUMMARY_TEXT.write_text(
        "\n".join(lines),
        encoding="utf-8",
        newline="\n",
    )

    print()
    print("\n".join(lines))


if __name__ == "__main__":
    try:
        main()
    except Exception as error:
        print(f"ERROR: {error}", file=sys.stderr)
        raise SystemExit(1)
