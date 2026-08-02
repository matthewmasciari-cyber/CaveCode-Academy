using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace CaveCode.Services;

public sealed class AchievementService(IJSRuntime js)
{
    public ValueTask<AchievementState> GetStateAsync() =>
        js.InvokeAsync<AchievementState>(
            "caveCodeAchievements.getState"
        );

    public ValueTask<AchievementState> ClaimAsync(string achievementId) =>
        js.InvokeAsync<AchievementState>(
            "caveCodeAchievements.claim",
            achievementId
        );

    public ValueTask<AchievementUnlockResult?> UnlockChapterAsync(
        string course,
        int chapter
    ) =>
        js.InvokeAsync<AchievementUnlockResult?>(
            "caveCodeAchievements.unlockChapter",
            course,
            chapter
        );

    public ValueTask<TitleUnlockOption[]> GetTitleOptionsAsync() =>
        js.InvokeAsync<TitleUnlockOption[]>(
            "caveCodeAchievements.getTitleOptions"
        );

    public ValueTask<AchievementFeatureOption[]>
        GetFeatureOptionsAsync() =>
        js.InvokeAsync<AchievementFeatureOption[]>(
            "caveCodeAchievements.getFeatureOptions"
        );
}

public sealed class AchievementState
{
    [JsonPropertyName("crystals")]
    public int Crystals { get; set; }

    [JsonPropertyName("unclaimedCount")]
    public int UnclaimedCount { get; set; }

    [JsonPropertyName("earnedCount")]
    public int EarnedCount { get; set; }

    [JsonPropertyName("claimedCount")]
    public int ClaimedCount { get; set; }

    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }

    [JsonPropertyName("achievements")]
    public AchievementRecord[] Achievements { get; set; } =
        Array.Empty<AchievementRecord>();
}

public sealed class AchievementRecord
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("course")]
    public string Course { get; set; } = "";

    [JsonPropertyName("courseName")]
    public string CourseName { get; set; } = "";

    [JsonPropertyName("chapter")]
    public int Chapter { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("description")]
    public string Description { get; set; } = "";

    [JsonPropertyName("titleReward")]
    public string TitleReward { get; set; } = "";

    [JsonPropertyName("crystals")]
    public int Crystals { get; set; }

    [JsonPropertyName("unlocked")]
    public bool Unlocked { get; set; }

    [JsonPropertyName("claimed")]
    public bool Claimed { get; set; }
}

public sealed class AchievementUnlockResult
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("course")]
    public string Course { get; set; } = "";

    [JsonPropertyName("chapter")]
    public int Chapter { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("description")]
    public string Description { get; set; } = "";

    [JsonPropertyName("titleReward")]
    public string TitleReward { get; set; } = "";

    [JsonPropertyName("crystals")]
    public int Crystals { get; set; }

    [JsonPropertyName("newlyUnlocked")]
    public bool NewlyUnlocked { get; set; }
}

public sealed class TitleUnlockOption
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = "";

    [JsonPropertyName("unlocked")]
    public bool Unlocked { get; set; }

    [JsonPropertyName("requirement")]
    public string Requirement { get; set; } = "";
}

public sealed class AchievementFeatureOption
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("unlocked")]
    public bool Unlocked { get; set; }

    [JsonPropertyName("requirement")]
    public string Requirement { get; set; } = "";
}
