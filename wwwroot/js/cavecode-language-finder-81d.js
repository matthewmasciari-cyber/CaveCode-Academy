(() => {
    "use strict";

    const AUTO_PROMPT_KEY = "cavecode.languageFinder.autoPrompt.v1";

    const PATHS = {
        csharp: {
            name: "C#",
            course: "C# Cave Adventure",
            icon: "gamepad-2",
            reason: "A strong all-around choice for games, desktop software, and structured beginner learning.",
            available: true
        },
        python: {
            name: "Python",
            course: "Python Automation Quest",
            icon: "bot",
            reason: "Excellent for automation, AI, data, cybersecurity, and practical beginner projects.",
            available: true
        },
        cpp: {
            name: "C++",
            course: "C++ Engine Foundry",
            icon: "microchip",
            reason: "Best suited to AAA games, embedded systems, robotics, and performance-heavy software.",
            available: true
        },
        htmlcss: {
            name: "HTML & CSS",
            course: "HTML & CSS Workshop",
            icon: "layout-dashboard",
            reason: "The clearest starting point for websites, interfaces, visual design, and browser layouts.",
            available: true
        },
        javascript: {
            name: "JavaScript",
            course: "JavaScript Web Forge",
            icon: "braces",
            reason: "Ideal for interactive websites, browser games, and modern web applications.",
            available: false
        },
        sql: {
            name: "SQL",
            course: "SQL Database Dungeon",
            icon: "database-zap",
            reason: "The strongest match for databases, reporting, analytics, and persistent application data.",
            available: false
        },
        typescript: {
            name: "TypeScript",
            course: "TypeScript Application Architect",
            icon: "file-code-2",
            reason: "A dependable choice for large web applications and team-based JavaScript projects.",
            available: false
        },
        java: {
            name: "Java",
            course: "Java Enterprise Expedition",
            icon: "coffee",
            reason: "Well suited to Android, enterprise applications, and large back-end systems.",
            available: false
        },
        go: {
            name: "Go",
            course: "Go Cloud Command",
            icon: "cloud-cog",
            reason: "A practical match for cloud infrastructure, APIs, networking, and DevOps tools.",
            available: false
        },
        rust: {
            name: "Rust",
            course: "Rust Systems Frontier",
            icon: "shield-check",
            reason: "Excellent for memory-safe systems work, performance, WebAssembly, and low-level tools.",
            available: false
        },
        gcl: {
            name: "GCL+",
            course: "GCL+ Control Line Lab",
            icon: "fan",
            reason: "Built for building-automation style control sequences, timers, staging, and point-driven logic.",
            available: true
        },
        arduino: {
            name: "Arduino C++",
            course: "Arduino C++ Maker Lab",
            icon: "circuit-board",
            reason: "Best when you want applied C++ on microcontrollers: LEDs, buttons, sensors, and sketches.",
            available: true
        },
        raspi: {
            name: "Raspberry Pi Python",
            course: "Raspberry Pi Python Lab",
            icon: "cpu",
            reason: "Best when you want applied Python on a Pi: GPIO, LEDs, buttons, and physical computing.",
            available: true
        }
    };

    const BUILD_OPTIONS = [
        ["games", "Games", "gamepad-2"],
        ["web", "Websites & web apps", "globe-2"],
        ["automation", "AI & automation", "bot"],
        ["hardware", "Hardware & robotics", "microchip"],
        ["business", "Business software", "building-2"],
        ["cyber", "Cybersecurity tools", "shield"],
        ["data", "Data & analytics", "chart-no-axes-combined"],
        ["unsure", "I’m not sure yet", "compass"]
    ];

    const SPECIALIZATIONS = {
        games: [
            ["indie", "2D or indie games", "sparkles"],
            ["sandbox", "Sandbox or survival games", "mountain"],
            ["aaa", "AAA or engine development", "gauge"],
            ["browser", "Browser games", "globe-2"],
            ["mobile", "Mobile games", "smartphone"]
        ],
        web: [
            ["sites", "Company or portfolio sites", "layout-template"],
            ["dashboards", "Dashboards and interfaces", "layout-dashboard"],
            ["apps", "Full web applications", "panels-top-left"],
            ["stores", "Online stores", "shopping-cart"],
            ["backend", "Servers and APIs", "server-cog"]
        ],
        automation: [
            ["workflow", "Workplace automation", "workflow"],
            ["ml", "Machine learning", "brain-circuit"],
            ["data", "Data science", "chart-no-axes-combined"],
            ["hardware", "Raspberry Pi and devices", "cpu"],
            ["apps", "AI-powered applications", "bot"]
        ],
        hardware: [
            ["embedded", "Embedded devices", "microchip"],
            ["robotics", "Robotics and controls", "bot"],
            ["simulation", "Real-time simulation", "gauge"],
            ["iot", "Connected devices and IoT", "wifi"],
            ["systems", "Operating systems", "terminal"]
        ],
        business: [
            ["desktop", "Desktop applications", "monitor"],
            ["enterprise", "Enterprise systems", "building-2"],
            ["cloud", "Cloud services", "cloud-cog"],
            ["database", "Databases and reporting", "database-zap"],
            ["mobile", "Mobile business apps", "smartphone"]
        ],
        cyber: [
            ["automation", "Security automation", "workflow"],
            ["tools", "Administration tools", "wrench"],
            ["systems", "Low-level security", "shield-check"],
            ["web", "Web security", "globe-lock"],
            ["data", "Security data analysis", "chart-no-axes-combined"]
        ],
        data: [
            ["analysis", "Analysis and visualization", "chart-column-big"],
            ["database", "Databases and queries", "database-zap"],
            ["ai", "Machine learning", "brain-circuit"],
            ["reports", "Business reporting", "file-chart-column"],
            ["backend", "Application data systems", "server-cog"]
        ],
        unsure: [
            ["visual", "I want visual results quickly", "palette"],
            ["practical", "I want practical workplace projects", "wrench"],
            ["games", "Games sound motivating", "gamepad-2"],
            ["career", "I want broad career options", "briefcase-business"],
            ["easy", "I want the easiest start", "graduation-cap"]
        ]
    };

    const EXPERIENCE_OPTIONS = [
        ["new", "I’m completely new", "sprout"],
        ["some", "I have some experience", "code-2"],
        ["challenge", "Challenge me", "flame"],
        ["fast", "Show me the fastest path", "rocket"]
    ];

    const SCORE_RULES = {
        games: { csharp: 8, cpp: 7, javascript: 3, htmlcss: 2, rust: 2 },
        web: { htmlcss: 8, javascript: 7, typescript: 5, csharp: 3, python: 2, go: 2 },
        automation: { python: 10, csharp: 2, go: 2 },
        hardware: { arduino: 10, raspi: 9, cpp: 7, python: 4, rust: 3, csharp: 2 },
        business: { csharp: 8, java: 7, sql: 6, typescript: 4, go: 3 },
        cyber: { python: 8, rust: 6, cpp: 5, javascript: 2 },
        data: { python: 8, sql: 8, go: 2 },
        unsure: { python: 5, htmlcss: 5, csharp: 4, javascript: 3 },

        indie: { csharp: 6, javascript: 2 },
        sandbox: { csharp: 7, cpp: 3 },
        aaa: { cpp: 9, csharp: 4, rust: 2 },
        browser: { javascript: 8, htmlcss: 6 },
        mobile: { java: 6, typescript: 4, csharp: 4, javascript: 3 },
        sites: { htmlcss: 9, javascript: 3 },
        dashboards: { htmlcss: 7, javascript: 6, typescript: 4 },
        apps: { javascript: 6, typescript: 6, csharp: 3, python: 3 },
        stores: { htmlcss: 5, javascript: 6, typescript: 3, sql: 2 },
        backend: { csharp: 5, python: 5, go: 5, java: 4, sql: 3 },
        workflow: { python: 9, csharp: 2 },
        ml: { python: 10 },
        data: { python: 7, sql: 6 },
        hardware: { cpp: 8, python: 5, rust: 4 },
        embedded: { cpp: 9, rust: 6 },
        robotics: { cpp: 8, python: 5 },
        simulation: { cpp: 9, csharp: 4 },
        iot: { cpp: 6, python: 5, rust: 3 },
        systems: { cpp: 8, rust: 8 },
        desktop: { csharp: 8, java: 3 },
        enterprise: { java: 8, csharp: 7, sql: 4 },
        cloud: { go: 8, csharp: 5, java: 4 },
        database: { sql: 10, csharp: 3, java: 3 },
        automation: { python: 9 },
        tools: { python: 7, rust: 4, cpp: 3 },
        web: { htmlcss: 6, javascript: 6, typescript: 4 },
        analysis: { python: 8, sql: 5 },
        ai: { python: 10 },
        reports: { sql: 8, python: 5 },
        visual: { htmlcss: 7, csharp: 3 },
        practical: { python: 7, csharp: 5 },
        career: { csharp: 6, python: 6, htmlcss: 4 },
        easy: { htmlcss: 8, python: 7, csharp: 4 }
    };

    let state = {
        step: 0,
        build: null,
        specialization: null,
        experience: null
    };

    function refreshIcons() {
        window.lucide?.createIcons?.({
            attrs: { "aria-hidden": "true" }
        });
    }

    function scorePaths() {
        const scores = Object.fromEntries(
            Object.keys(PATHS).map(key => [key, 0])
        );

        [state.build, state.specialization].forEach(answer => {
            const rule = SCORE_RULES[answer] || {};
            Object.entries(rule).forEach(([key, value]) => {
                scores[key] += value;
            });
        });

        if (state.experience === "new") {
            scores.python += 5;
            scores.htmlcss += 5;
            scores.csharp += 3;
            scores.cpp -= 4;
            scores.rust -= 4;
        }

        if (state.experience === "some") {
            scores.csharp += 3;
            scores.python += 3;
            scores.javascript += 2;
        }

        if (state.experience === "challenge") {
            scores.cpp += 5;
            scores.rust += 5;
            scores.typescript += 2;
        }

        if (state.experience === "fast") {
            scores.htmlcss += 5;
            scores.python += 5;
            scores.csharp += 2;
        }

        return Object.entries(scores)
            .map(([key, score]) => ({ key, score, ...PATHS[key] }))
            .sort((a, b) => b.score - a.score || Number(b.available) - Number(a.available))
            .slice(0, 3);
    }

    function clearHighlights() {
        document.querySelectorAll("[data-language-finder-rank]").forEach(card => {
            card.removeAttribute("data-language-finder-rank");
            card.removeAttribute("data-language-finder-label");
        });
    }

    function highlightResults(results) {
        clearHighlights();

        results.forEach((result, index) => {
            const card = document.querySelector(
                `[data-cavecode-language="${result.key}"]`
            );

            if (!card) return;

            card.setAttribute("data-language-finder-rank", String(index + 1));
            card.setAttribute(
                "data-language-finder-label",
                index === 0 ? "Best match" : `Match ${index + 1}`
            );
        });
    }

    function optionButton(value, label, icon) {
        const button = document.createElement("button");
        button.type = "button";
        button.className = "adaptive-finder-option";
        button.dataset.value = value;
        button.innerHTML = `
            <span class="adaptive-finder-option__icon">
                <i data-lucide="${icon}"></i>
            </span>
            <span>${label}</span>
            <i data-lucide="chevron-right"></i>
        `;
        return button;
    }

    function renderQuestion(dialog) {
        const body = dialog.querySelector(".language-finder-dialog__body");
        const footer = dialog.querySelector(".language-finder-dialog__footer");
        const progress = dialog.querySelector("[data-adaptive-progress]");

        body.innerHTML = "";
        footer.innerHTML = "";
        progress.textContent = `Question ${state.step + 1} of 3`;

        let title;
        let description;
        let options;

        if (state.step === 0) {
            title = "What do you want to build?";
            description = "Start with the result that sounds most exciting or useful.";
            options = BUILD_OPTIONS;
        } else if (state.step === 1) {
            title = "What kind of project sounds best?";
            description = "This narrows the recommendation to the right kind of work.";
            options = SPECIALIZATIONS[state.build] || SPECIALIZATIONS.unsure;
        } else {
            title = "How do you want to learn?";
            description = "Your experience and preferred pace affect the best starting point.";
            options = EXPERIENCE_OPTIONS;
        }

        const heading = document.createElement("div");
        heading.className = "adaptive-finder-question";
        heading.innerHTML = `
            <h3>${title}</h3>
            <p>${description}</p>
        `;

        const grid = document.createElement("div");
        grid.className = "adaptive-finder-options";

        options.forEach(([value, label, icon]) => {
            const button = optionButton(value, label, icon);

            button.addEventListener("click", () => {
                if (state.step === 0) state.build = value;
                if (state.step === 1) state.specialization = value;
                if (state.step === 2) state.experience = value;

                if (state.step < 2) {
                    state.step += 1;
                    renderQuestion(dialog);
                } else {
                    renderResults(dialog);
                }
            });

            grid.appendChild(button);
        });

        body.append(heading, grid);

        if (state.step > 0) {
            const back = document.createElement("button");
            back.type = "button";
            back.className = "language-finder-action language-finder-action--secondary";
            back.textContent = "Back";
            back.addEventListener("click", () => {
                state.step -= 1;
                renderQuestion(dialog);
            });
            footer.appendChild(back);
        }

        refreshIcons();
    }

    function renderResults(dialog) {
        const body = dialog.querySelector(".language-finder-dialog__body");
        const footer = dialog.querySelector(".language-finder-dialog__footer");
        const progress = dialog.querySelector("[data-adaptive-progress]");
        const results = scorePaths();

        progress.textContent = "Your recommendation";
        body.innerHTML = "";
        footer.innerHTML = "";

        const heading = document.createElement("div");
        heading.className = "adaptive-finder-question";
        heading.innerHTML = `
            <h3>Your best learning path</h3>
            <p>CaveCode matched your goals, project type, and preferred difficulty.</p>
        `;

        const resultsWrap = document.createElement("div");
        resultsWrap.className = "adaptive-finder-results";

        results.forEach((result, index) => {
            const item = document.createElement("article");
            item.className = "adaptive-finder-result";
            item.dataset.rank = String(index + 1);
            item.innerHTML = `
                <span class="adaptive-finder-result__rank">${index + 1}</span>
                <span class="adaptive-finder-result__icon">
                    <i data-lucide="${result.icon}"></i>
                </span>
                <span class="adaptive-finder-result__copy">
                    <strong>${result.course}</strong>
                    <small>${result.reason}</small>
                    <em>${result.available ? "Available now" : "Coming soon"}</em>
                </span>
            `;
            resultsWrap.appendChild(item);
        });

        body.append(heading, resultsWrap);

        const restart = document.createElement("button");
        restart.type = "button";
        restart.className = "language-finder-action language-finder-action--secondary";
        restart.textContent = "Start over";
        restart.addEventListener("click", () => {
            state = { step: 0, build: null, specialization: null, experience: null };
            renderQuestion(dialog);
        });

        const apply = document.createElement("button");
        apply.type = "button";
        apply.className = "language-finder-action";
        apply.textContent = "Highlight my matches";
        apply.addEventListener("click", () => {
            highlightResults(results);
            closeFinder();

            const best = document.querySelector(
                `[data-cavecode-language="${results[0].key}"]`
            );
            best?.scrollIntoView({ behavior: "smooth", block: "center" });
        });

        footer.append(restart, apply);
        refreshIcons();
    }

    function buildDialog() {
        let overlay = document.querySelector(".language-finder-overlay");

        if (overlay) return overlay;

        overlay = document.createElement("div");
        overlay.className = "language-finder-overlay";
        overlay.dataset.open = "false";
        overlay.innerHTML = `
            <section class="language-finder-dialog"
                     role="dialog"
                     aria-modal="true"
                     aria-labelledby="language-finder-title">
                <header class="language-finder-dialog__header">
                    <div>
                        <span class="adaptive-finder-progress"
                              data-adaptive-progress>Question 1 of 3</span>
                        <h2 id="language-finder-title">Find your CaveCode path</h2>
                    </div>
                    <button type="button"
                            class="language-finder-close"
                            aria-label="Close language finder">
                        <i data-lucide="x"></i>
                    </button>
                </header>
                <div class="language-finder-dialog__body"></div>
                <footer class="language-finder-dialog__footer"></footer>
            </section>
        `;

        overlay.querySelector(".language-finder-close")
            .addEventListener("click", closeFinder);

        overlay.addEventListener("click", event => {
            if (event.target === overlay) closeFinder();
        });

        document.body.appendChild(overlay);
        return overlay;
    }

    function openFinder({ automatic = false } = {}) {
        const overlay = buildDialog();

        state = {
            step: 0,
            build: null,
            specialization: null,
            experience: null
        };

        renderQuestion(overlay.querySelector(".language-finder-dialog"));
        overlay.dataset.open = "true";
        document.body.classList.add("language-finder-open");

        if (automatic) {
            try {
                localStorage.setItem(AUTO_PROMPT_KEY, "shown");
            } catch {
                // The finder still works if storage is unavailable.
            }
        }

        refreshIcons();
    }

    function closeFinder() {
        const overlay = document.querySelector(".language-finder-overlay");
        if (overlay) overlay.dataset.open = "false";
        document.body.classList.remove("language-finder-open");
    }

    function shouldAutoPrompt() {
        try {
            return localStorage.getItem(AUTO_PROMPT_KEY) !== "shown";
        } catch {
            return true;
        }
    }

    function installAutomaticPrompt() {
        const section = document.querySelector("#paths");

        if (!section || !shouldAutoPrompt()) return;

        const observer = new IntersectionObserver(entries => {
            const visible = entries.some(
                entry => entry.isIntersecting && entry.intersectionRatio >= 0.18
            );

            if (!visible || !shouldAutoPrompt()) return;

            observer.disconnect();
            window.setTimeout(
                () => openFinder({ automatic: true }),
                260
            );
        }, {
            threshold: [0.18, 0.35]
        });

        observer.observe(section);
    }

    // CAVECODE_LANGUAGE_FINDER_CLOSE_BUTTON_81D4
    function start() {
        document.addEventListener("click", event => {
            const closeButton = event.target.closest(".language-finder-close");

            if (closeButton) {
                event.preventDefault();
                event.stopPropagation();
                closeFinder();
                return;
            }

            const openOverlay = document.querySelector(
                '.language-finder-overlay[data-open="true"]'
            );

            if (openOverlay && event.target === openOverlay) {
                event.preventDefault();
                closeFinder();
                return;
            }

            const launcher = event.target.closest(
                "[data-language-finder-open]"
            );

            if (!launcher) return;

            event.preventDefault();
            openFinder({ automatic: false });
        });

        document.addEventListener("keydown", event => {
            if (event.key !== "Escape") return;

            const overlay = document.querySelector(
                '.language-finder-overlay[data-open="true"]'
            );

            if (!overlay) return;

            event.preventDefault();
            closeFinder();
        });

        installAutomaticPrompt();
        refreshIcons();
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", start, { once: true });
    } else {
        start();
    }
})();
