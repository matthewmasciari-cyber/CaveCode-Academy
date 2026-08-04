#!/usr/bin/env python3
from pathlib import Path
import re
import shutil

root = Path.cwd()
service_path = root / "Services" / "MinigameService.cs"
component_path = root / "Components" / "CodeMinigame.razor"
js_path = root / "wwwroot" / "js" / "caveCodeMinigames.js"
index_path = root / "wwwroot" / "index.html"

for path in [service_path, component_path, js_path, index_path]:
    if not path.exists():
        raise SystemExit(
            "Run this from the CaveCode-Academy repository root. "
            f"Missing: {path}"
        )

backup = root / ".minigame-missing-code-hints-backup"
for path in [service_path, component_path, js_path, index_path]:
    destination = backup / path.relative_to(root)
    destination.parent.mkdir(parents=True, exist_ok=True)
    shutil.copy2(path, destination)

# ---------------------------------------------------------------------
# Pass the current editor contents into the hint engine.
# ---------------------------------------------------------------------
service = service_path.read_text(encoding="utf-8")

service_pattern = re.compile(
    r'''public\s+ValueTask<MinigameHintResult>\s+UseHintAsync\(
        \s*string\s+course\s*
        \)\s*=>
        \s*js\.InvokeAsync<MinigameHintResult>\(
        \s*"caveCodeMinigames\.useHint",
        \s*course\s*
        \);''',
    re.VERBOSE,
)

service_replacement = '''public ValueTask<MinigameHintResult> UseHintAsync(
        string course,
        string code) =>
        js.InvokeAsync<MinigameHintResult>(
            "caveCodeMinigames.useHint",
            course,
            code);'''

if service_pattern.search(service):
    service = service_pattern.sub(
        service_replacement,
        service,
        count=1,
    )
elif not re.search(
    r"UseHintAsync\(\s*string\s+course,\s*string\s+code\s*\)",
    service,
):
    raise SystemExit(
        "Could not update UseHintAsync. "
        "The rules-and-hints pass must already be installed."
    )

service_path.write_text(service, encoding="utf-8")

# ---------------------------------------------------------------------
# Send StudentCode and clarify the hint labels.
# ---------------------------------------------------------------------
component = component_path.read_text(encoding="utf-8")

component, call_count = re.subn(
    r"\.UseHintAsync\(\s*Course\s*\)",
    ".UseHintAsync(Course, StudentCode)",
    component,
    count=1,
)

if call_count == 0 and \
   ".UseHintAsync(Course, StudentCode)" not in component:
    raise SystemExit(
        "Could not update the Razor hint call."
    )

component = component.replace(
    '$"Reveal {HintRevealPercent}%"',
    '$"Reveal {HintRevealPercent}% of missing code"',
)

component = component.replace(
    '% READABLE STRUCTURE REVEALED',
    '% OF MISSING STRUCTURE REVEALED',
)

component_path.write_text(component, encoding="utf-8")

# ---------------------------------------------------------------------
# Override the hint engine for every C# and Python scenario.
# ---------------------------------------------------------------------
js = js_path.read_text(encoding="utf-8")
marker = "CAVECODE_MISSING_CODE_HINT_ENGINE_V1"

