using Microsoft.JSInterop;

namespace CaveCode.Services;

public sealed class ProgressionService(IJSRuntime js)
{
    public event Action? StateChanged;

    public ValueTask<ProgressionState> GetStateAsync() =>
        js.InvokeAsync<ProgressionState>(
            "caveCodeProgression.getState"
        );

    public ValueTask<CourseResumeState> GetCourseResumeAsync(
        string course
    ) =>
        js.InvokeAsync<CourseResumeState>(
            "caveCodeProgression.getCourseResume",
            course
        );

    public async ValueTask<ProgressionState> AwardStageAsync(
        string course,
        int moduleIndex,
        int stageIndex,
        string code
    )
    {
        ProgressionState state =
            await js.InvokeAsync<ProgressionState>(
                "caveCodeProgression.awardStage",
                course,
                moduleIndex,
                stageIndex,
                code
            );

        NotifyChanged();
        return state;
    }

    public async ValueTask<ProgressionAwardResult> AwardModuleAsync(
        string course,
        int moduleIndex
    )
    {
        ProgressionAwardResult result =
            await js.InvokeAsync<ProgressionAwardResult>(
                "caveCodeProgression.awardModule",
                course,
                moduleIndex
            );

        NotifyChanged();
        return result;
    }

    public async ValueTask<ProgressionState>
        AwardMinigameRunAsync(
            string course,
            string rewardKey,
            int xp,
            int validatedLines
        )
    {
        ProgressionState state =
            await js.InvokeAsync<ProgressionState>(
                "caveCodeProgression.awardMinigameRun",
                course,
                rewardKey,
                xp,
                validatedLines
            );

        NotifyChanged();
        return state;
    }

    public void NotifyExternalChange() =>
        NotifyChanged();

    public async ValueTask<ProgressionState> SetPublicLeaderboardAsync(
        bool isPublic
    )
    {
        ProgressionState state =
            await js.InvokeAsync<ProgressionState>(
                "caveCodeProgression.setPublicLeaderboard",
                isPublic
            );

        NotifyChanged();
        return state;
    }

    public ValueTask<LeaderboardResult> GetLeaderboardAsync(
        string filter,
        ProfilePreferences profile
    ) =>
        js.InvokeAsync<LeaderboardResult>(
            "caveCodeProgression.getLeaderboard",
            filter,
            profile
        );

    private void NotifyChanged() =>
        StateChanged?.Invoke();
}

public sealed class ProgressionState
{
    public int TotalXp { get; set; }
    public int CSharpXp { get; set; }
    public int PythonXp { get; set; }
    public int TotalLines { get; set; }
    public int CSharpLines { get; set; }
    public int PythonLines { get; set; }
    public int Level { get; set; } = 1;
    public int XpIntoLevel { get; set; }
    public int XpForNextLevel { get; set; } = 500;
    public int LevelProgress { get; set; }
    public bool PublicLeaderboard { get; set; }
}

public sealed class CourseResumeState
{
    public bool HasProgress { get; set; }
    public bool CourseComplete { get; set; }
    public int CurrentModuleIndex { get; set; }
    public int CurrentStage { get; set; }
    public int CompletedModules { get; set; }
    public int TotalModules { get; set; } = 40;
    public int ModuleMastery { get; set; }
}

public sealed class ProgressionAwardResult
{
    public bool NewlyAwarded { get; set; }
    public int XpAwarded { get; set; }
    public ProgressionState State { get; set; } = new();
}

public sealed class LeaderboardResult
{
    public bool CloudAvailable { get; set; }
    public bool SignedIn { get; set; }
    public string Message { get; set; } = "";
    public LeaderboardEntry[] Entries { get; set; } =
        Array.Empty<LeaderboardEntry>();
}

public sealed class LeaderboardEntry
{
    public string Id { get; set; } = "";
    public string DisplayName { get; set; } = "CaveCode Learner";
    public string Emblem { get; set; } = "crystal";
    public string Title { get; set; } = "Cave Explorer";
    public int TotalXp { get; set; }
    public int CSharpXp { get; set; }
    public int PythonXp { get; set; }
    public int TotalLines { get; set; }
    public int CSharpLines { get; set; }
    public int PythonLines { get; set; }
    public int Level { get; set; } = 1;
    public bool IsCurrentUser { get; set; }
}
