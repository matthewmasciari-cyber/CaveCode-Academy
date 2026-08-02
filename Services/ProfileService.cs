using Microsoft.JSInterop;

namespace CaveCode.Services;

public sealed class ProfileService(IJSRuntime js)
{
    public ValueTask<ProfilePreferences> GetPreferencesAsync() =>
        js.InvokeAsync<ProfilePreferences>("caveCodeProfile.getPreferences");

    public ValueTask<ProfilePreferences> SetDisplayNameAsync(string displayName) =>
        js.InvokeAsync<ProfilePreferences>(
            "caveCodeProfile.setPreference",
            "displayName",
            displayName
        );

    public ValueTask<ProfilePreferences> SetTitleAsync(string title) =>
        js.InvokeAsync<ProfilePreferences>(
            "caveCodeProfile.setPreference",
            "title",
            title
        );

    public ValueTask<ProfilePreferences> SetEmblemAsync(string emblem) =>
        js.InvokeAsync<ProfilePreferences>(
            "caveCodeProfile.setPreference",
            "emblem",
            emblem
        );

    public ValueTask<ProfilePreferences> SetFeaturedAchievementAsync(
        string achievement
    ) =>
        js.InvokeAsync<ProfilePreferences>(
            "caveCodeProfile.setPreference",
            "featuredAchievement",
            achievement
        );

    public ValueTask<ProfilePreferences> ResetAsync() =>
        js.InvokeAsync<ProfilePreferences>("caveCodeProfile.reset");
}

public sealed class ProfilePreferences
{
    public string DisplayName { get; set; } = "";
    public string Title { get; set; } = "Cave Explorer";
    public string Emblem { get; set; } = "crystal";
    public string FeaturedAchievement { get; set; } =
        "Control Terminal Online";
}
