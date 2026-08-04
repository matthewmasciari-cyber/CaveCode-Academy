#!/usr/bin/env python3
from pathlib import Path
import re
import shutil

root = Path.cwd()
target = root / "Pages" / "Settings.razor"

if not target.exists():
    raise SystemExit(
        "Run this from /workspaces/CaveCode-Academy. "
        "Missing Pages/Settings.razor"
    )

backup_dir = root / ".username-input-runtime-fix-backup"
backup_dir.mkdir(exist_ok=True)
shutil.copy2(
    target,
    backup_dir / "Settings.razor"
)

text = target.read_text(encoding="utf-8")

# 1. Replace the computed property with a simple field.
computed_pattern = re.compile(
    r'''    private bool CanReviewNameChange =>
        RenameStatus\.CanChangeNow &&
        \(
            RenameStatus\.CurrentCost == 0 \|\|
            RenameStatus\.CanAfford
        \) &&
        NormalizeDisplayName\(DisplayNameDraft\) !=
        NormalizeDisplayName\(Profile\.DisplayName\);

''',
    re.VERBOSE,
)

if computed_pattern.search(text):
    text = computed_pattern.sub(
        "    private bool CanReviewNameChange;\n\n",
        text,
        count=1,
    )
elif "private bool CanReviewNameChange;" not in text:
    raise SystemExit(
        "Could not find the live CanReviewNameChange calculation."
    )

# 2. Replace the input handler with a guarded async handler.
handler_pattern = re.compile(
    r'''    private void UpdateDisplayNameDraft\(
        ChangeEventArgs args
    \)
    \{
.*?
    \}

''',
    re.DOTALL,
)

handler_replacement = r'''    private Task UpdateDisplayNameDraft(
        ChangeEventArgs args
    )
    {
        try
        {
            DisplayNameDraft =
                args.Value?.ToString() ?? "";

            RenameError = "";
            RefreshRenameButtonState();
        }
        catch (Exception exception)
        {
            CanReviewNameChange = false;
            RenameError =
                $"The name field could not be updated: {exception.Message}";
        }

        return Task.CompletedTask;
    }

    private void RefreshRenameButtonState()
    {
        string draft =
            NormalizeDisplayName(
                DisplayNameDraft
            );

        string currentName =
            NormalizeDisplayName(
                Profile?.DisplayName
            );

        CanReviewNameChange =
            RenameStatus is not null &&
            RenameStatus.CanChangeNow &&
            (
                RenameStatus.CurrentCost == 0 ||
                RenameStatus.CanAfford
            ) &&
            !string.Equals(
                draft,
                currentName,
                StringComparison.Ordinal
            );
    }

'''

if handler_pattern.search(text):
    text = handler_pattern.sub(
        handler_replacement,
        text,
        count=1,
    )
elif "RefreshRenameButtonState" not in text:
    raise SystemExit(
        "Could not replace UpdateDisplayNameDraft."
    )

# 3. Replace the normalizer with a short, null-safe version.
normalizer_pattern = re.compile(
    r'''    private static string NormalizeDisplayName\(
        string\? value
    \)
    \{
.*?
    \}

    protected override''',
    re.DOTALL,
)

normalizer_replacement = r'''    private static string NormalizeDisplayName(
        string? value
    )
    {
        string normalized =
            string.Join(
                " ",
                (value ?? string.Empty)
                    .Split(
                        ' ',
                        StringSplitOptions
                            .RemoveEmptyEntries
                    )
            )
            .Trim();

        if (normalized.Length > 24)
        {
            normalized =
                normalized.Substring(0, 24);
        }

        return normalized;
    }

    protected override'''

if normalizer_pattern.search(text):
    text = normalizer_pattern.sub(
        normalizer_replacement,
        text,
        count=1,
    )
else:
    raise SystemExit(
        "Could not replace NormalizeDisplayName."
    )

# 4. Refresh the button state wherever the draft/status is loaded.
load_snippet = '''        DisplayNameDraft =
            Profile.DisplayName;

        Ready = true;
'''

load_replacement = '''        DisplayNameDraft =
            Profile.DisplayName;

        RefreshRenameButtonState();

        Ready = true;
'''

if load_snippet in text:
    text = text.replace(
        load_snippet,
        load_replacement,
        1,
    )

# Reset profile path.
reset_snippet = '''        DisplayNameDraft =
            Profile.DisplayName;

        ProfileSaveMessage =
'''

reset_replacement = '''        DisplayNameDraft =
            Profile.DisplayName;

        RefreshRenameButtonState();

        ProfileSaveMessage =
'''

if reset_snippet in text:
    text = text.replace(
        reset_snippet,
        reset_replacement,
        1,
    )

# Rename confirmation path.
confirm_snippet = '''                DisplayNameDraft =
                    Profile.DisplayName;

                ShowRenameConfirmation = false;
'''

confirm_replacement = '''                DisplayNameDraft =
                    Profile.DisplayName;

                RefreshRenameButtonState();
                ShowRenameConfirmation = false;
'''

if confirm_snippet in text:
    text = text.replace(
        confirm_snippet,
        confirm_replacement,
        1,
    )

# 5. Add explicit input type and autocomplete behavior to avoid browser
# autofill injecting unexpected values.
text = text.replace(
    '''                                <input type="text"
                                       maxlength="24"
                                       value="@DisplayNameDraft"''',
    '''                                <input type="text"
                                       maxlength="24"
                                       autocomplete="off"
                                       autocapitalize="none"
                                       value="@DisplayNameDraft"''',
    1,
)

target.write_text(text, encoding="utf-8")

print("CaveCode username input runtime fix installed.")
print()
print("Fixed:")
print("  - Removed the render-time computed rename-button expression")
print("  - Added a guarded Task-based input handler")
print("  - Added a null-safe display-name normalizer")
print("  - Button eligibility now updates explicitly after input/load/reset/rename")
print("  - Browser autofill is disabled for the custom display-name field")
print()
print("No crystal balance, free-change, 500-crystal, or cooldown rules changed.")
print("Backup saved in .username-input-runtime-fix-backup/")
print("Next command: dotnet build")
