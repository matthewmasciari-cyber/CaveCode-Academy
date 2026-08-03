(function () {
    const storageKey = "cavecode.profile.v1";
    const renameCost = 500;
    const cooldownDays = 5;
    const cooldownMilliseconds =
        cooldownDays * 24 * 60 * 60 * 1000;

    const defaults = {
        displayName: "",
        title: "Cave Explorer",
        emblem: "crystal",
        featuredAchievement: "Control Terminal Online",
        previousDisplayName: "",
        displayNameChangeCount: 0,
        lastDisplayNameChangedAt: null,
        nextDisplayNameChangeAt: null
    };

    let current = load();

    function load() {
        try {
            const saved = JSON.parse(
                localStorage.getItem(storageKey) || "{}"
            );

            return normalizeStoredState({
                ...defaults,
                ...saved
            });
        } catch {
            return {
                ...defaults
            };
        }
    }

    function normalizeStoredState(value) {
        return {
            ...defaults,
            ...value,
            displayName:
                normalizeDisplayName(value.displayName),
            previousDisplayName:
                normalizeDisplayName(
                    value.previousDisplayName
                ),
            displayNameChangeCount:
                Number.isFinite(
                    Number(value.displayNameChangeCount)
                )
                    ? Math.max(
                        0,
                        Math.floor(
                            Number(
                                value.displayNameChangeCount
                            )
                        )
                    )
                    : 0,
            lastDisplayNameChangedAt:
                validDateOrNull(
                    value.lastDisplayNameChangedAt
                ),
            nextDisplayNameChangeAt:
                validDateOrNull(
                    value.nextDisplayNameChangeAt
                )
        };
    }

    function validDateOrNull(value) {
        if (!value) {
            return null;
        }

        const date = new Date(value);

        return Number.isNaN(date.getTime())
            ? null
            : date.toISOString();
    }

    function normalizeDisplayName(value) {
        return String(value ?? "")
            .replace(/\s+/g, " ")
            .trim()
            .slice(0, 24);
    }

    function normalizePreference(name, value) {
        if (name === "displayName") {
            return normalizeDisplayName(value);
        }

        return String(value ?? "")
            .slice(0, 80);
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

    function crystalBalance() {
        if (
            !window.caveCodeAchievements ||
            typeof window.caveCodeAchievements
                .getState !== "function"
        ) {
            return 0;
        }

        return Math.max(
            0,
            Math.floor(
                Number(
                    window.caveCodeAchievements
                        .getState()
                        .crystals
                ) || 0
            )
        );
    }

    function renameStatus() {
        current = load();

        const now = Date.now();
        const nextTimestamp =
            current.nextDisplayNameChangeAt
                ? new Date(
                    current.nextDisplayNameChangeAt
                ).getTime()
                : 0;
        const remainingMilliseconds =
            Math.max(
                0,
                nextTimestamp - now
            );
        const firstFree =
            current.displayNameChangeCount === 0;
        const cost =
            firstFree ? 0 : renameCost;
        const balance =
            crystalBalance();

        return {
            currentDisplayName:
                current.displayName,
            previousDisplayName:
                current.previousDisplayName,
            isFirstChangeFree:
                firstFree,
            canChangeNow:
                remainingMilliseconds === 0,
            currentCost:
                cost,
            futureCost:
                renameCost,
            cooldownDays,
            crystalBalance:
                balance,
            canAfford:
                cost === 0 ||
                balance >= cost,
            nextAvailableAt:
                remainingMilliseconds === 0
                    ? null
                    : current.nextDisplayNameChangeAt,
            remainingSeconds:
                Math.ceil(
                    remainingMilliseconds / 1000
                ),
            changeCount:
                current.displayNameChangeCount
        };
    }

    function sanitizeProgressionLocks(preferences) {
        if (!window.caveCodeAchievements) {
            return preferences;
        }

        const titleOptions =
            window.caveCodeAchievements
                .getTitleOptions();
        const featureOptions =
            window.caveCodeAchievements
                .getFeatureOptions();

        const titleAllowed = titleOptions.some(
            option =>
                option.title === preferences.title &&
                option.unlocked
        );

        const featureAllowed = featureOptions.some(
            option =>
                option.name ===
                    preferences.featuredAchievement &&
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
            current =
                sanitizeProgressionLocks(
                    load()
                );

            localStorage.setItem(
                storageKey,
                JSON.stringify(current)
            );

            return {
                ...current
            };
        },

        getRenameStatus: function () {
            return renameStatus();
        },

        renameDisplayName: function (
            requestedName
        ) {
            current = load();

            const nextName =
                normalizeDisplayName(
                    requestedName
                );
            const statusBefore =
                renameStatus();

            if (
                nextName === current.displayName
            ) {
                return {
                    success: false,
                    message:
                        "Enter a different display name.",
                    costCharged: 0,
                    usedFreeChange: false,
                    preferences: {
                        ...current
                    },
                    status: statusBefore
                };
            }

            if (!statusBefore.canChangeNow) {
                return {
                    success: false,
                    message:
                        "Your display name is still in its five-day cooldown.",
                    costCharged: 0,
                    usedFreeChange: false,
                    preferences: {
                        ...current
                    },
                    status: statusBefore
                };
            }

            const cost =
                statusBefore.currentCost;

            if (
                cost > 0 &&
                !statusBefore.canAfford
            ) {
                return {
                    success: false,
                    message:
                        `This name change costs ${cost} Code Crystals. ` +
                        `Your current balance is ${statusBefore.crystalBalance}.`,
                    costCharged: 0,
                    usedFreeChange: false,
                    preferences: {
                        ...current
                    },
                    status: statusBefore
                };
            }

            if (cost > 0) {
                if (
                    !window.caveCodeAchievements ||
                    typeof window
                        .caveCodeAchievements
                        .spendCrystals !== "function"
                ) {
                    return {
                        success: false,
                        message:
                            "The Code Crystal wallet is unavailable. No crystals were charged.",
                        costCharged: 0,
                        usedFreeChange: false,
                        preferences: {
                            ...current
                        },
                        status: statusBefore
                    };
                }

                const spend =
                    window.caveCodeAchievements
                        .spendCrystals(
                            cost,
                            "Display name change"
                        );

                if (!spend.success) {
                    return {
                        success: false,
                        message:
                            spend.message ||
                            "The Code Crystal charge could not be completed.",
                        costCharged: 0,
                        usedFreeChange: false,
                        preferences: {
                            ...current
                        },
                        status:
                            renameStatus()
                    };
                }
            }

            const changedAt =
                new Date();
            const nextAvailable =
                new Date(
                    changedAt.getTime() +
                    cooldownMilliseconds
                );

            current = {
                ...current,
                previousDisplayName:
                    current.displayName,
                displayName:
                    nextName,
                displayNameChangeCount:
                    current.displayNameChangeCount + 1,
                lastDisplayNameChangedAt:
                    changedAt.toISOString(),
                nextDisplayNameChangeAt:
                    nextAvailable.toISOString()
            };

            const preferences = save();
            const statusAfter =
                renameStatus();

            return {
                success: true,
                message:
                    cost === 0
                        ? `Your free display-name change was used. ` +
                          `The next change costs ${renameCost} Code Crystals ` +
                          `and becomes available in ${cooldownDays} days.`
                        : `Display name changed for ${renameCost} Code Crystals. ` +
                          `Another change becomes available in ${cooldownDays} days.`,
                costCharged:
                    cost,
                usedFreeChange:
                    cost === 0,
                preferences,
                status:
                    statusAfter
            };
        },

        setPreference: function (
            name,
            value
        ) {
            if (!(name in defaults)) {
                throw new Error(
                    "Unknown profile preference: " +
                    name
                );
            }

            if (name === "displayName") {
                throw new Error(
                    "Use the confirmed display-name change flow."
                );
            }

            const nextValue =
                normalizePreference(
                    name,
                    value
                );

            if (
                name === "title" &&
                window.caveCodeAchievements
            ) {
                const allowed =
                    window
                        .caveCodeAchievements
                        .getTitleOptions()
                        .some(
                            option =>
                                option.title ===
                                    nextValue &&
                                option.unlocked
                        );

                if (!allowed) {
                    throw new Error(
                        "Claim the matching achievement first."
                    );
                }
            }

            if (
                name ===
                    "featuredAchievement" &&
                window.caveCodeAchievements
            ) {
                const allowed =
                    window
                        .caveCodeAchievements
                        .getFeatureOptions()
                        .some(
                            option =>
                                option.name ===
                                    nextValue &&
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
                [name]:
                    nextValue
            };

            return save();
        },

        reset: function () {
            current = load();

            // Reset vanity selections without bypassing the display-name
            // cost, cooldown, previous-name, or rename-count rules.
            current = {
                ...current,
                title:
                    defaults.title,
                emblem:
                    defaults.emblem,
                featuredAchievement:
                    defaults.featuredAchievement
            };

            return save();
        }
    };
})();
