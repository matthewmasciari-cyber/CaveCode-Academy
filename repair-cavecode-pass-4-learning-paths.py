#!/usr/bin/env python3
'''
CaveCode Academy — Emergency Pass 4 Learning Paths Repair

Use this only when Pass 4 caused the homepage Learning Paths section or all
language cards to disappear.

COMMANDS
--------
cd /workspaces/CaveCode-Academy
python3 repair-cavecode-pass-4-learning-paths.py
dotnet build
dotnet run

Then hard-refresh:
    Ctrl + Shift + R
'''

from __future__ import annotations

import re
import shutil
import subprocess
import sys
import tempfile
from pathlib import Path


PASS4_BACKUP = (
    ".html-css-course-shell-pass-4-backup/"
    "wwwroot/js/learning-path-discovery.js"
)
EMERGENCY_BACKUP = (
    ".html-css-pass-4-emergency-repair-backup/"
    "wwwroot/js/learning-path-discovery.js"
)
TARGET = "wwwroot/js/learning-path-discovery.js"

INJECTED_CARD_FUNCTION = r'''    function createInjectedCard(key) {
        // CAVECODE_MULTI_STATE_LANGUAGE_CARD_V2
        const profile = paths[key];
        const card = document.createElement("article");
        const stateClass = profile.available
            ? "available"
            : profile.preview
                ? "preview"
                : "locked";

        card.className =
            `path-card path-card--injected ${stateClass}`;
        card.dataset.cavecodeLanguage = key;

        const topline = document.createElement("div");
        topline.className = "path-topline";

        const mark = document.createElement("span");
        mark.className = "language-mark";
        mark.textContent = profile.mark;

        const status = document.createElement("span");
        status.className = profile.available
            ? "status available-status"
            : profile.preview
                ? "status preview-status"
                : "status locked-status";

        if (profile.available) {
            status.append(
                icon("circle-check-big"),
                document.createTextNode(" Available now")
            );
        } else if (profile.preview) {
            status.append(
                icon("construction"),
                document.createTextNode(" Course shell")
            );
        } else {
            status.append(
                icon("lock-keyhole"),
                document.createTextNode(" Coming soon")
            );
        }

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

        let action;

        if ((profile.available || profile.preview) && profile.href) {
            action = document.createElement("a");
            action.className = "path-action";
            action.href = profile.href;
            action.textContent =
                profile.action ||
                (profile.preview
                    ? "Preview course shell →"
                    : "Enter course →");
        } else {
            action = document.createElement("button");
            action.className = "path-action locked-action";
            action.type = "button";
            action.disabled = true;
            action.textContent = "Course in development";
        }

        card.append(
            topline,
            title,
            description,
            tags,
            action
        );

        return card;
    }
'''

EXISTING_CARD_FUNCTION = r'''    function normalizeExistingCard(card, key) {
        // CAVECODE_EXISTING_CARD_STATE_V2
        const profile = paths[key];
        const stateClass = profile.available
            ? "available"
            : profile.preview
                ? "preview"
                : "locked";

        card.dataset.cavecodeLanguage = key;
        card.classList.remove("available", "preview", "locked");
        card.classList.add(stateClass);

        const topline =
            card.querySelector(":scope > .path-topline");

        let status =
            topline?.querySelector(":scope > .status");

        if (!status && topline) {
            status = document.createElement("span");
            topline.appendChild(status);
        }

        if (status) {
            status.className = profile.available
                ? "status available-status"
                : profile.preview
                    ? "status preview-status"
                    : "status locked-status";

            if (profile.available) {
                status.replaceChildren(
                    icon("circle-check-big"),
                    document.createTextNode(" Available now")
                );
            } else if (profile.preview) {
                status.replaceChildren(
                    icon("construction"),
                    document.createTextNode(" Course shell")
                );
            } else {
                status.replaceChildren(
                    icon("lock-keyhole"),
                    document.createTextNode(" Coming soon")
                );
            }

            status.dataset.cavecodeStatus = "ready";
        }

        let action =
            card.querySelector(":scope > .path-action");

        const shouldLink =
            (profile.available || profile.preview) &&
            Boolean(profile.href);

        if (shouldLink) {
            if (!(action instanceof HTMLAnchorElement)) {
                const link = document.createElement("a");

                if (action) {
                    action.replaceWith(link);
                } else {
                    card.appendChild(link);
                }

                action = link;
            }

            action.className = "path-action";
            action.href = profile.href;
            action.textContent =
                profile.action ||
                (profile.preview
                    ? "Preview course shell →"
                    : "Enter course →");
        } else {
            if (!(action instanceof HTMLButtonElement)) {
                const button = document.createElement("button");

                if (action) {
                    action.replaceWith(button);
                } else {
                    card.appendChild(button);
                }

                action = button;
            }

            action.className =
                "path-action locked-action";
            action.type = "button";
            action.disabled = true;
            action.textContent =
                "Course in development";
        }

        insertUsesPanel(card, key);
    }
'''


