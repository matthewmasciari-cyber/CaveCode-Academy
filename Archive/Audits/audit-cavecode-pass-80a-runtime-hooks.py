#!/usr/bin/env python3
from pathlib import Path
import re
import sys

FILES = [
    Path("Pages/Home.razor"),
    Path("wwwroot/index.html"),
    Path("wwwroot/js/learning-path-discovery.js"),
    Path("wwwroot/js/learning-path-real-world-uses.js"),
    Path("wwwroot/css/learning-path-discovery.css"),
    Path("wwwroot/css/learning-path-real-world-uses.css"),
]

TERMS = [
    "data-cavecode-language",
    "data-language",
    "data-cavecode-path-discovery",
    "path-discovery-button",
    "learning-path-discovery.js",
    "learning-path-real-world-uses.js",
    "lucide",
    "createIcons",
    "querySelector",
    "path-card",
    "real-world-uses",
    "DOMContentLoaded",
    "MutationObserver",
]

def find_root():
    root = Path.cwd().resolve()
    while root.parent != root and not (root / "CaveCode.csproj").is_file():
        root = root.parent
    if not (root / "CaveCode.csproj").is_file():
        raise RuntimeError("Run from /workspaces/CaveCode-Academy.")
    return root

def show_matches(path, text):
    print()
    print("=" * 96)
    print(path)
    print("=" * 96)

    lines = text.splitlines()
    hits = []

    for i, line in enumerate(lines, start=1):
        lowered = line.lower()
        if any(term.lower() in lowered for term in TERMS):
            hits.append(i)

    if not hits:
        print("No matching runtime hooks found.")
        return

    ranges = []
    for line_no in hits:
        start = max(1, line_no - 6)
        end = min(len(lines), line_no + 8)

        if ranges and start <= ranges[-1][1] + 1:
            ranges[-1] = (ranges[-1][0], max(ranges[-1][1], end))
        else:
            ranges.append((start, end))

    for start, end in ranges:
        print(f"\n--- lines {start}-{end} ---")
        for i in range(start, end + 1):
            print(f"{i:5}: {lines[i - 1]}")

def main():
    root = find_root()
    print(f"Root: {root}")

    for relative in FILES:
        path = root / relative
        if not path.is_file():
            print()
            print("=" * 96)
            print(relative)
            print("=" * 96)
            print("MISSING")
            continue

        text = path.read_text(encoding="utf-8")
        show_matches(relative, text)

    print()
    print("=" * 96)
    print("SUMMARY CHECK")
    print("=" * 96)

    home = (root / "Pages/Home.razor").read_text(encoding="utf-8")
    index = (root / "wwwroot/index.html").read_text(encoding="utf-8")

    checks = {
        "C# hook":
            'data-cavecode-language="csharp"' in home,
        "Python hook":
            'data-cavecode-language="python"' in home,
        "C++ hook":
            'data-cavecode-language="cpp"' in home,
        "HTML/CSS hook":
            'data-cavecode-language="htmlcss"' in home,
        "finder launcher markup":
            "data-cavecode-path-discovery-open" in home,
        "discovery script linked":
            "learning-path-discovery.js" in index,
        "real-world script linked":
            "learning-path-real-world-uses.js" in index,
        "Lucide script linked":
            "lucide" in index.lower(),
    }

    for label, passed in checks.items():
        print(f"{label}: {'YES' if passed else 'NO'}")

    print()
    print("Paste this output back into ChatGPT.")

if __name__ == "__main__":
    try:
        main()
    except Exception as error:
        print(f"ERROR: {error}", file=sys.stderr)
        raise SystemExit(1)
