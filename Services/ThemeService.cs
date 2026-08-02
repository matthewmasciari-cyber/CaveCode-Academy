using Microsoft.JSInterop;

namespace CaveCode.Services;

public sealed class ThemeService(IJSRuntime js)
{
    public ValueTask<ThemePreferences> GetPreferencesAsync() =>
        js.InvokeAsync<ThemePreferences>("caveCodeTheme.getPreferences");

    public ValueTask<ThemePreferences> SetThemeAsync(string theme) =>
        js.InvokeAsync<ThemePreferences>("caveCodeTheme.setPreference", "theme", theme);

    public ValueTask<ThemePreferences> SetModeAsync(string mode) =>
        js.InvokeAsync<ThemePreferences>("caveCodeTheme.setPreference", "mode", mode);

    public ValueTask<ThemePreferences> SetTextSizeAsync(string textSize) =>
        js.InvokeAsync<ThemePreferences>("caveCodeTheme.setPreference", "textSize", textSize);

    public ValueTask<ThemePreferences> SetReducedMotionAsync(bool reducedMotion) =>
        js.InvokeAsync<ThemePreferences>("caveCodeTheme.setPreference", "reducedMotion", reducedMotion);

    public ValueTask<ThemePreferences> ResetAsync() =>
        js.InvokeAsync<ThemePreferences>("caveCodeTheme.reset");
}

public sealed class ThemePreferences
{
    public string Theme { get; set; } = "cave-classic";
    public string Mode { get; set; } = "system";
    public string TextSize { get; set; } = "normal";
    public bool ReducedMotion { get; set; }
}
