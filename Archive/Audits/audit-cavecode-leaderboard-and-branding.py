#!/usr/bin/env python3
from pathlib import Path
import re
import sys

ROOT_MARKER = "CaveCode.csproj"

LEADERBOARD_TERMS = [
    "leaderboard",
    "leaderboard_profiles",
    "syncLeaderboardProfile",
    "GetLeaderboard",
    "LoadLeaderboard",
    "OnAfterRenderAsync",
    "OnInitializedAsync",
    "localStorage",
    "supabase",
]

BRANDING_TERMS = [
    "logo",
    "brand",
    "CaveCode",
    "img",
    "svg",
    "theme-glow",
    "glow",
    "justify-content",
    "align-items",
    "text-align",
    "margin:",
    "margin-inline",
]

TEXT_EXTENSIONS = {
    ".razor", ".cs", ".css", ".scss", ".js", ".ts",
    ".html", ".json", ".md"
}

def find_root() -> Path:
    root = Path.cwd().resolve()
    while root.parent != root and not (root / ROOT_MARKER).is_file():
        root = root.parent
    if not (root / ROOT_MARKER).is_file():
        raise RuntimeError(
            "Could not find CaveCode.csproj. Run this from the CaveCode-Academy workspace."
        )
    return root

def relevant_files(root: Path):
    ignored = {
        "bin", "obj", ".git", "node_modules",
        ".vs", ".idea"
    }
    for path in root.rglob("*"):
        if not path.is_file():
            continue
        if any(part in ignored for part in path.parts):
            continue
        if path.suffix.lower() in TEXT_EXTENSIONS:
            yield path

def line_matches(text: str, terms):
    results = []
    for number, line in enumerate(text.splitlines(), start=1):
        lowered = line.lower()
        if any(term.lower() in lowered for term in terms):
            results.append((number, line.rstrip()))
    return results

def print_section(title: str):
    print()
    print("=" * 88)
    print(title)
    print("=" * 88)

def main():
    root = find_root()
    files = list(relevant_files(root))

    print(f"CaveCode audit root: {root}")
    print(f"Text files scanned: {len(files)}")

    leaderboard_hits = []
    branding_hits = []

    for path in files:
        try:
            text = path.read_text(encoding="utf-8")
        except UnicodeDecodeError:
            continue

        leaderboard = line_matches(text, LEADERBOARD_TERMS)
        branding = line_matches(text, BRANDING_TERMS)

        if leaderboard:
            leaderboard_hits.append((path.relative_to(root), leaderboard))

        if branding:
            branding_hits.append((path.relative_to(root), branding))

    print_section("LEADERBOARD FILES AND LOAD/SYNC REFERENCES")

    if not leaderboard_hits:
        print("No leaderboard-related references found.")
    else:
        for relative, hits in leaderboard_hits:
            important = [
                item for item in hits
                if any(term.lower() in item[1].lower() for term in [
                    "leaderboard",
                    "onafterrenderasync",
                    "oninitializedasync",
                    "syncleaderboardprofile",
                    "localstorage",
                    "supabase",
                ])
            ]
            if not important:
                continue

            print(f"\n--- {relative} ---")
            for number, line in important[:120]:
                print(f"{number:5}: {line}")

    print_section("LIKELY LEADERBOARD REFRESH RISKS")

    risks = []

    for relative, hits in leaderboard_hits:
        joined = "\n".join(line for _, line in hits).lower()

        if "onafterrenderasync" in joined and "firstRender" in joined:
            risks.append(
                f"{relative}: leaderboard may load only on first render; "
                "check whether auth/profile readiness happens afterward."
            )

        if "localstorage" in joined and "leaderboard" in joined:
            risks.append(
                f"{relative}: localStorage appears involved in leaderboard state; "
                "fresh loads may briefly or permanently prefer the current user cache."
            )

        if "syncleaderboardprofile" in joined:
            risks.append(
                f"{relative}: contains leaderboard sync logic; verify it does not replace "
                "the full public list with only the signed-in profile."
            )

    if risks:
        for risk in sorted(set(risks)):
            print(f"- {risk}")
    else:
        print("- No obvious lifecycle/cache risk detected from keyword scanning.")
        print("- Inspect the printed leaderboard files manually for query filtering and auth timing.")

    print_section("BRANDING / LOGO / GRAPHIC LOCATIONS")

    preferred_names = (
        "Home", "Index", "Main", "Learning", "Appearance",
        "Achievements", "Leaderboard", "Minigames", "Layout",
        "Header", "Footer", "Nav"
    )

    selected = []
    for relative, hits in branding_hits:
        if any(name.lower() in str(relative).lower() for name in preferred_names):
            selected.append((relative, hits))

    if not selected:
        selected = branding_hits

    for relative, hits in selected:
        print(f"\n--- {relative} ---")
        for number, line in hits[:100]:
            print(f"{number:5}: {line}")

    print_section("CENTERING CHECKLIST")

    print("""
Review each branded page against these rules:

1. Main logo sits in its own centered wrapper.
2. Wrapper uses:
       display: flex or grid
       justify-content: center
       align-items: center
       width: 100%
3. Logo image/SVG uses:
       display: block
       margin-inline: auto
4. No inherited left padding or grid-column placement pulls the logo off-center.
5. Theme glow belongs to the logo wrapper or logo itself, not a white card background.
6. Page title remains below the centered logo.
7. Header/footer logos use intentional alignment and are not accidentally centered if navigation requires otherwise.
8. Check desktop, tablet, and mobile media queries for overrides.
""".strip())

    print_section("NEXT STEP")

    print("""
Paste this audit output back into ChatGPT.

The repair pass should then:
- fix leaderboard loading so a fresh page retrieves the full public list;
- preserve the signed-in user's row without filtering everyone else out;
- refresh again after auth/profile readiness;
- avoid replacing server results with a one-user local cache;
- center branded graphics on Home, Learning Paths, Appearance, Achievements,
  Leaderboard, and Minigames;
- preserve intentional header/footer navigation alignment.
""".strip())

if __name__ == "__main__":
    try:
        main()
    except Exception as error:
        print(f"ERROR: {error}", file=sys.stderr)
        raise SystemExit(1)
