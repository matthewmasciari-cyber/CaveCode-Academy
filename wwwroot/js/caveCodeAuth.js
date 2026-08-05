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

    try {
        const {
            data: { session },
            error
        } = await supabaseClient.auth.getSession();

        return {
            ready: !error,
            signedIn: Boolean(session?.user)
        };
    } catch {
        return {
            ready: false,
            signedIn: false
        };
    }
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
    const local = readLocalProgress("csharp");

    try {
        const cloud = await this.loadCloudProgress("csharp");

        if (cloud) {
const highestCompletedStage = Array.isArray(local.HighestCompletedStage)
    ? local.HighestCompletedStage
    : Array(40).fill(-1);

const moduleCompleted = Array.isArray(local.ModuleCompleted)
    ? local.ModuleCompleted
    : Array(40).fill(false);

for (let i = 0; i < (cloud.currentModuleIndex || 0); i++) {
    moduleCompleted[i] = true;
    highestCompletedStage[i] = 7;
}

return {
    ...local,
    HighestCompletedStage: highestCompletedStage,
    ModuleCompleted: moduleCompleted,
    CurrentModuleIndex: cloud.currentModuleIndex || 0,
    CurrentStage: cloud.currentStage || 0
};
        }
    } catch {
        // Fall back to local progress
    }

    return local;
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

        if (cloud) {
            return {
                ...local,
                currentModuleIndex: cloud.currentModuleIndex || 0,
                currentStage: cloud.currentStage || 0,
                CurrentModuleIndex: cloud.currentModuleIndex || 0,
                CurrentStage: cloud.currentStage || 0
            };
        }
    } catch {
        // Fall back to local progress
    }

    return local;
},

    syncLocalProgressToCloud: async function(courseKey = "csharp") {
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

    const progress = readLocalProgress(courseKey);

    if (!progress) {
        return {
            available: false,
            message: "No local progress found."
        };
    }

    const payload = {
        user_id: user.id,
        course_id: normalizeCourseKey(courseKey),
        awarded_modules: progress.awardedModules || {},
        awarded_stages: progress.awardedStages || {},
        awarded_chapters: progress.awardedChapters || {},
        current_module: Number(progress.currentModuleIndex || 0),
        current_stage: Number(progress.currentStage || 0),
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

loadCloudProgress: async function(courseKey = "csharp") {
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
            return { available: false, message: "Supabase is not initialized." };
        }

        const user = await currentUser();

        if (!user) {
            return { available: false, message: "Sign in to publish rankings." };
        }

        const payload = {
            id: user.id,
            display_name: String(profile.displayName || "CaveCode Learner").slice(0, 24),
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
            return { available: false, entries: [], message: "Supabase is not initialized." };
        }

        const orderColumn =
            filter === "csharp"
                ? "csharp_xp"
                : filter === "python"
                    ? "python_xp"
                    : "total_xp";

        const { data, error } = await supabaseClient
            .from("leaderboard_profiles")
            .select("id, display_name, emblem, title, total_xp, csharp_xp, python_xp, total_lines, csharp_lines, python_lines")
            .eq("is_public", true)
            .order(orderColumn, { ascending: false })
            .limit(100);

        if (error) {
            return { available: false, entries: [], message: error.message };
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