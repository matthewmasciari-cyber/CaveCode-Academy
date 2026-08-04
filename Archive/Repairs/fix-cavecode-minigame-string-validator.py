#!/usr/bin/env python3
from pathlib import Path
import shutil

root = Path.cwd()
target = root / "wwwroot" / "js" / "caveCodeMinigames.js"

if not target.exists():
    raise SystemExit(
        "Run this from the CaveCode-Academy repository root. "
        "Missing wwwroot/js/caveCodeMinigames.js"
    )

backup = root / ".minigame-string-validator-backup"
backup.mkdir(exist_ok=True)
shutil.copy2(
    target,
    backup / "caveCodeMinigames.js"
)

text = target.read_text(encoding="utf-8")

old = '''        return (s.all||[]).every(x=>v.includes(String(x).toLowerCase()))
            && (s.any||[]).every(group=>group.some(x=>v.includes(String(x).toLowerCase())))
            && (s.none||[]).every(x=>!v.includes(String(x).toLowerCase()));'''

new = '''        return (s.all||[]).every(x=>v.includes(compact(x)))
            && (s.any||[]).every(group=>group.some(x=>v.includes(compact(x))))
            && (s.none||[]).every(x=>!v.includes(compact(x)));'''

if old not in text:
    if new in text:
        print("String validator fix is already installed.")
    else:
        raise SystemExit(
            "Could not find the expected validator block. "
            "The file may differ from the current minigame overhaul."
        )
else:
    text = text.replace(old, new, 1)
    target.write_text(text, encoding="utf-8")
    print("Fixed minigame string validation.")
    print("Expected answers and submitted code now use identical normalization.")
    print('Multi-word strings such as "Shadow Gate" can now pass.')
    print("Backup saved in .minigame-string-validator-backup/")

index = root / "wwwroot" / "index.html"
if index.exists():
    html = index.read_text(encoding="utf-8")
    html = html.replace(
        "js/caveCodeMinigames.js?v=2",
        "js/caveCodeMinigames.js?v=3"
    )
    index.write_text(html, encoding="utf-8")

print("Next command: dotnet build")
