using Microsoft.JSInterop;

namespace CaveCode.Services;

public sealed class MinigameService(
    IJSRuntime js,
    ProgressionService progression,
    AchievementService achievements)
{
    public ValueTask<MinigameHubState> GetHubStateAsync() =>
        js.InvokeAsync<MinigameHubState>("caveCodeMinigames.getHubState");

    public ValueTask<MinigameCourseState> GetCourseStateAsync(string course) =>
        js.InvokeAsync<MinigameCourseState>("caveCodeMinigames.getCourseState", course);

    public ValueTask<MinigameCourseState> StartRunAsync(
        string course, string difficulty, bool endless) =>
        js.InvokeAsync<MinigameCourseState>(
            "caveCodeMinigames.startRun", course, difficulty, endless);

    public ValueTask<MinigameCourseState> StartPracticeAsync(
        string course,
        string difficulty,
        string mode,
        int chapter) =>
        js.InvokeAsync<MinigameCourseState>(
            "caveCodeMinigames.startPractice",
            course,
            difficulty,
            mode,
            chapter);

    public ValueTask<MinigameValidationResult> ValidateAsync(
        string course, string code) =>
        js.InvokeAsync<MinigameValidationResult>(
            "caveCodeMinigames.validate", course, code);

    public async ValueTask<MinigameCompletionResult> CompleteAsync(
        string course, string code)
    {
        MinigameCompletionResult result =
            await js.InvokeAsync<MinigameCompletionResult>(
                "caveCodeMinigames.complete", course, code);

        await progression.AwardMinigameRunAsync(
            course,
            result.RewardKey,
            result.XpAwarded,
            result.ValidatedLines);

        if (result.CrystalsAwarded > 0)
        {
            await achievements.AwardMinigameCrystalsAsync(
                result.RewardKey,
                result.CrystalsAwarded);
        }

        progression.NotifyExternalChange();
        return result;
    }

    public ValueTask<MinigameAnalysisResult> AnalyzeAsync(
        string course,
        string code) =>
        js.InvokeAsync<MinigameAnalysisResult>(
            "caveCodeMinigames.analyze", course, code);

    public ValueTask<MinigameHintResult> UseHintAsync(
        string course,
        string code) =>
        js.InvokeAsync<MinigameHintResult>(
            "caveCodeMinigames.useHint",
            course,
            code);

    public ValueTask<MinigameCourseState> ResetRunAsync(string course) =>
        js.InvokeAsync<MinigameCourseState>(
            "caveCodeMinigames.resetRun", course);







    public ValueTask<MinigameCourseState> EndRunAsync(string course) =>
        js.InvokeAsync<MinigameCourseState>(
            "caveCodeMinigames.endRun", course);
}

public sealed class MinigameHubState
{
    public MinigameCourseState CSharp { get; set; } = new();
    public MinigameCourseState Python { get; set; } = new();
}

public sealed class MinigameCourseState
{
    public string Course { get; set; } = "";
    public int CompletedChapters { get; set; }
    public int UnlockedChapters { get; set; } = 1;
    public int BestScore { get; set; }
    public int LastScore { get; set; }
    public int TotalRuns { get; set; }
    public int CompletedRuns { get; set; }
    public int FailedRuns { get; set; }
    public int TotalXpEarned { get; set; }
    public int TotalCrystalsEarned { get; set; }
    public int TotalValidatedLines { get; set; }
    public bool ActiveRun { get; set; }
    public bool RunComplete { get; set; }
    public bool RunFailed { get; set; }
    public bool EndlessMode { get; set; }
    public bool EndlessUnlocked { get; set; }
    public bool PracticeUnlocked { get; set; }
    public string PracticeMode { get; set; } = "";
    public int SelectedChapter { get; set; }
    public string PracticeLabel { get; set; } = "";
    public string RunSeed { get; set; } = "";
    public int UniqueStyles { get; set; }
    public string Difficulty { get; set; } = "standard";
    public int RoomNumber { get; set; } = 1;
    public int RoomsTotal { get; set; } = 5;
    public int Score { get; set; }
    public int Streak { get; set; }
    public int Mistakes { get; set; }
    public int PrimaryResource { get; set; } = 100;
    public int SecondaryResource { get; set; }
    public int Threat { get; set; }
    public bool HintUsed { get; set; }
    public int HintPercent { get; set; }
    public string HintReveal { get; set; } = "";
    public int HintPenalty { get; set; }
    public int RunHintsUsed { get; set; }
    public int AbandonedRuns { get; set; }
    public GeneratedScenario? Scenario { get; set; }
}

public sealed class GeneratedScenario
{
    public string Id { get; set; } = "";
    public string TemplateId { get; set; } = "";
    public int Chapter { get; set; } = 1;
    public string TaskType { get; set; } = "";
    public string Title { get; set; } = "";
    public string Skill { get; set; } = "";
    public string Concept { get; set; } = "";
    public string Brief { get; set; } = "";
    public string Objective { get; set; } = "";
    public string Hint { get; set; } = "";
    public string StarterCode { get; set; } = "";
    public string SystemName { get; set; } = "";
    public string VisualIcon { get; set; } = "";
    public string SuccessStatus { get; set; } = "";
    public string PrimaryTerm { get; set; } = "";
    public string Entity { get; set; } = "";
}

public sealed class MinigameValidationResult
{
    public bool Valid { get; set; }
    public string Heading { get; set; } = "";
    public string Message { get; set; } = "";
    public string SystemStatus { get; set; } = "";
    public MinigameCourseState State { get; set; } = new();
}

public sealed class MinigameAnalysisResult
{
    // Existing compatibility fields.
    public int CurrentCharacters { get; set; }
    public int TargetCharacters { get; set; }
    public int StructuralAccuracy { get; set; }
    public int MatchedElements { get; set; }
    public int RequiredElements { get; set; }

    // Clearer learner-facing measurements.
    public int SolutionCharacters { get; set; }
    public int EditorCharacters { get; set; }
    public int TargetMinimum { get; set; }
    public int TargetMaximum { get; set; }
    public int CompletionPercent { get; set; }
}

public sealed class MinigameHintResult
{
    public bool Allowed { get; set; }
    public string Message { get; set; } = "";
    public string Reveal { get; set; } = "";
    public int RevealPercent { get; set; }
    public int ScoreCost { get; set; }
    public int XpCost { get; set; }
    public MinigameCourseState State { get; set; } = new();
}

public sealed class MinigameCompletionResult
{
    public string RewardKey { get; set; } = "";
    public string ScenarioTitle { get; set; } = "";
    public bool NewVariation { get; set; }
    public bool FirstTemplateClear { get; set; }
    public bool RunCompleted { get; set; }
    public bool PerfectRun { get; set; }
    public int XpAwarded { get; set; }
    public int CrystalsAwarded { get; set; }
    public int ValidatedLines { get; set; }
    public int EventScore { get; set; }
    public MinigameCourseState State { get; set; } = new();
}
