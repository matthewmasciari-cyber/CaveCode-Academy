using System.Text.Json.Serialization;

namespace CaveCode.CourseEngine;

public sealed class CourseProgressSnapshot
{
    [JsonPropertyName("currentModuleIndex")]
    public int CurrentModuleIndex { get; set; }

    [JsonPropertyName("currentStage")]
    public int CurrentStage { get; set; }

    [JsonPropertyName("highestCompletedStage")]
    public int[] HighestCompletedStage { get; set; } = Array.Empty<int>();

    [JsonPropertyName("moduleCompleted")]
    public bool[] ModuleCompleted { get; set; } = Array.Empty<bool>();

    [JsonPropertyName("courseVersion")]
    public int CourseVersion { get; set; } = 1;

    [JsonPropertyName("updatedAt")]
    public string? UpdatedAt { get; set; }

    public int CompletedModuleCount =>
        ModuleCompleted.Count(completed => completed);

    public int ModuleMastery(int moduleIndex)
    {
        if (moduleIndex < 0 || moduleIndex >= ModuleCompleted.Length)
        {
            return 0;
        }

        if (ModuleCompleted[moduleIndex])
        {
            return 100;
        }

        int highest =
            moduleIndex < HighestCompletedStage.Length
                ? HighestCompletedStage[moduleIndex]
                : -1;

        return Math.Clamp(
            (int)Math.Floor(
                (highest + 1) * 100d /
                CourseStageCatalog.StageCount),
            0,
            100);
    }
}

public sealed record CourseProgressMigrationResult(
    CourseProgressSnapshot Snapshot,
    bool Changed,
    IReadOnlyList<string> Notes
);

public static class CourseProgressMigrator
{
    public static CourseProgressMigrationResult Normalize(
        CourseProgressSnapshot? source,
        int totalModules,
        int currentCourseVersion)
    {
        if (totalModules <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalModules));
        }

        CourseProgressSnapshot original = source ?? new();
        var notes = new List<string>();
        bool changed = source is null;

        int moduleIndex = Math.Clamp(
            original.CurrentModuleIndex,
            0,
            totalModules - 1);

        int stageIndex = Math.Clamp(
            original.CurrentStage,
            0,
            CourseStageCatalog.StageCount - 1);

        if (moduleIndex != original.CurrentModuleIndex)
        {
            changed = true;
            notes.Add("Clamped the current module.");
        }

        if (stageIndex != original.CurrentStage)
        {
            changed = true;
            notes.Add("Clamped the current stage.");
        }

        int[] highest = Enumerable
            .Repeat(-1, totalModules)
            .ToArray();

        bool[] completed = new bool[totalModules];

        if (original.HighestCompletedStage is not null)
        {
            for (int index = 0;
                 index < Math.Min(
                     original.HighestCompletedStage.Length,
                     totalModules);
                 index++)
            {
                highest[index] = Math.Clamp(
                    original.HighestCompletedStage[index],
                    -1,
                    CourseStageCatalog.StageCount - 1);
            }

            if (original.HighestCompletedStage.Length != totalModules)
            {
                changed = true;
                notes.Add("Resized stage history while preserving overlap.");
            }
        }
        else
        {
            changed = true;
            notes.Add("Created missing stage history.");
        }

        if (original.ModuleCompleted is not null)
        {
            Array.Copy(
                original.ModuleCompleted,
                completed,
                Math.Min(
                    original.ModuleCompleted.Length,
                    totalModules));

            if (original.ModuleCompleted.Length != totalModules)
            {
                changed = true;
                notes.Add("Resized module history while preserving overlap.");
            }
        }
        else
        {
            changed = true;
            notes.Add("Created missing module history.");
        }

        for (int index = 0; index < totalModules; index++)
        {
            if (completed[index] &&
                highest[index] < CourseStageCatalog.StageCount - 1)
            {
                highest[index] = CourseStageCatalog.StageCount - 1;
                changed = true;
            }
        }

        int version = Math.Max(
            1,
            Math.Max(
                original.CourseVersion,
                currentCourseVersion));

        if (version != original.CourseVersion)
        {
            changed = true;
            notes.Add($"Updated course version to {version}.");
        }

        return new CourseProgressMigrationResult(
            new CourseProgressSnapshot
            {
                CurrentModuleIndex = moduleIndex,
                CurrentStage = stageIndex,
                HighestCompletedStage = highest,
                ModuleCompleted = completed,
                CourseVersion = version,
                UpdatedAt = original.UpdatedAt
            },
            changed,
            notes);
    }
}
