(() => {
    "use strict";

    const ENGINE_VERSION = "course-engine-foundation-v1";
    const STAGE_COUNT = 8;

    const catalog = Object.freeze([
        Object.freeze({
            id: "csharp",
            displayName: "C# Cave Adventure",
            shortMark: "C#",
            languageName: "C#",
            route: "/csharp",
            projectName: "Cave Explorer",
            editorFileName: "PlayerTraining.cs",
            editorLanguageLabel: "C#",
            previewKind: "cave-game",
            moduleCount: 40,
            chapterCount: 5,
            modulesPerChapter: 8,
            courseVersion: 1,
            isAvailable: true,
            hasMinigame: true
        }),
        Object.freeze({
            id: "python",
            displayName: "Python Automation Quest",
            shortMark: "Py",
            languageName: "Python",
            route: "/python",
            projectName: "Crystal Cavern Control Room",
            editorFileName: "control_training.py",
            editorLanguageLabel: "PYTHON",
            previewKind: "automation-facility",
            moduleCount: 40,
            chapterCount: 5,
            modulesPerChapter: 8,
            courseVersion: 1,
            isAvailable: true,
            hasMinigame: true
        }),
        Object.freeze({
            id: "cpp",
            displayName: "C++ Engine Foundry",
            shortMark: "C++",
            languageName: "C++",
            route: "/cpp",
            projectName: "Engine Foundry",
            editorFileName: "EngineTraining.cpp",
            editorLanguageLabel: "C++",
            previewKind: "engine-workshop",
            moduleCount: 40,
            chapterCount: 5,
            modulesPerChapter: 8,
            courseVersion: 1,
            isAvailable: true,
            hasMinigame: true
        }),
        Object.freeze({
            id: "htmlcss",
            displayName: "HTML & CSS Workshop",
            shortMark: "HTML",
            languageName: "HTML & CSS",
            route: "/html-css",
            projectName: "Interface Workshop",
            editorFileName: "index.html",
            editorLanguageLabel: "HTML / CSS",
            previewKind: "live-web-preview",
            moduleCount: 40,
            chapterCount: 5,
            modulesPerChapter: 8,
            courseVersion: 1,
            isAvailable: true,
            hasMinigame: true
        }),
        Object.freeze({
            id: "gcl",
            displayName: "GCL+ Control Line Lab",
            shortMark: "GCL+",
            languageName: "GCL+",
            route: "/gcl",
            projectName: "Control Line Lab",
            editorFileName: "Sequence.gcl",
            editorLanguageLabel: "GCL+",
            previewKind: "control-line-lab",
            moduleCount: 40,
            chapterCount: 5,
            modulesPerChapter: 8,
            courseVersion: 1,
            isAvailable: true,
            hasMinigame: true
        })
    ]);

    const aliases = Object.freeze({
        "c#": "csharp",
        "cs": "csharp",
        "csharp": "csharp",
        "py": "python",
        "python": "python",
        "c++": "cpp",
        "cplusplus": "cpp",
        "cpp": "cpp",
        "html": "htmlcss",
        "css": "htmlcss",
        "html-css": "htmlcss",
        "htmlcss": "htmlcss",
        "gcl": "gcl",
        "gcl+": "gcl",
        "cgl": "gcl",
        "cgline": "gcl",
        "cgline+": "gcl"
    });

    function normalizeCourseId(value) {
        const source = String(value || "")
            .trim()
            .toLowerCase();

        return aliases[source] || source;
    }

    function getCourse(courseId) {
        const normalized = normalizeCourseId(courseId);
        const course = catalog.find(item => item.id === normalized);

        if (!course) {
            throw new Error(
                `Unknown CaveCode course: ${String(courseId)}`
            );
        }

        return { ...course };
    }

    function getStorageKey(courseId) {
        const course = getCourse(courseId);
        return `cavecode.${course.id}.progress.v1`;
    }

    function safeInteger(value, fallback = 0) {
        const number = Number(value);

        return Number.isFinite(number)
            ? Math.trunc(number)
            : fallback;
    }

    function normalizeHighestStages(source, totalModules) {
        const result = Array(totalModules).fill(-1);

        if (Array.isArray(source)) {
            for (
                let index = 0;
                index < Math.min(source.length, totalModules);
                index += 1
            ) {
                result[index] = Math.max(
                    -1,
                    Math.min(
                        STAGE_COUNT - 1,
                        safeInteger(source[index], -1)
                    )
                );
            }
        }

        return result;
    }

    function normalizeCompletedModules(source, totalModules) {
        const result = Array(totalModules).fill(false);

        if (Array.isArray(source)) {
            for (
                let index = 0;
                index < Math.min(source.length, totalModules);
                index += 1
            ) {
                result[index] = source[index] === true;
            }
        }

        return result;
    }

    function normalizeProgress(
        source,
        totalModules,
        currentCourseVersion
    ) {
        const moduleCount = Math.max(
            1,
            safeInteger(totalModules, 40)
        );

        const raw =
            source && typeof source === "object"
                ? source
                : {};

        const completed = normalizeCompletedModules(
            raw.moduleCompleted ?? raw.ModuleCompleted,
            moduleCount
        );

        const highest = normalizeHighestStages(
            raw.highestCompletedStage ??
                raw.HighestCompletedStage,
            moduleCount
        );

        for (let index = 0; index < moduleCount; index += 1) {
            if (completed[index]) {
                highest[index] = STAGE_COUNT - 1;
            }
        }

        return {
            ...raw,
            currentModuleIndex: Math.max(
                0,
                Math.min(
                    moduleCount - 1,
                    safeInteger(
                        raw.currentModuleIndex ??
                            raw.CurrentModuleIndex,
                        0
                    )
                )
            ),
            currentStage: Math.max(
                0,
                Math.min(
                    STAGE_COUNT - 1,
                    safeInteger(
                        raw.currentStage ??
                            raw.CurrentStage,
                        0
                    )
                )
            ),
            highestCompletedStage: highest,
            moduleCompleted: completed,
            courseVersion: Math.max(
                1,
                safeInteger(
                    raw.courseVersion ??
                        raw.CourseVersion,
                    1
                ),
                safeInteger(currentCourseVersion, 1)
            ),
            updatedAt:
                raw.updatedAt ??
                raw.UpdatedAt ??
                null
        };
    }

    function readRaw(courseId) {
        try {
            return JSON.parse(
                localStorage.getItem(
                    getStorageKey(courseId)
                ) || "null"
            );
        } catch {
            return null;
        }
    }

    function getProgress(
        courseId,
        totalModules,
        currentCourseVersion
    ) {
        return normalizeProgress(
            readRaw(courseId),
            totalModules,
            currentCourseVersion
        );
    }

    function saveProgress(
        courseId,
        snapshot,
        totalModules,
        currentCourseVersion
    ) {
        const course = getCourse(courseId);
        const previous = readRaw(course.id);

        const merged = {
            ...(previous && typeof previous === "object"
                ? previous
                : {}),
            ...(snapshot && typeof snapshot === "object"
                ? snapshot
                : {})
        };

        const normalized = normalizeProgress(
            merged,
            totalModules || course.moduleCount,
            currentCourseVersion || course.courseVersion
        );

        normalized.updatedAt = new Date().toISOString();

        localStorage.setItem(
            getStorageKey(course.id),
            JSON.stringify(normalized)
        );

        window.dispatchEvent(
            new CustomEvent(
                "cavecode-course-progress-changed",
                {
                    detail: {
                        courseId: course.id,
                        progress: normalized
                    }
                }
            )
        );

        return normalized;
    }

    function inspectProgress(courseId) {
        const course = getCourse(courseId);
        const raw = readRaw(course.id);
        const normalized = normalizeProgress(
            raw,
            course.moduleCount,
            course.courseVersion
        );

        return {
            course,
            storageKey: getStorageKey(course.id),
            hasSavedData: Boolean(raw),
            raw,
            normalized,
            completedModules:
                normalized.moduleCompleted.filter(Boolean).length
        };
    }

    window.caveCodeCourseEngine = Object.freeze({
        version: ENGINE_VERSION,
        stageCount: STAGE_COUNT,
        getCatalog: () =>
            catalog.map(item => ({ ...item })),
        getCourse,
        normalizeCourseId,
        getStorageKey,
        normalizeProgress,
        getProgress,
        saveProgress,
        inspectProgress
    });
})();
