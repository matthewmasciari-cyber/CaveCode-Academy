#!/usr/bin/env python3
"""
CaveCode Academy — Static Learning Paths homepage repair

This repair removes the fragile JavaScript-gated Learning Paths experience
and replaces it with ten normal Blazor cards that are always visible.

COMMANDS
--------
cd /workspaces/CaveCode-Academy
python3 repair-cavecode-static-learning-paths.py
dotnet build
dotnet run

Then hard-refresh with Ctrl + Shift + R.
"""


import re
import shutil
import sys
from pathlib import Path


BACKUP_FOLDER = ".static-learning-paths-repair-backup"
HOME_FILE = "Pages/Home.razor"
INDEX_FILE = "wwwroot/index.html"

STATIC_SECTION = '        <section class="path-section" id="paths">\n            <div class="section-heading">\n                <div>\n                    <p class="eyebrow">LEARNING PATHS</p>\n                    <h2>Choose what you want to build</h2>\n                </div>\n                <span class="path-count">4 available · 6 in development · 10 total</span>\n            </div>\n\n            <div class="path-grid path-grid--static">\n                <article class="path-card available csharp-card">\n                    <div class="path-topline">\n                        <span class="language-mark">C#</span>\n                        <span class="status available-status">Available now</span>\n                    </div>\n                    <h3>C# Cave Adventure</h3>\n                    <p>Build a growing cave-exploration game while mastering C# foundations, logic, collections, classes, and combat.</p>\n                    <p class="path-examples"><strong>Common uses:</strong> Unity games, Windows applications, ASP.NET websites, APIs, and business software.</p>\n                    <CourseResume Course="csharp" />\n                    <div class="skill-tags">\n                        <span>40 modules</span>\n                        <span>8-stage practice</span>\n                        <span>Live game preview</span>\n                    </div>\n                    <NavLink class="path-action" href="/csharp">Enter the cave →</NavLink>\n                </article>\n\n                <article class="path-card available python-card">\n                    <div class="path-topline">\n                        <span class="language-mark">Py</span>\n                        <span class="status available-status">Available now</span>\n                    </div>\n                    <h3>Python Automation Quest</h3>\n                    <p>Restore an underground facility while learning Python through sensors, alarms, sequences, data, files, and Raspberry Pi concepts.</p>\n                    <p class="path-examples"><strong>Common uses:</strong> automation, artificial intelligence, data analysis, scripting, cybersecurity, and Raspberry Pi projects.</p>\n                    <CourseResume Course="python" />\n                    <div class="skill-tags">\n                        <span>40 modules</span>\n                        <span>Automation simulation</span>\n                        <span>Optional hardware path</span>\n                    </div>\n                    <NavLink class="path-action" href="/python">Enter the control room →</NavLink>\n                </article>\n\n                <article class="path-card available cpp-card">\n                    <div class="path-topline">\n                        <span class="language-mark">C++</span>\n                        <span class="status available-status">Available now</span>\n                    </div>\n                    <h3>C++ Engine Foundry</h3>\n                    <p>Build a real-time engine workshop while learning program structure, variables, input, output, operators, debugging, and systems thinking.</p>\n                    <p class="path-examples"><strong>Common uses:</strong> Unreal Engine, game engines, embedded systems, simulations, robotics, and high-performance software.</p>\n                    <div class="skill-tags">\n                        <span>40 modules</span>\n                        <span>Engine simulation</span>\n                        <span>Chapter 1 playable</span>\n                    </div>\n                    <NavLink class="path-action" href="/cpp">Enter the foundry →</NavLink>\n                </article>\n\n                <article class="path-card available htmlcss-card">\n                    <div class="path-topline">\n                        <span class="language-mark">HTML</span>\n                        <span class="status available-status">Available now</span>\n                    </div>\n                    <h3>HTML &amp; CSS Workshop</h3>\n                    <p>Build the structure and visual systems behind polished websites, responsive interfaces, landing pages, dashboards, and browser-game menus.</p>\n                    <p class="path-examples"><strong>Common uses:</strong> websites, responsive layouts, portfolios, dashboards, email templates, and user interfaces.</p>\n                    <div class="skill-tags">\n                        <span>40 modules</span>\n                        <span>Live browser preview</span>\n                        <span>Chapter 1 playable</span>\n                    </div>\n                    <NavLink class="path-action" href="/html-css">Enter the workshop →</NavLink>\n                </article>\n\n                <article class="path-card locked javascript-card">\n                    <div class="path-topline">\n                        <span class="language-mark">JS</span>\n                        <span class="status locked-status"><LockMark Compact="true" /> Coming soon</span>\n                    </div>\n                    <h3>JavaScript Web Forge</h3>\n                    <p>Create interactive websites and browser games while learning the language of the modern web.</p>\n                    <p class="path-examples"><strong>Common uses:</strong> interactive websites, browser games, web applications, Node.js servers, and extensions.</p>\n                    <div class="skill-tags">\n                        <span>Web apps</span>\n                        <span>Browser games</span>\n                        <span>Interfaces</span>\n                    </div>\n                    <button class="path-action locked-action" type="button" disabled>Course in development</button>\n                </article>\n\n                <article class="path-card locked sql-card">\n                    <div class="path-topline">\n                        <span class="language-mark">SQL</span>\n                        <span class="status locked-status"><LockMark Compact="true" /> Coming soon</span>\n                    </div>\n                    <h3>SQL Database Dungeon</h3>\n                    <p>Master queries by managing players, items, quests, and persistent world data.</p>\n                    <p class="path-examples"><strong>Common uses:</strong> databases, reporting, analytics, application backends, inventories, and saved game data.</p>\n                    <div class="skill-tags">\n                        <span>Queries</span>\n                        <span>Databases</span>\n                        <span>Game data</span>\n                    </div>\n                    <button class="path-action locked-action" type="button" disabled>Course in development</button>\n                </article>\n\n                <article class="path-card locked typescript-card">\n                    <div class="path-topline">\n                        <span class="language-mark">TS</span>\n                        <span class="status locked-status"><LockMark Compact="true" /> Coming soon</span>\n                    </div>\n                    <h3>TypeScript Application Architect</h3>\n                    <p>Scale JavaScript into dependable applications with types, reusable systems, and safer team workflows.</p>\n                    <p class="path-examples"><strong>Common uses:</strong> React, Angular, Vue, large web applications, Node.js services, and developer tools.</p>\n                    <div class="skill-tags">\n                        <span>Typed JavaScript</span>\n                        <span>Large applications</span>\n                        <span>Modern frameworks</span>\n                    </div>\n                    <button class="path-action locked-action" type="button" disabled>Course in development</button>\n                </article>\n\n                <article class="path-card locked java-card">\n                    <div class="path-topline">\n                        <span class="language-mark">Java</span>\n                        <span class="status locked-status"><LockMark Compact="true" /> Coming soon</span>\n                    </div>\n                    <h3>Java Enterprise Expedition</h3>\n                    <p>Build durable applications while learning the language behind Android systems, business software, and large backends.</p>\n                    <p class="path-examples"><strong>Common uses:</strong> Android applications, enterprise systems, web backends, cloud services, and developer platforms.</p>\n                    <div class="skill-tags">\n                        <span>Android</span>\n                        <span>Enterprise</span>\n                        <span>Back-end systems</span>\n                    </div>\n                    <button class="path-action locked-action" type="button" disabled>Course in development</button>\n                </article>\n\n                <article class="path-card locked go-card">\n                    <div class="path-topline">\n                        <span class="language-mark">Go</span>\n                        <span class="status locked-status"><LockMark Compact="true" /> Coming soon</span>\n                    </div>\n                    <h3>Go Cloud Command</h3>\n                    <p>Build fast network services and infrastructure tools while learning clear, practical concurrent programming.</p>\n                    <p class="path-examples"><strong>Common uses:</strong> cloud infrastructure, APIs, DevOps tools, networking, containers, and monitoring services.</p>\n                    <div class="skill-tags">\n                        <span>Cloud</span>\n                        <span>DevOps</span>\n                        <span>Distributed systems</span>\n                    </div>\n                    <button class="path-action locked-action" type="button" disabled>Course in development</button>\n                </article>\n\n                <article class="path-card locked rust-card">\n                    <div class="path-topline">\n                        <span class="language-mark">Rust</span>\n                        <span class="status locked-status"><LockMark Compact="true" /> Coming soon</span>\n                    </div>\n                    <h3>Rust Systems Frontier</h3>\n                    <p>Explore memory-safe systems programming through reliable tools, embedded projects, WebAssembly, and performance work.</p>\n                    <p class="path-examples"><strong>Common uses:</strong> systems software, command-line tools, WebAssembly, embedded development, and performance tools.</p>\n                    <div class="skill-tags">\n                        <span>Memory safety</span>\n                        <span>Systems</span>\n                        <span>WebAssembly</span>\n                    </div>\n                    <button class="path-action locked-action" type="button" disabled>Course in development</button>\n                </article>\n            </div>\n        </section>'
EXTRA_CSS = '\n    /* CAVECODE_STATIC_LEARNING_PATHS_REPAIR_V1 */\n    .path-grid--static {\n        display: grid !important;\n        visibility: visible !important;\n        opacity: 1 !important;\n    }\n\n    .path-grid--static .path-card,\n    .path-grid--static .path-card > * {\n        visibility: visible !important;\n        opacity: 1 !important;\n    }\n\n    .path-grid--static .path-card {\n        min-height: 430px;\n    }\n\n    .path-grid--static .path-examples {\n        padding-top: 12px;\n        margin-top: 12px;\n        color: var(--text-muted);\n        border-top: 1px solid var(--border);\n        font-size: 11px;\n        line-height: 1.55;\n    }\n\n    .path-grid--static .path-examples strong {\n        color: var(--text);\n    }\n\n    .path-grid--static .available .language-mark {\n        color: var(--accent-contrast);\n        background: var(--accent);\n    }\n\n    @media (max-width: 820px) {\n        .path-grid--static .path-card {\n            min-height: 0;\n        }\n    }\n'


