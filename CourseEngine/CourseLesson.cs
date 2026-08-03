namespace CaveCode.CourseEngine;

public sealed record CourseLesson(
    string Chapter,
    string Topic,
    string Title,
    string Teaching,
    string ExampleCode,
    string TargetCode,
    string FillStarter,
    string PredictionQuestion,
    string[] PredictionOptions,
    int PredictionCorrect,
    string PredictionExplanation,
    string BrokenCode,
    string DebugPrompt,
    string RecallPrompt,
    string TransferPrompt,
    string TransferCode,
    string PreviewMessage
)
{
    public string[] ConceptPoints { get; init; } = Array.Empty<string>();
    public string? EditorFileNameOverride { get; init; }
    public string? PreviewInstruction { get; init; }
}

public sealed record CourseDefinition(
    CourseManifest Manifest,
    IReadOnlyList<CourseLesson> Lessons
)
{
    public CourseLesson LessonAt(int moduleIndex)
    {
        if (moduleIndex < 0 || moduleIndex >= Lessons.Count)
        {
            throw new ArgumentOutOfRangeException(
                nameof(moduleIndex),
                moduleIndex,
                $"Module index is outside the {Manifest.DisplayName} lesson list.");
        }

        return Lessons[moduleIndex];
    }
}
