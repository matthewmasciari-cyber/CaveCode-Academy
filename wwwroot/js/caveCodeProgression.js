(function () {
    const storageKey = "cavecode.progression.v1";
    const defaults = {
        totalXp: 0,
        cSharpXp: 0,
        pythonXp: 0,
        cppXp: 0,
        htmlCssXp: 0,
        totalLines: 0,
        cSharpLines: 0,
        pythonLines: 0,
        cppLines: 0,
        htmlCssLines: 0,
        publicLeaderboard: false,
        awardedModules: {},
        awardedStages: {},
        awardedChapters: {}
    };

    let current = {
        ...defaults,
        awardedModules: {},
        awardedStages: {},
        awardedChapters: {}
    };

    let progressionReady = false;
    let progressionReadyPromise = null;

    function profileFromCloud(profile) {
        return {
            ...defaults,
            totalXp: Number(profile.total_xp || 0),
            cSharpXp: Number(profile.csharp_xp || 0),
            pythonXp: Number(profile.python_xp || 0),
            cppXp: Number(profile.cpp_xp || 0),
            htmlCssXp: Number(profile.html_css_xp || 0),
            totalLines: Number(profile.total_lines || 0),
            cSharpLines: Number(profile.csharp_lines || 0),
            pythonLines: Number(profile.python_lines || 0),
            cppLines: Number(profile.cpp_lines || 0),
            htmlCssLines: Number(profile.html_css_lines || 0),
            publicLeaderboard: Boolean(
                profile.public_leaderboard ?? profile.publicLeaderboard
            ),
            awardedModules: profile.awarded_modules || {},
            awardedStages: profile.awarded_stages || {},
            awardedChapters: profile.awarded_chapters || {},
            awardedMinigameRuns: profile.awarded_minigame_runs || {}
        };
    }

    function cloudPayloadFromCurrent() {
        return {
            total_xp: current.totalXp,
            csharp_xp: current.cSharpXp,
            python_xp: current.pythonXp,
            cpp_xp: current.cppXp,
            html_css_xp: current.htmlCssXp,
            total_lines: current.totalLines,
            csharp_lines: current.cSharpLines,
            python_lines: current.pythonLines,
            cpp_lines: current.cppLines,
            html_css_lines: current.htmlCssLines,
            public_leaderboard: current.publicLeaderboard,
            awarded_modules: current.awardedModules || {},
            awarded_stages: current.awardedStages || {},
            awarded_chapters: current.awardedChapters || {},
            awarded_minigame_runs: current.awardedMinigameRuns || {}
        };
    }

    async function hydrateCourseProgressFromCloud() {
        if (!window.caveCodeAuth?.loadCourseProgressFor) {
            return;
        }

        const courses = ["csharp", "python", "cpp", "htmlcss"];

        for (const course of courses) {
            try {
                await window.caveCodeAuth.loadCourseProgressFor(course);
            } catch {
                // Keep going; one course failure must not block others.
            }
        }
    }

    async function initializeProgression() {
        try {
            await window.caveCodeAuth?.waitForReady?.(8000);

            const profile =
                await window.caveCodeAuth?.loadUserProfile?.();

            if (profile) {
                current = profileFromCloud(profile);

                // Mirror cloud into localStorage so UI/reconcile see it.
                localStorage.setItem(
                    storageKey,
                    JSON.stringify(current)
                );

                await hydrateCourseProgressFromCloud();

                progressionReady = true;
                window.dispatchEvent(
                    new CustomEvent("cavecode-progression-changed", {
                        detail: stateView()
                    })
                );
                return;
            }
        } catch (error) {
            console.error("Progression cloud load failed:", error);
        }

        current = load();

        try {
            await window.caveCodeAuth?.syncLocalProgressToCloud?.(
                "csharp"
            );
        } catch {
            // Keep local progress
        }

        // If signed in with local XP but no profile row yet, push it up.
        try {
            const signedIn =
                await window.caveCodeAuth?.isSignedIn?.();

            if (signedIn && (current.totalXp > 0 || current.totalLines > 0)) {
                await window.caveCodeAuth?.saveUserProfile?.(
                    cloudPayloadFromCurrent()
                );
            }
        } catch {
            // Offline / RLS — local still works
        }

        progressionReady = true;
        window.dispatchEvent(
            new CustomEvent("cavecode-progression-changed", {
                detail: stateView()
            })
        );
    }

    function load() {
        try {
            const saved = JSON.parse(
                localStorage.getItem(storageKey) || "{}"
            );

            return {
                ...defaults,
                ...saved,
                awardedModules: saved.awardedModules || {},
                awardedStages: saved.awardedStages || {},
                awardedChapters: saved.awardedChapters || {}
            };
        } catch {
            return {
                ...defaults,
                awardedModules: {},
                awardedStages: {},
                awardedChapters: {}
            };
        }
    }

    function normalizeCourse(course) {
        course = String(course || "").toLowerCase();

        if (course === "python") {
            return "python";
        }

        if (course === "cpp" || course === "c++") {
            return "cpp";
        }

        if (
            course === "html" ||
            course === "css" ||
            course === "html-css" ||
            course === "htmlcss"
        ) {
            return "htmlcss";
        }

        return "csharp";
    }

    function save() {
        localStorage.setItem(storageKey, JSON.stringify(current));

        // Push XP + awards to user_profiles (cross-browser source of truth).
        window.caveCodeAuth
            ?.saveUserProfile?.(cloudPayloadFromCurrent())
            .catch(() => {});

        // Sync course rows (awards merged from progression in auth.js).
        const courses = ["csharp", "python", "cpp", "htmlcss"];

        courses.forEach(course => {
            window.caveCodeAuth
                ?.syncLocalProgressToCloud?.(course)
                .catch(() => {});
        });

        window.dispatchEvent(
            new CustomEvent("cavecode-progression-changed", {
                detail: stateView()
            })
        );
    }

    function levelView(totalXp) {
        let level = 1;
        let remaining = Math.max(0, Number(totalXp) || 0);
        let required = 500;

        while (remaining >= required) {
            remaining -= required;
            level += 1;
            required = 400 + level * 100;
        }

        return {
            level,
            xpIntoLevel: remaining,
            xpForNextLevel: required,
            levelProgress: Math.floor((remaining * 100) / required)
        };
    }

    function stateView() {
        return {
            totalXp: current.totalXp,
            cSharpXp: current.cSharpXp,
            pythonXp: current.pythonXp,
            cppXp: current.cppXp,
            htmlCssXp: current.htmlCssXp,
            totalLines: current.totalLines,
            cSharpLines: current.cSharpLines,
            pythonLines: current.pythonLines,
            cppLines: current.cppLines,
            htmlCssLines: current.htmlCssLines,
            publicLeaderboard: current.publicLeaderboard,
            ...levelView(current.totalXp)
        };
    }

    function addXp(course, amount) {
        current.totalXp += amount;

        if (course === "python") {
            current.pythonXp += amount;
        } else if (course === "cpp") {
            current.cppXp += amount;
        } else if (course === "htmlcss") {
            current.htmlCssXp += amount;
        } else {
            current.cSharpXp += amount;
        }
    }

    function addLines(course, amount) {
        current.totalLines += amount;

        if (course === "python") {
            current.pythonLines += amount;
        } else if (course === "cpp") {
            current.cppLines += amount;
        } else if (course === "htmlcss") {
            current.htmlCssLines += amount;
        } else {
            current.cSharpLines += amount;
        }
    }

    function countLines(code) {
        return String(code || "")
            .replace(/\r\n/g, "\n")
            .split("\n")
            .filter(line => line.trim().length > 0).length;
    }

    function reconcile(course) {
        let snapshot = null;

        try {
            snapshot = JSON.parse(
                localStorage.getItem(
                    `cavecode.${course}.progress.v1`
                ) || "null"
            );
        } catch {
            snapshot = null;
        }

        if (!snapshot || !Array.isArray(snapshot.moduleCompleted)) {
            return;
        }

        snapshot.moduleCompleted.forEach((complete, index) => {
            if (!complete) {
                return;
            }

            const moduleKey = `${course}:${index}`;

            if (!current.awardedModules[moduleKey]) {
                current.awardedModules[moduleKey] = true;
                addXp(course, index % 8 === 7 ? 200 : 100);
            }

            if (index % 8 === 7) {
                const chapter = Math.floor(index / 8) + 1;
                const chapterKey = `${course}:${chapter}`;

                if (!current.awardedChapters[chapterKey]) {
                    current.awardedChapters[chapterKey] = true;
                    addXp(course, 250);
                }
            }
        });
    }

    function reconcileAll() {
        const before = JSON.stringify(current);
        reconcile("csharp");
        reconcile("python");
        reconcile("cpp");
        reconcile("htmlcss");

        if (before !== JSON.stringify(current)) {
            save();
        }
    }

    function localEntry(profile, user) {
        const state = stateView();
        return {
            id: user?.id || "local-player",
            displayName:
                String(profile?.displayName || "").trim() ||
                user?.userName ||
                "CaveCode Learner",
            emblem: profile?.emblem || "crystal",
            title: profile?.title || "Cave Explorer",
            totalXp: state.totalXp,
            cSharpXp: state.cSharpXp,
            pythonXp: state.pythonXp,
            cppXp: state.cppXp,
            htmlCssXp: state.htmlCssXp,
            totalLines: state.totalLines,
            cSharpLines: state.cSharpLines,
            pythonLines: state.pythonLines,
            cppLines: state.cppLines,
            htmlCssLines: state.htmlCssLines,
            level: state.level,
            isCurrentUser: true
        };
    }

    function sortEntries(entries, filter) {
        const xpField =
            filter === "csharp"
                ? "cSharpXp"
                : filter === "python"
                    ? "pythonXp"
                    : filter === "cpp"
                        ? "cppXp"
                        : "totalXp";

        const linesField =
            filter === "csharp"
                ? "cSharpLines"
                : filter === "python"
                    ? "pythonLines"
                    : filter === "cpp"
                        ? "cppLines"
                        : "totalLines";

        return [...entries].sort((a, b) => {
            const xp =
                Number(b[xpField] || 0) - Number(a[xpField] || 0);

            if (xp !== 0) {
                return xp;
            }

            return (
                Number(b[linesField] || 0) -
                Number(a[linesField] || 0)
            );
        });
    }

    progressionReadyPromise = initializeProgression();

    window.caveCodeProgression = {
        ready: function () {
            return progressionReadyPromise;
        },

        reloadFromCloud: async function () {
            progressionReady = false;
            progressionReadyPromise = initializeProgression();
            await progressionReadyPromise;
            return stateView();
        },

        getState: async function () {
            if (!progressionReady) {
                await progressionReadyPromise;
            }

            reconcileAll();
            return stateView();
        },

        awardStage: function (course, moduleIndex, stageIndex, code) {
            if (!progressionReady) {
                return stateView();
            }
            course = normalizeCourse(course);

            if (![1, 2, 4, 5, 6].includes(Number(stageIndex))) {
                return stateView();
            }

            const key = `${course}:${moduleIndex}:${stageIndex}`;

            if (current.awardedStages[key]) {
                return stateView();
            }

            current.awardedStages[key] = true;
            addLines(course, countLines(code));
            save();

            return stateView();
        },

        awardModule: function (course, moduleIndex) {
            if (!progressionReady) {
                return stateView();
            }
            course = normalizeCourse(course);

            const key = `${course}:${moduleIndex}`;
            let xpAwarded = 0;

            if (!current.awardedModules[key]) {
                current.awardedModules[key] = true;
                const moduleXp =
                    Number(moduleIndex) % 8 === 7 ? 200 : 100;

                addXp(course, moduleXp);
                xpAwarded += moduleXp;
            }

            if (Number(moduleIndex) % 8 === 7) {
                const chapter =
                    Math.floor(Number(moduleIndex) / 8) + 1;
                const chapterKey = `${course}:${chapter}`;

                if (!current.awardedChapters[chapterKey]) {
                    current.awardedChapters[chapterKey] = true;
                    addXp(course, 250);
                    xpAwarded += 250;
                }
            }

            if (xpAwarded > 0) {
                save();
            }

            return {
                newlyAwarded: xpAwarded > 0,
                xpAwarded,
                state: stateView()
            };
        },

        getCourseResume: function (course) {
            course = normalizeCourse(course);

            let snapshot = null;

            try {
                snapshot = JSON.parse(
                    localStorage.getItem(
                        `cavecode.${course}.progress.v1`
                    ) || "null"
                );
            } catch {
                snapshot = null;
            }

            const totalModules = 40;

            if (!snapshot) {
                return {
                    hasProgress: false,
                    courseComplete: false,
                    currentModuleIndex: 0,
                    currentStage: 0,
                    completedModules: 0,
                    totalModules,
                    moduleMastery: 0
                };
            }

            const completedFlags = Array.isArray(
                snapshot.moduleCompleted
            )
                ? snapshot.moduleCompleted
                : [];

            const highestStages = Array.isArray(
                snapshot.highestCompletedStage
            )
                ? snapshot.highestCompletedStage
                : [];

            const completedModules =
                completedFlags.filter(Boolean).length;

            const currentModuleIndex = Math.max(
                0,
                Math.min(
                    totalModules - 1,
                    Number(snapshot.currentModuleIndex) || 0
                )
            );

            const currentStage = Math.max(
                0,
                Math.min(7, Number(snapshot.currentStage) || 0)
            );

            const moduleMastery = completedFlags[currentModuleIndex]
                ? 100
                : Math.max(
                    0,
                    Math.min(
                        100,
                        (
                            Number(
                                highestStages[currentModuleIndex] ??
                                    -1
                            ) + 1
                        ) *
                            100 /
                            8
                    )
                );

            return {
                hasProgress:
                    completedModules > 0 ||
                    currentModuleIndex > 0 ||
                    currentStage > 0 ||
                    moduleMastery > 0,
                courseComplete: completedModules >= totalModules,
                currentModuleIndex,
                currentStage,
                completedModules,
                totalModules,
                moduleMastery: Math.floor(moduleMastery)
            };
        },

        awardMinigameRun: function (
            course,
            rewardKey,
            xp,
            validatedLines
        ) {
            if (!progressionReady) {
                return stateView();
            }
            course = normalizeCourse(course);

            current.awardedMinigameRuns =
                current.awardedMinigameRuns || {};

            const key = String(rewardKey);

            if (current.awardedMinigameRuns[key]) {
                return stateView();
            }

            current.awardedMinigameRuns[key] = true;

            addXp(course, Math.max(0, Number(xp) || 0));

            addLines(
                course,
                Math.max(0, Number(validatedLines) || 0)
            );

            save();
            return stateView();
        },

        setPublicLeaderboard: function (isPublic) {
            if (!progressionReady) {
                return stateView();
            }
            current.publicLeaderboard = Boolean(isPublic);
            save();
            return stateView();
        },

        getLeaderboard: async function (filter, profile) {
            if (!progressionReady) {
                await progressionReadyPromise;
            }
            reconcileAll();

            filter =
                filter === "csharp" ||
                filter === "python" ||
                filter === "cpp"
                    ? filter
                    : "overall";

            let user = null;

            try {
                user =
                    await window.caveCodeAuth?.getCurrentUser?.();
            } catch {
                user = null;
            }

            const local = localEntry(profile, user);
            const cacheKey = `cavecode.leaderboard.cache.v1.${filter}`;

            let cloudAvailable = false;
            let entries = [];
            let message = "";

            const readCachedEntries = () => {
                try {
                    const raw =
                        window.localStorage.getItem(cacheKey);

                    if (!raw) {
                        return [];
                    }

                    const parsed = JSON.parse(raw);

                    return Array.isArray(parsed) ? parsed : [];
                } catch {
                    return [];
                }
            };

            const saveCachedEntries = value => {
                try {
                    window.localStorage.setItem(
                        cacheKey,
                        JSON.stringify(value)
                    );
                } catch {
                    // The live leaderboard still works when storage
                    // is unavailable.
                }
            };

            const cachedEntries = readCachedEntries();

            if (user && window.caveCodeAuth) {
                try {
                    const sync =
                        await window.caveCodeAuth.upsertLeaderboardProfile(
                            {
                                ...local,
                                isPublic: current.publicLeaderboard
                            }
                        );

                    cloudAvailable = Boolean(sync?.available);

                    const cloud =
                        await window.caveCodeAuth.getLeaderboardProfiles(
                            filter
                        );

                    if (cloud?.available) {
                        cloudAvailable = true;

                        const cloudEntries = Array.isArray(
                            cloud.entries
                        )
                            ? cloud.entries
                            : [];

                        entries =
                            cachedEntries.length >
                            cloudEntries.length
                                ? cachedEntries
                                : cloudEntries;

                        if (
                            cloudEntries.length >=
                            cachedEntries.length
                        ) {
                            saveCachedEntries(cloudEntries);
                        }
                    } else {
                        entries = cachedEntries;
                        message = cloud?.message || "";
                    }
                } catch (error) {
                    entries = cachedEntries;
                    message =
                        error?.message ||
                        "Global leaderboard is not configured.";
                }
            } else {
                entries = cachedEntries;
            }

            const reconciledById = new Map();

            for (const entry of entries) {
                if (!entry || !entry.id) {
                    continue;
                }

                reconciledById.set(entry.id, {
                    ...entry,
                    isCurrentUser: entry.id === local.id
                });
            }

            const existing = reconciledById.get(local.id);

            reconciledById.set(
                local.id,
                existing
                    ? {
                        ...existing,
                        isCurrentUser: true
                    }
                    : local
            );

            entries = [...reconciledById.values()];

            if (entries.length > cachedEntries.length) {
                saveCachedEntries(
                    entries.map(entry => ({
                        ...entry,
                        isCurrentUser: false
                    }))
                );
            }

            if (!user) {
                message =
                    "Sign in to publish your profile and join global rankings.";
            } else if (!cloudAvailable && !message) {
                message =
                    "Showing the last saved shared leaderboard while cloud rankings reconnect.";
            } else if (!current.publicLeaderboard) {
                message =
                    "Your profile is private. Enable public visibility to appear for other players.";
            }

            return {
                cloudAvailable,
                signedIn: Boolean(user),
                message,
                entries: sortEntries(entries, filter)
            };
        }
    };
})();