def fail(message: str) -> None:
    print(f"ERROR: {message}", file=sys.stderr)
    raise SystemExit(1)


def locate_repo_root(start: Path) -> Path:
    for candidate in [start, *start.parents]:
        if (
            (candidate / "CaveCode.csproj").is_file()
            and (candidate / HOME_FILE).is_file()
            and (candidate / INDEX_FILE).is_file()
        ):
            return candidate

    fail(
        "Could not find the CaveCode repository root. "
        "Run this from /workspaces/CaveCode-Academy."
    )


def backup_once(source: Path, backup_root: Path, repo_root: Path) -> None:
    if not source.exists():
        return

    destination = backup_root / source.relative_to(repo_root)

    if destination.exists():
        return

    destination.parent.mkdir(parents=True, exist_ok=True)
    shutil.copy2(source, destination)


def patch_home(text: str) -> str:
    section_pattern = re.compile(
        r'(?P<indent>\s*)'
        r'<section class="path-section" id="paths">.*?'
        r'</section>'
        r'(?=\s*<section class="minigame-section")',
        re.DOTALL,
    )

    if section_pattern.search(text):
        text = section_pattern.sub(
            "\n" + STATIC_SECTION,
            text,
            count=1,
        )
    elif "CAVECODE_STATIC_LEARNING_PATHS_REPAIR_V1" not in text:
        fail(
            "Could not locate the homepage Learning Paths section."
        )

    text = text.replace(
        "C# Cave Adventure and Python Automation Quest are available.",
        "C#, Python, C++, and HTML & CSS learning paths are available.",
    )

    if "CAVECODE_STATIC_LEARNING_PATHS_REPAIR_V1" not in text:
        style_close = text.rfind("</style>")

        if style_close < 0:
            fail("Could not locate </style> in Pages/Home.razor.")

        text = (
            text[:style_close]
            + EXTRA_CSS
            + "\n"
            + text[style_close:]
        )

    return text


