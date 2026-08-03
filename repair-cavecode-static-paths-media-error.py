#!/usr/bin/env python3
"""
CaveCode Academy — Fix static Learning Paths Razor @media error

This repair fixes:
    Pages/Home.razor(...): error CS0103:
    The name 'media' does not exist in the current context

COMMANDS
--------
cd /workspaces/CaveCode-Academy
python3 repair-cavecode-static-paths-media-error.py
dotnet build
dotnet run

Then hard-refresh with Ctrl + Shift + R.
"""

import shutil
import sys
from pathlib import Path


HOME_FILE = Path("Pages/Home.razor")
BACKUP_FILE = Path(
    ".static-learning-paths-media-fix-backup/"
    "Pages/Home.razor"
)
MARKER = "CAVECODE_STATIC_LEARNING_PATHS_REPAIR_V1"


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
        "Run this from /workspaces/CaveCode-Academy."
    )


def main() -> None:
    repo_root = locate_repo_root(Path.cwd().resolve())
    home_path = repo_root / HOME_FILE
    backup_path = repo_root / BACKUP_FILE

    text = home_path.read_text(encoding="utf-8")

    if MARKER not in text:
        fail(
            "The static Learning Paths repair marker was not found "
            "in Pages/Home.razor."
        )

    backup_path.parent.mkdir(parents=True, exist_ok=True)

    if not backup_path.exists():
        shutil.copy2(home_path, backup_path)

    marker_index = text.index(MARKER)
    before_marker = text[:marker_index]
    repaired_section = text[marker_index:]

    replacements = 0

    # Razor requires a literal CSS @ symbol inside a .razor style block
    # to be escaped as @@.
    if "@@media" not in repaired_section:
        repaired_section, replacements = repaired_section.replace(
            "@media (max-width: 820px)",
            "@@media (max-width: 820px)",
            1,
        )

    repaired = before_marker + repaired_section

    if (
        "@media (max-width: 820px)" in repaired_section
        and "@@media (max-width: 820px)" not in repaired_section
    ):
        fail(
            "The Razor media query could not be escaped."
        )

    required = [
        MARKER,
        "@@media (max-width: 820px)",
        "4 available · 6 in development · 10 total",
        'href="/csharp"',
        'href="/python"',
        'href="/cpp"',
        'href="/html-css"',
    ]

    missing = [
        item
        for item in required
        if item not in repaired
    ]

    if missing:
        fail(
            "Validation failed. Missing: "
            + ", ".join(missing)
        )

    card_count = repaired.count('class="path-card ')

    if card_count != 10:
        fail(
            f"Expected 10 static Learning Paths cards, "
            f"found {card_count}."
        )

    home_path.write_text(
        repaired,
        encoding="utf-8",
    )

    print("Static Learning Paths Razor media fix completed.")
    print()
    print("Fixed:")
    print("  - Escaped CSS @media as Razor @@media")
    print("  - Preserved all ten Learning Paths cards")
    print("  - Preserved C#, Python, C++, and HTML/CSS links")
    print("  - Preserved the existing homepage design and progress")
    print()
    print(
        "Backup: "
        f"{backup_path.relative_to(repo_root)}"
    )
    print()
    if replacements:
        print("Replacement applied: 1")
    else:
        print("The media query was already corrected.")
    print()
    print("Next commands:")
    print("  dotnet build")
    print("  dotnet run")
    print()
    print("Then hard-refresh with Ctrl + Shift + R.")


if __name__ == "__main__":
    main()
