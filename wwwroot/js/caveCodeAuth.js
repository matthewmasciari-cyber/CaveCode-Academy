let supabaseClient = null;

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

    signInWithGitHub: async function () {
        if (!supabaseClient) {
            throw new Error("Supabase has not been initialized.");
        }

        const { error } = await supabaseClient.auth.signInWithOAuth({
            provider: "github",
            options: {
                redirectTo:
                    "https://matthewmasciari-cyber.github.io/CaveCode-Academy/"
            }
        });

        if (error) {
            throw error;
        }
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

    getCurrentUser: async function () {
        if (!supabaseClient) {
            return null;
        }

        const {
            data: { user },
            error
        } = await supabaseClient.auth.getUser();

        if (error || !user) {
            return null;
        }

        return {
            id: user.id,
            email: user.email ?? "",
            userName:
                user.user_metadata?.user_name ??
                user.user_metadata?.preferred_username ??
                "",
            avatarUrl:
                user.user_metadata?.avatar_url ??
                ""
        };
    }
};