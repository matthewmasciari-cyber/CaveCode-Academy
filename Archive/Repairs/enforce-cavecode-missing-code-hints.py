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
            "Run this from /workspaces/CaveCode-Academy. "
            f"Missing: {path}"
        )

backup = root / ".minigame-hint-engine-enforcement-backup"
for path in [service_path, component_path, js_path, index_path]:
    destination = backup / path.relative_to(root)
    destination.parent.mkdir(parents=True, exist_ok=True)
    shutil.copy2(path, destination)

service = service_path.read_text(encoding="utf-8")

old_signature = re.compile(
    r'''public\s+ValueTask<MinigameHintResult>\s+UseHintAsync\(
        \s*string\s+course\s*
        \)\s*=>
        \s*js\.InvokeAsync<MinigameHintResult>\(
        \s*"caveCodeMinigames\.useHint",
        \s*course\s*
        \);''',
    re.VERBOSE,
)

new_signature = '''public ValueTask<MinigameHintResult> UseHintAsync(
        string course,
        string code) =>
        js.InvokeAsync<MinigameHintResult>(
            "caveCodeMinigames.useHint",
            course,
            code);'''

if old_signature.search(service):
    service = old_signature.sub(new_signature, service, count=1)
elif not re.search(
    r"UseHintAsync\(\s*string\s+course,\s*string\s+code\s*\)",
    service,
):
    raise SystemExit(
        "Could not find a compatible UseHintAsync method."
    )

service_path.write_text(service, encoding="utf-8")

component = component_path.read_text(encoding="utf-8")

component = re.sub(
    r"\.UseHintAsync\(\s*Course\s*\)",
    ".UseHintAsync(Course, StudentCode)",
    component,
    count=1,
)

if ".UseHintAsync(Course, StudentCode)" not in component:
    raise SystemExit(
        "Could not confirm UseHintAsync(Course, StudentCode) in Razor."
    )

component = component.replace(
    '$"Reveal {HintRevealPercent}%"',
    '$"Reveal {HintRevealPercent}% of missing code"',
)

component = component.replace(
    "% READABLE STRUCTURE REVEALED",
    "% OF MISSING STRUCTURE REVEALED",
)

component_path.write_text(component, encoding="utf-8")

js = js_path.read_text(encoding="utf-8")
marker = "CAVECODE_AUTHORITATIVE_MISSING_HINT_ENGINE_V2"

