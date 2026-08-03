namespace CaveCode.CourseEngine;

public sealed record CourseManifest
{
    public required string Id { get; init; }
    public required string DisplayName { get; init; }
    public required string ShortMark { get; init; }
    public required string LanguageName { get; init; }
    public required string Route { get; init; }
    public required string Description { get; init; }
    public required string ProjectName { get; init; }
    public required string EditorFileName { get; init; }
    public required string EditorLanguageLabel { get; init; }
    public required string PreviewKind { get; init; }

    public int ModuleCount { get; init; } = 40;
    public int ChapterCount { get; init; } = 5;
    public int ModulesPerChapter { get; init; } = 8;
    public int CourseVersion { get; init; } = 1;
    public bool IsAvailable { get; init; }
    public bool HasMinigame { get; init; }

    public string ProgressStorageKey =>
        $"cavecode.{CourseIds.Normalize(Id)}.progress.v1";
}