if marker not in js:
    js += r'''

/* CAVECODE_MISSING_CODE_HINT_ENGINE_V1 */
(function () {
    const KEY = "cavecode.minigames.v2";
    const api = window.caveCodeMinigames;

    if (!api) {
        console.error(
            "CaveCode missing-code hint engine could not attach."
        );
        return;
    }

    function courseKey(course) {
        return course === "python"
            ? "python"
            : "csharp";
    }

    function loadState() {
        try {
            return JSON.parse(
                localStorage.getItem(KEY) ||
                '{"csharp":{},"python":{}}'
            );
        } catch {
            return {
                csharp: {},
                python: {}
            };
        }
    }

    function saveState(state) {
        localStorage.setItem(
            KEY,
            JSON.stringify(state)
        );

        window.dispatchEvent(
            new CustomEvent(
                "cavecode-minigames-changed"
            )
        );
    }

    function compact(value) {
        return String(value || "")
            .toLowerCase()
            .replace(/\s+/g, "");
    }

    function titleCase(value) {
        return String(value || "")
            .replace(/([a-z])([A-Z])/g, "$1 $2")
            .replace(/[_-]+/g, " ")
            .replace(
                /\b\w/g,
                letter => letter.toUpperCase()
            );
    }

    function restoreQuotedText(
        value,
        scenario
    ) {
        const source = [
            scenario?.title,
            scenario?.brief,
            scenario?.objective,
            scenario?.hint,
            scenario?.starterCode,
            scenario?.hintCode
        ]
            .filter(Boolean)
            .join("\n");

        return String(value || "").replace(
            /(["'])(.*?)\1/g,
            (match, quote, phrase) => {
                if (!phrase) {
                    return match;
                }

                const index = source
                    .toLowerCase()
                    .indexOf(
                        phrase.toLowerCase()
                    );

                if (index < 0) {
                    return match;
                }

                return (
                    quote +
                    source.slice(
                        index,
                        index + phrase.length
                    ) +
                    quote
                );
            }
        );
    }

    function requirements(scenario) {
        const validator =
            scenario?.validator || {};
        const result = [];

        for (const item of validator.all || []) {
            result.push({
                choices: [String(item)]
            });
        }

        for (const group of validator.any || []) {
            if (
                Array.isArray(group) &&
                group.length > 0
            ) {
                result.push({
                    choices:
                        group.map(String)
                });
            }
        }

        return result;
    }

    function requirementPresent(
        code,
        requirement
    ) {
        const value = compact(code);

        return requirement.choices.some(
            choice =>
                value.includes(
                    compact(choice)
                )
        );
    }

    function formatExpression(
        expression,
        course
    ) {
        let value = String(expression || "")
            .trim();

        value = value
            .replace(/&&/g, " && ")
            .replace(/\|\|/g, " || ")
            .replace(/\band\b/g, " and ")
            .replace(/\bor\b/g, " or ")
            .replace(/\bnot\b/g, "not ")
            .replace(/>=/g, " >= ")
            .replace(/<=/g, " <= ")
            .replace(/==/g, " == ")
            .replace(/!=/g, " != ")
            .replace(/\+=/g, " += ")
            .replace(/-=/g, " -= ")
            .replace(/\*=/g, " *= ")
            .replace(/\/=/g, " /= ")
            .replace(/(?<![<>=!+\-*/])=(?!=)/g, " = ")
            .replace(/(?<![<>=])>(?!=)/g, " > ")
            .replace(/(?<![<>=])<(?!=)/g, " < ")
            .replace(/\s+/g, " ")
            .trim();

        if (course === "csharp") {
            value = value
                .replace(/\.add\(/gi, ".Add(")
                .replace(/\.remove\(/gi, ".Remove(")
                .replace(/\.contains\(/gi, ".Contains(");
        }

        return value;
    }

    function formatTypedArguments(value) {
        return String(value || "")
            .replace(
                /\b(int|string|bool|double|float|decimal)([A-Za-z_]\w*)/g,
                "$1 $2"
            )
            .replace(/,/g, ", ");
    }

    function formatCSharp(
        raw,
        scenario
    ) {
        let value = restoreQuotedText(
            String(raw || "").trim(),
            scenario
        );

        if (!value) {
            return "";
        }

        if (/^["'].*["']$/.test(value)) {
            return value;
        }

        let match = value.match(
            /^class([A-Za-z_]\w*)$/
        );

        if (match) {
            return `class ${titleCase(match[1]).replace(/\s/g, "")}`;
        }

        match = value.match(
            /^(void|int|bool|double|string)([A-Za-z_]\w*)\((.*)$/
        );

        if (match) {
            const returnType = match[1];
            const methodName =
                titleCase(match[2])
                    .replace(/\s/g, "");
            const argumentsText =
                formatTypedArguments(
                    match[3]
                );

            return (
                `${returnType} ${methodName}(` +
                argumentsText
            );
        }

        match = value.match(
            /^(int|bool|double|string)([A-Za-z_]\w*)=(.*)$/
        );

        if (match) {
            const right =
                formatExpression(
                    match[3],
                    "csharp"
                );

            return (
                `${match[1]} ${match[2]} =` +
                (right ? ` ${right}` : "") +
                (right ? ";" : "")
            );
        }

        match = value.match(
            /^(int|bool|double|string)([A-Za-z_]\w*)$/
        );

        if (match) {
            return `${match[1]} ${match[2]}`;
        }

        match = value.match(
            /^return(.+)$/
        );

        if (match) {
            return (
                "return " +
                formatExpression(
                    match[1],
                    "csharp"
                ) +
                ";"
            );
        }

        match = value.match(
            /^(if|while)\((.*)\)$/
        );

        if (match) {
            return (
                `${match[1]} (` +
                formatExpression(
                    match[2],
                    "csharp"
                ) +
                ")"
            );
        }

        if (value.startsWith("foreach(")) {
            return "foreach (...)";
        }

        match = value.match(
            /^([A-Za-z_]\w*)(\+=|-=|\*=|\/=|=)(.+)$/
        );

        if (match) {
            return (
                `${match[1]} ${match[2]} ` +
                formatExpression(
                    match[3],
                    "csharp"
                ) +
                ";"
            );
        }

        const formatted =
            formatExpression(
                value,
                "csharp"
            );

        if (
            /\)$/.test(formatted) ||
            /^[A-Za-z_]\w*\./.test(formatted)
        ) {
            return formatted + ";";
        }

        return formatted;
    }

    function formatPython(
        raw,
        scenario
    ) {
        let value = restoreQuotedText(
            String(raw || "").trim(),
            scenario
        );

        if (!value) {
            return "";
        }

        if (/^["'].*["']$/.test(value)) {
            return value;
        }

        let match = value.match(
            /^def([A-Za-z_]\w*)\((.*)\):?$/
        );

        if (match) {
            return (
                `def ${match[1]}(` +
                match[2]
                    .replace(/,/g, ", ") +
                "):"
            );
        }

        match = value.match(
            /^(if|while)(.+):$/
        );

        if (match) {
            return (
                `${match[1]} ` +
                formatExpression(
                    match[2],
                    "python"
                ) +
                ":"
            );
        }

        match = value.match(
            /^for([A-Za-z_]\w*)in(.+):$/
        );

        if (match) {
            return (
                `for ${match[1]} in ` +
                formatExpression(
                    match[2],
                    "python"
                ) +
                ":"
            );
        }

        match = value.match(
            /^return(.+)$/
        );

        if (match) {
            return (
                "return " +
                formatExpression(
                    match[1],
                    "python"
                )
            );
        }

        match = value.match(
            /^([A-Za-z_]\w*)(\+=|-=|\*=|\/=|=)(.*)$/
        );

        if (match) {
            const right =
                formatExpression(
                    match[3],
                    "python"
                );

            return (
                `${match[1]} ${match[2]}` +
                (right ? ` ${right}` : "")
            );
        }

        return formatExpression(
            value,
            "python"
        );
    }

    function formatFragment(
        raw,
        course,
        scenario
    ) {
        return course === "python"
            ? formatPython(raw, scenario)
            : formatCSharp(raw, scenario);
    }

    function standardPartialReveal(
        formatted
    ) {
        const value =
            String(formatted || "").trim();

        if (!value) {
            return "";
        }

        const lines = value.split("\n");

        if (lines.length > 1) {
            return lines[0];
        }

        const assignment = value.match(
            /^(.*?)(\+=|-=|\*=|\/=|(?<![<>=!])=(?!=))(.*)$/
        );

        if (assignment) {
            return (
                assignment[1].trimEnd() +
                " " +
                assignment[2]
            ).trim();
        }

        const comparison = value.match(
            /^(return\s+.*?|if\s*\(.*?|while\s*\(.*?)(>=|<=|==|!=|>|<)(.*)$/
        );

        if (comparison) {
            return (
                comparison[1].trimEnd() +
                " " +
                comparison[2]
            ).trim();
        }

        return value;
    }

    function chooseMissingHint(
        scenario,
        currentCode,
        percent,
        course
    ) {
        const missing =
            requirements(scenario)
                .filter(requirement =>
                    !requirementPresent(
                        currentCode,
                        requirement
                    )
                );

        if (missing.length === 0) {
            return {
                reveal: "",
                missingCount: 0,
                totalCount: 0
            };
        }

        const revealCount = Math.max(
            1,
            Math.ceil(
                missing.length *
                percent /
                100
            )
        );

        const reveals = [];

        for (const requirement of missing) {
            let selected = "";

            for (const choice of requirement.choices) {
                const formatted =
                    formatFragment(
                        choice,
                        course,
                        scenario
                    );

                if (
                    formatted &&
                    !compact(currentCode)
                        .includes(
                            compact(formatted)
                        )
                ) {
                    selected = formatted;
                    break;
                }
            }

            if (!selected) {
                continue;
            }

            if (percent <= 35) {
                selected =
                    standardPartialReveal(
                        selected
                    );
            }

            if (
                selected &&
                !reveals.some(
                    existing =>
                        compact(existing) ===
                        compact(selected)
                )
            ) {
                reveals.push(selected);
            }

            if (
                reveals.length >=
                revealCount
            ) {
                break;
            }
        }

        return {
            reveal: reveals.join("\n"),
            missingCount:
                missing.length,
            totalCount:
                requirements(scenario).length
        };
    }

    api.useHint = function (
        course,
        currentCode
    ) {
        const key = courseKey(course);
        const state = loadState();
        const value = state[key] || {};

        if (
            !value.activeRun ||
            !value.scenario
        ) {
            return {
                allowed: false,
                message:
                    "Start a run before requesting a hint.",
                reveal: "",
                revealPercent: 0,
                scoreCost: 0,
                xpCost: 0,
                state: value
            };
        }

        if (
            !["training", "standard"]
                .includes(value.difficulty)
        ) {
            return {
                allowed: false,
                message:
                    "Hints are available only in Training and Standard.",
                reveal: "",
                revealPercent: 0,
                scoreCost: 0,
                xpCost: 0,
                state: value
            };
        }

        if (value.hintUsed) {
            return {
                allowed: false,
                message:
                    "The one hint for this room has already been used.",
                reveal:
                    value.hintReveal || "",
                revealPercent:
                    Number(
                        value.hintPercent || 0
                    ),
                scoreCost:
                    Number(
                        value.hintPenalty || 0
                    ),
                xpCost:
                    value.difficulty === "training"
                        ? 10
                        : 15,
                state: value
            };
        }

        const percent =
            value.difficulty === "training"
                ? 50
                : 35;
        const scoreCost =
            value.difficulty === "training"
                ? 150
                : 200;
        const xpCost =
            value.difficulty === "training"
                ? 10
                : 15;

        const generated =
            chooseMissingHint(
                value.scenario,
                currentCode || "",
                percent,
                key
            );

        if (!generated.reveal) {
            return {
                allowed: false,
                message:
                    "Every required structure already appears in the editor. Run the system to validate it; no hint cost was charged.",
                reveal: "",
                revealPercent: 0,
                scoreCost: 0,
                xpCost: 0,
                state: value
            };
        }

        value.hintUsed = true;
        value.hintPercent = percent;
        value.hintPenalty = scoreCost;
        value.hintReveal =
            generated.reveal;
        value.runHintsUsed =
            Number(
                value.runHintsUsed || 0
            ) + 1;

        saveState(state);

        return {
            allowed: true,
            message:
                `Revealed ${percent}% of the code structure that was still missing. ` +
                `Already-visible starter code was skipped. ` +
                `This room loses ${scoreCost} possible points and ${xpCost} XP. ` +
                "The run is no longer eligible for a perfect-run bonus.",
            reveal:
                value.hintReveal,
            revealPercent:
                percent,
            scoreCost,
            xpCost,
            state: value
        };
    };

    // Repair a hint already used in an active room by comparing
    // it with that room's starter code.
    const initialState = loadState();
    let changed = false;

    for (const key of ["csharp", "python"]) {
        const value = initialState[key];

        if (
            value?.activeRun &&
            value?.hintUsed &&
            value?.scenario
        ) {
            const generated =
                chooseMissingHint(
                    value.scenario,
                    value.scenario.starterCode || "",
                    Number(
                        value.hintPercent || 35
                    ),
                    key
                );

            if (
                generated.reveal &&
                generated.reveal !==
                    value.hintReveal
            ) {
                value.hintReveal =
                    generated.reveal;
                changed = true;
            }
        }
    }

    if (changed) {
        saveState(initialState);
    }
})();
'''

