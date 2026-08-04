#!/usr/bin/env python3
from pathlib import Path
import re
import shutil

root = Path.cwd()
target = root / "Components" / "CodeMinigame.razor"

if not target.exists():
    raise SystemExit(
        "Run this from /workspaces/CaveCode-Academy. "
        "Missing Components/CodeMinigame.razor"
    )

backup_dir = root / ".minigame-navigation-callback-backup"
backup_dir.mkdir(exist_ok=True)
shutil.copy2(
    target,
    backup_dir / "CodeMinigame.razor"
)

text = target.read_text(encoding="utf-8")

pattern = re.compile(
    r"private\s+async\s+ValueTask\s+HandleInternalNavigation\s*\("
)

if pattern.search(text):
    text = pattern.sub(
        "private async Task HandleInternalNavigation(",
        text,
        count=1
    )
elif re.search(
    r"private\s+async\s+Task\s+HandleInternalNavigation\s*\(",
    text
):
    print("Navigation callback already returns Task.")
else:
    raise SystemExit(
        "Could not find HandleInternalNavigation in "
        "Components/CodeMinigame.razor."
    )

target.write_text(text, encoding="utf-8")

print("Fixed the NavigationLock callback signature.")
print("Changed HandleInternalNavigation from ValueTask to Task.")
print("No minigame logic, saved data, hints, or styling were changed.")
print("Backup saved in .minigame-navigation-callback-backup/")
print("Next command: dotnet build")
