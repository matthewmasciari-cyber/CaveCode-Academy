using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace CaveCode.Services;

public sealed class ProfileService(IJSRuntime js)
{
    public ValueTask<ProfilePreferences> GetPreferencesAsync() =>
        js.InvokeAsync<ProfilePreferences>(
            "caveCodeProfile.getPreferences"
        );

    // Retained for compatibility with older components. Direct display-name
    // changes are blocked in JavaScript so the rename economy cannot be
    // bypassed through the old preference setter.
    public ValueTask<ProfilePreferences> SetDisplayNameAsync(
        string displayName
    ) =>
        js.InvokeAsync<ProfilePreferences>(
            "caveCodeProfile.setPreference",
            "displayName",
            displayName
        );

    public ValueTask<ProfileRenameStatus> GetRenameStatusAsync() =>
        js.InvokeAsync<ProfileRenameStatus>(
            "caveCodeProfile.getRenameStatus"
        );

    public ValueTask<ProfileRenameResult> RenameDisplayNameAsync(
        string displayName
    ) =>
        js.InvokeAsync<ProfileRenameResult>(
            "caveCodeProfile.renameDisplayName",
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
        js.InvokeAsync<ProfilePreferences>(
            "caveCodeProfile.reset"
        );
}

public sealed class ProfilePreferences
{
    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = "";

    [JsonPropertyName("title")]
    public string Title { get; set; } = "Cave Explorer";

    [JsonPropertyName("emblem")]
    public string Emblem { get; set; } = "crystal";

    [JsonPropertyName("featuredAchievement")]
    public string FeaturedAchievement { get; set; } =
        "Control Terminal Online";

    [JsonPropertyName("previousDisplayName")]
    public string PreviousDisplayName { get; set; } = "";

    [JsonPropertyName("displayNameChangeCount")]
    public int DisplayNameChangeCount { get; set; }

    [JsonPropertyName("lastDisplayNameChangedAt")]
    public string? LastDisplayNameChangedAt { get; set; }

    [JsonPropertyName("nextDisplayNameChangeAt")]
    public string? NextDisplayNameChangeAt { get; set; }
}

public sealed class ProfileRenameStatus
{
    [JsonPropertyName("currentDisplayName")]
    public string CurrentDisplayName { get; set; } = "";

    [JsonPropertyName("previousDisplayName")]
    public string PreviousDisplayName { get; set; } = "";

    [JsonPropertyName("isFirstChangeFree")]
    public bool IsFirstChangeFree { get; set; } = true;

    [JsonPropertyName("canChangeNow")]
    public bool CanChangeNow { get; set; } = true;

    [JsonPropertyName("currentCost")]
    public int CurrentCost { get; set; }

    [JsonPropertyName("futureCost")]
    public int FutureCost { get; set; } = 500;

    [JsonPropertyName("cooldownDays")]
    public int CooldownDays { get; set; } = 5;

    [JsonPropertyName("crystalBalance")]
    public int CrystalBalance { get; set; }

    [JsonPropertyName("canAfford")]
    public bool CanAfford { get; set; } = true;

    [JsonPropertyName("nextAvailableAt")]
    public string? NextAvailableAt { get; set; }

    [JsonPropertyName("remainingSeconds")]
    public long RemainingSeconds { get; set; }

    [JsonPropertyName("changeCount")]
    public int ChangeCount { get; set; }
}

public sealed class ProfileRenameResult
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = "";

    [JsonPropertyName("costCharged")]
    public int CostCharged { get; set; }

    [JsonPropertyName("usedFreeChange")]
    public bool UsedFreeChange { get; set; }

    [JsonPropertyName("preferences")]
    public ProfilePreferences Preferences { get; set; } = new();

    [JsonPropertyName("status")]
    public ProfileRenameStatus Status { get; set; } = new();
}
