#!/usr/bin/env python3
from pathlib import Path
import re
import shutil
import sys

root = Path.cwd()
settings_path = root / "Pages" / "Settings.razor"
service_path = root / "Services" / "ProfileService.cs"

backup_dir = root / ".username-confirmed-flow-runtime-backup"
backup_settings = backup_dir / "Settings.razor"
backup_service = backup_dir / "ProfileService.cs"

for path in [settings_path, service_path, backup_settings, backup_service]:
    if not path.exists():
        raise SystemExit(
            "Required file is missing: "
            + str(path.relative_to(root))
        )

# Preserve the current broken state before restoring.
broken_backup = root / ".username-broken-settings-recovery-backup"
broken_backup.mkdir(exist_ok=True)

shutil.copy2(
    settings_path,
    broken_backup / "Settings.razor"
)
shutil.copy2(
    service_path,
    broken_backup / "ProfileService.cs"
)

# Restore the exact files from immediately before the malformed repair.
shutil.copy2(backup_settings, settings_path)
shutil.copy2(backup_service, service_path)

text = settings_path.read_text(encoding="utf-8")

target = "SetDisplayNameAsync"
call_index = text.find(target)

if call_index < 0:
    raise SystemExit(
        "The restored Settings.razor does not contain "
        "SetDisplayNameAsync, so the expected old input handler "
        "could not be located."
    )

# Find the nearest preceding ChangeEventArgs method.
method_matches = list(
    re.finditer(
        r"private\s+(?:async\s+)?Task\s+([A-Za-z_]\w*)"
        r"\s*\(\s*ChangeEventArgs\s+args\s*\)",
        text[:call_index],
    )
)

if not method_matches:
    raise SystemExit(
        "Could not locate the display-name input method."
    )

method_match = method_matches[-1]
method_name = method_match.group(1)

open_brace = text.find("{", method_match.end())

if open_brace < 0 or open_brace > call_index:
    raise SystemExit(
        "Could not locate the opening brace for "
        + method_name
    )

# Find the matching closing brace while respecting quoted strings.
depth = 0
quote = None
escaped = False
close_brace = None

for index in range(open_brace, len(text)):
    character = text[index]

    if quote is not None:
        if escaped:
            escaped = False
        elif character == "\\":
            escaped = True
        elif character == quote:
            quote = None
        continue

    if character in ('"', "'"):
        quote = character
        continue

    if character == "{":
        depth += 1
    elif character == "}":
        depth -= 1

        if depth == 0:
            close_brace = index
            break

if close_brace is None:
    raise SystemExit(
        "Could not find the closing brace for "
        + method_name
    )

# Adapt the safe method body to whichever rename fields are actually
# present in the restored Settings page.
has_draft = bool(
    re.search(
        r"\bDisplayNameDraft\b",
        text
    )
)
has_error = bool(
    re.search(
        r"\bRenameError\b",
        text
    )
)
has_review = bool(
    re.search(
        r"\bCanReviewNameChange\b",
        text
    )
)

lines = [
    f"    private Task {method_name}(",
    "        ChangeEventArgs args",
    "    )",
    "    {",
]

if has_draft:
    lines.extend([
        "        DisplayNameDraft =",
        "            args.Value?.ToString() ?? string.Empty;",
    ])
else:
    lines.extend([
        "        Profile.DisplayName =",
        "            args.Value?.ToString() ?? string.Empty;",
    ])

if has_error:
    lines.extend([
        "",
        "        RenameError = string.Empty;",
    ])

if has_review:
    draft_expression = (
        "DisplayNameDraft"
        if has_draft
        else "Profile.DisplayName"
    )

    lines.extend([
        "",
        "        CanReviewNameChange =",
        "            !string.Equals(",
        f"                ({draft_expression} ?? string.Empty).Trim(),",
        "                (Profile.DisplayName ?? string.Empty).Trim(),",
        "                StringComparison.Ordinal",
        "            );",
    ])

lines.extend([
    "",
    "        return Task.CompletedTask;",
    "    }",
])

replacement = "\n".join(lines)

text = (
    text[:method_match.start()] +
    replacement +
    text[close_brace + 1:]
)

# Safety checks.
if "SetDisplayNameAsync" in text:
    raise SystemExit(
        "The old automatic-save call still exists after the repair."
    )

# Basic brace balance for the @code section.
code_index = text.find("@code")

if code_index >= 0:
    code_text = text[code_index:]
    balance = code_text.count("{") - code_text.count("}")

    if balance != 0:
        raise SystemExit(
            f"Settings.razor still has an unbalanced @code block "
            f"(brace difference: {balance})."
        )

settings_path.write_text(text, encoding="utf-8")

print("CaveCode Settings page recovered.")
print()
print("Recovery performed:")
print("  - Restored Settings.razor from the pre-error backup")
print("  - Restored ProfileService.cs from the pre-error backup")
print(f"  - Replaced only the {method_name} input method")
print("  - Removed the forbidden SetDisplayNameAsync auto-save call")
print("  - Preserved the existing rename UI fields when present")
print("  - Verified the @code braces are balanced")
print()
print("Typing now updates only the browser-side draft/UI state.")
print("It does not save a name, spend crystals, or consume the free rename.")
print()
print("The broken files were preserved in:")
print("  .username-broken-settings-recovery-backup/")
print()
print("Next command: dotnet build")
