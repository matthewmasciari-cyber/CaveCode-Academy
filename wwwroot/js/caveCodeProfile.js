(function () {
    const storageKey = "cavecode.profile.v1";

    const defaults = {
        displayName: "",
        title: "Cave Explorer",
        emblem: "crystal",
        featuredAchievement: "Control Terminal Online"
    };

    let current = load();

    function load() {
        try {
            const saved = JSON.parse(
                localStorage.getItem(storageKey) || "{}"
            );

            return {
                ...defaults,
                ...saved
            };
        } catch {
            return {
                ...defaults
            };
        }
    }

    function normalize(name, value) {
        if (name === "displayName") {
            return String(value ?? "")
                .replace(/\s+/g, " ")
                .trimStart()
                .slice(0, 24);
        }

        return String(value ?? "").slice(0, 80);
    }

    function save() {
        localStorage.setItem(
            storageKey,
            JSON.stringify(current)
        );

        window.dispatchEvent(
            new CustomEvent(
                "cavecode-profile-changed",
                {
                    detail: {
                        ...current
                    }
                }
            )
        );

        return {
            ...current
        };
    }

    function sanitizeProgressionLocks(preferences) {
        if (!window.caveCodeAchievements) {
            return preferences;
        }

        const titleOptions =
            window.caveCodeAchievements.getTitleOptions();
        const featureOptions =
            window.caveCodeAchievements.getFeatureOptions();

        const titleAllowed = titleOptions.some(
            option =>
                option.title === preferences.title &&
                option.unlocked
        );

        const featureAllowed = featureOptions.some(
            option =>
                option.name === preferences.featuredAchievement &&
                option.unlocked
        );

        return {
            ...preferences,
            title: titleAllowed
                ? preferences.title
                : "Cave Explorer",
            featuredAchievement: featureAllowed
                ? preferences.featuredAchievement
                : "First Steps"
        };
    }

    window.caveCodeProfile = {
        getPreferences: function () {
            current = sanitizeProgressionLocks(load());
            localStorage.setItem(
                storageKey,
                JSON.stringify(current)
            );

            return {
                ...current
            };
        },

        setPreference: function (name, value) {
            if (!(name in defaults)) {
                throw new Error(
                    "Unknown profile preference: " + name
                );
            }

            const nextValue = normalize(name, value);

            if (
                name === "title" &&
                window.caveCodeAchievements
            ) {
                const allowed =
                    window.caveCodeAchievements
                        .getTitleOptions()
                        .some(
                            option =>
                                option.title === nextValue &&
                                option.unlocked
                        );

                if (!allowed) {
                    throw new Error(
                        "Claim the matching achievement first."
                    );
                }
            }

            if (
                name === "featuredAchievement" &&
                window.caveCodeAchievements
            ) {
                const allowed =
                    window.caveCodeAchievements
                        .getFeatureOptions()
                        .some(
                            option =>
                                option.name === nextValue &&
                                option.unlocked
                        );

                if (!allowed) {
                    throw new Error(
                        "Claim the matching achievement first."
                    );
                }
            }

            current = {
                ...current,
                [name]: nextValue
            };

            return save();
        },

        reset: function () {
            current = {
                ...defaults
            };

            localStorage.removeItem(storageKey);

            return save();
        }
    };
})();
