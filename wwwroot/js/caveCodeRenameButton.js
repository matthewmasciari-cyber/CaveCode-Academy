(function () {
    const profileStorageKey = "cavecode.profile.v1";
    const renameCost = 500;
    const cooldownDays = 5;
    const cooldownMs =
        cooldownDays * 24 * 60 * 60 * 1000;

    function normalizeName(value) {
        return String(value ?? "")
            .replace(/\s+/g, " ")
            .trim()
            .slice(0, 24);
    }

    function loadProfile() {
        try {
            const parsed = JSON.parse(
                localStorage.getItem(
                    profileStorageKey
                ) || "{}"
            );

            return {
                ...parsed,
                displayName:
                    normalizeName(parsed.displayName),
                title:
                    String(parsed.title || "Cave Explorer"),
                emblem:
                    String(parsed.emblem || "crystal"),
                featuredAchievement:
                    String(
                        parsed.featuredAchievement ||
                        "Control Terminal Online"
                    ),
                previousDisplayName:
                    normalizeName(
                        parsed.previousDisplayName
                    ),
                displayNameChangeCount:
                    Math.max(
                        0,
                        Math.floor(
                            Number(
                                parsed.displayNameChangeCount
                            ) || 0
                        )
                    ),
                lastDisplayNameChangedAt:
                    parsed.lastDisplayNameChangedAt || null,
                nextDisplayNameChangeAt:
                    parsed.nextDisplayNameChangeAt || null
            };
        } catch {
            return {
                displayName: "",
                title: "Cave Explorer",
                emblem: "crystal",
                featuredAchievement:
                    "Control Terminal Online",
                previousDisplayName: "",
                displayNameChangeCount: 0,
                lastDisplayNameChangedAt: null,
                nextDisplayNameChangeAt: null
            };
        }
    }

    function getCrystalBalance() {
        if (
            !window.caveCodeAchievements ||
            typeof window.caveCodeAchievements
                .getState !== "function"
        ) {
            return 0;
        }

        const state =
            window.caveCodeAchievements.getState();

        return Math.max(
            0,
            Math.floor(
                Number(state.crystals) || 0
            )
        );
    }

    function statusFor(profile) {
        const firstFree =
            Number(
                profile.displayNameChangeCount
            ) === 0;

        const cost =
            firstFree ? 0 : renameCost;

        const parsedNext =
            profile.nextDisplayNameChangeAt
                ? new Date(
                    profile.nextDisplayNameChangeAt
                )
                : null;

        const nextTimestamp =
            parsedNext &&
            !Number.isNaN(parsedNext.getTime())
                ? parsedNext.getTime()
                : 0;

        const canChangeNow =
            nextTimestamp <= Date.now();

        const balance =
            getCrystalBalance();

        return {
            currentDisplayName:
                normalizeName(profile.displayName),
            previousDisplayName:
                normalizeName(
                    profile.previousDisplayName
                ),
            isFirstChangeFree:
                firstFree,
            canChangeNow,
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
                canChangeNow || !nextTimestamp
                    ? null
                    : new Date(
                        nextTimestamp
                    ).toISOString(),
            changeCount:
                Number(
                    profile.displayNameChangeCount
                ) || 0
        };
    }

    function saveProfile(profile) {
        localStorage.setItem(
            profileStorageKey,
            JSON.stringify(profile)
        );

        window.dispatchEvent(
            new CustomEvent(
                "cavecode-profile-changed",
                {
                    detail: {
                        ...profile
                    }
                }
            )
        );
    }

    window.caveCodeRenameButton = {
        getStatus: function () {
            return statusFor(
                loadProfile()
            );
        },

        confirmRename: function (
            requestedName
        ) {
            const profile =
                loadProfile();

            const statusBefore =
                statusFor(profile);

            const nextName =
                normalizeName(requestedName);

            if (
                nextName ===
                normalizeName(profile.displayName)
            ) {
                return {
                    success: false,
                    message:
                        "Enter a different display name.",
                    status: statusBefore
                };
            }

            if (!statusBefore.canChangeNow) {
                return {
                    success: false,
                    message:
                        "Your display name is still in its five-day cooldown.",
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
                        `This change costs ${cost} ◆, ` +
                        `but your balance is ` +
                        `${statusBefore.crystalBalance} ◆.`,
                    status: statusBefore
                };
            }

            if (cost > 0) {
                if (
                    !window.caveCodeAchievements ||
                    typeof window.caveCodeAchievements
                        .spendCrystals !== "function"
                ) {
                    return {
                        success: false,
                        message:
                            "The Code Crystal wallet is unavailable. No crystals were charged.",
                        status: statusBefore
                    };
                }

                const spending =
                    window.caveCodeAchievements
                        .spendCrystals(
                            cost,
                            "Display name change"
                        );

                if (!spending.success) {
                    return {
                        success: false,
                        message:
                            spending.message ||
                            "The Code Crystal charge could not be completed.",
                        status:
                            statusFor(profile)
                    };
                }
            }

            const now = new Date();
            const nextAvailable =
                new Date(
                    now.getTime() +
                    cooldownMs
                );

            const updated = {
                ...profile,
                previousDisplayName:
                    normalizeName(profile.displayName),
                displayName:
                    nextName,
                displayNameChangeCount:
                    Number(
                        profile.displayNameChangeCount
                    ) + 1,
                lastDisplayNameChangedAt:
                    now.toISOString(),
                nextDisplayNameChangeAt:
                    nextAvailable.toISOString()
            };

            saveProfile(updated);

            return {
                success: true,
                message:
                    cost === 0
                        ? "Your free name change was used. Future changes cost 500 ◆."
                        : "Display name changed for 500 ◆.",
                status:
                    statusFor(updated)
            };
        },

        reloadPage: function () {
            window.location.reload();
        }
    };
})();