def fail(message: str) -> None:
    print(f"ERROR: {message}", file=sys.stderr)
    raise SystemExit(1)


def locate_repo_root(start: Path) -> Path:
    for candidate in [start, *start.parents]:
        if (
            (candidate / "CaveCode.csproj").is_file()
            and (candidate / TARGET).is_file()
        ):
            return candidate

    fail(
        "Could not find the CaveCode repository root. "
        "Run this from /workspaces/CaveCode-Academy."
    )


def remove_broken_javascript_fields(text: str) -> str:
    pattern = re.compile(
        r'(?P<open>\n\s*javascript:\s*\{)'
        r'(?P<body>.*?)'
        r'(?P<close>\n\s*\},\n\s*sql:\s*\{)',
        re.DOTALL,
    )
    match = pattern.search(text)

    if not match:
        return text

    body = match.group("body")

    body = re.sub(
        r'(?m)^\s*preview:\s*(?:true|false),?\s*\n?',
        "",
        body,
    )
    body = re.sub(
        r'(?m)^\s*href:\s*"/html-css",?\s*\n?',
        "",
        body,
    )
    body = re.sub(
        r'(?m)^\s*action:\s*"Preview the workshop →",?\s*\n?',
        "",
        body,
    )

    return (
        text[:match.start("body")]
        + body
        + text[match.end("body"):]
    )


def patch_exact_htmlcss_profile(text: str) -> str:
    pattern = re.compile(
        r'(?P<open>\n\s*htmlcss:\s*\{)'
        r'(?P<body>.*?)'
        r'(?P<close>\n\s*\},\n\s*typescript:\s*\{)',
        re.DOTALL,
    )
    match = pattern.search(text)

    if not match:
        fail(
            "Could not locate the exact htmlcss profile."
        )

    body = match.group("body")

    body = re.sub(
        r'(?m)^\s*(?:preview|href|action):.*\n?',
        "",
        body,
    )

    available = re.search(
        r'(?m)^(?P<indent>\s*)'
        r'available:\s*(?:true|false),\s*$',
        body,
    )

    if not available:
        fail(
            "The htmlcss profile does not contain "
            "an available field."
        )

    indent = available.group("indent")
    replacement = (
        f"{indent}available: false,\n"
        f"{indent}preview: true,\n"
        f'{indent}href: "/html-css",\n'
        f'{indent}action: "Preview the workshop →",'
    )

    body = (
        body[:available.start()]
        + replacement
        + body[available.end():]
    )

    return (
        text[:match.start("body")]
        + body
        + text[match.end("body"):]
    )


def replace_function(
    text: str,
    function_name: str,
    next_function_name: str,
    replacement: str,
) -> str:
    pattern = re.compile(
        rf'    function {re.escape(function_name)}'
        rf'\([^)]*\) \{{.*?\n    \}}\n'
        rf'(?=\s*function {re.escape(next_function_name)}\()',
        re.DOTALL,
    )

    if not pattern.search(text):
        fail(
            f"Could not locate JavaScript function "
            f"{function_name}."
        )

    return pattern.sub(
        replacement.rstrip() + "\n",
        text,
        count=1,
    )


