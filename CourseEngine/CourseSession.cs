namespace CaveCode.CourseEngine;

/// <summary>
/// Shared mutable course-session state used by language course pages.
/// This class intentionally does not own validation, rewards, persistence,
/// lesson content, or UI behavior.
/// </summary>
public sealed class CourseSession
{
    public int CurrentModuleIndex { get; set; }

    public int CurrentStageIndex { get; set; }

    public int[] HighestCompletedStage { get; set; } = Array.Empty<int>();

    public bool[] ModuleCompleted { get; set; } = Array.Empty<bool>();

    public int CompletedModuleCount =>
        ModuleCompleted.Count(completed => completed);

    public void Initialize(int moduleCount)
    {
        if (moduleCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(moduleCount));
        }

        CurrentModuleIndex = 0;
        CurrentStageIndex = 0;
        HighestCompletedStage = Enumerable
            .Repeat(-1, moduleCount)
            .ToArray();
        ModuleCompleted = new bool[moduleCount];
    }

    public int ProgressPercent(int totalModules) =>
        totalModules <= 0
            ? 0
            : CompletedModuleCount * 100 / totalModules;

    public int ModuleMastery(int moduleIndex, int stageCount)
    {
        ValidateModuleIndex(moduleIndex);

        if (stageCount <= 0)
        {
            return 0;
        }

        if (ModuleCompleted[moduleIndex])
        {
            return 100;
        }

        int completedStages = HighestCompletedStage[moduleIndex] + 1;

        return Math.Clamp(
            completedStages * 100 / stageCount,
            0,
            100);
    }

    public bool IsLessonUnlocked(int moduleIndex)
    {
        ValidateModuleIndex(moduleIndex);

        return moduleIndex == 0 ||
               ModuleCompleted[moduleIndex - 1];
    }

    public bool IsStageUnlocked(int stageIndex)
    {
        ValidateCurrentModule();

        return ModuleCompleted[CurrentModuleIndex] ||
               stageIndex <= HighestCompletedStage[CurrentModuleIndex] + 1;
    }

    public bool IsStageComplete(int stageIndex)
    {
        ValidateCurrentModule();

        return ModuleCompleted[CurrentModuleIndex] ||
               stageIndex <= HighestCompletedStage[CurrentModuleIndex];
    }


    // CAVECODE_COURSE_SESSION_TRANSITIONS_87B
public bool TrySelectLesson(int moduleIndex, int stageCount)
    {
        if (!IsLessonUnlocked(moduleIndex))
        {
            return false;
        }

        CurrentModuleIndex = moduleIndex;
        CurrentStageIndex = ModuleCompleted[moduleIndex]
            ? Math.Max(0, stageCount - 1)
            : Math.Clamp(
                HighestCompletedStage[moduleIndex] + 1,
                0,
                Math.Max(0, stageCount - 1));

        return true;
    }

    public bool TrySelectStage(int stageIndex)
    {
        if (!IsStageUnlocked(stageIndex))
        {
            return false;
        }

        CurrentStageIndex = stageIndex;
        return true;
    }

    public void MarkCurrentStageComplete()
    {
        ValidateCurrentModule();

        HighestCompletedStage[CurrentModuleIndex] = Math.Max(
            HighestCompletedStage[CurrentModuleIndex],
            CurrentStageIndex);
    }

    public void CompleteModule(int moduleIndex, int stageCount)
    {
        ValidateModuleIndex(moduleIndex);

        HighestCompletedStage[moduleIndex] =
            Math.Max(0, stageCount - 1);
        ModuleCompleted[moduleIndex] = true;
    }

    public bool MoveToNextModule(int totalModules)
    {
        if (CurrentModuleIndex >= totalModules - 1)
        {
            return false;
        }

        CurrentModuleIndex++;
        CurrentStageIndex = 0;
        return true;
    }

    private void ValidateCurrentModule() =>
        ValidateModuleIndex(CurrentModuleIndex);

    private void ValidateModuleIndex(int moduleIndex)
    {
        if (moduleIndex < 0 ||
            moduleIndex >= ModuleCompleted.Length ||
            moduleIndex >= HighestCompletedStage.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(moduleIndex));
        }
    }
}
