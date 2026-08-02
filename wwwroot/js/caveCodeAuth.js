let supabaseClient = null;

const caveCodeLiveOrigin = "https://cavecodeacademy.dev";
const caveCodeProgressKey = "cavecode.csharp.progress.v1";

function caveCodeReturnUrl() {
    const path = window.location.pathname.endsWith("/csharp")
        ? "/csharp"
        : "/";

    return `${caveCodeLiveOrigin}${path}`;
}

function readLocalProgress() {
    const raw = window.localStorage.getItem(caveCodeProgressKey);

    if (!raw) {
        return null;
    }

    try {
        return JSON.parse(raw);
    } catch {
        return null;
    }
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

    saveCourseProgress: function (progress) {
        const snapshot = {
            ...progress,
            updatedAt: new Date().toISOString()
        };

        window.localStorage.setItem(
            caveCodeProgressKey,
            JSON.stringify(snapshot)
        );
    },

    loadCourseProgress: function () {
        return readLocalProgress();
    },

    syncLocalProgressToCloud: async function () {
        // This intentionally preserves the local checkpoint now.
        // The Supabase course_progress table will be connected in the
        // dedicated cloud-progress database pass.
        return readLocalProgress();
    }
};