if marker not in js:
    js += r'''

/* CAVECODE_AUTHORITATIVE_MISSING_HINT_ENGINE_V2 */
(function () {
    const KEY = "cavecode.minigames.v2";
    const api = window.caveCodeMinigames;

    if (!api) {
        console.error(
            "CaveCode authoritative hint engine could not attach."
        );
        return;
    }

    function keyFor(course) {
        return course === "python" ? "python" : "csharp";
    }

    function loadState() {
        try {
            return JSON.parse(
                localStorage.getItem(KEY) ||
                '{"csharp":{},"python":{}}'
            );
        } catch {
            return { csharp: {}, python: {} };
        }
    }

    function saveState(state) {
        localStorage.setItem(KEY, JSON.stringify(state));
        window.dispatchEvent(
            new CustomEvent("cavecode-minigames-changed")
        );
    }

    function stripComments(code, course) {
        let value = String(code || "");

        if (course === "python") {
            return value
                .split(/\r?\n/)
                .map(line => {
                    const index = line.indexOf("#");
                    return index >= 0 ? line.slice(0, index) : line;
                })
                .join("\n");
        }

        value = value.replace(/\/\*[\s\S]*?\*\//g, "");

        return value
            .split(/\r?\n/)
            .map(line => {
                const index = line.indexOf("//");
                return index >= 0 ? line.slice(0, index) : line;
            })
            .join("\n");
    }

    function compact(value) {
        return String(value || "")
            .toLowerCase()
            .replace(/\s+/g, "");
    }

    function requirements(scenario) {
        const validator = scenario?.validator || {};
        const result = [];

        for (const item of validator.all || []) {
            result.push({ choices: [String(item)] });
        }

        for (const group of validator.any || []) {
            if (Array.isArray(group) && group.length > 0) {
                result.push({ choices: group.map(String) });
            }
        }

        return result;
    }

    function isPresent(code, requirement, course) {
        const value = compact(stripComments(code, course));

        return requirement.choices.some(
            choice => value.includes(compact(choice))
        );
    }

    function restoreQuotedText(value, scenario) {
        const source = [
            scenario?.brief,
            scenario?.objective,
            scenario?.hint,
            scenario?.starterCode,
            scenario?.hintCode
        ].filter(Boolean).join("\n");

        return String(value || "").replace(
            /(["'])(.*?)\1/g,
            (match, quote, phrase) => {
                if (!phrase) return match;

                const index = source
                    .toLowerCase()
                    .indexOf(phrase.toLowerCase());

                return index < 0
                    ? match
                    : quote + source.slice(index, index + phrase.length) + quote;
            }
        );
    }

    function spaceOperators(value) {
        return String(value || "")
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
    }

    function formatCSharp(raw, scenario) {
        let value = restoreQuotedText(
            String(raw || "").trim(),
            scenario
        );

        if (!value) return "";

        let match = value.match(
            /^(int|double|bool|string)([A-Za-z_]\w*)=(.*)$/
        );

        if (match) {
            const right = spaceOperators(match[3]);

            return (
                `${match[1]} ${match[2]} =` +
                (right ? ` ${right};` : "")
            );
        }

        match = value.match(
            /^([A-Za-z_]\w*)(\+=|-=|\*=|\/=|=)(.*)$/
        );

        if (match) {
            const right = spaceOperators(match[3]);

            return (
                `${match[1]} ${match[2]}` +
                (right ? ` ${right};` : "")
            );
        }

        match = value.match(/^return(.+)$/);

        if (match) {
            return `return ${spaceOperators(match[1])};`;
        }

        match = value.match(/^(if|while)\((.*)\)$/);

        if (match) {
            return `${match[1]} (${spaceOperators(match[2])})`;
        }

        match = value.match(
            /^(void|int|double|bool|string)([A-Za-z_]\w*)\((.*)$/
        );

        if (match) {
            return `${match[1]} ${match[2]}(${match[3]}`;
        }

        if (/^["'].*["']$/.test(value)) {
            return value;
        }

        const formatted = spaceOperators(value);

        if (
            /\)$/.test(formatted) ||
            /^[A-Za-z_]\w*\./.test(formatted)
        ) {
            return formatted + ";";
        }

        return formatted;
    }

    function formatPython(raw, scenario) {
        let value = restoreQuotedText(
            String(raw || "").trim(),
            scenario
        );

        if (!value) return "";

        let match = value.match(
            /^([A-Za-z_]\w*)(\+=|-=|\*=|\/=|=)(.*)$/
        );

        if (match) {
            const right = spaceOperators(match[3]);

            return (
                `${match[1]} ${match[2]}` +
                (right ? ` ${right}` : "")
            );
        }

        match = value.match(/^return(.+)$/);

        if (match) {
            return `return ${spaceOperators(match[1])}`;
        }

        match = value.match(/^def([A-Za-z_]\w*)\((.*)\):?$/);

        if (match) {
            return `def ${match[1]}(${match[2]}):`;
        }

        match = value.match(/^(if|while)(.+):$/);

        if (match) {
            return `${match[1]} ${spaceOperators(match[2])}:`;
        }

        match = value.match(/^for([A-Za-z_]\w*)in(.+):$/);

        if (match) {
            return `for ${match[1]} in ${spaceOperators(match[2])}:`;
        }

        return spaceOperators(value);
    }

    function formatFragment(raw, course, scenario) {
        return course === "python"
            ? formatPython(raw, scenario)
            : formatCSharp(raw, scenario);
    }

    function partialForStandard(formatted) {
        const value = String(formatted || "").trim();

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

        return value.split("\n")[0];
    }

    function generateMissingHint(
        scenario,
        currentCode,
        percent,
        course
    ) {
        const missing = requirements(scenario).filter(
            requirement => !isPresent(
                currentCode,
                requirement,
                course
            )
        );

        if (missing.length === 0) {
            return "";
        }

        const revealCount = Math.max(
            1,
            Math.ceil(missing.length * percent / 100)
        );

        const reveals = [];

        for (const requirement of missing) {
            let formatted = "";

            for (const choice of requirement.choices) {
                const candidate = formatFragment(
                    choice,
                    course,
                    scenario
                );

                if (
                    candidate &&
                    !compact(stripComments(currentCode, course))
                        .includes(compact(candidate))
                ) {
                    formatted = candidate;
                    break;
                }
            }

            if (!formatted) continue;

            if (percent <= 35) {
                formatted = partialForStandard(formatted);
            }

            if (
                !reveals.some(
                    item => compact(item) === compact(formatted)
                )
            ) {
                reveals.push(formatted);
            }

            if (reveals.length >= revealCount) {
                break;
            }
        }

        return reveals.join("\n");
    }

    api.hintEngineVersion = "missing-code-v2";

    api.getHintEngineVersion = function () {
        return api.hintEngineVersion;
    };

    api.useHint = function (course, currentCode) {
        const key = keyFor(course);
        const state = loadState();
        const value = state[key] || {};

        if (!value.activeRun || !value.scenario) {
            return {
                allowed: false,
                message: "Start a run before requesting a hint.",
                reveal: "",
                revealPercent: 0,
                scoreCost: 0,
                xpCost: 0,
                state: value
            };
        }

        if (!["training", "standard"].includes(value.difficulty)) {
            return {
                allowed: false,
                message: "Hints are available only in Training and Standard.",
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
                message: "The one hint for this room has already been used.",
                reveal: value.hintReveal || "",
                revealPercent: Number(value.hintPercent || 0),
                scoreCost: Number(value.hintPenalty || 0),
                xpCost: value.difficulty === "training" ? 10 : 15,
                state: value
            };
        }

        const percent =
            value.difficulty === "training" ? 50 : 35;
        const scoreCost =
            value.difficulty === "training" ? 150 : 200;
        const xpCost =
            value.difficulty === "training" ? 10 : 15;

        const reveal = generateMissingHint(
            value.scenario,
            currentCode || "",
            percent,
            key
        );

        if (!reveal) {
            return {
                allowed: false,
                message:
                    "Every required structure already appears in the editor. " +
                    "Run the challenge check; no hint cost was charged.",
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
        value.hintReveal = reveal;
        value.runHintsUsed =
            Number(value.runHintsUsed || 0) + 1;

        saveState(state);

        return {
            allowed: true,
            message:
                `Revealed ${percent}% of the structure that was still missing. ` +
                `Already-visible starter code was skipped. ` +
                `This room loses ${scoreCost} possible points and ${xpCost} XP. ` +
                "The run is no longer eligible for a perfect-run bonus.",
            reveal,
            revealPercent: percent,
            scoreCost,
            xpCost,
            state: value
        };
    };

    const state = loadState();
    let changed = false;

    for (const key of ["csharp", "python"]) {
        const value = state[key];

        if (
            value?.activeRun &&
            value?.hintUsed &&
            value?.scenario
        ) {
            const repaired = generateMissingHint(
                value.scenario,
                value.scenario.starterCode || "",
                Number(value.hintPercent || 35),
                key
            );

            if (repaired && repaired !== value.hintReveal) {
                value.hintReveal = repaired;
                changed = true;
            }
        }
    }

    if (changed) {
        saveState(state);
    }

    console.info(
        "CaveCode hint engine active:",
        api.hintEngineVersion
    );
})();
'''

js_path.write_text(js, encoding="utf-8")

index = index_path.read_text(encoding="utf-8")
index = re.sub(
    r"js/caveCodeMinigames\.js\?v=\d+",
    "js/caveCodeMinigames.js?v=8",
    index,
    count=1,
)
index_path.write_text(index, encoding="utf-8")

print("Authoritative CaveCode missing-code hint engine installed.")
print()
print("Guaranteed changes:")
print("  - Latest hint engine is the final useHint definition")
print("  - Current editor code is sent to the hint engine")
print("  - Visible starter code is skipped")
print("  - Only missing validator elements are revealed")
print("  - Existing active-room hint text is repaired")
print("  - Script cache bumped to v=8")
print("  - Diagnostic version: missing-code-v2")
print()
print("Backups saved in .minigame-hint-engine-enforcement-backup/")
print("Next command: dotnet build")
