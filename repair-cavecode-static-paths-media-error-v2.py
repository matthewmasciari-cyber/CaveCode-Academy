#!/usr/bin/env python3
"""
CaveCode Academy — Static Learning Paths Razor Media Fix v2

Fixes this build error:
    Pages/Home.razor(...): error CS0103:
    The name 'media' does not exist in the current context

Run:
    cd /workspaces/CaveCode-Academy
    python3 repair-cavecode-static-paths-media-error-v2.py
    dotnet build
    dotnet run

Then hard-refresh with Ctrl + Shift + R.
"""

import re
import shutil
import sys
from pathlib import Path


HOME_FILE = Path("Pages/Home.razor")
BACKUP_FILE = Path(
    ".static-learning-paths-media-fix-v2-backup/"
    "Pages/Home.razor"
)
STATIC_MARKER = "CAVECODE_STATIC_LEARNING_PATHS_REPAIR_V1"


def fail(message: str) -> None:
    print(f"ERROR: {message}", file=sys.stderr)
    raise SystemExit(1)


def locate_repo_root(start: Path) -> Path:
    for candidate in [start, *start.parents]:
        if (
            (candidate / "CaveCode.csproj").is_file()
            and (candidate / HOME_FILE).is_file()
        ):
            return candidate

    fail(
        "Could not find the CaveCode repository root. "
        "Run from /workspaces/CaveCode-Academy."
    )


def main() -> None:
    repo_root = locate_repo_root(Path.cwd().resolve())
    home_path = repo_root / HOME_FILE
    backup_path = repo_root / BACKUP_FILE

    original = home_path.read_text(encoding="utf-8")

    if STATIC_MARKER not in original:
        fail(
            "The static Learning Paths repair marker was not found "
            "inside Pages/Home.razor."
        )

    backup_path.parent.mkdir(parents=True, exist_ok=True)

    if not backup_path.exists():
        shutil.copy2(home_path, backup_path)

    marker_index = original.index(STATIC_MARKER)
    prefix = original[:marker_index]
    static_section = original[marker_index:]

    # Razor interprets a single @ as C#.
    # A literal CSS media query inside a .razor file must start with @@media.
    repaired_section, replacement_count = re.subn(
        r"(?<!@)@media\b",
        "@@media",
        static_section,
    )

    repaired = prefix + repaired_section

    remaining_unescaped = re.findall(
        r"(?<!@)@media\b",
        repaired_section,
    )

    if remaining_unescaped:
        fail(
            "One or more unescaped @media directives remain "
            "inside the static Learning Paths section."
        )

    required_markers = [
        STATIC_MARKER,
        "@@media",
        "4 available · 6 in development · 10 total",
        'href="/csharp"',
        'href="/python"',
        'href="/cpp"',
        'href="/html-css"',
    ]

    missing = [
        marker
        for marker in required_markers
        if marker not in repaired
    ]

    if missing:
        fail(
            "Validation failed. Missing expected homepage content: "
            + ", ".join(missing)
        )

    home_path.write_text(
        repaired,
        encoding="utf-8",
        newline="\n",
    )

    print("Static Learning Paths Razor media fix v2 completed.")
    print()
    print(f"Escaped media queries: {replacement_count}")
    print("Preserved:")
    print("  - All static Learning Paths cards")
    print("  - C#, Python, C++, and HTML/CSS links")
    print("  - Existing homepage content and progression")
    print()
    print(
        "Backup: "
        f"{backup_path.relative_to(repo_root)}"
    )
    print()
    print("Next commands:")
    print("  dotnet build")
    print("  dotnet run")
    print()
    print("Then hard-refresh with Ctrl + Shift + R.")


if __name__ == "__main__":
    main()
