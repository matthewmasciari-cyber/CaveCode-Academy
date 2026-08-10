let supabaseClient = null;

const caveCodeLiveOrigin = "https://cavecodeacademy.dev";

function normalizeCourseKey(courseKey) {
    // cavecode-auth-multicourse-course-keys-v1
    const value = String(courseKey || "").toLowerCase();

    if (value === "python") {
        return "python";
    }

    if (value === "cpp" || value === "c++") {
        return "cpp";
    }

    if (
        value === "html" ||
        value === "css" ||
        value === "html-css" ||
        value === "htmlcss"
    ) {
        return "htmlcss";
    }

    return "csharp";
}

function courseProgressKey(courseKey) {
    return `cavecode.${normalizeCourseKey(courseKey)}.progress.v1`;
}

function caveCodeReturnUrl() {
    // cavecode-auth-multicourse-return-v1
    const supportedPaths = new Set([
        "/csharp",
        "/python",
        "/cpp",
        "/html-css"
    ]);

    const path = supportedPaths.has(window.location.pathname)
        ? window.location.pathname
        : "/";

    return `${caveCodeLiveOrigin}${path}`;
}

function readLocalProgress(courseKey) {
    const raw = window.localStorage.getItem(
        courseProgressKey(courseKey)
    );

    if (!raw) {
        return null;
    }

    try {
        return JSON.parse(raw);
    } catch {
        return null;
    }
}

function saveLocalProgress(courseKey, progress) {
    const snapshot = {
        ...progress,
        updatedAt: new Date().toISOString()
    };

    window.localStorage.setItem(
        courseProgressKey(courseKey),
        JSON.stringify(snapshot)
    );
}

function filterAwardsByCourse(awards, courseKey) {
    const prefix = `${normalizeCourseKey(courseKey)}:`;
    const source = awards && typeof awards === "object" ? awards : {};

    return Object.fromEntries(
        Object.entries(source).filter(([key]) =>
            String(key).startsWith(prefix)
        )
    );
}

function readProgressionAwards(courseKey) {
    try {
        const raw = window.localStorage.getItem("cavecode.progression.v1");
        if (!raw) {
            return {
                awardedModules: {},
                awardedStages: {},
                awardedChapters: {}
            };
        }

        const prog = JSON.parse(raw);

        return {
            awardedModules: filterAwardsByCourse(
                prog.awardedModules,
                courseKey
            ),
            awardedStages: filterAwardsByCourse(
                prog.awardedStages,
                courseKey
            ),
            awardedChapters: filterAwardsByCourse(
                prog.awardedChapters,
                courseKey
            )
        };
    } catch {
        return {
            awardedModules: {},
            awardedStages: {},
            awardedChapters: {}
        };
    }
}

function mergeAwardMaps(a, b) {
    return {
        ...(a && typeof a === "object" ? a : {}),
        ...(b && typeof b === "object" ? b : {})
    };
}

function buildModuleCompletedFromCloud(courseKey, cloud, local) {
    const key = normalizeCourseKey(courseKey);
    const length = 40;

    const fromLocal =
        Array.isArray(local?.moduleCompleted)
            ? local.moduleCompleted
            : Array.isArray(local?.ModuleCompleted)
                ? local.ModuleCompleted
                : null;

    const moduleCompleted = fromLocal
        ? [...fromLocal]
        : Array(length).fill(false);

    while (moduleCompleted.length < length) {
        moduleCompleted.push(false);
    }

    const awarded = cloud?.awardedModules || {};

    Object.keys(awarded).forEach(awardKey => {
        const match = String(awardKey).match(
            new RegExp(`^${key}:(\\d+)$`)
        );

        if (match) {
            const index = Number(match[1]);

            if (index >= 0 && index < length) {
                moduleCompleted[index] = true;
            }
        }
    });

    for (let i = 0; i < (cloud?.currentModuleIndex || 0); i++) {
        if (i < length) {
            moduleCompleted[i] = true;
        }
    }

    return moduleCompleted;
}

