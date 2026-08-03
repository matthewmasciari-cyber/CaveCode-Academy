(() => {
    "use strict";

    const storageKey = "cavecode.htmlcss.minigame.v1";
    const maxAttemptHistory = 160;

    const defaults = Object.freeze({
        mission1Completed: false,
        bestScore: 0,
        fastestSeconds: null,
        totalClears: 0,
        totalXpEarned: 0,
        totalCrystalsEarned: 0,
        lastScore: 0,
        lastElapsedSeconds: null,
        lastProfile: "",
        mission2Completed: false,
        mission2BestScore: 0,
        mission2FastestSeconds: null,
        mission2Clears: 0,
        mission2LastScore: 0,
        mission2LastElapsedSeconds: null,
        mission2LastProfile: "",
        mission3Completed: false,
        mission3BestScore: 0,
        mission3FastestSeconds: null,
        mission3Clears: 0,
        mission3LastScore: 0,
        mission3LastElapsedSeconds: null,
        mission3LastProfile: "",
        mission4Completed: false,
        mission4BestScore: 0,
        mission4FastestSeconds: null,
        mission4Clears: 0,
        mission4LastScore: 0,
        mission4LastElapsedSeconds: null,
        mission4LastProfile: "",
        mission5Completed: false,
        mission5BestScore: 0,
        mission5FastestSeconds: null,
        mission5Clears: 0,
        mission5LastScore: 0,
        mission5LastElapsedSeconds: null,
        mission5LastProfile: "",
        campaignRewardClaimed: false,
        campaignCompletedAt: "",
        campaignXpAwarded: 0,
        campaignCrystalsAwarded: 0,
        endlessRuns: 0,
        endlessLastMission: 0,
        endlessLastProfile: "",
        endlessLastLaunchedAt: "",
        processedAttempts: {},
        attemptOrder: []
    });

    function finiteInteger(value, fallback = 0) {
        const number = Number(value);
        return Number.isFinite(number)
            ? Math.max(0, Math.floor(number))
            : fallback;
    }

    function finiteSeconds(value) {
        const number = Number(value);
        return Number.isFinite(number)
            ? Math.max(0, number)
            : null;
    }

    function normalize(raw) {
        const source = raw && typeof raw === "object" ? raw : {};

        return {
            mission1Completed: source.mission1Completed === true,
            bestScore: Math.min(1000, finiteInteger(source.bestScore)),
            fastestSeconds: finiteSeconds(source.fastestSeconds),
            totalClears: finiteInteger(source.totalClears),
            totalXpEarned: finiteInteger(source.totalXpEarned),
            totalCrystalsEarned: finiteInteger(source.totalCrystalsEarned),
            lastScore: Math.min(1000, finiteInteger(source.lastScore)),
            lastElapsedSeconds: finiteSeconds(source.lastElapsedSeconds),
            lastProfile: String(source.lastProfile || ""),
            mission2Completed: source.mission2Completed === true,
            mission2BestScore: Math.min(1000, finiteInteger(source.mission2BestScore)),
            mission2FastestSeconds: finiteSeconds(source.mission2FastestSeconds),
            mission2Clears: finiteInteger(source.mission2Clears),
            mission2LastScore: Math.min(1000, finiteInteger(source.mission2LastScore)),
            mission2LastElapsedSeconds: finiteSeconds(source.mission2LastElapsedSeconds),
            mission2LastProfile: String(source.mission2LastProfile || ""),
            mission3Completed: source.mission3Completed === true,
            mission3BestScore: Math.min(1000, finiteInteger(source.mission3BestScore)),
            mission3FastestSeconds: finiteSeconds(source.mission3FastestSeconds),
            mission3Clears: finiteInteger(source.mission3Clears),
            mission3LastScore: Math.min(1000, finiteInteger(source.mission3LastScore)),
            mission3LastElapsedSeconds: finiteSeconds(source.mission3LastElapsedSeconds),
            mission3LastProfile: String(source.mission3LastProfile || ""),
            mission4Completed: source.mission4Completed === true,
            mission4BestScore: Math.min(1000, finiteInteger(source.mission4BestScore)),
            mission4FastestSeconds: finiteSeconds(source.mission4FastestSeconds),
            mission4Clears: finiteInteger(source.mission4Clears),
            mission4LastScore: Math.min(1000, finiteInteger(source.mission4LastScore)),
            mission4LastElapsedSeconds: finiteSeconds(source.mission4LastElapsedSeconds),
            mission4LastProfile: String(source.mission4LastProfile || ""),
            mission5Completed: source.mission5Completed === true,
            mission5BestScore: Math.min(1000, finiteInteger(source.mission5BestScore)),
            mission5FastestSeconds: finiteSeconds(source.mission5FastestSeconds),
            mission5Clears: finiteInteger(source.mission5Clears),
            mission5LastScore: Math.min(1000, finiteInteger(source.mission5LastScore)),
            mission5LastElapsedSeconds: finiteSeconds(source.mission5LastElapsedSeconds),
            mission5LastProfile: String(source.mission5LastProfile || ""),
            campaignRewardClaimed: source.campaignRewardClaimed === true,
            campaignCompletedAt: String(source.campaignCompletedAt || ""),
            campaignXpAwarded: finiteInteger(source.campaignXpAwarded),
            campaignCrystalsAwarded: finiteInteger(source.campaignCrystalsAwarded),
            endlessRuns: finiteInteger(source.endlessRuns),
            endlessLastMission: Math.min(5, finiteInteger(source.endlessLastMission)),
            endlessLastProfile: String(source.endlessLastProfile || ""),
            endlessLastLaunchedAt: String(source.endlessLastLaunchedAt || ""),
            processedAttempts:
                source.processedAttempts && typeof source.processedAttempts === "object"
                    ? { ...source.processedAttempts }
                    : {},
            attemptOrder:
                Array.isArray(source.attemptOrder)
                    ? source.attemptOrder.map(item => String(item)).filter(Boolean).slice(-maxAttemptHistory)
                    : []
        };
    }

    function load() {
        try {
            return normalize(JSON.parse(localStorage.getItem(storageKey) || "{}"));
        } catch {
            return normalize({});
        }
    }

    function campaignCompleted(state) {
        return state.mission1Completed === true &&
            state.mission2Completed === true &&
            state.mission3Completed === true &&
            state.mission4Completed === true &&
            state.mission5Completed === true;
    }

    function publicState(state) {
        return {
            campaignCompleted: campaignCompleted(state),
            mission1Completed: state.mission1Completed,
            bestScore: state.bestScore,
            fastestSeconds: state.fastestSeconds,
            totalClears: state.totalClears,
            totalXpEarned: state.totalXpEarned,
            totalCrystalsEarned: state.totalCrystalsEarned,
            lastScore: state.lastScore,
            lastElapsedSeconds: state.lastElapsedSeconds,
            lastProfile: state.lastProfile,
            mission2Completed: state.mission2Completed,
            mission2BestScore: state.mission2BestScore,
            mission2FastestSeconds: state.mission2FastestSeconds,
            mission2Clears: state.mission2Clears,
            mission2LastScore: state.mission2LastScore,
            mission2LastElapsedSeconds: state.mission2LastElapsedSeconds,
            mission2LastProfile: state.mission2LastProfile,
            mission3Completed: state.mission3Completed,
            mission3BestScore: state.mission3BestScore,
            mission3FastestSeconds: state.mission3FastestSeconds,
            mission3Clears: state.mission3Clears,
            mission3LastScore: state.mission3LastScore,
            mission3LastElapsedSeconds: state.mission3LastElapsedSeconds,
            mission3LastProfile: state.mission3LastProfile,
            mission4Completed: state.mission4Completed,
            mission4BestScore: state.mission4BestScore,
            mission4FastestSeconds: state.mission4FastestSeconds,
            mission4Clears: state.mission4Clears,
            mission4LastScore: state.mission4LastScore,
            mission4LastElapsedSeconds: state.mission4LastElapsedSeconds,
            mission4LastProfile: state.mission4LastProfile,
            mission5Completed: state.mission5Completed,
            mission5BestScore: state.mission5BestScore,
            mission5FastestSeconds: state.mission5FastestSeconds,
            mission5Clears: state.mission5Clears,
            mission5LastScore: state.mission5LastScore,
            mission5LastElapsedSeconds: state.mission5LastElapsedSeconds,
            mission5LastProfile: state.mission5LastProfile,
            campaignRewardClaimed: state.campaignRewardClaimed,
            campaignCompletedAt: state.campaignCompletedAt,
            campaignXpAwarded: state.campaignXpAwarded,
            campaignCrystalsAwarded: state.campaignCrystalsAwarded,
            endlessRuns: state.endlessRuns,
            endlessLastMission: state.endlessLastMission,
            endlessLastProfile: state.endlessLastProfile,
            endlessLastLaunchedAt: state.endlessLastLaunchedAt
        };
    }

    function save(state) {
        localStorage.setItem(storageKey, JSON.stringify(state));
        window.dispatchEvent(new CustomEvent(
            "cavecode-htmlcss-minigame-changed",
            { detail: publicState(state) }
        ));
    }

    function trimAttempts(state) {
        while (state.attemptOrder.length > maxAttemptHistory) {
            const removed = state.attemptOrder.shift();
            if (removed) {
                delete state.processedAttempts[removed];
            }
        }
    }

    function previousAttempt(state, id) {
        const previous = state.processedAttempts[id];
        return previous
            ? { ...previous, duplicateAttempt: true, state: publicState(state) }
            : null;
    }

    function recordClear(attemptId, score, elapsedSeconds, profile) {
        const state = load();
        const id = String(attemptId || "").trim();
        if (!id) throw new Error("A mission attempt identifier is required.");
        const duplicate = previousAttempt(state, id);
        if (duplicate) return duplicate;

        const clearNumber = state.totalClears + 1;
        const firstClear = clearNumber === 1;
        const xpAwarded = firstClear ? 100 : 40;
        const crystalsAwarded = firstClear ? 6 : 2;
        const normalizedScore = Math.min(1000, finiteInteger(score));
        const normalizedSeconds = finiteSeconds(elapsedSeconds) ?? 0;
        const newBestScore = normalizedScore > state.bestScore;
        const newFastestTime = state.fastestSeconds === null || normalizedSeconds < state.fastestSeconds;

        state.mission1Completed = true;
        state.totalClears = clearNumber;
        state.totalXpEarned += xpAwarded;
        state.totalCrystalsEarned += crystalsAwarded;
        state.lastScore = normalizedScore;
        state.lastElapsedSeconds = normalizedSeconds;
        state.lastProfile = String(profile || "");
        if (newBestScore) state.bestScore = normalizedScore;
        if (newFastestTime) state.fastestSeconds = normalizedSeconds;

        const result = {
            rewardKey: `htmlcss-interface-rescue-m1-clear-${clearNumber}`,
            xpAwarded,
            crystalsAwarded,
            clearNumber,
            firstClear,
            newBestScore,
            newFastestTime
        };

        state.processedAttempts[id] = result;
        state.attemptOrder.push(id);
        trimAttempts(state);
        save(state);
        return { ...result, duplicateAttempt: false, state: publicState(state) };
    }

    function recordMission2Clear(attemptId, score, elapsedSeconds, profile) {
        const state = load();
        const id = String(attemptId || "").trim();
        if (!id) throw new Error("A Mission 2 attempt identifier is required.");
        const duplicate = previousAttempt(state, id);
        if (duplicate) return duplicate;

        const clearNumber = state.mission2Clears + 1;
        const firstClear = clearNumber === 1;
        const xpAwarded = firstClear ? 100 : 40;
        const crystalsAwarded = firstClear ? 6 : 2;
        const normalizedScore = Math.min(1000, finiteInteger(score));
        const normalizedSeconds = finiteSeconds(elapsedSeconds) ?? 0;
        const newBestScore = normalizedScore > state.mission2BestScore;
        const newFastestTime = state.mission2FastestSeconds === null || normalizedSeconds < state.mission2FastestSeconds;

        state.mission2Completed = true;
        state.mission2Clears = clearNumber;
        state.totalXpEarned += xpAwarded;
        state.totalCrystalsEarned += crystalsAwarded;
        state.mission2LastScore = normalizedScore;
        state.mission2LastElapsedSeconds = normalizedSeconds;
        state.mission2LastProfile = String(profile || "");
        if (newBestScore) state.mission2BestScore = normalizedScore;
        if (newFastestTime) state.mission2FastestSeconds = normalizedSeconds;

        const result = {
            rewardKey: `htmlcss-interface-rescue-m2-clear-${clearNumber}`,
            xpAwarded,
            crystalsAwarded,
            clearNumber,
            firstClear,
            newBestScore,
            newFastestTime
        };

        state.processedAttempts[id] = result;
        state.attemptOrder.push(id);
        trimAttempts(state);
        save(state);
        return { ...result, duplicateAttempt: false, state: publicState(state) };
    }

    function recordMission3Clear(attemptId, score, elapsedSeconds, profile) {
        const state = load();
        const id = String(attemptId || "").trim();
        if (!id) throw new Error("A Mission 3 attempt identifier is required.");
        const duplicate = previousAttempt(state, id);
        if (duplicate) return duplicate;

        const clearNumber = state.mission3Clears + 1;
        const firstClear = clearNumber === 1;
        const xpAwarded = firstClear ? 100 : 40;
        const crystalsAwarded = firstClear ? 6 : 2;
        const normalizedScore = Math.min(1000, finiteInteger(score));
        const normalizedSeconds = finiteSeconds(elapsedSeconds) ?? 0;
        const newBestScore = normalizedScore > state.mission3BestScore;
        const newFastestTime = state.mission3FastestSeconds === null || normalizedSeconds < state.mission3FastestSeconds;

        state.mission3Completed = true;
        state.mission3Clears = clearNumber;
        state.totalXpEarned += xpAwarded;
        state.totalCrystalsEarned += crystalsAwarded;
        state.mission3LastScore = normalizedScore;
        state.mission3LastElapsedSeconds = normalizedSeconds;
        state.mission3LastProfile = String(profile || "");
        if (newBestScore) state.mission3BestScore = normalizedScore;
        if (newFastestTime) state.mission3FastestSeconds = normalizedSeconds;

        const result = {
            rewardKey: `htmlcss-interface-rescue-m3-clear-${clearNumber}`,
            xpAwarded,
            crystalsAwarded,
            clearNumber,
            firstClear,
            newBestScore,
            newFastestTime
        };

        state.processedAttempts[id] = result;
        state.attemptOrder.push(id);
        trimAttempts(state);
        save(state);
        return { ...result, duplicateAttempt: false, state: publicState(state) };
    }

    function recordMission4Clear(attemptId, score, elapsedSeconds, profile) {
        const state = load();
        const id = String(attemptId || "").trim();
        if (!id) throw new Error("A Mission 4 attempt identifier is required.");
        const duplicate = previousAttempt(state, id);
        if (duplicate) return duplicate;

        const clearNumber = state.mission4Clears + 1;
        const firstClear = clearNumber === 1;
        const xpAwarded = firstClear ? 100 : 40;
        const crystalsAwarded = firstClear ? 6 : 2;
        const normalizedScore = Math.min(1000, finiteInteger(score));
        const normalizedSeconds = finiteSeconds(elapsedSeconds) ?? 0;
        const newBestScore = normalizedScore > state.mission4BestScore;
        const newFastestTime = state.mission4FastestSeconds === null || normalizedSeconds < state.mission4FastestSeconds;

        state.mission4Completed = true;
        state.mission4Clears = clearNumber;
        state.totalXpEarned += xpAwarded;
        state.totalCrystalsEarned += crystalsAwarded;
        state.mission4LastScore = normalizedScore;
        state.mission4LastElapsedSeconds = normalizedSeconds;
        state.mission4LastProfile = String(profile || "");
        if (newBestScore) state.mission4BestScore = normalizedScore;
        if (newFastestTime) state.mission4FastestSeconds = normalizedSeconds;

        const result = {
            rewardKey: `htmlcss-interface-rescue-m4-clear-${clearNumber}`,
            xpAwarded,
            crystalsAwarded,
            clearNumber,
            firstClear,
            newBestScore,
            newFastestTime
        };

        state.processedAttempts[id] = result;
        state.attemptOrder.push(id);
        trimAttempts(state);
        save(state);
        return { ...result, duplicateAttempt: false, state: publicState(state) };
    }

    function recordMission5Clear(attemptId, score, elapsedSeconds, profile) {
        const state = load();
        const id = String(attemptId || "").trim();
        if (!id) throw new Error("A Mission 5 attempt identifier is required.");
        const duplicate = previousAttempt(state, id);
        if (duplicate) return duplicate;

        const clearNumber = state.mission5Clears + 1;
        const firstClear = clearNumber === 1;
        const xpAwarded = firstClear ? 100 : 40;
        const crystalsAwarded = firstClear ? 6 : 2;
        const normalizedScore = Math.min(1000, finiteInteger(score));
        const normalizedSeconds = finiteSeconds(elapsedSeconds) ?? 0;
        const newBestScore = normalizedScore > state.mission5BestScore;
        const newFastestTime = state.mission5FastestSeconds === null || normalizedSeconds < state.mission5FastestSeconds;

        state.mission5Completed = true;
        state.mission5Clears = clearNumber;
        state.totalXpEarned += xpAwarded;
        state.totalCrystalsEarned += crystalsAwarded;
        state.mission5LastScore = normalizedScore;
        state.mission5LastElapsedSeconds = normalizedSeconds;
        state.mission5LastProfile = String(profile || "");
        if (newBestScore) state.mission5BestScore = normalizedScore;
        if (newFastestTime) state.mission5FastestSeconds = normalizedSeconds;

        const result = {
            rewardKey: `htmlcss-interface-rescue-m5-clear-${clearNumber}`,
            xpAwarded,
            crystalsAwarded,
            clearNumber,
            firstClear,
            newBestScore,
            newFastestTime
        };

        state.processedAttempts[id] = result;
        state.attemptOrder.push(id);
        trimAttempts(state);
        save(state);
        return { ...result, duplicateAttempt: false, state: publicState(state) };
    }

    function claimCampaignReward() {
        const state = load();
        if (!campaignCompleted(state)) {
            throw new Error("All five Interface Rescue missions must be completed first.");
        }

        const rewardKey = "htmlcss-interface-rescue-campaign-complete";
        if (state.campaignRewardClaimed) {
            return {
                rewardKey,
                xpAwarded: 0,
                crystalsAwarded: 0,
                duplicateReward: true,
                state: publicState(state)
            };
        }

        const xpAwarded = 200;
        const crystalsAwarded = 15;
        state.campaignRewardClaimed = true;
        state.campaignCompletedAt = new Date().toISOString();
        state.campaignXpAwarded = xpAwarded;
        state.campaignCrystalsAwarded = crystalsAwarded;
        state.totalXpEarned += xpAwarded;
        state.totalCrystalsEarned += crystalsAwarded;
        save(state);

        return {
            rewardKey,
            xpAwarded,
            crystalsAwarded,
            duplicateReward: false,
            state: publicState(state)
        };
    }

    function recordEndlessLaunch(mission, profile) {
        const state = load();
        if (!campaignCompleted(state)) {
            throw new Error("Complete all five missions before using Endless Mode.");
        }

        const missionNumber = Math.min(5, Math.max(1, finiteInteger(mission, 1)));
        const profileName = String(profile || "");
        const allowedProfiles = new Set(["FullIncident", "HtmlSystems", "CssSystems"]);
        if (!allowedProfiles.has(profileName)) {
            throw new Error("The Endless Mode damage profile is invalid.");
        }

        state.endlessRuns += 1;
        state.endlessLastMission = missionNumber;
        state.endlessLastProfile = profileName;
        state.endlessLastLaunchedAt = new Date().toISOString();
        save(state);
        return publicState(state);
    }

    window.caveCodeHtmlCssMinigame = Object.freeze({
        version: "htmlcss-minigame-pass-6e5-v1",
        getState: () => publicState(load()),
        getHubState: () => publicState(load()),
        recordClear,
        recordMission2Clear,
        recordMission3Clear,
        recordMission4Clear,
        recordMission5Clear,
        claimCampaignReward,
        recordEndlessLaunch
    });
})();
