namespace CaveCode.CourseEngine;

public enum CourseTrainingStage
{
    Learn = 0,
    Guided = 1,
    Fill = 2,
    Predict = 3,
    Debug = 4,
    Recall = 5,
    Transfer = 6,
    Complete = 7
}

public sealed record CourseStageDefinition(
    CourseTrainingStage Stage,
    string Label,
    string Heading,
    string SupportLabel,
    bool AcceptsCode,
    bool AwardsValidatedLines
);

public static class CourseStageCatalog
{
    public const int StageCount = 8;

    public static IReadOnlyList<CourseStageDefinition> All { get; } =
        new[]
        {
            new CourseStageDefinition(CourseTrainingStage.Learn, "Learn", "Understand the concept", "Full support", false, false),
            new CourseStageDefinition(CourseTrainingStage.Guided, "Guided", "Type with full guidance", "Character guidance", true, true),
            new CourseStageDefinition(CourseTrainingStage.Fill, "Fill", "Complete the missing code", "Partial support", true, true),
            new CourseStageDefinition(CourseTrainingStage.Predict, "Predict", "Predict the result", "Reason first", false, false),
            new CourseStageDefinition(CourseTrainingStage.Debug, "Debug", "Repair the broken code", "Compiler clue", true, true),
            new CourseStageDefinition(CourseTrainingStage.Recall, "Recall", "Rebuild it from memory", "Minimal support", true, true),
            new CourseStageDefinition(CourseTrainingStage.Transfer, "Transfer", "Apply the idea somewhere new", "Independent", true, true),
            new CourseStageDefinition(CourseTrainingStage.Complete, "Complete", "Confirm module mastery", "Mastered", false, false)
        };

    public static CourseStageDefinition Get(int stageIndex)
    {
        if (stageIndex < 0 || stageIndex >= StageCount)
        {
            throw new ArgumentOutOfRangeException(
                nameof(stageIndex),
                stageIndex,
                $"A CaveCode stage must be between 0 and {StageCount - 1}.");
        }

        return All[stageIndex];
    }

    public static string Label(int stageIndex) => Get(stageIndex).Label;
}