async function currentUser() {
    if (!supabaseClient) {
        return null;
    }

    const {
        data: { user },
        error
    } = await supabaseClient.auth.getUser();

    return error ? null : user;
}

async function waitForAuthReady(timeoutMs = 5000) {
    const deadline =
        Date.now() + Math.max(250, Number(timeoutMs) || 5000);

    while (!supabaseClient && Date.now() < deadline) {
        await new Promise(resolve => setTimeout(resolve, 50));
    }

    if (!supabaseClient) {
        return {
            ready: false,
            signedIn: false
        };
    }

    // Poll briefly: mobile storage / URL session detection can lag.
    let last = { ready: true, signedIn: false };

    while (Date.now() < deadline) {
        try {
            const {
                data: { session },
                error
            } = await supabaseClient.auth.getSession();

            last = {
                ready: !error,
                signedIn: Boolean(session && session.user)
            };

            if (last.signedIn) {
                return last;
            }
        } catch {
            last = { ready: false, signedIn: false };
        }

        await new Promise(resolve => setTimeout(resolve, 100));
    }

    return last;
}

window.caveCodeAuth = {
    initialize: function (projectUrl, publishableKey) {
        if (supabaseClient) {
            return;
        }

        supabaseClient = supabase.createClient(
            projectUrl,
            publishableKey,
            {
                auth: {
                    persistSession: true,
                    autoRefreshToken: true,
                    detectSessionInUrl: true
                }
            }
        );

        // Mobile: session often restores after first page paint. Broadcast
        // auth changes so progression can re-merge XP from cloud.
        try {
            supabaseClient.auth.onAuthStateChange(function (event, session) {
                try {
                    window.dispatchEvent(
                        new CustomEvent("cavecode-auth-changed", {
                            detail: {
                                event: String(event || ""),
                                signedIn: Boolean(session && session.user),
                                userId:
                                    session && session.user
                                        ? session.user.id
                                        : null,
                                email:
                                    session && session.user
                                        ? session.user.email
                                        : null
                            }
                        })
                    );
                } catch (e) {
                    // Never break auth for UI listeners.
                }
            });
        } catch (e) {
            // Older clients — manual reload still works.
        }
    },

    signInWithProvider: async function (provider) {
        if (!supabaseClient) {
            throw new Error("Supabase has not been initialized.");
        }

        if (provider !== "google" && provider !== "github") {
            throw new Error("Unsupported login provider.");
        }

        const { error } = await supabaseClient.auth.signInWithOAuth({
            provider,
            options: {
                redirectTo: caveCodeReturnUrl()
            }
        });

        if (error) {
            throw error;
        }
    },

    signInWithGoogle: async function () {
        return window.caveCodeAuth.signInWithProvider("google");
    },

    signInWithGitHub: async function () {
        return window.caveCodeAuth.signInWithProvider("github");
    },

    signOut: async function () {
        if (!supabaseClient) {
            return;
        }

        const { error } = await supabaseClient.auth.signOut();

        if (error) {
            throw error;
        }
    },

    isSignedIn: async function () {
        return (await currentUser()) !== null;
    },

    waitForReady: async function (timeoutMs = 5000) {
        return await waitForAuthReady(timeoutMs);
    },

    getCurrentUser: async function () {
        const user = await currentUser();

        if (!user) {
            return null;
        }

        return {
            id: user.id,
            email: user.email ?? "",
            userName:
                user.user_metadata?.full_name ??
                user.user_metadata?.name ??
                user.user_metadata?.user_name ??
                user.user_metadata?.preferred_username ??
                "",
            avatarUrl:
                user.user_metadata?.avatar_url ??
                user.user_metadata?.picture ??
                ""
        };
    },

    loadCourseProgress: async function () {
        return window.caveCodeAuth.loadCourseProgressFor("csharp");
    },

    // Course-specific functions used by all new learning paths.
    saveCourseProgressFor: async function (courseKey, progress) {
        saveLocalProgress(courseKey, progress);
        await this.syncLocalProgressToCloud(courseKey);
    },

    loadCourseProgressFor: async function (courseKey) {
        const local = readLocalProgress(courseKey) || {};

        try {
            const cloud = await this.loadCloudProgress(courseKey);

            if (!cloud) {
                return local;
            }

            const moduleCompleted = buildModuleCompletedFromCloud(
                courseKey,
                cloud,
                local
            );

            const highestCompletedStage = Array.isArray(
                local.HighestCompletedStage
            )
                ? [...local.HighestCompletedStage]
                : Array.isArray(local.highestCompletedStage)
                    ? [...local.highestCompletedStage]
                    : Array(40).fill(-1);

            while (highestCompletedStage.length < 40) {
                highestCompletedStage.push(-1);
            }

            for (let i = 0; i < (cloud.currentModuleIndex || 0); i++) {
                highestCompletedStage[i] = Math.max(
                    Number(highestCompletedStage[i] ?? -1),
                    7
                );
            }

            const merged = {
                ...local,
                moduleCompleted,
                ModuleCompleted: moduleCompleted,
                highestCompletedStage,
                HighestCompletedStage: highestCompletedStage,
                currentModuleIndex: cloud.currentModuleIndex || 0,
                currentStage: cloud.currentStage || 0,
                CurrentModuleIndex: cloud.currentModuleIndex || 0,
                CurrentStage: cloud.currentStage || 0,
                awardedModules: cloud.awardedModules || {},
                awardedStages: cloud.awardedStages || {},
                awardedChapters: cloud.awardedChapters || {}
            };

            // Persist so refresh / other code paths see cloud state.
            saveLocalProgress(courseKey, merged);
            return merged;
        } catch {
            // Fall back to local progress
        }

        return local;
    },

    syncLocalProgressToCloud: async function (courseKey = "csharp") {
        if (!supabaseClient) {
            return {
                available: false,
                message: "Supabase is not initialized."
            };
        }

        const user = await currentUser();

        if (!user) {
            return {
                available: false,
                message: "Sign in required."
            };
        }

        const progress = readLocalProgress(courseKey) || {};
        const progressionAwards = readProgressionAwards(courseKey);

        const awardedModules = mergeAwardMaps(
            progressionAwards.awardedModules,
            progress.awardedModules
        );
        const awardedStages = mergeAwardMaps(
            progressionAwards.awardedStages,
            progress.awardedStages
        );
        const awardedChapters = mergeAwardMaps(
            progressionAwards.awardedChapters,
            progress.awardedChapters
        );

        // Also encode moduleCompleted[] as awarded_modules when present.
        const completedFlags = Array.isArray(progress.moduleCompleted)
            ? progress.moduleCompleted
            : Array.isArray(progress.ModuleCompleted)
                ? progress.ModuleCompleted
                : null;

        if (completedFlags) {
            const key = normalizeCourseKey(courseKey);

            completedFlags.forEach((complete, index) => {
                if (complete) {
                    awardedModules[`${key}:${index}`] = true;
                }
            });
        }

        const payload = {
            user_id: user.id,
            course_id: normalizeCourseKey(courseKey),
            awarded_modules: awardedModules,
            awarded_stages: awardedStages,
            awarded_chapters: awardedChapters,
            current_module: Number(
                progress.currentModuleIndex ??
                    progress.CurrentModuleIndex ??
                    0
            ),
            current_stage: Number(
                progress.currentStage ?? progress.CurrentStage ?? 0
            ),
            updated_at: new Date().toISOString()
        };

        const { error } = await supabaseClient
            .from("user_course_progress")
            .upsert(payload, {
                onConflict: "user_id,course_id"
            });

        return error
            ? {
                available: false,
                message: error.message
            }
            : {
                available: true,
                message: "Progress synced."
            };
    },

    loadCloudProgress: async function (courseKey = "csharp") {
        if (!supabaseClient) {
            return null;
        }

        const user = await currentUser();

        if (!user) {
            return null;
        }

        const { data, error } = await supabaseClient
            .from("user_course_progress")
            .select("*")
            .eq("user_id", user.id)
            .eq("course_id", normalizeCourseKey(courseKey))
            .maybeSingle();

        if (error || !data) {
            return null;
        }

        return {
            awardedModules: data.awarded_modules || {},
            awardedStages: data.awarded_stages || {},
            awardedChapters: data.awarded_chapters || {},
            currentModuleIndex: data.current_module || 0,
            currentStage: data.current_stage || 0,
            updatedAt: data.updated_at
        };
    },

    upsertLeaderboardProfile: async function (profile) {
        if (!supabaseClient) {
            return {
                available: false,
                message: "Supabase is not initialized."
            };
        }

        const user = await currentUser();

        if (!user) {
            return {
                available: false,
                message: "Sign in to publish rankings."
            };
        }

        const payload = {
            id: user.id,
            display_name: String(
                profile.displayName || "CaveCode Learner"
            ).slice(0, 24),
            emblem: profile.emblem || "crystal",
            title: profile.title || "Cave Explorer",
            total_xp: Number(profile.totalXp || 0),
            csharp_xp: Number(profile.cSharpXp || 0),
            python_xp: Number(profile.pythonXp || 0),
            total_lines: Number(profile.totalLines || 0),
            csharp_lines: Number(profile.cSharpLines || 0),
            python_lines: Number(profile.pythonLines || 0),
            is_public: Boolean(profile.isPublic),
            updated_at: new Date().toISOString()
        };

        const { error } = await supabaseClient
            .from("leaderboard_profiles")
            .upsert(payload, { onConflict: "id" });

        return error
            ? { available: false, message: error.message }
            : { available: true, message: "" };
    },

    getLeaderboardProfiles: async function (filter = "overall") {
        if (!supabaseClient) {
            return {
                available: false,
                entries: [],
                message: "Supabase is not initialized."
            };
        }

        const orderColumn =
            filter === "csharp"
                ? "csharp_xp"
                : filter === "python"
                    ? "python_xp"
                    : "total_xp";

        const { data, error } = await supabaseClient
            .from("leaderboard_profiles")
            .select(
                "id, display_name, emblem, title, total_xp, csharp_xp, python_xp, total_lines, csharp_lines, python_lines"
            )
            .eq("is_public", true)
            .order(orderColumn, { ascending: false })
            .limit(100);

        if (error) {
            return {
                available: false,
                entries: [],
                message: error.message
            };
        }

        const entries = (data || []).map(row => {
            let level = 1;
            let remaining = Number(row.total_xp || 0);
            let required = 500;

            while (remaining >= required) {
                remaining -= required;
                level += 1;
                required = 400 + level * 100;
            }

            return {
                id: row.id,
                displayName: row.display_name,
                emblem: row.emblem,
                title: row.title,
                totalXp: Number(row.total_xp || 0),
                cSharpXp: Number(row.csharp_xp || 0),
                pythonXp: Number(row.python_xp || 0),
                totalLines: Number(row.total_lines || 0),
                cSharpLines: Number(row.csharp_lines || 0),
                pythonLines: Number(row.python_lines || 0),
                level,
                isCurrentUser: false
            };
        });

        return { available: true, entries, message: "" };
    },

    loadUserProfile: async function () {
        if (!supabaseClient) {
            return null;
        }

        const user = await currentUser();

        if (!user) {
            return null;
        }

        const { data, error } = await supabaseClient
            .from("user_profiles")
            .select("*")
            .eq("id", user.id)
            .maybeSingle();

        if (error) {
            console.error("Failed loading user profile:", error);
            return null;
        }

        return data;
    },

    saveUserProfile: async function (profile) {
        if (!supabaseClient) {
            return {
                available: false,
                message: "Supabase is not initialized."
            };
        }

        const user = await currentUser();

        if (!user) {
            return {
                available: false,
                message: "Sign in required."
            };
        }

        const payload = {
            id: user.id,
            ...profile,
            updated_at: new Date().toISOString()
        };

        const { data, error } = await supabaseClient
            .from("user_profiles")
            .upsert(payload, {
                onConflict: "id"
            })
            .select()
            .single();

        if (error) {
            console.error("Failed saving user profile:", error);

            return {
                available: false,
                message: error.message
            };
        }

        return {
            available: true,
            profile: data
        };
    }
};
