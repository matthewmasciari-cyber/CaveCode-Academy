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

    function publicState(state) {
        return {
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
            mission3LastProfile: state.mission3LastProfile

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

    window.caveCodeHtmlCssMinigame = Object.freeze({
        version: "htmlcss-minigame-pass-6e2-v1",
        getState: () => publicState(load()),
        getHubState: () => publicState(load()),
        recordClear,
        recordMission2Clear,
        recordMission3Clear
    });
})();