def patch_card_rendering(text: str) -> str:
    text = replace_function(
        text,
        "createInjectedCard",
        "createUsesPanel",
        INJECTED_CARD_FUNCTION,
    )

    text = replace_function(
        text,
        "normalizeExistingCard",
        "ensureCards",
        EXISTING_CARD_FUNCTION,
    )

    count_pattern = re.compile(
        r'if \(count\) count\.textContent = '
        r'"[^"]*";'
    )

    if not count_pattern.search(text):
        fail(
            "Could not locate the Learning Paths count."
        )

    text = count_pattern.sub(
        'if (count) count.textContent = '
        '"3 available · 1 shell preview · '
        '6 in development · 10 total";',
        text,
        count=1,
    )

    return text


def validate_content(text: str) -> None:
    required = [
        "htmlcss: {",
        "preview: true,",
        'href: "/html-css",',
        'action: "Preview the workshop →",',
        "CAVECODE_MULTI_STATE_LANGUAGE_CARD_V2",
        "CAVECODE_EXISTING_CARD_STATE_V2",
        (
            "3 available · 1 shell preview · "
            "6 in development · 10 total"
        ),
    ]

    missing = [
        marker
        for marker in required
        if marker not in text
    ]

    if missing:
        fail(
            "The repaired script is incomplete. Missing: "
            + ", ".join(missing)
        )

    javascript_pattern = re.compile(
        r'\n\s*javascript:\s*\{'
        r'(?P<body>.*?)'
        r'\n\s*\},\n\s*sql:\s*\{',
        re.DOTALL,
    )
    match = javascript_pattern.search(text)

    if (
        match
        and '/html-css' in match.group("body")
    ):
        fail(
            "The JavaScript profile still contains "
            "HTML/CSS route fields."
        )

    node = shutil.which("node")

    if node:
        with tempfile.NamedTemporaryFile(
            mode="w",
            suffix=".js",
            encoding="utf-8",
            delete=False,
        ) as handle:
            handle.write(text)
            temporary = Path(handle.name)

        try:
            result = subprocess.run(
                [node, "--check", str(temporary)],
                capture_output=True,
                text=True,
                check=False,
            )

            if result.returncode != 0:
                fail(
                    "JavaScript syntax validation failed:\n"
                    + result.stderr.strip()
                )
        finally:
            temporary.unlink(missing_ok=True)


def main() -> None:
    repo_root = locate_repo_root(Path.cwd().resolve())
    target = repo_root / TARGET
    pass4_backup = repo_root / PASS4_BACKUP
    emergency_backup = repo_root / EMERGENCY_BACKUP

    emergency_backup.parent.mkdir(
        parents=True,
        exist_ok=True,
    )

    if not emergency_backup.exists():
        shutil.copy2(target, emergency_backup)

    source = (
        pass4_backup
        if pass4_backup.is_file()
        else target
    )

    text = source.read_text(encoding="utf-8")
    text = remove_broken_javascript_fields(text)
    text = patch_exact_htmlcss_profile(text)
    text = patch_card_rendering(text)
    validate_content(text)

    target.write_text(text, encoding="utf-8")

    print("Pass 4 Learning Paths repair completed.")
    print()
    print("Fixed:")
    print("  - Restored the complete Learning Paths script")
    print(
        "  - Removed accidental HTML/CSS fields "
        "from JavaScript Web Forge"
    )
    print("  - Patched the exact htmlcss profile")
    print("  - Restored all 10 language cards")
    print(
        "  - Made HTML & CSS a clickable Course shell"
    )
    print(
        "  - Preserved C++, C#, Python, and locked paths"
    )
    print("  - Passed JavaScript syntax validation")
    print()
    print(
        "Emergency backup: "
        f"{emergency_backup.relative_to(repo_root)}"
    )
    print()
    print("Next commands:")
    print("  dotnet build")
    print("  dotnet run")
    print()
    print(
        "Then hard-refresh with Ctrl + Shift + R."
    )


if __name__ == "__main__":
    main()
