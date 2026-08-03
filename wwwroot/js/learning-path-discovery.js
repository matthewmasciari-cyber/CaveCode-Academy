(() => {
    "use strict";

    const PASS_NAME = "learning-path-discovery-v1.1";
    const STORAGE_KEY = "cavecode:path-discovery:v1";
    const SESSION_OPEN_KEY = "cavecode:path-discovery:auto-opened:v1";
    const DESKTOP_BREAKPOINT = 820;

    const paths = {
        csharp: {
            mark: "C#",
            title: "C# Cave Adventure",
            description: "Build a growing cave-exploration game while mastering C# foundations, logic, collections, classes, and combat.",
            tags: ["40 modules", "8-stage practice", "Live game preview"],
            available: true,
            href: "/csharp",
            action: "Enter the cave →",
            reason: "A strong match for game development, structured applications, and learning object-oriented programming.",
            uses: [
                ["gamepad-2", "Unity game development"],
                ["monitor", "Windows desktop applications"],
                ["globe-2", "ASP.NET websites and web APIs"],
                ["cloud", "Enterprise and cloud software"],
                ["smartphone", "Cross-platform .NET applications"],
                ["blocks", "Tools, simulations, and business systems"]
            ],
            examples: "RimWorld, Stardew Valley, Hollow Knight, Cities: Skylines, Lethal Company, Subnautica"
        },
        python: {
            mark: "Py",
            title: "Python Automation Quest",
            description: "Restore an underground facility while learning Python through sensors, alarms, sequences, data, files, and Raspberry Pi concepts.",
            tags: ["40 modules", "Automation simulation", "Optional hardware path"],
            available: true,
            href: "/python",
            action: "Enter the control room →",
            reason: "A beginner-friendly route into automation, AI, data, scripting, and hardware projects.",
            uses: [
                ["bot", "Artificial intelligence and machine learning"],
                ["cog", "Automation and scripting"],
                ["chart-no-axes-combined", "Data analysis and visualization"],
                ["building-2", "Building automation and BMS integrations"],
                ["shield-check", "Cybersecurity and administration tools"],
                ["cpu", "Robotics and Raspberry Pi projects"]
            ],
            examples: "Instagram, Dropbox, Home Assistant, TensorFlow, PyTorch"
        },
        javascript: {
            mark: "JS",
            title: "JavaScript Web Forge",
            description: "Create interactive websites and browser games while learning the language of the modern web.",
            tags: ["Web apps", "Browser games", "Interfaces"],
            available: false,
            reason: "The direct route into interactive websites, browser experiences, and full-stack JavaScript projects.",
            uses: [
                ["globe-2", "Interactive websites"],
                ["gamepad-2", "Browser games"],
                ["panels-top-left", "Front-end web applications"],
                ["server", "Node.js servers and APIs"],
                ["smartphone", "Mobile and desktop applications"],
                ["puzzle", "Browser extensions and interface tools"]
            ],
            examples: "Google Maps web features, Discord web, Netflix interfaces, interactive pages across the web"
        },
        sql: {
            mark: "SQL",
            title: "SQL Database Dungeon",
            description: "Master queries by managing players, items, quests, and persistent world data.",
            tags: ["Queries", "Databases", "Game data"],
            available: false,
            reason: "Ideal for understanding how applications store, search, connect, and report on real information.",
            uses: [
                ["database", "Application data storage and retrieval"],
                ["chart-no-axes-combined", "Reports, dashboards, and analytics"],
                ["gamepad-2", "Player accounts, inventories, and save data"],
                ["building-2", "Business and enterprise databases"],
                ["server", "Website and application backends"],
                ["search", "Filtering and combining large data sets"]
            ],
            examples: "PostgreSQL, Microsoft SQL Server, MySQL, SQLite, Oracle Database"
        },
        htmlcss: {
            mark: "HTML",
            title: "HTML & CSS Workshop",
            description: "Build the structure and visual systems behind polished websites and game interfaces.",
            tags: ["Layouts", "Responsive design", "UI styling"],
            available: false,
            reason: "The most visual starting point for learning how websites are structured, styled, and adapted to every screen.",
            uses: [
                ["layout-template", "Website structure and page layout"],
                ["palette", "Visual design, themes, and animation"],
                ["smartphone", "Responsive phone and tablet layouts"],
                ["panels-top-left", "User interfaces and landing pages"],
                ["mail", "Styled email templates"],
                ["gamepad-2", "Browser-game menus and HUDs"]
            ],
            examples: "The visible structure and styling behind nearly every website"
        },
        typescript: {
            mark: "TS",
            title: "TypeScript Application Architect",
            description: "Scale JavaScript into dependable applications by adding types, structure, reusable systems, and safer team workflows.",
            tags: ["Typed JavaScript", "Large applications", "Modern frameworks"],
            available: false,
            reason: "Best for learners interested in large web applications and safer, more maintainable JavaScript projects.",
            uses: [
                ["blocks", "Large, maintainable web applications"],
                ["component", "React, Angular, and Vue projects"],
                ["server", "Node.js servers and APIs"],
                ["smartphone", "Cross-platform applications"],
                ["cloud", "Cloud services and developer tools"],
                ["badge-check", "Safer team projects through type checking"]
            ],
            examples: "Visual Studio Code, Angular, Deno, and modern large-scale web projects"
        },
        java: {
            mark: "Java",
            title: "Java Enterprise Expedition",
            description: "Build durable applications while learning the language behind Android systems, business software, and large backends.",
            tags: ["Android", "Enterprise", "Back-end systems"],
            available: false,
            reason: "A strong route into Android, enterprise software, and large back-end systems.",
            uses: [
                ["smartphone", "Android applications"],
                ["building-2", "Enterprise business software"],
                ["server", "Large web backends and APIs"],
                ["cloud", "Cloud and distributed systems"],
                ["gamepad-2", "Desktop games and tools"],
                ["wrench", "Developer platforms and build systems"]
            ],
            examples: "Minecraft: Java Edition, IntelliJ IDEA, Jenkins, Hadoop"
        },
        cpp: {
            mark: "C++",
            title: "C++ Engine Foundry",
            description: "Work close to the machine while building high-performance systems, simulations, engines, and real-time software.",
            tags: ["Unreal Engine", "Systems", "Performance"],
            available: false,
            reason: "The advanced choice for game engines, performance-heavy software, embedded systems, and low-level control.",
            uses: [
                ["gamepad-2", "AAA game development"],
                ["hammer", "Unreal Engine and custom engines"],
                ["monitor-cog", "Operating systems and system software"],
                ["microchip", "Embedded and real-time systems"],
                ["rocket", "High-performance applications"],
                ["orbit", "Robotics, graphics, and simulation"]
            ],
            examples: "Unreal Engine projects, Counter-Strike 2, Half-Life 2, performance-heavy desktop software"
        },
        go: {
            mark: "Go",
            title: "Go Cloud Command",
            description: "Build fast network services and infrastructure tools while learning clear, practical concurrent programming.",
            tags: ["Cloud", "DevOps", "Distributed systems"],
            available: false,
            reason: "A practical choice for cloud infrastructure, networking, DevOps, and efficient back-end services.",
            uses: [
                ["cloud", "Cloud infrastructure"],
                ["server", "Fast web services and APIs"],
                ["terminal", "DevOps command-line tools"],
                ["network", "Networking and distributed systems"],
                ["package", "Containers and deployment platforms"],
                ["activity", "Monitoring and back-end services"]
            ],
            examples: "Docker, Kubernetes, Terraform, Prometheus"
        },
        rust: {
            mark: "Rust",
            title: "Rust Systems Frontier",
            description: "Explore memory-safe systems programming through reliable tools, embedded projects, WebAssembly, and performance work.",
            tags: ["Memory safety", "Systems", "WebAssembly"],
            available: false,
            reason: "An advanced route into safe systems programming, WebAssembly, embedded software, and high-performance tools.",
            uses: [
                ["zap", "Fast, memory-safe software"],
                ["monitor-cog", "Systems programming"],
                ["terminal", "Command-line tools"],
                ["globe-2", "WebAssembly applications"],
                ["microchip", "Embedded development"],
                ["gamepad-2", "Game engines and performance tools"]
            ],
            examples: "Firefox components, ripgrep, parts of Deno, Linux kernel support"
        }
    };

    const pathOrder = ["csharp", "python", "javascript", "sql", "htmlcss", "typescript", "java", "cpp", "go", "rust"];

    const questions = [
        {
            question: "What do you most want to build?",
            description: "Choose the result that would make learning code feel worthwhile.",
            options: [
                { id: "games", icon: "gamepad-2", title: "Games and interactive worlds", detail: "Gameplay, engines, simulations, and creative systems", scores: { csharp: 8, cpp: 7, javascript: 3, rust: 2 } },
                { id: "web", icon: "globe-2", title: "Websites and applications", detail: "Interfaces, full web apps, services, and online products", scores: { javascript: 8, htmlcss: 8, typescript: 7, csharp: 3, java: 2 } },
                { id: "automation", icon: "cog", title: "Automation and hardware", detail: "Scripts, controls, sensors, robotics, and real-world tasks", scores: { python: 9, cpp: 4, rust: 4, go: 2 } },
                { id: "data", icon: "database", title: "Data and connected systems", detail: "Databases, analytics, cloud services, and infrastructure", scores: { sql: 9, python: 6, go: 7, java: 4, typescript: 2 } }
            ]
        },
        {
            question: "Which kind of progress sounds best?",
            description: "Think about what you would enjoy seeing improve from lesson to lesson.",
            options: [
                { id: "visual", icon: "palette", title: "Visible results quickly", detail: "Pages, interfaces, animation, and things I can immediately see", scores: { htmlcss: 8, javascript: 7, csharp: 3, typescript: 2 } },
                { id: "practical", icon: "wrench", title: "Solving practical problems", detail: "Automating work and making useful tools", scores: { python: 8, csharp: 4, go: 4, sql: 3 } },
                { id: "systems", icon: "microchip", title: "Understanding how systems work", detail: "Memory, hardware, performance, and low-level control", scores: { cpp: 8, rust: 8, java: 3, go: 3 } },
                { id: "organized", icon: "blocks", title: "Building reliable structures", detail: "Large applications, databases, and maintainable systems", scores: { typescript: 7, java: 7, sql: 6, csharp: 5, go: 4 } }
            ]
        },
        {
            question: "How technical should your first path feel?",
            description: "There is no wrong choice. This only changes the recommended starting point.",
            options: [
                { id: "gentle", icon: "feather", title: "Give me the gentlest start", detail: "Clear syntax and early wins matter most", scores: { python: 9, htmlcss: 8, javascript: 5, sql: 4 } },
                { id: "balanced", icon: "scale", title: "Balanced and structured", detail: "I want challenge, but with a clear learning curve", scores: { csharp: 8, javascript: 5, java: 5, typescript: 4, sql: 3 } },
                { id: "deep", icon: "mountain", title: "I want the deep challenge", detail: "I am comfortable earning progress slowly", scores: { cpp: 9, rust: 9, java: 4, go: 4 } },
                { id: "career", icon: "briefcase-business", title: "Prioritize professional usefulness", detail: "Aim toward widely used workplace systems", scores: { csharp: 7, python: 7, java: 7, typescript: 7, sql: 6, go: 5 } }
            ]
        },
        {
            question: "Where do you picture using these skills?",
            description: "Your answer helps distinguish similar paths with different real-world strengths.",
            options: [
                { id: "personal", icon: "lightbulb", title: "Personal projects and experiments", detail: "Flexible tools I can use for many ideas", scores: { python: 7, javascript: 5, htmlcss: 5, csharp: 4 } },
                { id: "business", icon: "building-2", title: "Business and workplace systems", detail: "Applications, automation, reporting, and operations", scores: { csharp: 7, python: 6, sql: 7, java: 6, typescript: 5 } },
                { id: "creative", icon: "sparkles", title: "Creative and game projects", detail: "Games, visual interfaces, and interactive experiences", scores: { csharp: 8, cpp: 6, javascript: 5, htmlcss: 4 } },
                { id: "infrastructure", icon: "network", title: "Infrastructure and engineering", detail: "Cloud, systems, devices, and reliable technical platforms", scores: { go: 8, rust: 7, cpp: 6, java: 4, python: 4 } }
            ]
        }
    ];

    let grid;
    let section;
    let modal;
    let currentStep = 0;
    let answers = [];
    let recommendations = [];
    let previousFocus = null;
    let equalizeTimer = 0;
    let mutationTimer = 0;
    let pageTimer = 0;
    let initializedGrid = null;
    let globalEventsBound = false;

    function normalize(value) {
        return String(value || "")
            .toLowerCase()
            .replace(/\u00a0/g, " ")
            .replace(/[–—]/g, "-")
            .replace(/\s+/g, " ")
            .trim();
    }

    function safeReadState() {
        try {
            const parsed = JSON.parse(localStorage.getItem(STORAGE_KEY) || "null");
            return parsed && typeof parsed === "object" ? parsed : null;
        } catch {
            return null;
        }
    }

    function saveState(state) {
        try {
            localStorage.setItem(STORAGE_KEY, JSON.stringify({ ...state, version: PASS_NAME, updatedAt: new Date().toISOString() }));
        } catch {
            // The experience still works when browser storage is unavailable.
        }
    }

    function icon(name, className = "") {
        const element = document.createElement("i");
        element.setAttribute("data-lucide", name);
        if (className) element.className = className;
        element.setAttribute("aria-hidden", "true");
        return element;
    }

    function refreshIcons(root = document) {
        if (!window.lucide || typeof window.lucide.createIcons !== "function") return;
        try {
            window.lucide.createIcons({ attrs: { "aria-hidden": "true" } });
        } catch {
            try { window.lucide.createIcons(); } catch { /* no-op */ }
        }
    }

    function identifyCard(card) {
        const existing = normalize(card.dataset.cavecodeLanguage || card.dataset.language);
        if (paths[existing]) return existing;

        const mark = normalize(card.querySelector(".language-mark")?.textContent);
        const title = normalize(card.querySelector("h3")?.textContent);
        const combined = `${mark} ${title}`;

        if (combined.includes("c++") || combined.includes("engine foundry")) return "cpp";
        if (combined.includes("c#") || combined.includes("csharp") || combined.includes("cave adventure")) return "csharp";
        if (combined.includes("typescript")) return "typescript";
        if (combined.includes("javascript")) return "javascript";
        if (combined.includes("python")) return "python";
        if (combined.includes("sql")) return "sql";
        if (combined.includes("html") || combined.includes("css")) return "htmlcss";
        if (/\bjava\b/.test(combined)) return "java";
        if (/\brust\b/.test(combined)) return "rust";
        if (/\bgo\b/.test(combined) || combined.includes("cloud command")) return "go";
        return null;
    }

    function createInjectedCard(key) {
        const profile = paths[key];
        const card = document.createElement("article");
        card.className = "path-card locked path-card--injected";
        card.dataset.cavecodeLanguage = key;

        const topline = document.createElement("div");
        topline.className = "path-topline";

        const mark = document.createElement("span");
        mark.className = "language-mark";
        mark.textContent = profile.mark;

        const status = document.createElement("span");
        status.className = "status locked-status";
        status.append(icon("lock-keyhole"), document.createTextNode(" Coming soon"));

        topline.append(mark, status);

        const title = document.createElement("h3");
        title.textContent = profile.title;

        const description = document.createElement("p");
        description.textContent = profile.description;

        const tags = document.createElement("div");
        tags.className = "skill-tags";
        profile.tags.forEach(tag => {
            const span = document.createElement("span");
            span.textContent = tag;
            tags.appendChild(span);
        });

        const action = document.createElement("button");
        action.className = "path-action locked-action";
        action.type = "button";
        action.disabled = true;
        action.textContent = "Course in development";

        card.append(topline, title, description, tags, action);
        return card;
    }

    function createUsesPanel(key) {
        const profile = paths[key];
        const panel = document.createElement("section");
        panel.className = "real-world-uses";
        panel.dataset.cavecodeLanguage = key;
        panel.setAttribute("aria-label", `${profile.title} real-world uses`);

        const heading = document.createElement("h4");
        heading.className = "real-world-uses__heading";

        const headingIcon = document.createElement("span");
        headingIcon.className = "real-world-uses__heading-icon";
        headingIcon.appendChild(icon("waypoints"));
        heading.append(headingIcon, document.createTextNode("What this language is used for"));

        const list = document.createElement("ul");
        list.className = "real-world-uses__list";

        profile.uses.slice(0, 6).forEach(([iconName, text]) => {
            const item = document.createElement("li");
            item.className = "real-world-uses__item";

            const tile = document.createElement("span");
            tile.className = "real-world-uses__icon-tile";
            tile.appendChild(icon(iconName));

            const label = document.createElement("span");
            label.textContent = text;

            item.append(tile, label);
            list.appendChild(item);
        });

        const examples = document.createElement("p");
        examples.className = "real-world-uses__examples";
        const strong = document.createElement("strong");
        strong.textContent = "Famous examples: ";
        examples.append(strong, document.createTextNode(profile.examples));

        panel.append(heading, list, examples);
        return panel;
    }

    function insertUsesPanel(card, key) {
        // CAVECODE_STABLE_CARD_RECONCILIATION:
        // Reuse an already-correct panel instead of removing and rebuilding it.
        const existing = card.querySelector(":scope > .real-world-uses");
        if (
            existing &&
            existing.dataset.cavecodeLanguage === key &&
            existing.dataset.cavecodePanelReady === "true"
        ) {
            return existing;
        }

        card.querySelectorAll(":scope > .real-world-uses").forEach(element => element.remove());

        const panel = createUsesPanel(key);
        panel.dataset.cavecodePanelReady = "true";

        const anchor = card.querySelector(":scope > .skill-tags");
        if (anchor) card.insertBefore(panel, anchor);
        else card.appendChild(panel);

        return panel;
    }

    function normalizeExistingCard(card, key) {
        const profile = paths[key];
        card.dataset.cavecodeLanguage = key;

        const status = card.querySelector(".locked-status");
        if (status && status.dataset.cavecodeStatus !== "ready") {
            status.replaceChildren(icon("lock-keyhole"), document.createTextNode(" Coming soon"));
            status.dataset.cavecodeStatus = "ready";
        }

        insertUsesPanel(card, key);
    }

    function ensureCards() {
        const cardsByKey = new Map();
        grid.querySelectorAll(":scope > .path-card").forEach(card => {
            const key = identifyCard(card);
            if (key && !cardsByKey.has(key)) {
                normalizeExistingCard(card, key);
                cardsByKey.set(key, card);
            }
        });

        pathOrder.forEach(key => {
            if (!cardsByKey.has(key)) {
                const card = createInjectedCard(key);
                insertUsesPanel(card, key);
                grid.appendChild(card);
                cardsByKey.set(key, card);
            }
        });

        // Reorder only when the current order differs from the desired order.
        // Re-appending every card on every observer pass caused the injected
        // TypeScript, Java, C++, Go, and Rust cards to flash continuously.
        const desiredCards = pathOrder
            .map(key => cardsByKey.get(key))
            .filter(Boolean);

        const currentCards = [
            ...grid.querySelectorAll(":scope > .path-card[data-cavecode-language]")
        ];

        const desiredOrder = desiredCards
            .map(card => card.dataset.cavecodeLanguage)
            .join("|");

        const currentOrder = currentCards
            .map(card => card.dataset.cavecodeLanguage)
            .join("|");

        if (currentOrder !== desiredOrder) {
            const fragment = document.createDocumentFragment();
            desiredCards.forEach(card => fragment.appendChild(card));
            grid.appendChild(fragment);
        }

        const count = section.querySelector(".path-count");
        if (count) count.textContent = "2 available · 8 in development · 10 total";

        refreshIcons(grid);
        scheduleEqualize();
    }

    function createControls() {
        const existing = section.querySelector(".path-discovery-controls");
        if (existing) return existing;

        const controls = document.createElement("div");
        controls.className = "path-discovery-controls";
        controls.innerHTML = `
            <div class="path-discovery-controls__copy">
                <p class="path-discovery-controls__eyebrow">Not sure where to begin?</p>
                <h3>Let CaveCode match you with a learning path.</h3>
                <p>Answer four short questions, or reveal all ten paths immediately.</p>
            </div>
            <div class="path-discovery-controls__actions">
                <button class="path-discovery-button path-discovery-button--primary" type="button" data-path-action="open-wizard"><i data-lucide="wand-sparkles"></i>Help me choose</button>
                <button class="path-discovery-button" type="button" data-path-action="show-all"><i data-lucide="layout-grid"></i>Show all 10 paths</button>
            </div>`;

        const heading = section.querySelector(".section-heading");
        if (heading?.nextSibling) heading.parentNode.insertBefore(controls, heading.nextSibling);
        else section.insertBefore(controls, grid);

        controls.addEventListener("click", event => {
            const button = event.target.closest("[data-path-action]");
            if (!button) return;
            if (button.dataset.pathAction === "open-wizard") openWizard(true);
            if (button.dataset.pathAction === "show-all") revealAll("all");
        });

        refreshIcons(controls);
        return controls;
    }

    function updateControlsForState(state) {
        const controls = createControls();
        const copy = controls.querySelector(".path-discovery-controls__copy");
        const actions = controls.querySelector(".path-discovery-controls__actions");

        if (state?.completed || state?.mode === "all") {
            copy.innerHTML = `
                <p class="path-discovery-controls__eyebrow">All ten paths are visible</p>
                <h3>${state?.completed ? "Your recommendations are highlighted below." : "Compare every CaveCode learning path."}</h3>
                <p>You can retake the quiz whenever your goals change.</p>`;
            actions.innerHTML = `
                <button class="path-discovery-button path-discovery-button--primary" type="button" data-path-action="open-wizard"><i data-lucide="rotate-ccw"></i>Retake path quiz</button>`;
        }

        refreshIcons(controls);
    }

    function revealAll(mode = "all", rankedKeys = []) {
        grid.classList.remove("path-grid--gated");
        grid.classList.add("path-grid--revealed");
        grid.removeAttribute("aria-hidden");

        applyRecommendationRanks(rankedKeys);

        if (mode === "all") {
            saveState({ mode: "all", completed: false, recommendations: [] });
            updateControlsForState({ mode: "all", completed: false });
        }

        document.documentElement.classList.remove("path-discovery-pending");
        scheduleEqualize();
    }

    function gateGrid() {
        grid.classList.add("path-grid--gated");
        grid.classList.remove("path-grid--revealed");
        grid.setAttribute("aria-hidden", "true");
        document.documentElement.classList.remove("path-discovery-pending");
    }

    function createModal() {
        if (modal) return modal;

        modal = document.createElement("div");
        modal.className = "path-wizard-backdrop";
        modal.hidden = true;
        modal.innerHTML = `
            <div class="path-wizard-dialog" role="dialog" aria-modal="true" aria-labelledby="path-wizard-title">
                <header class="path-wizard-header">
                    <div class="path-wizard-brand">
                        <span class="path-wizard-brand__mark"><i data-lucide="map"></i></span>
                        <span><strong>CaveCode Path Finder</strong><small>Four questions · about one minute</small></span>
                    </div>
                    <button class="path-wizard-close" type="button" aria-label="Close path finder"><i data-lucide="x"></i></button>
                </header>
                <div class="path-wizard-body"></div>
            </div>`;

        document.body.appendChild(modal);
        modal.querySelector(".path-wizard-close").addEventListener("click", closeWizard);
        modal.addEventListener("click", event => {
            if (event.target === modal) closeWizard();
        });
        document.addEventListener("keydown", event => {
            if (event.key === "Escape" && modal && !modal.hidden) closeWizard();
        });
        refreshIcons(modal);
        return modal;
    }

    function openWizard(reset = false) {
        createModal();
        if (reset) {
            currentStep = 0;
            answers = [];
            recommendations = [];
        }
        previousFocus = document.activeElement;
        modal.hidden = false;
        document.body.classList.add("path-wizard-open");
        renderWizard();
        requestAnimationFrame(() => modal.querySelector("button")?.focus());
    }

    function closeWizard() {
        if (!modal) return;
        modal.hidden = true;
        document.body.classList.remove("path-wizard-open");
        if (previousFocus instanceof HTMLElement) previousFocus.focus();
    }

    function progressMarkup() {
        return `<div class="path-wizard-progress" aria-label="Quiz progress">${questions.map((_, index) => {
            const className = index < currentStep ? "is-complete" : index === currentStep ? "is-current" : "";
            return `<span class="${className}"></span>`;
        }).join("")}</div>`;
    }

    function renderWizard() {
        const body = modal.querySelector(".path-wizard-body");
        if (currentStep >= questions.length) {
            renderResults(body);
            return;
        }

        const question = questions[currentStep];
        body.innerHTML = `
            ${progressMarkup()}
            <section class="path-wizard-step">
                <p class="path-wizard-step__eyebrow">Question ${currentStep + 1} of ${questions.length}</p>
                <h2 id="path-wizard-title">${question.question}</h2>
                <p class="path-wizard-step__description">${question.description}</p>
                <div class="path-wizard-options"></div>
                <footer class="path-wizard-footer">
                    <button class="path-discovery-button" type="button" data-wizard-action="back" ${currentStep === 0 ? "disabled" : ""}><i data-lucide="arrow-left"></i>Back</button>
                    <div class="path-wizard-footer__right">
                        <button class="path-discovery-button" type="button" data-wizard-action="show-all">Show all paths</button>
                        <button class="path-discovery-button path-discovery-button--primary" type="button" data-wizard-action="next" ${answers[currentStep] ? "" : "disabled"}>${currentStep === questions.length - 1 ? "See my matches" : "Next"}<i data-lucide="arrow-right"></i></button>
                    </div>
                </footer>
            </section>`;

        const options = body.querySelector(".path-wizard-options");
        question.options.forEach(option => {
            const button = document.createElement("button");
            button.className = `path-wizard-option${answers[currentStep]?.id === option.id ? " is-selected" : ""}`;
            button.type = "button";
            button.innerHTML = `
                <span class="path-wizard-option__icon"><i data-lucide="${option.icon}"></i></span>
                <span><strong>${option.title}</strong><small>${option.detail}</small></span>`;
            button.addEventListener("click", () => {
                answers[currentStep] = option;
                renderWizard();
            });
            options.appendChild(button);
        });

        body.querySelector("[data-wizard-action='back']")?.addEventListener("click", () => {
            if (currentStep > 0) currentStep -= 1;
            renderWizard();
        });

        body.querySelector("[data-wizard-action='show-all']")?.addEventListener("click", () => {
            revealAll("all");
            closeWizard();
            section.scrollIntoView({ behavior: "smooth", block: "start" });
        });

        body.querySelector("[data-wizard-action='next']")?.addEventListener("click", () => {
            if (!answers[currentStep]) return;
            currentStep += 1;
            renderWizard();
        });

        refreshIcons(body);
    }

    function calculateRecommendations() {
        const scores = Object.fromEntries(pathOrder.map(key => [key, 0]));
        answers.forEach(answer => {
            Object.entries(answer?.scores || {}).forEach(([key, score]) => {
                if (key in scores) scores[key] += score;
            });
        });

        // A small bonus keeps immediately available courses competitive without
        // overriding a clearly better subject match.
        scores.csharp += 1;
        scores.python += 1;

        return pathOrder
            .map(key => ({ key, score: scores[key] }))
            .sort((a, b) => b.score - a.score || pathOrder.indexOf(a.key) - pathOrder.indexOf(b.key))
            .slice(0, 3)
            .map(item => item.key);
    }

    function renderResults(body) {
        recommendations = calculateRecommendations();
        const top = paths[recommendations[0]];

        body.innerHTML = `
            <section class="path-wizard-step">
                <p class="path-wizard-step__eyebrow">Your CaveCode matches</p>
                <h2 id="path-wizard-title">Start with ${top.title}.</h2>
                <p class="path-wizard-step__description">${top.reason} Your next two matches are included so you can compare before committing.</p>
                <div class="path-wizard-results"></div>
                <footer class="path-wizard-footer">
                    <button class="path-discovery-button" type="button" data-result-action="back"><i data-lucide="arrow-left"></i>Change answers</button>
                    <div class="path-wizard-footer__right">
                        <button class="path-discovery-button path-discovery-button--primary" type="button" data-result-action="view"><i data-lucide="sparkles"></i>View my recommended paths</button>
                    </div>
                </footer>
            </section>`;

        const results = body.querySelector(".path-wizard-results");
        recommendations.forEach((key, index) => {
            const profile = paths[key];
            const row = document.createElement("div");
            row.className = "path-wizard-result";
            row.innerHTML = `
                <span class="path-wizard-result__mark">${profile.mark}</span>
                <span><strong>${index + 1}. ${profile.title}</strong><small>${profile.reason}</small></span>
                <span class="path-wizard-result__status${profile.available ? " is-available" : ""}">${profile.available ? "Available now" : "Coming soon"}</span>`;
            results.appendChild(row);
        });

        body.querySelector("[data-result-action='back']").addEventListener("click", () => {
            currentStep = questions.length - 1;
            renderWizard();
        });

        body.querySelector("[data-result-action='view']").addEventListener("click", () => {
            saveState({
                mode: "recommended",
                completed: true,
                answers: answers.map(answer => answer.id),
                recommendations
            });
            revealAll("recommended", recommendations);
            updateControlsForState({ completed: true, recommendations });
            closeWizard();
            section.scrollIntoView({ behavior: "smooth", block: "start" });
        });

        refreshIcons(body);
    }

    function applyRecommendationRanks(keys = []) {
        grid.querySelectorAll(".path-recommendation-rank").forEach(element => element.remove());
        grid.querySelectorAll("[data-cavecode-recommendation-rank]").forEach(card => {
            card.removeAttribute("data-cavecode-recommendation-rank");
        });

        keys.slice(0, 3).forEach((key, index) => {
            const card = grid.querySelector(`[data-cavecode-language="${key}"]`);
            if (!card) return;
            card.dataset.cavecodeRecommendationRank = String(index + 1);
            const badge = document.createElement("span");
            badge.className = "path-recommendation-rank";
            badge.append(icon(index === 0 ? "trophy" : "sparkles"), document.createTextNode(index === 0 ? "Best match" : `Match ${index + 1}`));
            card.appendChild(badge);
        });
        refreshIcons(grid);
    }

    function equalizeCards() {
        if (!grid || grid.classList.contains("path-grid--gated")) return;
        const cards = [...grid.querySelectorAll(":scope > .path-card[data-cavecode-language]")];
        cards.forEach(card => { card.style.minHeight = ""; });
        if (window.innerWidth <= DESKTOP_BREAKPOINT) return;
        const maxHeight = Math.ceil(Math.max(...cards.map(card => card.scrollHeight), 0));
        if (!maxHeight) return;
        cards.forEach(card => { card.style.minHeight = `${maxHeight}px`; });
    }

    function scheduleEqualize() {
        clearTimeout(equalizeTimer);
        equalizeTimer = window.setTimeout(equalizeCards, 80);
    }

    function observeGrid() {
        const observer = new MutationObserver(mutations => {
            const relevant = mutations.some(mutation =>
                [...mutation.addedNodes].some(node => node instanceof Element && !node.closest?.(".real-world-uses"))
            );
            if (!relevant) {
                scheduleEqualize();
                return;
            }
            clearTimeout(mutationTimer);
            mutationTimer = window.setTimeout(() => {
                ensureCards();
            }, 100);
        });
        observer.observe(grid, { childList: true, subtree: true });
    }

    function initialize() {
        const nextSection = document.querySelector(".path-section#paths, #paths.path-section, .path-section");
        const nextGrid = nextSection?.querySelector(".path-grid");

        if (!nextSection || !nextGrid) {
            document.documentElement.classList.remove("path-discovery-pending");
            return false;
        }

        if (nextGrid === initializedGrid && nextGrid.isConnected) {
            return true;
        }

        section = nextSection;
        grid = nextGrid;
        initializedGrid = nextGrid;
        document.documentElement.classList.add("path-discovery-pending");

        ensureCards();
        createControls();
        createModal();
        observeGrid();

        const state = safeReadState();
        if (state?.completed || state?.mode === "all") {
            revealAll(state.mode || "recommended", state.recommendations || []);
            updateControlsForState(state);
        } else {
            gateGrid();
            updateControlsForState(null);
            let alreadyOpened = false;
            try { alreadyOpened = sessionStorage.getItem(SESSION_OPEN_KEY) === "1"; } catch { /* no-op */ }
            if (!alreadyOpened) {
                try { sessionStorage.setItem(SESSION_OPEN_KEY, "1"); } catch { /* no-op */ }
                window.setTimeout(() => openWizard(true), 300);
            }
        }

        if (!globalEventsBound) {
            globalEventsBound = true;
            window.addEventListener("resize", scheduleEqualize, { passive: true });
            window.addEventListener("pageshow", scheduleEqualize);
        }

        window.caveCodePathDiscovery = Object.freeze({
            version: PASS_NAME,
            open: () => openWizard(true),
            showAll: () => revealAll("all"),
            reset: () => {
                try { localStorage.removeItem(STORAGE_KEY); sessionStorage.removeItem(SESSION_OPEN_KEY); } catch { /* no-op */ }
                window.location.reload();
            },
            recognizedPaths: [...pathOrder]
        });

        refreshIcons(document);
        return true;
    }

    function start() {
        initialize();
        const pageObserver = new MutationObserver(() => {
            clearTimeout(pageTimer);
            pageTimer = window.setTimeout(() => initialize(), 40);
        });
        pageObserver.observe(document.documentElement, { childList: true, subtree: true });
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", start, { once: true });
    } else {
        start();
    }
})();