def patch_index(text: str) -> str:
    patterns = [
        re.compile(
            r'\s*<script[^>]*data-cavecode-pass='
            r'["\']learning-path-discovery-bootstrap["\']'
            r'[^>]*>.*?</script>\s*',
            re.IGNORECASE | re.DOTALL,
        ),
        re.compile(
            r'\s*<link[^>]*href=["\']'
            r'css/learning-path-discovery\.css'
            r'(?:\?v=[^"\']*)?["\'][^>]*?/?>\s*',
            re.IGNORECASE,
        ),
        re.compile(
            r'\s*<script[^>]*src=["\']'
            r'js/learning-path-discovery\.js'
            r'(?:\?v=[^"\']*)?["\'][^>]*>\s*</script>\s*',
            re.IGNORECASE,
        ),
    ]

    for pattern in patterns:
        text = pattern.sub("\n", text)

    return text


def validate(home: str, index: str) -> None:
    required_home = [
        "CAVECODE_STATIC_LEARNING_PATHS_REPAIR_V1",
        "4 available · 6 in development · 10 total",
        'href="/csharp"',
        'href="/python"',
        'href="/cpp"',
        'href="/html-css"',
        "C++ Engine Foundry",
        "HTML &amp; CSS Workshop",
        "Rust Systems Frontier",
    ]

    missing = [item for item in required_home if item not in home]

    if missing:
        fail(
            "The homepage repair is incomplete. Missing: "
            + ", ".join(missing)
        )

    card_count = home.count('class="path-card ')

    if card_count != 10:
        fail(
            f"Expected 10 static learning-path cards, found {card_count}."
        )

    forbidden_index = [
        "learning-path-discovery-bootstrap",
        "css/learning-path-discovery.css",
        "js/learning-path-discovery.js",
    ]

    remaining = [item for item in forbidden_index if item in index]

    if remaining:
        fail(
            "The fragile discovery system is still loaded: "
            + ", ".join(remaining)
        )


