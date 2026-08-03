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

    let current = load();

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

        window.dispatchEvent(
            new CustomEvent(
                "cavecode-progression-changed",
                { detail: stateView() }
            )
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
            levelProgress:
                Math.floor(remaining * 100 / required)
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
            .filter(line => line.trim().length > 0)
            .length;
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
            const xp = Number(b[xpField] || 0) -
                Number(a[xpField] || 0);

            if (xp !== 0) {
                return xp;
            }

            return Number(b[linesField] || 0) -
                Number(a[linesField] || 0);
        });
    }

    window.caveCodeProgression = {
        getState: function () {
            current = load();
            reconcileAll();
            return stateView();
        },

        awardStage: function (
            course,
            moduleIndex,
            stageIndex,
            code
        ) {
            current = load();
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
            current = load();
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

            const completedFlags =
                Array.isArray(snapshot.moduleCompleted)
                    ? snapshot.moduleCompleted
                    : [];

            const highestStages =
                Array.isArray(snapshot.highestCompletedStage)
                    ? snapshot.highestCompletedStage
                    : [];

            const completedModules =
                completedFlags.filter(Boolean).length;

            const currentModuleIndex =
                Math.max(
                    0,
                    Math.min(
                        totalModules - 1,
                        Number(snapshot.currentModuleIndex) || 0
                    )
                );

            const currentStage =
                Math.max(
                    0,
                    Math.min(
                        7,
                        Number(snapshot.currentStage) || 0
                    )
                );

            const moduleMastery =
                completedFlags[currentModuleIndex]
                    ? 100
                    : Math.max(
                        0,
                        Math.min(
                            100,
                            (
                                Number(
                                    highestStages[
                                        currentModuleIndex
                                    ] ?? -1
                                ) + 1
                            ) * 100 / 8
                        )
                    );

            return {
                hasProgress:
                    completedModules > 0 ||
                    currentModuleIndex > 0 ||
                    currentStage > 0 ||
                    moduleMastery > 0,
                courseComplete:
                    completedModules >= totalModules,
                currentModuleIndex,
                currentStage,
                completedModules,
                totalModules,
                moduleMastery:
                    Math.floor(moduleMastery)
            };
        },

        awardMinigameRun: function (
            course,
            rewardKey,
            xp,
            validatedLines
        ) {
            current = load();
            course = normalizeCourse(course);

            current.awardedMinigameRuns =
                current.awardedMinigameRuns || {};

            const key = String(rewardKey);

            if (current.awardedMinigameRuns[key]) {
                return stateView();
            }

            current.awardedMinigameRuns[key] = true;

            addXp(
                course,
                Math.max(0, Number(xp) || 0)
            );

            addLines(
                course,
                Math.max(
                    0,
                    Number(validatedLines) || 0
                )
            );

            save();
            return stateView();
        },

        setPublicLeaderboard: function (isPublic) {
            current = load();
            current.publicLeaderboard = Boolean(isPublic);
            save();
            return stateView();
        },

        getLeaderboard: async function (filter, profile) {
            current = load();
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
            let cloudAvailable = false;
            let entries = [];
            let message = "";

            if (user && window.caveCodeAuth) {
                try {
                    const sync =
                        await window.caveCodeAuth
                            .upsertLeaderboardProfile({
                                ...local,
                                isPublic:
                                    current.publicLeaderboard
                            });

                    cloudAvailable = Boolean(sync?.available);

                    const cloud =
                        await window.caveCodeAuth
                            .getLeaderboardProfiles(filter);

                    if (cloud?.available) {
                        cloudAvailable = true;
                        entries = cloud.entries || [];
                    } else {
                        message = cloud?.message || "";
                    }
                } catch (error) {
                    message =
                        error?.message ||
                        "Global leaderboard is not configured.";
                }
            }

            const existing = entries.findIndex(
                entry => entry.id === local.id
            );

            if (existing >= 0) {
                entries[existing] = {
                    ...entries[existing],
                    isCurrentUser: true
                };
            } else {
                entries.push(local);
            }

            if (!user) {
                message =
                    "Sign in to publish your profile and join global rankings.";
            } else if (!cloudAvailable && !message) {
                message =
                    "Run the included Supabase SQL to activate global rankings.";
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
