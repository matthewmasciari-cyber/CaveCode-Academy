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
        raspiXp: 0,
        javascriptXp: 0,
        totalLines: 0,
        cSharpLines: 0,
        pythonLines: 0,
        cppLines: 0,
        htmlCssLines: 0,
        gclLines: 0,
        arduinoLines: 0,
        raspiLines: 0,
        javascriptLines: 0,
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
            raspiXp: Number(profile.raspi_xp || 0),
            totalLines: Number(profile.total_lines || 0),
            cSharpLines: Number(profile.csharp_lines || 0),
            pythonLines: Number(profile.python_lines || 0),
            cppLines: Number(profile.cpp_lines || 0),
            htmlCssLines: Number(profile.html_css_lines || 0),
            gclLines: Number(profile.gcl_lines || 0),
            arduinoLines: Number(profile.arduino_lines || 0),
            raspiLines: Number(profile.raspi_lines || 0),
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
            raspi_xp: current.raspiXp,
            javascript_xp: current.javascriptXp,
            total_lines: current.totalLines,
            csharp_lines: current.cSharpLines,
            python_lines: current.pythonLines,
            cpp_lines: current.cppLines,
            html_css_lines: current.htmlCssLines,
            gcl_lines: current.gclLines,
            arduino_lines: current.arduinoLines,
            raspi_lines: current.raspiLines,
            javascript_lines: current.javascriptLines,
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

        const courses = ["csharp", "python", "cpp", "htmlcss", "gcl", "arduino", "raspi", "javascript"];

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
            raspiXp: Math.max(
                Number(localState.raspiXp || 0),
                Number(cloud.raspiXp || 0)
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
            raspiLines: Math.max(
                Number(localState.raspiLines || 0),
                Number(cloud.raspiLines || 0)
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

        const courses = ["csharp", "python", "cpp", "htmlcss", "gcl", "arduino", "raspi", "javascript"];

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
    const local = load();

    try {
        await window.caveCodeAuth?.waitForReady?.(8000);
    } catch (e) {}

    let cloud = null;
    try {
        cloud = await window.caveCodeAuth?.loadUserProfile?.();
    } catch (error) {
        console.error("Progression cloud load failed:", error);
    }

    function num(v) {
        const n = Number(v);
        return Number.isFinite(n) ? Math.max(0, Math.floor(n)) : 0;
    }

    function mergeMaps(a, b) {
        return Object.assign({}, a || {}, b || {});
    }

    if (cloud) {
        const cloudTotal = num(cloud.total_xp);
        const localTotal = num(local.totalXp);

        current = {
            ...defaults,
            ...local,
            totalXp: Math.max(localTotal, cloudTotal),
            cSharpXp: Math.max(num(local.cSharpXp), num(cloud.csharp_xp)),
            pythonXp: Math.max(num(local.pythonXp), num(cloud.python_xp)),
            cppXp: Math.max(num(local.cppXp), num(cloud.cpp_xp)),
            htmlCssXp: Math.max(num(local.htmlCssXp), num(cloud.html_css_xp)),
            gclXp: Math.max(num(local.gclXp), num(cloud.gcl_xp)),
            arduinoXp: Math.max(num(local.arduinoXp), num(cloud.arduino_xp)),
            raspiXp: Math.max(num(local.raspiXp), num(cloud.raspi_xp)),
            javascriptXp: Math.max(num(local.javascriptXp), num(cloud.javascript_xp)),
            totalLines: Math.max(num(local.totalLines), num(cloud.total_lines)),
            cSharpLines: Math.max(num(local.cSharpLines), num(cloud.csharp_lines)),
            pythonLines: Math.max(num(local.pythonLines), num(cloud.python_lines)),
            cppLines: Math.max(num(local.cppLines), num(cloud.cpp_lines)),
            htmlCssLines: Math.max(num(local.htmlCssLines), num(cloud.html_css_lines)),
            awardedModules: mergeMaps(cloud.awarded_modules, local.awardedModules),
            awardedStages: mergeMaps(cloud.awarded_stages, local.awardedStages),
            awardedChapters: mergeMaps(cloud.awarded_chapters, local.awardedChapters),
            publicLeaderboard: Boolean(local.publicLeaderboard || cloud.is_public)
        };

        try {
            localStorage.setItem(storageKey, JSON.stringify(current));
        } catch (e) {}

        if (localTotal > cloudTotal || num(local.totalLines) > num(cloud.total_lines)) {
            try {
                await window.caveCodeAuth?.saveUserProfile?.({
                    total_xp: current.totalXp,
                    csharp_xp: current.cSharpXp,
                    python_xp: current.pythonXp,
                    cpp_xp: current.cppXp,
                    html_css_xp: current.htmlCssXp,
                    gcl_xp: current.gclXp || 0,
                    arduino_xp: current.arduinoXp || 0,
                    raspi_xp: current.raspiXp || 0,
                    javascript_xp: current.javascriptXp || 0,
                    total_lines: current.totalLines,
                    csharp_lines: current.cSharpLines,
                    python_lines: current.pythonLines,
                    cpp_lines: current.cppLines,
                    html_css_lines: current.htmlCssLines,
                    awarded_modules: current.awardedModules,
                    awarded_stages: current.awardedStages,
                    awarded_chapters: current.awardedChapters
                });
            } catch (e) {
                console.error("Progression cloud push failed:", e);
            }
        }
    } else {
        current = local;
        try {
            const signedIn = await window.caveCodeAuth?.isSignedIn?.();
            if (signedIn && num(local.totalXp) > 0) {
                await window.caveCodeAuth?.saveUserProfile?.({
                    total_xp: local.totalXp,
                    csharp_xp: local.cSharpXp,
                    python_xp: local.pythonXp,
                    cpp_xp: local.cppXp,
                    html_css_xp: local.htmlCssXp,
                    total_lines: local.totalLines,
                    csharp_lines: local.cSharpLines,
                    python_lines: local.pythonLines,
                    cpp_lines: local.cppLines,
                    html_css_lines: local.htmlCssLines,
                    awarded_modules: local.awardedModules,
                    awarded_stages: local.awardedStages,
                    awarded_chapters: local.awardedChapters
                });
            }
        } catch (e) {}
    }

    progressionReady = true;
}
)();
