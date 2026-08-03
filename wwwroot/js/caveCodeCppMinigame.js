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
            recordMissionTwoClear,
            recordClear
        });
})();
