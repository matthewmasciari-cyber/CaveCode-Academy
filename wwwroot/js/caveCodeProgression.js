(function () {
    const storageKey = "cavecode.progression.v1";
    const defaults = {
        totalXp: 0,
        cSharpXp: 0,
        pythonXp: 0,
        cppXp: 0,
        htmlCssXp: 0,
        gclXp: 0,
        arduinoXp: 0,
        totalLines: 0,
        cSharpLines: 0,
        pythonLines: 0,
        cppLines: 0,
        htmlCssLines: 0,
        gclLines: 0,
        arduinoLines: 0,
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
            gclXp: Number(profile.gcl_xp || 0),
            arduinoXp: Number(profile.arduino_xp || 0),
            totalLines: Number(profile.total_lines || 0),
            cSharpLines: Number(profile.csharp_lines || 0),
            pythonLines: Number(profile.python_lines || 0),
            cppLines: Number(profile.cpp_lines || 0),
            htmlCssLines: Number(profile.html_css_lines || 0),
            gclLines: Number(profile.gcl_lines || 0),
            arduinoLines: Number(profile.arduino_lines || 0),
            publicLeaderboard: Boolean(
                profile.public_leaderboard ?? profile.publicLeaderboard
            ),
            awardedModules: profile.awarded_modules || {},
            awardedStages: profile.awarded_stages || {},
            awardedChapters: profile.awarded_chapters || {},
        };
    }

    function cloudPayloadFromCurrent() {
        return {
            total_xp: current.totalXp,
            csharp_xp: current.cSharpXp,
            python_xp: current.pythonXp,
            cpp_xp: current.cppXp,
            html_css_xp: current.htmlCssXp,
            gcl_xp: current.gclXp,
            arduino_xp: current.arduinoXp,
            total_lines: current.totalLines,
            csharp_lines: current.cSharpLines,
            python_lines: current.pythonLines,
            cpp_lines: current.cppLines,
            html_css_lines: current.htmlCssLines,
            gcl_lines: current.gclLines,
            arduino_lines: current.arduinoLines,
            public_leaderboard: current.publicLeaderboard,
            awarded_modules: current.awardedModules || {},
            awarded_stages: current.awardedStages || {},
            awarded_chapters: current.awardedChapters || {},
        };
    }

    async function hydrateCourseProgressFromCloud() {
        if (!window.caveCodeAuth?.loadCourseProgressFor) {
            return;
        }

        const courses = ["csharp", "python", "cpp", "htmlcss", "gcl", "arduino"];

        for (const course of courses) {
            try {
                await window.caveCodeAuth.loadCourseProgressFor(course);
            } catch {
                // Keep going; one course failure must not block others.
            }
        }
    }

    function mergeAwardMaps(a, b) {
        return {
            ...(a || {}),
            ...(b || {})
        };
    }

    // Take the richer of local + cloud so a new device never wins with zeros.
    function mergeLocalAndCloud(localState, cloudProfile) {
        const cloud = profileFromCloud(cloudProfile || {});

        return {
            ...defaults,
            totalXp: Math.max(
                Number(localState.totalXp || 0),
                Number(cloud.totalXp || 0)
            ),
            cSharpXp: Math.max(
                Number(localState.cSharpXp || 0),
                Number(cloud.cSharpXp || 0)
            ),
            pythonXp: Math.max(
                Number(localState.pythonXp || 0),
                Number(cloud.pythonXp || 0)
            ),
            cppXp: Math.max(
                Number(localState.cppXp || 0),
                Number(cloud.cppXp || 0)
            ),
            htmlCssXp: Math.max(
                Number(localState.htmlCssXp || 0),
                Number(cloud.htmlCssXp || 0)
            ),
            gclXp: Math.max(
                Number(localState.gclXp || 0),
                Number(cloud.gclXp || 0)
            ),
            arduinoXp: Math.max(
                Number(localState.arduinoXp || 0),
                Number(cloud.arduinoXp || 0)
            ),
            totalLines: Math.max(
                Number(localState.totalLines || 0),
                Number(cloud.totalLines || 0)
            ),
            cSharpLines: Math.max(
                Number(localState.cSharpLines || 0),
                Number(cloud.cSharpLines || 0)
            ),
            pythonLines: Math.max(
                Number(localState.pythonLines || 0),
                Number(cloud.pythonLines || 0)
            ),
            cppLines: Math.max(
                Number(localState.cppLines || 0),
                Number(cloud.cppLines || 0)
            ),
            htmlCssLines: Math.max(
                Number(localState.htmlCssLines || 0),
                Number(cloud.htmlCssLines || 0)
            ),
            gclLines: Math.max(
                Number(localState.gclLines || 0),
                Number(cloud.gclLines || 0)
            ),
            arduinoLines: Math.max(
                Number(localState.arduinoLines || 0),
                Number(cloud.arduinoLines || 0)
            ),
            publicLeaderboard: Boolean(
                localState.publicLeaderboard || cloud.publicLeaderboard
            ),
            awardedModules: mergeAwardMaps(
                localState.awardedModules,
                cloud.awardedModules
            ),
            awardedStages: mergeAwardMaps(
                localState.awardedStages,
                cloud.awardedStages
            ),
            awardedChapters: mergeAwardMaps(
                localState.awardedChapters,
                cloud.awardedChapters
            ),
            awardedMinigameRuns: mergeAwardMaps(
                localState.awardedMinigameRuns,
                cloud.awardedMinigameRuns
            )
        };
    }

    function persistLocalOnly() {
        localStorage.setItem(storageKey, JSON.stringify(current));
        window.dispatchEvent(
            new CustomEvent("cavecode-progression-changed", {
                detail: stateView()
            })
        );
    }

    function clearLeaderboardCache() {
        try {
            Object.keys(window.localStorage || {})
                .filter(key =>
                    key.startsWith("cavecode.leaderboard.cache")
                )
                .forEach(key => window.localStorage.removeItem(key));
        } catch {
            // Cache clear is best-effort.
        }
    }

    function readProfileForLeaderboard() {
        try {
            if (
                window.caveCodeProfile &&
                typeof window.caveCodeProfile.getPreferences ===
                    "function"
            ) {
                return window.caveCodeProfile.getPreferences() || {};
            }
        } catch {
            // Fall through.
        }

        try {
            return JSON.parse(
                window.localStorage.getItem("cavecode.profile.v1") ||
                    "{}"
            );
        } catch {
            return {};
        }
    }

    // Keep leaderboard_profiles aligned with XP + display name.
    async function publishLeaderboardRow(forcePublic) {
        if (!window.caveCodeAuth?.upsertLeaderboardProfile) {
            return { available: false, message: "Auth unavailable." };
        }

        const signedIn = await window.caveCodeAuth.isSignedIn?.();

        if (!signedIn) {
            return { available: false, message: "Sign in required." };
        }

        const isPublic =
            forcePublic === true
                ? true
                : forcePublic === false
                    ? false
                    : Boolean(current.publicLeaderboard);

        if (!isPublic) {
            clearLeaderboardCache();
            window.dispatchEvent(
                new CustomEvent("cavecode-leaderboard-changed", {
                    detail: { isPublic: false }
                })
            );
            return { available: true, message: "Private." };
        }

        const profile = readProfileForLeaderboard();
        const displayName =
            String(profile.displayName || "").trim() ||
            "CaveCode Learner";

        clearLeaderboardCache();

        const result =
            await window.caveCodeAuth.upsertLeaderboardProfile({
                displayName,
                emblem: profile.emblem || "crystal",
                title: profile.title || "Cave Explorer",
                totalXp: Number(current.totalXp || 0),
                cSharpXp: Number(current.cSharpXp || 0),
                pythonXp: Number(current.pythonXp || 0),
                cppXp: Number(current.cppXp || 0),
                htmlCssXp: Number(current.htmlCssXp || 0),
                gclXp: Number(current.gclXp || 0),
                totalLines: Number(current.totalLines || 0),
                cSharpLines: Number(current.cSharpLines || 0),
                pythonLines: Number(current.pythonLines || 0),
                gclLines: Number(current.gclLines || 0),
                isPublic: true
            });

        window.dispatchEvent(
            new CustomEvent("cavecode-leaderboard-changed", {
                detail: {
                    isPublic: true,
                    totalXp: Number(current.totalXp || 0),
                    displayName
                }
            })
        );

        return result;
    }

    // Upload only when local is at least as rich as cloud (never clobber up).
    async function pushToCloudIfSafe() {
        if (!window.caveCodeAuth?.saveUserProfile) {
            return;
        }

        const signedIn = await window.caveCodeAuth.isSignedIn?.();

        if (!signedIn) {
            return;
        }

        let cloudXp = 0;

        try {
            const cloud = await window.caveCodeAuth.loadUserProfile?.();
            cloudXp = Number(cloud?.total_xp || 0);

            if (cloud) {
                current = mergeLocalAndCloud(current, cloud);
                localStorage.setItem(
                    storageKey,
                    JSON.stringify(current)
                );
            }
        } catch {
            // If cloud cannot be read, only upload when we have real local XP.
        }

        const localXp = Number(current.totalXp || 0);

        // Empty/new device must not overwrite real cloud progress.
        if (localXp < cloudXp) {
            return;
        }

        // Nothing meaningful to upload.
        if (localXp <= 0 && Number(current.totalLines || 0) <= 0) {
            return;
        }

        await window.caveCodeAuth.saveUserProfile(
            cloudPayloadFromCurrent()
        );

        const courses = ["csharp", "python", "cpp", "htmlcss", "gcl", "arduino"];

        for (const course of courses) {
            try {
                await window.caveCodeAuth.syncLocalProgressToCloud?.(
                    course
                );
            } catch {
                // One course failure must not block the rest.
            }
        }

        try {
            await publishLeaderboardRow();
        } catch {
            // Leaderboard publish must never block progression.
        }
    }

    async function initializeProgression() {
        const localSnapshot = load();

        try {
            await window.caveCodeAuth?.waitForReady?.(8000);

            const profile =
                await window.caveCodeAuth?.loadUserProfile?.();

            if (profile) {
                // Merge so a device with higher local XP is not discarded,
                // and a device with empty local adopts cloud XP.
                current = mergeLocalAndCloud(localSnapshot, profile);

                localStorage.setItem(
                    storageKey,
                    JSON.stringify(current)
                );

                await hydrateCourseProgressFromCloud();

                // Only push if local brought new progress above cloud.
                const cloudXp = Number(profile.total_xp || 0);

                if (Number(current.totalXp || 0) > cloudXp) {
                    try {
                        await window.caveCodeAuth.saveUserProfile?.(
                            cloudPayloadFromCurrent()
                        );
                    } catch {
                        // Keep merged local state even if upload fails.
                    }
                }

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

        current = localSnapshot;

        // No cloud row yet: only upload when this device has real progress.
        try {
            const signedIn =
                await window.caveCodeAuth?.isSignedIn?.();

            if (
                signedIn &&
                (Number(current.totalXp || 0) > 0 ||
                    Number(current.totalLines || 0) > 0)
            ) {
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

        if (
            course === "gcl" ||
            course === "gcl+" ||
            course === "cgl" ||
            course === "cgline" ||
            course === "cgline+"
        ) {
            return "gcl";
        }

        if (course === "arduino" || course === "arduino-cpp" || course === "arduinocpp") {
            return "arduino";
        }

        return "csharp";
    }

    function save() {
        // Always keep local cache for offline play.
        localStorage.setItem(storageKey, JSON.stringify(current));

        window.dispatchEvent(
            new CustomEvent("cavecode-progression-changed", {
                detail: stateView()
            })
        );

        // Cloud upload is async and guarded: never clobber higher cloud XP
        // with empty/stale local (the phone wipe bug).
        pushToCloudIfSafe().catch(() => {});
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
            gclXp: current.gclXp,
            arduinoXp: current.arduinoXp,
            totalLines: current.totalLines,
            cSharpLines: current.cSharpLines,
            pythonLines: current.pythonLines,
            cppLines: current.cppLines,
            htmlCssLines: current.htmlCssLines,
            gclLines: current.gclLines,
            arduinoLines: current.arduinoLines,
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
        } else if (course === "gcl") {
            current.gclXp += amount;
        } else if (course === "arduino") {
            current.arduinoXp += amount;
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
        } else if (course === "gcl") {
            current.gclLines += amount;
        } else if (course === "arduino") {
            current.arduinoLines += amount;
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
        reconcile("gcl");
        reconcile("arduino");

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
            gclXp: state.gclXp,
            arduinoXp: state.arduinoXp,
            totalLines: state.totalLines,
            cSharpLines: state.cSharpLines,
            pythonLines: state.pythonLines,
            cppLines: state.cppLines,
            htmlCssLines: state.htmlCssLines,
            gclLines: state.gclLines,
            arduinoLines: state.arduinoLines,
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
                        : filter === "gcl"
                            ? "gclXp"
                            : "totalXp";

        const linesField =
            filter === "csharp"
                ? "cSharpLines"
                : filter === "python"
                    ? "pythonLines"
                    : filter === "cpp"
                        ? "cppLines"
                        : filter === "gcl"
                            ? "gclLines"
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
            // Immediate board publish/hide when the toggle changes.
            publishLeaderboardRow(Boolean(isPublic)).catch(() => {});
            return stateView();
        },

        publishLeaderboardNow: async function (forcePublic) {
            if (!progressionReady) {
                await progressionReadyPromise;
            }
            return await publishLeaderboardRow(forcePublic);
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

    // Bridge: leaderboard page listens and re-renders without full reload.
    let leaderboardDotNetRef = null;
    let leaderboardHandler = null;

    window.caveCodeLeaderboard = {
        attach: function (dotNetRef) {
            leaderboardDotNetRef = dotNetRef;

            if (leaderboardHandler) {
                return;
            }

            leaderboardHandler = function () {
                if (!leaderboardDotNetRef) {
                    return;
                }

                leaderboardDotNetRef
                    .invokeMethodAsync("OnLeaderboardNeedsRefresh")
                    .catch(() => {});
            };

            window.addEventListener(
                "cavecode-leaderboard-changed",
                leaderboardHandler
            );
            window.addEventListener(
                "cavecode-progression-changed",
                leaderboardHandler
            );
            window.addEventListener(
                "cavecode-profile-changed",
                leaderboardHandler
            );
        },

        detach: function () {
            if (leaderboardHandler) {
                window.removeEventListener(
                    "cavecode-leaderboard-changed",
                    leaderboardHandler
                );
                window.removeEventListener(
                    "cavecode-progression-changed",
                    leaderboardHandler
                );
                window.removeEventListener(
                    "cavecode-profile-changed",
                    leaderboardHandler
                );
                leaderboardHandler = null;
            }

            leaderboardDotNetRef = null;
        }
    };
})();