js_path.write_text(js, encoding="utf-8")

# Cache-bust JavaScript.
index = index_path.read_text(encoding="utf-8")
index = re.sub(
    r"js/caveCodeMinigames\.js\?v=\d+",
    "js/caveCodeMinigames.js?v=6",
    index,
    count=1,
)
index_path.write_text(index, encoding="utf-8")

print("CaveCode missing-code hint engine installed.")
print()
print("Fixed across all C# and Python minigame questions:")
print("  - Current editor code is checked before generating a hint")
print("  - Starter code already visible is never repeated")
print("  - The hint selects only missing validator requirements")
print("  - Alternative valid solutions are handled as one requirement")
print("  - C# hints use readable types, identifiers, operators, and semicolons")
print("  - Python hints use readable assignments, loops, conditions, and functions")
print("  - Standard reveals a partial missing syntax boundary")
print("  - Training reveals a larger missing code element")
print("  - Multiple missing requirements are revealed proportionally")
print("  - No useful missing element means no charge and no consumed hint")
print("  - Existing active hints are repaired against their starter code")
print()
print("Example fixed behavior:")
print("  Starter: int dragonEggs = 37;")
print("  Standard hint: dragonEggs +=")
print("  It will no longer repeat: int dragonEggs = 37;")
print()
print("Backups saved in .minigame-missing-code-hints-backup/")
print("Next command: dotnet build")
