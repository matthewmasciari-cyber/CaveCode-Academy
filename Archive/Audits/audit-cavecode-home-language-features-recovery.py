#!/usr/bin/env python3
from pathlib import Path
import re
import sys

FEATURE_TERMS = {
    "language finder prompt": [
        "path-discovery",
        "help me choose",
        "let cavecode match",
        "path finder",
    ],
    "famous examples / real-world uses": [
        "real-world-uses",
        "what this language is used for",
        "notable games",
        "famous",
    ],
    "themed language icons": [
        "data-lucide",
        "path-card-icon",
        "language-icon",
        "icon-tile",
    ],
    "learning path card state logic": [
        "data-cavecode-language",
        "data-cavecode-panel-ready",
        "path-discovery-button",
    ],
}

CANDIDATE_GLOBS = [
    "Pages/Home.razor",
    ".*backup*/Pages/Home.razor",
    ".*/Pages/Home.razor",
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

def score(text: str):
    lowered = text.lower()
    details = {}
    total = 0
    for label, terms in FEATURE_TERMS.items():
        matches = [term for term in terms if term in lowered]
        details[label] = matches
        total += len(matches)
    return total, details

def main():
    root = find_root()
    current = root / "Pages/Home.razor"

    if not current.is_file():
        raise RuntimeError("Pages/Home.razor is missing.")

    candidates = [current]

    for path in root.glob(".*backup*/Pages/Home.razor"):
        if path not in candidates:
            candidates.append(path)

    for path in root.rglob("Home.razor"):
        if "Pages" not in path.parts:
            continue
        if path not in candidates:
            candidates.append(path)

    ranked = []

    for path in candidates:
        try:
            text = path.read_text(encoding="utf-8")
        except Exception:
            continue

        total, details = score(text)

        ranked.append({
            "path": path,
            "relative": path.relative_to(root),
            "score": total,
            "details": details,
            "modified": path.stat().st_mtime,
            "has_center_v2":
                "CAVECODE_HOME_LEARNING_PATHS_CENTER_V2" in text,
            "has_small_logo":
                "cavecode-learning-path-brandline" in text,
        })

    ranked.sort(
        key=lambda item: (
            item["score"],
            item["modified"],
        ),
        reverse=True,
    )

    print("CaveCode Home feature recovery audit")
    print(f"Root: {root}")
    print()

    print("CURRENT HOME")
    current_item = next(
        item for item in ranked
        if item["path"] == current
    )
    print(f"  Feature score: {current_item['score']}")
    print(f"  Centered heading V2: {current_item['has_center_v2']}")
    print(f"  Redundant logo present: {current_item['has_small_logo']}")

    for label, matches in current_item["details"].items():
        print(
            f"  {label}: "
            + (", ".join(matches) if matches else "MISSING")
        )

    print()
    print("TOP RECOVERY CANDIDATES")

    shown = 0

    for item in ranked:
        if item["path"] == current:
            continue

        if item["score"] == 0:
            continue

        shown += 1
        print()
        print(f"{shown}. {item['relative']}")
        print(f"   Feature score: {item['score']}")
        print(f"   Centered heading V2: {item['has_center_v2']}")
        print(f"   Redundant logo present: {item['has_small_logo']}")

        for label, matches in item["details"].items():
            print(
                f"   {label}: "
                + (", ".join(matches) if matches else "MISSING")
            )

        if shown >= 10:
            break

    if shown == 0:
        print("No backup containing the missing feature markers was found.")

    print()
    print("SUPPORTING LIVE ASSETS")

    assets = [
        Path("wwwroot/js/learning-path-discovery.js"),
        Path("wwwroot/css/learning-path-discovery.css"),
        Path("wwwroot/js/learning-path-real-world-uses.js"),
        Path("wwwroot/css/learning-path-real-world-uses.css"),
    ]

    for relative in assets:
        path = root / relative
        print(
            f"  {relative}: "
            + ("present" if path.is_file() else "MISSING")
        )

    print()
    print("NEXT STEP")
    print(
        "Paste this output back into ChatGPT. "
        "The recovery pass will restore the newest intact Home-page "
        "feature markup, keep the V2 centered heading, and avoid "
        "reintroducing the redundant Learning Paths logo."
    )

if __name__ == "__main__":
    try:
        main()
    except Exception as error:
        print(f"ERROR: {error}", file=sys.stderr)
        raise SystemExit(1)
