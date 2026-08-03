#!/usr/bin/env python3
from pathlib import Path
import re
import shutil

root = Path.cwd()
settings_path = root / "Pages" / "Settings.razor"
service_path = root / "Services" / "ProfileService.cs"

for path in [settings_path, service_path]:
    if not path.exists():
        raise SystemExit(
            "Run this from /workspaces/CaveCode-Academy. "
            f"Missing: {path}"
        )

backup = root / ".username-confirmed-flow-runtime-backup"
backup.mkdir(exist_ok=True)

shutil.copy2(
    settings_path,
    backup / "Settings.razor"
)
shutil.copy2(
    service_path,
    backup / "ProfileService.cs"
)

settings = settings_path.read_text(encoding="utf-8")

method_pattern = re.compile(
    r'''    private\s+async\s+Task\s+
        (?P<name>[A-Za-z_]\w*)
        \(\s*ChangeEventArgs\s+args\s*\)
        \s*\{
        (?P<body>.*?
        ProfileService\s*\.\s*SetDisplayNameAsync
        .*?)
        \n    \}
''',
    re.VERBOSE | re.DOTALL,
)

match = method_pattern.search(settings)

if match:
    method_name = match.group("name")

    replacement = f'''    private Task {method_name}(
        ChangeEventArgs args
    )
    {{
        DisplayNameDraft =
            args.Value?.ToString() ?? string.Empty;

        RenameError = string.Empty;

        CanReviewNameChange =
            !string.Equals(
                DisplayNameDraft.Trim(),
                (Profile.DisplayName ?? string.Empty).Trim(),
                StringComparison.Ordinal
            );

        return Task.CompletedTask;
    }}
'''

    settings = (
        settings[:match.start()] +
        replacement +
        settings[match.end():]
    )
else:
    direct_call_pattern = re.compile(
        r'''Profile\s*=\s*await\s+
            ProfileService\s*\.\s*SetDisplayNameAsync
            \(
            .*?
            \);
''',
        re.VERBOSE | re.DOTALL,
    )

    settings, count = direct_call_pattern.subn(
        '''DisplayNameDraft =
            args.Value?.ToString() ?? string.Empty;

        RenameError = string.Empty;

        CanReviewNameChange =
            !string.Equals(
                DisplayNameDraft.Trim(),
                (Profile.DisplayName ?? string.Empty).Trim(),
                StringComparison.Ordinal
            );''',
        settings,
        count=1,
    )

    if count == 0 and "SetDisplayNameAsync" in settings:
        raise SystemExit(
            "A SetDisplayNameAsync reference remains in Settings.razor "
            "but its surrounding handler could not be safely identified."
        )

if "SetDisplayNameAsync" in settings:
    raise SystemExit(
        "The old automatic display-name save call still exists "
        "in Settings.razor."
    )

settings_path.write_text(settings, encoding="utf-8")

service = service_path.read_text(encoding="utf-8")

legacy_pattern = re.compile(
    r'''    public\s+ValueTask<ProfilePreferences>\s+
        SetDisplayNameAsync
        \(
        \s*string\s+displayName\s*
        \)
        \s*=>
        \s*js\.InvokeAsync<ProfilePreferences>
        \(
        \s*"caveCodeProfile\.setPreference",
        \s*"displayName",
        \s*displayName
        \s*
        \);
''',
    re.VERBOSE | re.DOTALL,
)

safe_legacy = '''    public ValueTask<ProfilePreferences> SetDisplayNameAsync(
        string displayName
    ) =>
        GetPreferencesAsync();
'''

if legacy_pattern.search(service):
    service = legacy_pattern.sub(
        safe_legacy,
        service,
        count=1,
    )

service_path.write_text(service, encoding="utf-8")

print("CaveCode confirmed-name-flow runtime fix installed.")
print()
print("Fixed:")
print("  - Typing no longer calls SetDisplayNameAsync")
print("  - Typing updates only the local DisplayNameDraft")
print("  - No crystals are charged while typing")
print("  - The free rename is not consumed while typing")
print("  - The Review Change button can update from the draft")
print("  - The old service method is now a harmless compatibility fallback")
print()
print("The confirmed rename modal remains the only save path.")
print("Backup saved in .username-confirmed-flow-runtime-backup/")
print("Next command: dotnet build")
