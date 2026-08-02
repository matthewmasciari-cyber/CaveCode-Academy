let supabaseClient = null;

const caveCodeLiveOrigin = "https://cavecodeacademy.dev";

function normalizeCourseKey(courseKey) {
    return courseKey === "python" ? "python" : "csharp";
}

function courseProgressKey(courseKey) {
    return `cavecode.${normalizeCourseKey(courseKey)}.progress.v1`;
}

function caveCodeReturnUrl() {
    const path = window.location.pathname;

    if (path.endsWith("/csharp")) {
        return `${caveCodeLiveOrigin}/csharp`;
    }

    if (path.endsWith("/python")) {
        return `${caveCodeLiveOrigin}/python`;
    }

    return `${caveCodeLiveOrigin}/`;
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

    // Backward-compatible C# progress functions.
    saveCourseProgress: function (progress) {
        saveLocalProgress("csharp", progress);
    },

    loadCourseProgress: function () {
        return readLocalProgress("csharp");
    },

    // Course-specific functions used by all new learning paths.
    saveCourseProgressFor: function (courseKey, progress) {
        saveLocalProgress(courseKey, progress);
    },

    loadCourseProgressFor: function (courseKey) {
        return readLocalProgress(courseKey);
    },

    syncLocalProgressToCloud: async function (courseKey = "csharp") {
        // Local checkpoints are preserved now. The Supabase database
        // synchronization pass will connect these snapshots to the cloud.
        return readLocalProgress(courseKey);
    }
};
