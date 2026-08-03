(() => {
    "use strict";

    const PASS_VERSION = "learning-path-real-world-uses-v1";

    const languageProfiles = {
        python: {
            label: "Python",
            uses: [
                ["🤖", "Artificial intelligence and machine learning"],
                ["⚙️", "Automation and scripting"],
                ["📊", "Data analysis and visualization"],
                ["🏢", "Building automation and BMS integrations"],
                ["🔒", "Cybersecurity and administration tools"],
                ["🧰", "Robotics and Raspberry Pi projects"],
                ["🌐", "Web backends with Django, Flask, and FastAPI"]
            ],
            examples: "Instagram, Dropbox, Home Assistant, TensorFlow, PyTorch"
        },
        csharp: {
            label: "C#",
            uses: [
                ["🎮", "Unity game development"],
                ["🖥️", "Windows desktop applications"],
                ["🌐", "ASP.NET websites and web APIs"],
                ["☁️", "Enterprise and cloud software"],
                ["📱", "Cross-platform apps with .NET MAUI"],
                ["🧩", "Tools, simulations, and business systems"]
            ],
            examples: "RimWorld, Stardew Valley, Hollow Knight, Cities: Skylines, Lethal Company, Subnautica"
        },
        cpp: {
            label: "C++",
            uses: [
                ["🎮", "AAA game development"],
                ["🛠️", "Unreal Engine and custom game engines"],
                ["💻", "Operating systems and system software"],
                ["🔌", "Embedded systems and device software"],
                ["🚀", "High-performance applications"],
                ["🤖", "Robotics, graphics, and real-time simulation"]
            ],
            examples: "Baldur's Gate 3, Elden Ring, Counter-Strike 2, Half-Life 2, Cyberpunk 2077"
        },
        javascript: {
            label: "JavaScript",
            uses: [
                ["🌐", "Interactive websites"],
                ["🎮", "Browser games"],
                ["🧭", "Front-end web applications"],
                ["🖥️", "Node.js servers and APIs"],
                ["📱", "Mobile and desktop apps"],
                ["🧩", "Browser extensions and interface tools"]
            ],
            examples: "Google Maps web features, Discord web, Netflix interfaces, interactive pages across the web"
        },
        typescript: {
            label: "TypeScript",
            uses: [
                ["🏗️", "Large, maintainable web applications"],
                ["⚛️", "React, Angular, and Vue projects"],
                ["🖥️", "Node.js servers and APIs"],
                ["📱", "Cross-platform desktop and mobile apps"],
                ["☁️", "Cloud services and development tools"],
                ["🧪", "Safer team projects with type checking"]
            ],
            examples: "Visual Studio Code, Angular, Deno, modern web-development tooling"
        },
        htmlcss: {
            label: "HTML & CSS",
            uses: [
                ["🏗️", "Website structure and page layout"],
                ["🎨", "Visual design, themes, and animation"],
                ["📱", "Responsive phone and tablet layouts"],
                ["🧭", "User interfaces and landing pages"],
                ["✉️", "Styled email templates"],
                ["🎮", "Browser-game menus and HUDs"]
            ],
            examples: "The visible structure and styling behind nearly every website"
        },
        sql: {
            label: "SQL",
            uses: [
                ["🗄️", "Storing and retrieving application data"],
                ["📊", "Reports, dashboards, and analytics"],
                ["🎮", "Player accounts, inventories, and save data"],
                ["🏢", "Business and enterprise databases"],
                ["🌐", "Website and application backends"],
                ["🔎", "Searching, filtering, and combining large data sets"]
            ],
            examples: "PostgreSQL, Microsoft SQL Server, MySQL, SQLite, Oracle Database"
        },
        java: {
            label: "Java",
            uses: [
                ["📱", "Android applications"],
                ["🏢", "Enterprise business software"],
                ["🌐", "Back-end services and APIs"],
                ["☁️", "Large cloud and distributed systems"],
                ["🎮", "Desktop games and tools"],
                ["🔧", "Build tools and developer platforms"]
            ],
            examples: "Minecraft: Java Edition, IntelliJ IDEA, Jenkins, Hadoop"
        },
        rust: {
            label: "Rust",
            uses: [
                ["⚡", "Fast and memory-safe system software"],
                ["🧰", "Command-line tools"],
                ["🌐", "WebAssembly applications"],
                ["🔌", "Embedded and low-level development"],
                ["☁️", "Reliable servers and networking"],
                ["🎮", "Game engines and performance-heavy tools"]
            ],
            examples: "Firefox components, ripgrep, parts of Deno, Linux kernel support"
        },
        go: {
            label: "Go",
            uses: [
                ["☁️", "Cloud infrastructure"],
                ["🌐", "Fast web APIs and services"],
                ["🧰", "DevOps and command-line tools"],
                ["🔗", "Networking and distributed systems"],
                ["📦", "Containers and deployment platforms"],
                ["📈", "Monitoring and backend systems"]
            ],
            examples: "Docker, Kubernetes, Terraform, Prometheus"
        },
        c: {
            label: "C",
            uses: [
                ["💻", "Operating systems and kernels"],
                ["🔌", "Microcontrollers and embedded firmware"],
                ["🧠", "Memory-constrained software"],
                ["🧩", "Device drivers"],
                ["📡", "Networking and hardware interfaces"],
                ["⚙️", "Compilers and language runtimes"]
            ],
            examples: "Linux kernel, Git, SQLite, many embedded-device firmware systems"
        },
        swift: {
            label: "Swift",
            uses: [
                ["📱", "iPhone and iPad applications"],
                ["⌚", "Apple Watch applications"],
                ["🖥️", "macOS desktop software"],
                ["🥽", "visionOS applications"],
                ["🌐", "Server-side Swift services"],
                ["🎮", "Games and interactive Apple-platform apps"]
            ],
            examples: "Modern applications across Apple's iOS, macOS, watchOS, and visionOS platforms"
        },
        kotlin: {
            label: "Kotlin",
            uses: [
                ["📱", "Modern Android applications"],
                ["🌐", "Back-end services"],
                ["🖥️", "Cross-platform applications"],
                ["🏢", "Enterprise JVM software"],
                ["☁️", "Cloud services"],
                ["🧪", "Safer alternatives to older Java code"]
            ],
            examples: "Trello Android, Pinterest Android, many modern Android applications"
        },
        php: {
            label: "PHP",
            uses: [
                ["🌐", "Server-rendered websites"],
                ["📰", "Content-management systems"],
                ["🛒", "Online stores"],
                ["🔌", "Web APIs and integrations"],
                ["🏢", "Business portals"],
                ["🧰", "Rapid web-development projects"]
            ],
            examples: "WordPress, Wikipedia, Laravel applications"
        },
        ruby: {
            label: "Ruby",
            uses: [
                ["🌐", "Web applications with Ruby on Rails"],
                ["⚙️", "Automation and scripting"],
                ["🧪", "Testing and developer tools"],
                ["🚀", "Rapid prototypes and startup products"],
                ["🛒", "Commerce platforms"],
                ["🧰", "Command-line utilities"]
            ],
            examples: "GitHub's original Rails application, Shopify, GitLab"
        },
        r: {
            label: "R",
            uses: [
                ["📊", "Statistics and data analysis"],
                ["📈", "Data visualization"],
                ["🧪", "Scientific and academic research"],
                ["🤖", "Machine-learning experiments"],
                ["🏥", "Healthcare and clinical analysis"],
                ["💹", "Financial and forecasting models"]
            ],
            examples: "RStudio, Shiny dashboards, research and analytics workflows"
        },
        bash: {
            label: "Bash",
            uses: [
                ["⚙️", "Linux and macOS automation"],
                ["☁️", "DevOps and deployment scripts"],
                ["📁", "File and server administration"],
                ["🔗", "Connecting command-line tools"],
                ["🧪", "Build and test pipelines"],
                ["🛡️", "System-maintenance and security tasks"]
            ],
            examples: "Shell scripts used throughout Linux servers, CI systems, and developer workflows"
        },
        lua: {
            label: "Lua",
            uses: [
                ["🎮", "Game scripting and modding"],
                ["🧩", "Embedding scripts inside applications"],
                ["🔌", "Small and constrained systems"],
                ["🛠️", "Custom tools and configuration"],
                ["🤖", "Simulation logic"],
                ["🖥️", "Extending editors and applications"]
            ],
            examples: "World of Warcraft add-ons, Roblox Luau, Neovim configuration, game mods"
        },
        dart: {
            label: "Dart",
            uses: [
                ["📱", "Cross-platform mobile applications"],
                ["🖥️", "Desktop applications with Flutter"],
                ["🌐", "Web applications"],
                ["🎨", "Highly customized interfaces"],
                ["🧪", "Rapid app prototyping"],
                ["📦", "Single-codebase product development"]
            ],
            examples: "Flutter applications for Android, iOS, web, Windows, macOS, and Linux"
        }
    };

    const exactMarks = {
        "py": "python",
        "python": "python",
        "c#": "csharp",
        "cs": "csharp",
        "csharp": "csharp",
        "c++": "cpp",
        "cpp": "cpp",
        "js": "javascript",
        "javascript": "javascript",
        "ts": "typescript",
        "typescript": "typescript",
        "html": "htmlcss",
        "html/css": "htmlcss",
        "html & css": "htmlcss",
        "css": "htmlcss",
        "sql": "sql",
        "java": "java",
        "rust": "rust",
        "rs": "rust",
        "go": "go",
        "golang": "go",
        "c": "c",
        "swift": "swift",
        "kotlin": "kotlin",
        "kt": "kotlin",
        "php": "php",
        "ruby": "ruby",
        "rb": "ruby",
        "r": "r",
        "bash": "bash",
        "sh": "bash",
        "lua": "lua",
        "dart": "dart"
    };

    const cardSelector = [
        "article.path-card",
        "article.language-card",
        "article.learning-path-card",
        ".path-card",
        ".language-card",
        ".learning-path-card",
        "[data-language]",
        "[data-path-language]",
        "[class*='language-card']",
        "[class*='learning-path-card']"
    ].join(",");

    function normalized(value) {
        return String(value || "")
            .toLowerCase()
            .replace(/\u00a0/g, " ")
            .replace(/[–—]/g, "-")
            .replace(/\s+/g, " ")
            .trim();
    }

    function firstText(card, selectors) {
        for (const selector of selectors) {
            const element = card.querySelector(selector);
            if (element && normalized(element.textContent)) {
                return normalized(element.textContent);
            }
        }
        return "";
    }

    function detectLanguage(card) {
        const dataValue = normalized(
            card.dataset.language ||
            card.dataset.pathLanguage ||
            card.getAttribute("data-course-language")
        );

        if (exactMarks[dataValue]) {
            return exactMarks[dataValue];
        }

        const mark = firstText(card, [
            ".language-mark",
            ".language-icon",
            ".language-badge",
            ".path-mark",
            "[class*='language-mark']",
            "[class*='language-badge']"
        ]);

        if (exactMarks[mark]) {
            return exactMarks[mark];
        }

        const title = firstText(card, ["h2", "h3", "h4", ".title", "[class*='title']"]);
        const combined = `${title} ${dataValue} ${mark}`;

        if (/\b(type\s*script|typescript)\b/.test(combined)) return "typescript";
        if (combined.includes("c++") || /\bcpp\b/.test(combined)) return "cpp";
        if (combined.includes("c#") || /\bc sharp\b/.test(combined) || /\bcsharp\b/.test(combined)) return "csharp";
        if (/\bhtml\b/.test(combined) || /\bcss\b/.test(combined)) return "htmlcss";
        if (/\bjavascript\b/.test(combined) || /^js\b/.test(combined)) return "javascript";
        if (/\bpython\b/.test(combined)) return "python";
        if (/\bsql\b/.test(combined)) return "sql";
        if (/\bjava\b/.test(combined)) return "java";
        if (/\brust\b/.test(combined)) return "rust";
        if (/\bgolang\b/.test(combined) || /^go\b/.test(combined)) return "go";
        if (/\bswift\b/.test(combined)) return "swift";
        if (/\bkotlin\b/.test(combined)) return "kotlin";
        if (/\bphp\b/.test(combined)) return "php";
        if (/\bruby\b/.test(combined)) return "ruby";
        if (/^r\b/.test(combined) || /\br language\b/.test(combined)) return "r";
        if (/\bbash\b/.test(combined) || /\bshell scripting\b/.test(combined)) return "bash";
        if (/\blua\b/.test(combined)) return "lua";
        if (/\bdart\b/.test(combined) || /\bflutter\b/.test(combined)) return "dart";
        if (/^c(?:\s|$)/.test(title) || mark === "c") return "c";

        return null;
    }

    function createUsesPanel(languageKey) {
        const profile = languageProfiles[languageKey];
        const section = document.createElement("section");
        section.className = "real-world-uses";
        section.dataset.cavecodeLanguage = languageKey;
        section.dataset.cavecodePass = PASS_VERSION;
        section.setAttribute("aria-label", `${profile.label} real-world uses`);

        const heading = document.createElement("h4");
        heading.className = "real-world-uses__heading";
        heading.textContent = "What this language is used for";
        section.appendChild(heading);

        const list = document.createElement("ul");
        list.className = "real-world-uses__list";

        for (const [icon, text] of profile.uses) {
            const item = document.createElement("li");
            item.className = "real-world-uses__item";

            const iconElement = document.createElement("span");
            iconElement.className = "real-world-uses__icon";
            iconElement.setAttribute("aria-hidden", "true");
            iconElement.textContent = icon;

            const textElement = document.createElement("span");
            textElement.textContent = text;

            item.append(iconElement, textElement);
            list.appendChild(item);
        }

        section.appendChild(list);

        const examples = document.createElement("p");
        examples.className = "real-world-uses__examples";

        const examplesLabel = document.createElement("strong");
        examplesLabel.textContent = "Famous examples: ";

        examples.append(examplesLabel, document.createTextNode(profile.examples));
        section.appendChild(examples);

        return section;
    }

    function findInsertAnchor(card) {
        const preferredSelectors = [
            ":scope > .skill-tags",
            ":scope > [class*='skill-tag']",
            ":scope > [class*='course-tag']",
            ":scope > .path-action",
            ":scope > [class*='path-action']",
            ":scope > button",
            ":scope > a"
        ];

        for (const selector of preferredSelectors) {
            try {
                const anchor = card.querySelector(selector);
                if (anchor) return anchor;
            } catch {
                // A browser without :scope support falls through below.
            }
        }

        return null;
    }

    function enhanceCard(card) {
        if (!(card instanceof HTMLElement)) return;

        const languageKey = detectLanguage(card);
        if (!languageKey || !languageProfiles[languageKey]) return;

        const existing = card.querySelector(":scope .real-world-uses");
        if (existing && existing.dataset.cavecodeLanguage === languageKey) {
            return;
        }

        if (existing) {
            existing.remove();
        }

        const panel = createUsesPanel(languageKey);
        const anchor = findInsertAnchor(card);

        if (anchor && anchor.parentElement === card) {
            card.insertBefore(panel, anchor);
        } else {
            card.appendChild(panel);
        }

        card.dataset.cavecodeUsesEnhanced = languageKey;
    }

    function collectCards(root) {
        const cards = new Set();

        if (root instanceof Element && root.matches(cardSelector)) {
            cards.add(root);
        }

        if (root.querySelectorAll) {
            root.querySelectorAll(cardSelector).forEach(card => cards.add(card));
        }

        return cards;
    }

    let scanQueued = false;

    function scan(root = document) {
        collectCards(root).forEach(enhanceCard);
    }

    function queueScan() {
        if (scanQueued) return;
        scanQueued = true;

        requestAnimationFrame(() => {
            scanQueued = false;
            scan(document);
        });
    }

    function start() {
        scan(document);

        const observer = new MutationObserver(mutations => {
            for (const mutation of mutations) {
                for (const node of mutation.addedNodes) {
                    if (node instanceof Element) {
                        queueScan();
                    }
                }
            }
        });

        observer.observe(document.documentElement, {
            childList: true,
            subtree: true
        });

        window.addEventListener("popstate", () => queueScan());
        window.addEventListener("pageshow", () => queueScan());

        window.caveCodeLearningPathUses = Object.freeze({
            version: PASS_VERSION,
            refresh: () => scan(document),
            recognizedLanguages: Object.keys(languageProfiles)
        });
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", start, { once: true });
    } else {
        start();
    }
})();
