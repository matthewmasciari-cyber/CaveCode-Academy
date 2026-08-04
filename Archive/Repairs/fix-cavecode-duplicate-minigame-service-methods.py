#!/usr/bin/env python3
from pathlib import Path
import re
import shutil

root = Path.cwd()
target = root / "Services" / "MinigameService.cs"

if not target.exists():
    raise SystemExit(
        "Run this from /workspaces/CaveCode-Academy. "
        "Missing Services/MinigameService.cs"
    )

backup_dir = root / ".minigame-duplicate-service-methods-backup"
backup_dir.mkdir(exist_ok=True)
shutil.copy2(
    target,
    backup_dir / "MinigameService.cs"
)

text = target.read_text(encoding="utf-8")

patterns = {
    "AnalyzeAsync": re.compile(
        r"""
        \n[ \t]*public[ \t]+ValueTask<MinigameAnalysisResult>
        [ \t]+AnalyzeAsync\(
        [ \t\r\n]*string[ \t]+course,
        [ \t\r\n]*string[ \t]+code\)
        [ \t\r\n]*=>
        [ \t\r\n]*js\.InvokeAsync<MinigameAnalysisResult>\(
        [ \t\r\n]*"caveCodeMinigames\.analyze",
        [ \t\r\n]*course,
        [ \t\r\n]*code\);
        """,
        re.VERBOSE,
    ),
    "UseHintAsync": re.compile(
        r"""
        \n[ \t]*public[ \t]+ValueTask<MinigameHintResult>
        [ \t]+UseHintAsync\(
        [ \t\r\n]*string[ \t]+course\)
        [ \t\r\n]*=>
        [ \t\r\n]*js\.InvokeAsync<MinigameHintResult>\(
        [ \t\r\n]*"caveCodeMinigames\.useHint",
        [ \t\r\n]*course\);
        """,
        re.VERBOSE,
    ),
    "ResetRunAsync": re.compile(
        r"""
        \n[ \t]*public[ \t]+ValueTask<MinigameCourseState>
        [ \t]+ResetRunAsync\(
        [ \t\r\n]*string[ \t]+course\)
        [ \t\r\n]*=>
        [ \t\r\n]*js\.InvokeAsync<MinigameCourseState>\(
        [ \t\r\n]*"caveCodeMinigames\.resetRun",
        [ \t\r\n]*course\);
        """,
        re.VERBOSE,
    ),
}

removed = {}

for name, pattern in patterns.items():
    matches = list(pattern.finditer(text))

    if not matches:
        raise SystemExit(
            f"Could not find {name} in Services/MinigameService.cs."
        )

    removed[name] = max(0, len(matches) - 1)

    for match in reversed(matches[1:]):
        text = text[:match.start()] + "\n" + text[match.end():]

target.write_text(text, encoding="utf-8")

print("Duplicate MinigameService methods cleaned up.")
for name, count in removed.items():
    print(f"  - {name}: removed {count} duplicate copy/copies")

print()
print("Kept one valid copy of every method.")
print("No JavaScript, Razor UI, hint rules, or saved data were changed.")
print("Backup saved in .minigame-duplicate-service-methods-backup/")
print("Next command: dotnet build")
