#!/usr/bin/env python3
from pathlib import Path
import shutil

root = Path.cwd()
target = root / "Components" / "CodeMinigame.razor"

if not target.exists():
    raise SystemExit(
        "Run this from /workspaces/CaveCode-Academy. "
        "Missing Components/CodeMinigame.razor"
    )

backup_dir = root / ".minigame-training-duplicate-guidance-backup"
backup_dir.mkdir(exist_ok=True)
shutil.copy2(
    target,
    backup_dir / "CodeMinigame.razor"
)

text = target.read_text(encoding="utf-8")

old = '''                <div class="objective">
                    <span>OBJECTIVE</span>
                    <strong>@State.Scenario.Objective</strong>
                </div>

                @if (State.Difficulty == "training")
                {
                    <div class="hint training-guidance">
                        <span>TRAINING GUIDANCE</span>
                        <strong>@State.Scenario.Hint</strong>
                    </div>
                }

                @if (ShowCoaching)
'''

new = '''                @if (State.Difficulty != "training")
                {
                    <div class="objective">
                        <span>OBJECTIVE</span>
                        <strong>@State.Scenario.Objective</strong>
                    </div>
                }

                @if (ShowCoaching)
'''

if old not in text:
    if 'State.Difficulty != "training"' in text and "TRAINING GUIDANCE" not in text:
        print("Training duplicate-guidance fix is already installed.")
    else:
        raise SystemExit(
            "Could not find the expected Objective/Training Guidance block. "
            "Install the UX refinement pass first."
        )
else:
    text = text.replace(old, new, 1)
    target.write_text(text, encoding="utf-8")

print("Training-mode duplicate answer blocks removed.")
print()
print("Training now shows:")
print("  - Mission title")
print("  - Mission brief")
print("  - Live coding guidance")
print("  - Optional paid hint")
print("  - Code editor")
print()
print("Training no longer shows:")
print("  - Exact Objective answer")
print("  - Exact Training Guidance answer")
print()
print("Standard, Advanced, and Expert still show the Objective block.")
print("Backup saved in .minigame-training-duplicate-guidance-backup/")
print("Next command: dotnet build")
