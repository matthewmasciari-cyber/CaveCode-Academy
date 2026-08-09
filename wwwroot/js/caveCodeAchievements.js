(function () {
    const storageKey = "cavecode.achievements.v1";

    const definitions = [
        {
            id: "csharp-chapter-1",
            course: "csharp",
            courseName: "C# Cave Adventure",
            chapter: 1,
            name: "Cave Entrance Opened",
            description: "Complete C# Chapter 1 and establish your coding foundation.",
            titleReward: "Code Apprentice",
            crystals: 100
        },
        {
            id: "csharp-chapter-2",
            course: "csharp",
            courseName: "C# Cave Adventure",
            chapter: 2,
            name: "Logic Torch Lit",
            description: "Complete C# Chapter 2 and master decisions and control flow.",
            titleReward: "Logic Pathfinder",
            crystals: 125
        },
        {
            id: "csharp-chapter-3",
            course: "csharp",
            courseName: "C# Cave Adventure",
            chapter: 3,
            name: "Explorer's Toolkit",
            description: "Complete C# Chapter 3 and build reusable systems and collections.",
            titleReward: "Systems Builder",
            crystals: 150
        },
        {
            id: "csharp-chapter-4",
            course: "csharp",
            courseName: "C# Cave Adventure",
            chapter: 4,
            name: "Creature Architect",
            description: "Complete C# Chapter 4 and shape the cave game's living systems.",
            titleReward: "C# Adventurer",
            crystals: 175
        },
        {
            id: "csharp-chapter-5",
            course: "csharp",
            courseName: "C# Cave Adventure",
            chapter: 5,
            name: "Cave Adventure Master",
            description: "Complete the full C# Cave Adventure learning path.",
            titleReward: "CaveCode Champion",
            crystals: 250
        },
        {
            id: "python-chapter-1",
            course: "python",
            courseName: "Python Automation Quest",
            chapter: 1,
            name: "Control Terminal Online",
            description: "Complete Python Chapter 1 and restore the underground control room.",
            titleReward: "Control Room Operator",
            crystals: 100
        },
        {
            id: "python-chapter-2",
            course: "python",
            courseName: "Python Automation Quest",
            chapter: 2,
            name: "Safety Systems Certified",
            description: "Complete Python Chapter 2 and commission the safety logic.",
            titleReward: "Safety Technician",
            crystals: 125
        },
        {
            id: "python-chapter-3",
            course: "python",
            courseName: "Python Automation Quest",
            chapter: 3,
            name: "Sequence Controller Online",
            description: "Complete Python Chapter 3 and automate repeated equipment sequences.",
            titleReward: "Automation Trainee",
            crystals: 150
        },
        {
            id: "python-chapter-4",
            course: "python",
            courseName: "Python Automation Quest",
            chapter: 4,
            name: "Monitoring Network Established",
            description: "Complete Python Chapter 4 and reconnect the facility monitoring network.",
            titleReward: "Systems Monitor",
            crystals: 175
        },
        {
            id: "python-chapter-5",
            course: "python",
            courseName: "Python Automation Quest",
            chapter: 5,
            name: "Python Automation Technician",
            description: "Complete the full Python Automation Quest learning path.",
            titleReward: "Python Technician",
            crystals: 250
        },
        {
            id: "cpp-chapter-1",
            course: "cpp",
            courseName: "C++ Engine Foundry",
            chapter: 1,
            name: "Forge Core Ignited",
            description: "Complete C++ Chapter 1 and bring the first Engine Foundry dashboard online.",
            titleReward: "Foundry Apprentice",
            crystals: 100
        },
        {
            id: "cpp-chapter-2",
            course: "cpp",
            courseName: "C++ Engine Foundry",
            chapter: 2,
            name: "Control Grid Commissioned",
            description: "Complete C++ Chapter 2 and commission the foundry control systems.",
            titleReward: "Control Systems Coder",
            crystals: 125
        },
        {
            id: "cpp-chapter-3",
            course: "cpp",
            courseName: "C++ Engine Foundry",
            chapter: 3,
            name: "Resource Pipeline Built",
            description: "Complete C++ Chapter 3 and build reusable functions and collections.",
            titleReward: "C++ Systems Builder",
            crystals: 150
        },
        {
            id: "cpp-chapter-4",
            course: "cpp",
            courseName: "C++ Engine Foundry",
            chapter: 4,
            name: "Ownership Secured",
            description: "Complete C++ Chapter 4 and master objects, pointers, and ownership.",
            titleReward: "Memory Steward",
            crystals: 175
        },
        {
            id: "cpp-chapter-5",
            course: "cpp",
            courseName: "C++ Engine Foundry",
            chapter: 5,
            name: "Engine Foundry Master",
            description: "Complete the full C++ Engine Foundry learning path.",
            titleReward: "C++ Engine Architect",
            crystals: 250
        },
        {
            id: "htmlcss-chapter-1",
            course: "htmlcss",
            courseName: "HTML & CSS Workshop",
            chapter: 1,
            name: "Workshop Structure Complete",
            description: "Complete HTML & CSS Chapter 1 and build the first semantic Interface Workshop page.",
            titleReward: "Markup Builder",
            crystals: 100
        },
        {
            id: "htmlcss-minigame-1",
            course: "htmlcss-minigame",
            courseName: "Interface Rescue",
            chapter: 1,
            name: "Interface Restored",
            description: "Complete Interface Rescue Mission 1 with all six automated repair requirements.",
            titleReward: "Interface Rescuer",
            crystals: 0
        },
        {
            id: "htmlcss-minigame-campaign",
            course: "htmlcss-minigame",
            courseName: "Interface Rescue",
            chapter: 2,
            name: "Workshop Defender",
            description: "Complete all five Interface Rescue missions and claim the full campaign reward.",
            titleReward: "Interface Guardian",
            crystals: 0
        },
        {
            id: "cpp-minigame-1",
            course: "cpp-minigame",
            courseName: "Engine Foundry",
            chapter: 1,
            name: "Engine Restored",
            description: "Complete Engine Foundry Mission 1 with all six startup requirements.",
            titleReward: "Engine Technician",
            crystals: 0
        },
        {
            id: "cpp-minigame-2",
            course: "cpp-minigame",
            courseName: "Engine Foundry",
            chapter: 2,
            name: "Cooling Loop Stabilized",
            description: "Complete Engine Foundry Mission 2 with all six cooling-loop requirements.",
            titleReward: "Cooling Systems Programmer",
            crystals: 0
        },
        {
            id: "cpp-minigame-3",
            course: "cpp-minigame",
            courseName: "Engine Foundry",
            chapter: 3,
            name: "Pump Logic Restored",
            description: "Complete Engine Foundry Mission 3.",
            titleReward: "Pump Control Programmer",
            crystals: 0
        },
        {
            id: "cpp-minigame-4",
            course: "cpp-minigame",
            courseName: "Engine Foundry",
            chapter: 4,
            name: "Sensor Package Restored",
            description: "Complete Engine Foundry Mission 4 with all six sensor-package requirements.",
            titleReward: "Systems Data Builder",
            crystals: 0
        },
        {
            id: "cpp-minigame-5",
            course: "cpp-minigame",
            courseName: "Engine Foundry",
            chapter: 5,
            name: "Full System Recovered",
            description: "Complete Engine Foundry Mission 5 with all six finale requirements.",
            titleReward: "Control Systems Programmer",
            crystals: 0
        },
        {
            id: "cpp-minigame-campaign-1",
            course: "cpp-minigame-campaign",
            courseName: "Engine Foundry Campaign",
            chapter: 1,
            name: "Engine Foundry Restored",
            description: "Complete all five Engine Foundry Chapter 1 missions.",
            titleReward: "Foundry Systems Engineer",
            crystals: 0
        }
        ,
        {
            id: "gcl-chapter-1",
            course: "gcl",
            courseName: "GCL+ Control Line Lab",
            chapter: 1,
            name: "Control Lab Booted",
            description: "Complete GCL+ Chapter 1 and learn variables, modes, and startup shells.",
            titleReward: "Control Cadet",
            crystals: 100
        },
        {
            id: "gcl-chapter-2",
            course: "gcl",
            courseName: "GCL+ Control Line Lab",
            chapter: 2,
            name: "Sequence Tech Certified",
            description: "Complete GCL+ Chapter 2 and work timing, enables, and interlocks.",
            titleReward: "Sequence Tech",
            crystals: 125
        },
        {
            id: "gcl-chapter-5",
            course: "gcl",
            courseName: "GCL+ Control Line Lab",
            chapter: 5,
            name: "Control Line Master",
            description: "Complete the full GCL+ Control Line Lab path.",
            titleReward: "Line Boss",
            crystals: 250
        },
        {
            id: "arduino-chapter-1",
            course: "arduino",
            courseName: "Arduino C++",
            chapter: 1,
            name: "Maker Lab Powered",
            description: "Complete Arduino Chapter 1 and get sketches, pins, and blink online.",
            titleReward: "Pin Rookie",
            crystals: 100
        },
        {
            id: "arduino-chapter-2",
            course: "arduino",
            courseName: "Arduino C++",
            chapter: 2,
            name: "Sketch Smith Badge",
            description: "Complete Arduino Chapter 2 digital output patterns.",
            titleReward: "Sketch Smith",
            crystals: 125
        },
        {
            id: "arduino-chapter-5",
            course: "arduino",
            courseName: "Arduino C++",
            chapter: 5,
            name: "Board Captain",
            description: "Complete the full Arduino C++ maker path.",
            titleReward: "Board Captain",
            crystals: 250
        },
        {
            id: "raspi-chapter-1",
            course: "raspi",
            courseName: "Raspberry Pi Python",
            chapter: 1,
            name: "Pi Lab Online",
            description: "Complete Raspberry Pi Chapter 1 script foundations.",
            titleReward: "GPIO Scout",
            crystals: 100
        },
        {
            id: "raspi-chapter-2",
            course: "raspi",
            courseName: "Raspberry Pi Python",
            chapter: 2,
            name: "Pi Tinkerer",
            description: "Complete Raspberry Pi Chapter 2 digital output with gpiozero.",
            titleReward: "Pi Tinkerer",
            crystals: 125
        },
        {
            id: "raspi-chapter-5",
            course: "raspi",
            courseName: "Raspberry Pi Python",
            chapter: 5,
            name: "Lab Lead",
            description: "Complete the full Raspberry Pi Python path.",
            titleReward: "Lab Lead",
            crystals: 250
        },
        {
            id: "javascript-chapter-1",
            course: "javascript",
            courseName: "JavaScript Web Forge",
            chapter: 1,
            name: "Forge Boot Complete",
            description: "Complete JavaScript Chapter 1 console and value foundations.",
            titleReward: "Console Cadet",
            crystals: 100
        },
        {
            id: "javascript-chapter-2",
            course: "javascript",
            courseName: "JavaScript Web Forge",
            chapter: 2,
            name: "Decision Branch",
            description: "Complete JavaScript Chapter 2 decisions and branching.",
            titleReward: "DOM Jockey",
            crystals: 125
        },
        {
            id: "javascript-chapter-5",
            course: "javascript",
            courseName: "JavaScript Web Forge",
            chapter: 5,
            name: "Web Forge Ace",
            description: "Complete the full JavaScript Web Forge path.",
            titleReward: "Web Forge Ace",
            crystals: 250
        }

    ];

    function emptyState() {
        return {
            unlocked: [],
            claimed: [],
            crystals: 0
        };
    }

    function load() {
        try {
            const parsed = JSON.parse(
                localStorage.getItem(storageKey) || "{}"
            );

            return {
                unlocked: Array.isArray(parsed.unlocked)
                    ? parsed.unlocked
                    : [],
                claimed: Array.isArray(parsed.claimed)
                    ? parsed.claimed
                    : [],
                crystals: Number.isFinite(parsed.crystals)
                    ? Math.max(0, Math.floor(parsed.crystals))
                    : 0
            };
        } catch {
            return emptyState();
        }
    }

    function save(state) {
        localStorage.setItem(
            storageKey,
            JSON.stringify(state)
        );

        window.dispatchEvent(
            new CustomEvent(
                "cavecode-achievements-changed",
                { detail: buildView(state) }
            )
        );
    }

    function progressSnapshot(course) {
        const key = `cavecode.${course}.progress.v1`;

        try {
            return JSON.parse(
                localStorage.getItem(key) || "null"
            );
        } catch {
            return null;
        }
    }

    function completedModules(snapshot) {
        if (!snapshot) {
            return [];
        }

        const modules =
            snapshot.moduleCompleted ??
            snapshot.ModuleCompleted ??
            [];

        return Array.isArray(modules) ? modules : [];
    }

    function syncFromCourseProgress(state) {
        let changed = false;

        for (const course of ["csharp", "python", "cpp", "htmlcss"]) {
            const modules = completedModules(
                progressSnapshot(course)
            );

            for (let chapter = 1; chapter <= 5; chapter++) {
                const finalModuleIndex = chapter * 8 - 1;
                const definition = definitions.find(
                    item =>
                        item.course === course &&
                        item.chapter === chapter
                );

                if (
                    definition &&
                    modules[finalModuleIndex] === true &&
                    !state.unlocked.includes(definition.id)
                ) {
                    state.unlocked.push(definition.id);
                    changed = true;
                }
            }
        }

        if (changed) {
            save(state);
        }

        return state;
    }

    function buildView(state) {
        const achievements = definitions.map(definition => ({
            ...definition,
            unlocked: state.unlocked.includes(definition.id),
            claimed: state.claimed.includes(definition.id)
        }));

        return {
            crystals: state.crystals,
            unclaimedCount: achievements.filter(
                item => item.unlocked && !item.claimed
            ).length,
            earnedCount: achievements.filter(
                item => item.unlocked
            ).length,
            claimedCount: achievements.filter(
                item => item.claimed
            ).length,
            totalCount: achievements.length,
            achievements
        };
    }

    function currentView() {
        const state = syncFromCourseProgress(load());
        return buildView(state);
    }

    window.caveCodeAchievements = {
        getState: function () {
            return currentView();
        },

        unlockChapter: function (course, chapter) {
            const definition = definitions.find(
                item =>
                    item.course === String(course) &&
                    item.chapter === Number(chapter)
            );

            if (!definition) {
                return null;
            }

            const state = load();
            const newlyUnlocked =
                !state.unlocked.includes(definition.id);

            if (newlyUnlocked) {
                state.unlocked.push(definition.id);
                save(state);
            }

            return {
                ...definition,
                newlyUnlocked
            };
        },

        claim: function (achievementId) {
            const definition = definitions.find(
                item => item.id === String(achievementId)
            );

            if (!definition) {
                throw new Error("Unknown achievement.");
            }

            const state = syncFromCourseProgress(load());

            if (!state.unlocked.includes(definition.id)) {
                throw new Error(
                    "Complete the required chapter first."
                );
            }

            if (!state.claimed.includes(definition.id)) {
                state.claimed.push(definition.id);
                state.crystals += definition.crystals;
                save(state);
            }

            return buildView(state);
        },

        awardMinigameCrystals: function (
            rewardKey,
            amount
        ) {
            const dedupeKey =
                "cavecode.minigame.crystal-rewards.v1";

            let claimed = [];

            try {
                const parsed = JSON.parse(
                    localStorage.getItem(dedupeKey) || "[]"
                );

                claimed = Array.isArray(parsed)
                    ? parsed
                    : [];
            } catch {
                claimed = [];
            }

            const key = String(rewardKey);
            const state = syncFromCourseProgress(load());

            if (!claimed.includes(key)) {
                claimed.push(key);

                localStorage.setItem(
                    dedupeKey,
                    JSON.stringify(claimed)
                );

                state.crystals += Math.max(
                    0,
                    Math.floor(Number(amount) || 0)
                );

                save(state);
            }

            return buildView(state);
        },

        spendCrystals: function (
            amount,
            reason
        ) {
            const cost = Math.max(
                0,
                Math.floor(Number(amount) || 0)
            );

            const state = syncFromCourseProgress(load());

            if (cost === 0) {
                return {
                    success: true,
                    amountSpent: 0,
                    balance: state.crystals,
                    reason: String(reason || "")
                };
            }

            if (state.crystals < cost) {
                return {
                    success: false,
                    amountSpent: 0,
                    balance: state.crystals,
                    reason: String(reason || ""),
                    message:
                        `You need ${cost} Code Crystals but have ${state.crystals}.`
                };
            }

            state.crystals -= cost;
            save(state);

            return {
                success: true,
                amountSpent: cost,
                balance: state.crystals,
                reason: String(reason || "")
            };
        },

        getTitleOptions: function () {
            const state = syncFromCourseProgress(load());

            return [
                {
                    title: "Cave Explorer",
                    unlocked: true,
                    requirement: "Available from the beginning"
                },
                ...definitions.map(item => ({
                    title: item.titleReward,
                    unlocked: state.claimed.includes(item.id),
                    requirement:
                        `Claim ${item.name} — ${item.courseName} Chapter ${item.chapter}`
                }))
            ];
        },

        getFeatureOptions: function () {
            const state = syncFromCourseProgress(load());

            return [
                {
                    name: "First Steps",
                    unlocked: true,
                    requirement: "Available from the beginning"
                },
                ...definitions.map(item => ({
                    name: item.name,
                    unlocked: state.claimed.includes(item.id),
                    requirement:
                        `Claim the Chapter ${item.chapter} reward`
                }))
            ];
        }
    };
})();