def main() -> None:
    repo_root = locate_repo_root(Path.cwd().resolve())
    home_file = repo_root / HOME_FILE
    index_file = repo_root / INDEX_FILE
    backup_root = repo_root / BACKUP_FOLDER

    backup_once(home_file, backup_root, repo_root)
    backup_once(index_file, backup_root, repo_root)

    home = patch_home(home_file.read_text(encoding="utf-8"))
    index = patch_index(index_file.read_text(encoding="utf-8"))

    validate(home, index)

    home_file.write_text(home, encoding="utf-8")
    index_file.write_text(index, encoding="utf-8")

    print("Static Learning Paths repair installed successfully.")
    print()
    print("Fixed:")
    print("  - Removed the JavaScript pending-state gate")
    print("  - Removed the discovery stylesheet and script")
    print("  - Added ten always-visible Blazor cards")
    print("  - Marked C#, Python, C++, and HTML/CSS available")
    print("  - Kept six future languages visible and locked")
    print("  - Added common-use examples beneath each language")
    print("  - Preserved course pages, progress, XP, and achievements")
    print()
    print(
        "Backup: "
        f"{backup_root.relative_to(repo_root)}/"
    )
    print()
    print("Next commands:")
    print("  dotnet build")
    print("  dotnet run")
    print()
    print("Then hard-refresh with Ctrl + Shift + R.")


if __name__ == "__main__":
    main()
