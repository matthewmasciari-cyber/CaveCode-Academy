(function () {
    "use strict";

    const storageKey = "cavecode.cpp.minigame.v1";
    const maxAttemptHistory = 80;

    function defaultState() {
        return {
            version: 1,
            mission1Completed: false,
            bestScore: 0,
            fastestSeconds: null,
            totalClears: 0,
            totalXpEarned: 0,
            totalCrystalsEarned: 0,
            lastScore: 0,
            lastElapsedSeconds: 0,
            lastProfile: "",
            mission2: {
                completed: false,
                bestScore: 0,
                fastestSeconds: null,
                totalClears: 0,
                totalXpEarned: 0,
                totalCrystalsEarned: 0,
                lastScore: 0,
                lastElapsedSeconds: 0,
                lastProfile: ""
            },
            processedMissionTwoAttempts: {},
            missionTwoAttemptOrder: [],
            processedAttempts: {},
            attemptOrder: []
        };
    }

    function load() {
        try {
            const parsed = JSON.parse(
                window.localStorage.getItem(storageKey) || "null"
            );

            return parsed && typeof parsed === "object"
                ? {
                    ...defaultState(),
                    ...parsed,
                    processedAttempts:
                        parsed.processedAttempts || {},
                    attemptOrder:
                        Array.isArray(parsed.attemptOrder)
                            ? parsed.attemptOrder
                            : []
                }
                : defaultState();
        } catch {
            return defaultState();
        }
    }

    function publicState(state) {
        return {
            mission1Completed:
                Boolean(state.mission1Completed),
            bestScore:
                Number(state.bestScore || 0),
            fastestSeconds:
                state.fastestSeconds === null
                    ? null
                    : Number(state.fastestSeconds),
            totalClears:
                Number(state.totalClears || 0),
            totalXpEarned:
                Number(state.totalXpEarned || 0),
            totalCrystalsEarned:
                Number(state.totalCrystalsEarned || 0),
            lastScore:
                Number(state.lastScore || 0),
            lastElapsedSeconds:
                Number(state.lastElapsedSeconds || 0),
            lastProfile:
                String(state.lastProfile || ""),
            mission2:
                publicMissionTwoState(state)
        };
    }

    function save(state) {
        window.localStorage.setItem(
            storageKey,
            JSON.stringify(state)
        );

        window.dispatchEvent(
            new CustomEvent(
                "cavecode-cpp-minigame-changed",
                { detail: publicState(state) }
            )
        );
    }

    function recordClear(attemptId, score, elapsedSeconds, profile) {
        const state = load();
        const id = String(attemptId || "").trim();

        if (!id) {
            throw new Error(
                "A mission attempt identifier is required."
            );
        }

        const previous =
            state.processedAttempts[id];

        if (previous) {
            return {
                ...previous,
                duplicateAttempt: true,
                state: publicState(state)
            };
        }

        const normalizedScore =
            Math.max(
                0,
                Math.min(
                    1000,
                    Math.floor(Number(score) || 0)
                )
            );

        const normalizedSeconds =
            Math.max(
                0,
                Number(elapsedSeconds) || 0
            );

        const firstClear =
            state.totalClears === 0;

        const xpAwarded =
            firstClear ? 100 : 40;

        const crystalsAwarded =
            firstClear ? 6 : 2;

        const newBestScore =
            normalizedScore > state.bestScore;

        const newFastestTime =
            state.fastestSeconds === null ||
            normalizedSeconds < state.fastestSeconds;

        state.mission1Completed = true;
        state.totalClears += 1;
        state.totalXpEarned += xpAwarded;
        state.totalCrystalsEarned +=
            crystalsAwarded;
        state.lastScore = normalizedScore;
        state.lastElapsedSeconds =
            normalizedSeconds;
        state.lastProfile =
            String(profile || "");

        if (newBestScore) {
            state.bestScore =
                normalizedScore;
        }

        if (newFastestTime) {
            state.fastestSeconds =
                normalizedSeconds;
        }

        const result = {
            rewardKey:
                `cpp-engine-foundry-m1-clear-${state.totalClears}`,
            xpAwarded,
            crystalsAwarded,
            firstClear,
            newBestScore,
            newFastestTime
        };

        state.processedAttempts[id] = result;
        state.attemptOrder.push(id);

        while (
            state.attemptOrder.length >
            maxAttemptHistory
        ) {
            const removed =
                state.attemptOrder.shift();

            if (removed) {
                delete state
                    .processedAttempts[removed];
            }
        }

        save(state);

        return {
            ...result,
            duplicateAttempt: false,
            state: publicState(state)
        };
    }


    function publicMissionTwoState(state) {
        const mission =
            state.mission2 || {};

        return {
            completed:
                Boolean(mission.completed),
            bestScore:
                Number(mission.bestScore || 0),
            fastestSeconds:
                mission.fastestSeconds === null ||
                mission.fastestSeconds === undefined
                    ? null
                    : Number(mission.fastestSeconds),
            totalClears:
                Number(mission.totalClears || 0),
            totalXpEarned:
                Number(mission.totalXpEarned || 0),
            totalCrystalsEarned:
                Number(mission.totalCrystalsEarned || 0)
        };
    }

    function recordMissionTwoClear(
        attemptId,
        score,
        elapsedSeconds,
        profile
    ) {
        const state = load();
        const id =
            String(attemptId || "").trim();

        if (!id) {
            throw new Error(
                "A Mission 2 attempt identifier is required."
            );
        }

        state.mission2 =
            state.mission2 || {
                completed: false,
                bestScore: 0,
                fastestSeconds: null,
                totalClears: 0,
                totalXpEarned: 0,
                totalCrystalsEarned: 0
            };

        state.processedMissionTwoAttempts =
            state.processedMissionTwoAttempts || {};

        state.missionTwoAttemptOrder =
            Array.isArray(state.missionTwoAttemptOrder)
                ? state.missionTwoAttemptOrder
                : [];

        const previous =
            state.processedMissionTwoAttempts[id];

        if (previous) {
            return {
                ...previous,
                duplicateAttempt: true,
                state:
                    publicMissionTwoState(state)
            };
        }

        const normalizedScore =
            Math.max(
                0,
                Math.min(
                    1000,
                    Math.floor(Number(score) || 0)
                )
            );

        const normalizedSeconds =
            Math.max(
                0,
                Number(elapsedSeconds) || 0
            );

        const firstClear =
            state.mission2.totalClears === 0;

        const xpAwarded =
            firstClear ? 100 : 40;

        const crystalsAwarded =
            firstClear ? 6 : 2;

        state.mission2.completed = true;
        state.mission2.totalClears += 1;
        state.mission2.totalXpEarned +=
            xpAwarded;
        state.mission2.totalCrystalsEarned +=
            crystalsAwarded;
        state.mission2.lastScore =
            normalizedScore;
        state.mission2.lastElapsedSeconds =
            normalizedSeconds;
        state.mission2.lastProfile =
            String(profile || "");

        if (
            normalizedScore >
            state.mission2.bestScore
        ) {
            state.mission2.bestScore =
                normalizedScore;
        }

        if (
            state.mission2.fastestSeconds === null ||
            normalizedSeconds <
                state.mission2.fastestSeconds
        ) {
            state.mission2.fastestSeconds =
                normalizedSeconds;
        }

        const result = {
            rewardKey:
                `cpp-engine-foundry-m2-clear-${state.mission2.totalClears}`,
            xpAwarded,
            crystalsAwarded,
            firstClear
        };

        state.processedMissionTwoAttempts[id] =
            result;

        state.missionTwoAttemptOrder.push(id);

        while (
            state.missionTwoAttemptOrder.length >
            maxAttemptHistory
        ) {
            const removed =
                state.missionTwoAttemptOrder.shift();

            if (removed) {
                delete state
                    .processedMissionTwoAttempts[removed];
            }
        }

        save(state);

        return {
            ...result,
            duplicateAttempt: false,
            state:
                publicMissionTwoState(state)
        };
    }


    function publicMissionThreeState(state) {
        const m = state.mission3 || {};
        return {
            completed: Boolean(m.completed),
            bestScore: Number(m.bestScore || 0),
            fastestSeconds: m.fastestSeconds == null ? null : Number(m.fastestSeconds),
            totalClears: Number(m.totalClears || 0),
            totalXpEarned: Number(m.totalXpEarned || 0),
            totalCrystalsEarned: Number(m.totalCrystalsEarned || 0)
        };
    }

    function recordMissionThreeClear(attemptId, score, elapsedSeconds, profile) {
        const state = load();
        state.mission3 = state.mission3 || {
            completed: false, bestScore: 0, fastestSeconds: null,
            totalClears: 0, totalXpEarned: 0, totalCrystalsEarned: 0
        };
        state.processedMissionThreeAttempts = state.processedMissionThreeAttempts || {};
        const id = String(attemptId || "").trim();
        if (!id) throw new Error("Mission 3 attempt ID required.");
        if (state.processedMissionThreeAttempts[id]) {
            return { ...state.processedMissionThreeAttempts[id], state: publicMissionThreeState(state) };
        }

        const firstClear = state.mission3.totalClears === 0;
        const xpAwarded = firstClear ? 100 : 40;
        const crystalsAwarded = firstClear ? 6 : 2;
        const normalizedScore = Math.max(0, Math.min(1000, Math.floor(Number(score) || 0)));
        const seconds = Math.max(0, Number(elapsedSeconds) || 0);

        state.mission3.completed = true;
        state.mission3.totalClears += 1;
        state.mission3.totalXpEarned += xpAwarded;
        state.mission3.totalCrystalsEarned += crystalsAwarded;
        state.mission3.bestScore = Math.max(state.mission3.bestScore, normalizedScore);
        state.mission3.fastestSeconds =
            state.mission3.fastestSeconds == null ? seconds : Math.min(state.mission3.fastestSeconds, seconds);

        const result = {
            rewardKey: `cpp-engine-foundry-m3-clear-${state.mission3.totalClears}`,
            xpAwarded, crystalsAwarded
        };
        state.processedMissionThreeAttempts[id] = result;
        save(state);
        return { ...result, state: publicMissionThreeState(state) };
    }


    function publicMissionFourState(state) {
        const mission = state.mission4 || {};
        return {
            completed: Boolean(mission.completed),
            bestScore: Number(mission.bestScore || 0),
            fastestSeconds:
                mission.fastestSeconds == null
                    ? null
                    : Number(mission.fastestSeconds),
            totalClears: Number(mission.totalClears || 0),
            totalXpEarned: Number(mission.totalXpEarned || 0),
            totalCrystalsEarned:
                Number(mission.totalCrystalsEarned || 0)
        };
    }

    function recordMissionFourClear(
        attemptId,
        score,
        elapsedSeconds,
        profile
    ) {
        const state = load();
        const id = String(attemptId || "").trim();

        if (!id) {
            throw new Error("Mission 4 attempt ID required.");
        }

        state.mission4 = state.mission4 || {
            completed: false,
            bestScore: 0,
            fastestSeconds: null,
            totalClears: 0,
            totalXpEarned: 0,
            totalCrystalsEarned: 0,
            lastScore: 0,
            lastElapsedSeconds: 0,
            lastProfile: ""
        };

        state.processedMissionFourAttempts =
            state.processedMissionFourAttempts || {};

        const previous =
            state.processedMissionFourAttempts[id];

        if (previous) {
            return {
                ...previous,
                duplicateAttempt: true,
                state: publicMissionFourState(state)
            };
        }

        const normalizedScore =
            Math.max(
                0,
                Math.min(
                    1000,
                    Math.floor(Number(score) || 0)
                )
            );

        const seconds =
            Math.max(0, Number(elapsedSeconds) || 0);

        const firstClear =
            state.mission4.totalClears === 0;

        const xpAwarded = firstClear ? 100 : 40;
        const crystalsAwarded = firstClear ? 6 : 2;

        state.mission4.completed = true;
        state.mission4.totalClears += 1;
        state.mission4.totalXpEarned += xpAwarded;
        state.mission4.totalCrystalsEarned +=
            crystalsAwarded;
        state.mission4.lastScore = normalizedScore;
        state.mission4.lastElapsedSeconds = seconds;
        state.mission4.lastProfile =
            String(profile || "");

        state.mission4.bestScore =
            Math.max(
                state.mission4.bestScore,
                normalizedScore
            );

        state.mission4.fastestSeconds =
            state.mission4.fastestSeconds == null
                ? seconds
                : Math.min(
                    state.mission4.fastestSeconds,
                    seconds
                );

        const result = {
            rewardKey:
                `cpp-engine-foundry-m4-clear-${state.mission4.totalClears}`,
            xpAwarded,
            crystalsAwarded
        };

        state.processedMissionFourAttempts[id] =
            result;

        save(state);

        return {
            ...result,
            duplicateAttempt: false,
            state: publicMissionFourState(state)
        };
    }


    function publicMissionFiveState(state) {
        const mission = state.mission5 || {};
        return {
            completed: Boolean(mission.completed),
            bestScore: Number(mission.bestScore || 0),
            fastestSeconds:
                mission.fastestSeconds == null
                    ? null
                    : Number(mission.fastestSeconds),
            totalClears: Number(mission.totalClears || 0),
            totalXpEarned: Number(mission.totalXpEarned || 0),
            totalCrystalsEarned:
                Number(mission.totalCrystalsEarned || 0)
        };
    }

    function publicChapterOneCampaignState(state) {
        const campaign = state.chapterOneCampaign || {};
        return {
            completed: Boolean(campaign.completed),
            bonusAwarded: Boolean(campaign.bonusAwarded),
            completedAt: String(campaign.completedAt || "")
        };
    }

    function recordMissionFiveClear(
        attemptId,
        score,
        elapsedSeconds,
        profile
    ) {
        const state = load();
        const id = String(attemptId || "").trim();

        if (!id) {
            throw new Error("Mission 5 attempt ID required.");
        }

        state.mission5 = state.mission5 || {
            completed: false,
            bestScore: 0,
            fastestSeconds: null,
            totalClears: 0,
            totalXpEarned: 0,
            totalCrystalsEarned: 0
        };

        state.chapterOneCampaign =
            state.chapterOneCampaign || {
                completed: false,
                bonusAwarded: false,
                completedAt: ""
            };

        state.processedMissionFiveAttempts =
            state.processedMissionFiveAttempts || {};

        const previous =
            state.processedMissionFiveAttempts[id];

        if (previous) {
            return {
                ...previous,
                duplicateAttempt: true,
                state: publicMissionFiveState(state),
                campaignState:
                    publicChapterOneCampaignState(state)
            };
        }

        const normalizedScore =
            Math.max(
                0,
                Math.min(
                    1000,
                    Math.floor(Number(score) || 0)
                )
            );

        const seconds =
            Math.max(0, Number(elapsedSeconds) || 0);

        const firstClear =
            state.mission5.totalClears === 0;

        const xpAwarded = firstClear ? 100 : 40;
        const crystalsAwarded = firstClear ? 6 : 2;

        state.mission5.completed = true;
        state.mission5.totalClears += 1;
        state.mission5.totalXpEarned += xpAwarded;
        state.mission5.totalCrystalsEarned +=
            crystalsAwarded;
        state.mission5.bestScore =
            Math.max(
                state.mission5.bestScore,
                normalizedScore
            );
        state.mission5.fastestSeconds =
            state.mission5.fastestSeconds == null
                ? seconds
                : Math.min(
                    state.mission5.fastestSeconds,
                    seconds
                );

        const allMissionsComplete =
            Boolean(state.mission1Completed) &&
            Boolean(state.mission2?.completed) &&
            Boolean(state.mission3?.completed) &&
            Boolean(state.mission4?.completed) &&
            Boolean(state.mission5?.completed);

        const campaignBonusAwarded =
            allMissionsComplete &&
            !state.chapterOneCampaign.bonusAwarded;

        if (allMissionsComplete) {
            state.chapterOneCampaign.completed = true;

            if (!state.chapterOneCampaign.completedAt) {
                state.chapterOneCampaign.completedAt =
                    new Date().toISOString();
            }
        }

        if (campaignBonusAwarded) {
            state.chapterOneCampaign.bonusAwarded = true;
        }

        const result = {
            rewardKey:
                `cpp-engine-foundry-m5-clear-${state.mission5.totalClears}`,
            xpAwarded,
            crystalsAwarded,
            campaignBonusAwarded,
            campaignRewardKey:
                "cpp-engine-foundry-chapter-1-campaign-bonus",
            campaignXpAwarded:
                campaignBonusAwarded ? 200 : 0,
            campaignCrystalsAwarded:
                campaignBonusAwarded ? 15 : 0
        };

        state.processedMissionFiveAttempts[id] =
            result;

        save(state);

        return {
            ...result,
            duplicateAttempt: false,
            state: publicMissionFiveState(state),
            campaignState:
                publicChapterOneCampaignState(state)
        };
    }


    function publicHubState(state) {
        return {
            mission1Completed:
                Boolean(state.mission1Completed),
            mission2Completed:
                Boolean(state.mission2?.completed),
            mission3Completed:
                Boolean(state.mission3?.completed),
            mission4Completed:
                Boolean(state.mission4?.completed),
            mission5Completed:
                Boolean(state.mission5?.completed),
            campaignCompleted:
                Boolean(state.chapterOneCampaign?.completed)
        };
    }

    window.caveCodeCppMinigame =
        Object.freeze({
            version: "cpp-minigame-pass-7d-v1",
            getState: () =>
                publicState(load()),
            getHubState: () =>
                publicState(load()),
            isMissionOneComplete: () =>
                Boolean(load().mission1Completed),
            getMissionTwoState: () =>
                publicMissionTwoState(load()),
            getMissionThreeState: () =>
                publicMissionThreeState(load()),
            getMissionFourState: () =>
                publicMissionFourState(load()),
            getMissionFiveState: () =>
                publicMissionFiveState(load()),
            getChapterOneCampaignState: () =>
                publicChapterOneCampaignState(load()),
            getHubState: () =>
                publicHubState(load()),
            recordMissionFiveClear,
            recordMissionFourClear,
            recordMissionThreeClear,
            recordMissionTwoClear,
            recordClear
        });
})();
