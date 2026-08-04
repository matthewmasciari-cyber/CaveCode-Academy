#!/usr/bin/env python3
from pathlib import Path
import re
import sys

FILES = [
    Path("Pages/Leaderboard.razor"),
    Path("Services/ProgressionService.cs"),
    Path("wwwroot/js/caveCodeProgression.js"),
    Path("wwwroot/js/caveCodeAuth.js"),
]

PATTERNS = [
    r"leaderboard",
    r"leaderboard_profiles",
    r"getLeaderboard",
    r"setPublicLeaderboard",
    r"syncLeaderboardProfile",
    r"OnAfterRenderAsync",
    r"OnInitializedAsync",
    r"firstRender",
    r"initialize",
    r"getCurrentUser",
    r"supabase",
    r"localStorage",
    r"public",
    r"visibility",
]

CONTEXT = 18

def find_root() -> Path:
    root = Path.cwd().resolve()
    while root.parent != root and not (root / "CaveCode.csproj").is_file():
        root = root.parent
    if not (root / "CaveCode.csproj").is_file():
        raise RuntimeError("Run this inside /workspaces/CaveCode-Academy.")
    return root

def ranges_for_matches(lines):
    matched = []
    regexes = [re.compile(pattern, re.I) for pattern in PATTERNS]
    for index, line in enumerate(lines):
        if any(regex.search(line) for regex in regexes):
            matched.append(index)

    ranges = []
    for index in matched:
        start = max(0, index - CONTEXT)
        end = min(len(lines), index + CONTEXT + 1)
        if ranges and start <= ranges[-1][1]:
            ranges[-1] = (ranges[-1][0], max(ranges[-1][1], end))
        else:
            ranges.append((start, end))
    return ranges

def main():
    root = find_root()
    print(f"Root: {root}")
    print("Current files only; backup folders are excluded.")

    for relative in FILES:
        path = root / relative
        print()
        print("=" * 100)
        print(relative)
        print("=" * 100)

        if not path.is_file():
            print("MISSING")
            continue

        text = path.read_text(encoding="utf-8")
        lines = text.splitlines()
        ranges = ranges_for_matches(lines)

        if not ranges:
            print("No matching leaderboard/auth/progression references.")
            continue

        for start, end in ranges:
            print(f"\n--- lines {start + 1}-{end} ---")
            for index in range(start, end):
                print(f"{index + 1:5}: {lines[index]}")

    print()
    print("=" * 100)
    print("END OF CURRENT-FILE LEADERBOARD CAPTURE")
    print("=" * 100)
    print("Paste this complete output back into ChatGPT.")

if __name__ == "__main__":
    try:
        main()
    except Exception as error:
        print(f"ERROR: {error}", file=sys.stderr)
        raise SystemExit(1)
