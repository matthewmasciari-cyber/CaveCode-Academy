#!/usr/bin/env python3
from pathlib import Path
import shutil

root = Path.cwd()
service_path = root / "Services" / "MinigameService.cs"
component_path = root / "Components" / "CodeMinigame.razor"
js_path = root / "wwwroot" / "js" / "caveCodeMinigames.js"
index_path = root / "wwwroot" / "index.html"

for path in [service_path, component_path, js_path, index_path]:
    if not path.exists():
        raise SystemExit(
            "Run this installer from the CaveCode-Academy repository root. "
            f"Missing: {path}"
        )

backup = root / ".minigame-rules-hints-backup"
for path in [service_path, component_path, js_path, index_path]:
    relative = path.relative_to(root)
    destination = backup / relative
    destination.parent.mkdir(parents=True, exist_ok=True)
    shutil.copy2(path, destination)

# ---------------------------------------------------------------------
# MinigameService.cs
# ---------------------------------------------------------------------
service = service_path.read_text(encoding="utf-8")

old_methods = '''    public ValueTask<MinigameCourseState> EndRunAsync(string course) =>
        js.InvokeAsync<MinigameCourseState>(
            "caveCodeMinigames.endRun", course);
}'''

new_methods = '''    public ValueTask<MinigameAnalysisResult> AnalyzeAsync(
        string course,
        string code) =>
        js.InvokeAsync<MinigameAnalysisResult>(
            "caveCodeMinigames.analyze", course, code);

    public ValueTask<MinigameHintResult> UseHintAsync(string course) =>
        js.InvokeAsync<MinigameHintResult>(
            "caveCodeMinigames.useHint", course);

    public ValueTask<MinigameCourseState> ResetRunAsync(string course) =>
        js.InvokeAsync<MinigameCourseState>(
            "caveCodeMinigames.resetRun", course);

    public ValueTask<MinigameCourseState> EndRunAsync(string course) =>
        js.InvokeAsync<MinigameCourseState>(
            "caveCodeMinigames.endRun", course);
}'''

if old_methods not in service:
    if "UseHintAsync" not in service:
        raise SystemExit(
            "Could not locate MinigameService method insertion point. "
            "Install the minigame variety overhaul first."
        )
else:
    service = service.replace(old_methods, new_methods, 1)

state_anchor = '''    public int Threat { get; set; }
    public GeneratedScenario? Scenario { get; set; }
}'''

state_replacement = '''    public int Threat { get; set; }
    public bool HintUsed { get; set; }
    public int HintPercent { get; set; }
    public string HintReveal { get; set; } = "";
    public int HintPenalty { get; set; }
    public int RunHintsUsed { get; set; }
    public int AbandonedRuns { get; set; }
    public GeneratedScenario? Scenario { get; set; }
}'''

if state_anchor in service:
    service = service.replace(state_anchor, state_replacement, 1)
elif "public bool HintUsed" not in service:
    raise SystemExit("Could not add hint fields to MinigameCourseState.")

analysis_classes_anchor = '''public sealed class MinigameCompletionResult
{'''

analysis_classes = '''public sealed class MinigameAnalysisResult
{
    public int CurrentCharacters { get; set; }
    public int TargetCharacters { get; set; }
    public int StructuralAccuracy { get; set; }
    public int MatchedElements { get; set; }
    public int RequiredElements { get; set; }
}

public sealed class MinigameHintResult
{
    public bool Allowed { get; set; }
    public string Message { get; set; } = "";
    public string Reveal { get; set; } = "";
    public int RevealPercent { get; set; }
    public int ScoreCost { get; set; }
    public int XpCost { get; set; }
    public MinigameCourseState State { get; set; } = new();
}

public sealed class MinigameCompletionResult
{'''

if analysis_classes_anchor in service and "public sealed class MinigameAnalysisResult" not in service:
    service = service.replace(
        analysis_classes_anchor,
        analysis_classes,
        1
    )

service_path.write_text(service, encoding="utf-8")

# ---------------------------------------------------------------------
# caveCodeMinigames.js
# ---------------------------------------------------------------------
js = js_path.read_text(encoding="utf-8")

# Keep spaces inside generated multi-word strings valid by applying
# identical normalization to submitted code and expected fragments.
old_validator = """        return (s.all||[]).every(x=>v.includes(String(x).toLowerCase()))
            && (s.any||[]).every(group=>group.some(x=>v.includes(String(x).toLowerCase())))
            && (s.none||[]).every(x=>!v.includes(String(x).toLowerCase()));"""

new_validator = """        return (s.all||[]).every(x=>v.includes(compact(x)))
            && (s.any||[]).every(group=>group.some(x=>v.includes(compact(x))))
            && (s.none||[]).every(x=>!v.includes(compact(x)));"""

if old_validator in js:
    js = js.replace(old_validator, new_validator, 1)
elif new_validator not in js:
    raise SystemExit(
        "Could not find the minigame validator. "
        "Install the minigame variety overhaul first."
    )

old_empty = '''            primaryResource:100,secondaryResource:0,threat:0,scenario:null,
            clearedVariations:{},clearedTemplates:{},codeHashes:{},'''

new_empty = '''            primaryResource:100,secondaryResource:0,threat:0,
            hintUsed:false,hintPercent:0,hintReveal:"",hintPenalty:0,
            runHintsUsed:0,abandonedRuns:0,scenario:null,
            clearedVariations:{},clearedTemplates:{},codeHashes:{},'''

if old_empty in js:
    js = js.replace(old_empty, new_empty, 1)
elif "hintUsed:false" not in js:
    raise SystemExit("Could not patch minigame saved-state defaults.")

helper_anchor = '''    const damage=d=>d==="training"?5:d==="advanced"?15:d==="expert"?20:10;
    const score=d=>d==="training"?600:d==="advanced"?1000:d==="expert"?1250:800;
    const xp=d=>d==="training"?60:d==="advanced"?100:d==="expert"?125:80;

    window.caveCodeMinigames = {'''

helper_replacement = '''    const damage=d=>d==="training"?5:d==="advanced"?15:d==="expert"?20:10;
    const score=d=>d==="training"?600:d==="advanced"?1000:d==="expert"?1250:800;
    const xp=d=>d==="training"?60:d==="advanced"?100:d==="expert"?125:80;

    let guardedCourse = null;
    let pageHideBound = false;

    function requiredParts(scenario) {
        const specification = scenario?.validator || {};
        const parts = [];

        for (const item of specification.all || []) {
            parts.push(String(item));
        }

        for (const group of specification.any || []) {
            if (Array.isArray(group) && group.length > 0) {
                parts.push(String(group[0]));
            }
        }

        return parts.filter(Boolean);
    }

    function analyzeCode(value, scenario) {
        const code = String(value || "");
        const normalized = compact(code);
        const specification = scenario?.validator || {};
        const all = specification.all || [];
        const any = specification.any || [];

        let matched = 0;

        for (const item of all) {
            if (normalized.includes(compact(item))) {
                matched += 1;
            }
        }

        for (const group of any) {
            if (
                Array.isArray(group) &&
                group.some(item =>
                    normalized.includes(compact(item))
                )
            ) {
                matched += 1;
            }
        }

        const required = Math.max(1, all.length + any.length);
        const target = Math.max(
            12,
            requiredParts(scenario)
                .map(item => compact(item).length)
                .reduce((total, length) => total + length, 0)
        );

        return {
            currentCharacters:
                code.replace(/\\s/g, "").length,
            targetCharacters: target,
            structuralAccuracy:
                Math.round(matched * 100 / required),
            matchedElements: matched,
            requiredElements: required
        };
    }

    function buildHintReveal(scenario, percent) {
        const parts = requiredParts(scenario);
        const count = Math.max(
            1,
            Math.ceil(parts.length * percent / 100)
        );

        return parts
            .slice(0, count)
            .join("\\n");
    }

    function clearRoomHint(value) {
        value.hintUsed = false;
        value.hintPercent = 0;
        value.hintReveal = "";
        value.hintPenalty = 0;
    }

    function terminateActiveRun(course) {
        const state = load();
        const key = course === "python" ? "python" : "csharp";
        const value = state[key];

        if (!value.activeRun) {
            return;
        }

        value.activeRun = false;
        value.runComplete = false;
        value.runFailed = false;
        value.abandonedRuns =
            Number(value.abandonedRuns || 0) + 1;
        value.lastScore = value.score;
        value.scenario = null;
        value.score = 0;
        value.streak = 0;
        value.mistakes = 0;
        value.primaryResource = 100;
        value.secondaryResource = 0;
        value.threat = 0;
        value.runHintsUsed = 0;
        clearRoomHint(value);

        localStorage.setItem(KEY, JSON.stringify(state));
    }

    function pageHideHandler() {
        if (guardedCourse) {
            terminateActiveRun(guardedCourse);
        }
    }

    window.caveCodeMinigames = {'''

if helper_anchor in js:
    js = js.replace(helper_anchor, helper_replacement, 1)
elif "function analyzeCode" not in js:
    raise SystemExit("Could not add minigame analysis and exit helpers.")

start_old = '''            v.roomNumber=1;v.roomsTotal=v.endlessMode?10:5;v.score=0;v.streak=0;v.mistakes=0;
            v.primaryResource=100;v.secondaryResource=0;v.threat=0;v.scenario=generate(v,k);'''

start_new = '''            v.roomNumber=1;v.roomsTotal=v.endlessMode?10:5;v.score=0;v.streak=0;v.mistakes=0;
            v.primaryResource=100;v.secondaryResource=0;v.threat=0;
            v.runHintsUsed=0;clearRoomHint(v);v.scenario=generate(v,k);'''

if start_old in js:
    js = js.replace(start_old, start_new, 1)
elif "v.runHintsUsed=0;clearRoomHint(v)" not in js:
    raise SystemExit("Could not patch run initialization.")

score_old = '''            const eventScore=Math.max(250,score(v.difficulty)-v.mistakes*75+(v.streak>=2?100:0));
            let awardXp=fresh?xp(v.difficulty):20, crystals=fresh?(first?6:2):0;'''

score_new = '''            const eventScore=Math.max(
                250,
                score(v.difficulty)
                    - v.mistakes * 75
                    - Number(v.hintPenalty || 0)
                    + (v.streak >= 2 ? 100 : 0)
            );
            let awardXp=fresh?xp(v.difficulty):20, crystals=fresh?(first?6:2):0;

            if (v.hintUsed) {
                awardXp = Math.max(
                    0,
                    awardXp - (v.difficulty === "training" ? 10 : 15)
                );
            }'''

if score_old in js:
    js = js.replace(score_old, score_new, 1)
elif "awardXp - (v.difficulty" not in js:
    raise SystemExit("Could not add hint reward costs.")

perfect_old = '''                runCompleted=true;perfectRun=v.mistakes===0;awardXp+=100;crystals+=5;'''

perfect_new = '''                runCompleted=true;
                perfectRun=v.mistakes===0&&Number(v.runHintsUsed||0)===0;
                awardXp+=100;crystals+=5;'''

if perfect_old in js:
    js = js.replace(perfect_old, perfect_new, 1)
elif "Number(v.runHintsUsed||0)===0" not in js:
    raise SystemExit("Could not patch perfect-run eligibility.")

next_old = '''            }else{v.roomNumber++;v.scenario=generate(v,k);}
            v.totalXpEarned+=awardXp;v.totalCrystalsEarned+=crystals;'''

next_new = '''            }else{
                v.roomNumber++;
                clearRoomHint(v);
                v.scenario=generate(v,k);
            }
            v.totalXpEarned+=awardXp;v.totalCrystalsEarned+=crystals;'''

if next_old in js:
    js = js.replace(next_old, next_new, 1)
elif "clearRoomHint(v);" not in js.split("v.totalXpEarned")[0][-250:]:
    raise SystemExit("Could not reset hint state between rooms.")

end_method_anchor = '''        endRun(course){
            const s=load(),k=course==="python"?"python":"csharp",v=s[k];
            v.activeRun=false;v.runComplete=false;v.runFailed=false;v.scenario=null;v.score=0;v.streak=0;v.mistakes=0;v.primaryResource=100;v.secondaryResource=0;v.threat=0;
            save(s);return view(s,k);
        }
    };'''

end_method_replacement = '''        analyze(course,code){
            const s=load(),k=course==="python"?"python":"csharp",v=s[k];
            return analyzeCode(code,v.scenario);
        },
        useHint(course){
            const s=load(),k=course==="python"?"python":"csharp",v=s[k];

            if(!v.activeRun||!v.scenario){
                return{allowed:false,message:"Start a run before requesting a hint.",reveal:"",revealPercent:0,scoreCost:0,xpCost:0,state:view(s,k)};
            }

            if(!["training","standard"].includes(v.difficulty)){
                return{allowed:false,message:"Hints are available only in Training and Standard.",reveal:"",revealPercent:0,scoreCost:0,xpCost:0,state:view(s,k)};
            }

            if(v.hintUsed){
                return{allowed:false,message:"The one hint for this room has already been used.",reveal:v.hintReveal,revealPercent:v.hintPercent,scoreCost:v.hintPenalty,xpCost:v.difficulty==="training"?10:15,state:view(s,k)};
            }

            const percent=v.difficulty==="training"?50:35;
            const scoreCost=v.difficulty==="training"?150:200;
            const xpCost=v.difficulty==="training"?10:15;

            v.hintUsed=true;
            v.hintPercent=percent;
            v.hintPenalty=scoreCost;
            v.hintReveal=buildHintReveal(v.scenario,percent);
            v.runHintsUsed=Number(v.runHintsUsed||0)+1;

            save(s);

            return{
                allowed:true,
                message:`Revealed ${percent}% of the required code structure. This room loses ${scoreCost} possible points and ${xpCost} XP. The run is no longer eligible for a perfect-run bonus.`,
                reveal:v.hintReveal,
                revealPercent:percent,
                scoreCost,
                xpCost,
                state:view(s,k)
            };
        },
        resetRun(course){
            const s=load(),k=course==="python"?"python":"csharp",v=s[k];
            const difficulty=v.difficulty||"standard";
            const endless=Boolean(v.endlessMode)&&chapters(k)>=5;

            v.activeRun=true;
            v.runComplete=false;
            v.runFailed=false;
            v.endlessMode=endless;
            v.difficulty=difficulty;
            v.roomNumber=1;
            v.roomsTotal=endless?10:5;
            v.score=0;
            v.streak=0;
            v.mistakes=0;
            v.primaryResource=100;
            v.secondaryResource=0;
            v.threat=0;
            v.runHintsUsed=0;
            clearRoomHint(v);
            v.scenario=generate(v,k);

            save(s);
            return view(s,k);
        },
        bindExitGuard(course){
            guardedCourse=course==="python"?"python":"csharp";

            if(!pageHideBound){
                window.addEventListener("pagehide",pageHideHandler);
                pageHideBound=true;
            }

            return true;
        },
        unbindExitGuard(){
            guardedCourse=null;

            if(pageHideBound){
                window.removeEventListener("pagehide",pageHideHandler);
                pageHideBound=false;
            }

            return true;
        },
        endRun(course){
            const s=load(),k=course==="python"?"python":"csharp",v=s[k];

            if(v.activeRun){
                v.abandonedRuns=Number(v.abandonedRuns||0)+1;
                v.lastScore=v.score;
            }

            v.activeRun=false;
            v.runComplete=false;
            v.runFailed=false;
            v.scenario=null;
            v.score=0;
            v.streak=0;
            v.mistakes=0;
            v.primaryResource=100;
            v.secondaryResource=0;
            v.threat=0;
            v.runHintsUsed=0;
            clearRoomHint(v);

            save(s);
            return view(s,k);
        }
    };'''

if end_method_anchor in js:
    js = js.replace(
        end_method_anchor,
        end_method_replacement,
        1
    )
elif "useHint(course)" not in js:
    raise SystemExit("Could not add hint, reset, and exit-guard methods.")

js_path.write_text(js, encoding="utf-8")

# ---------------------------------------------------------------------
# CodeMinigame.razor
# ---------------------------------------------------------------------
component = component_path.read_text(encoding="utf-8")

header_old = '''@inject MinigameService MinigameService

<div class="mg-page">'''

header_new = '''@using Microsoft.AspNetCore.Components.Routing
@inject MinigameService MinigameService
@inject IJSRuntime JS

<NavigationLock ConfirmExternalNavigation="@State.ActiveRun"
                OnBeforeInternalNavigation="HandleInternalNavigation" />

<div class="mg-page">'''

if header_old in component:
    component = component.replace(header_old, header_new, 1)
elif "NavigationLock" not in component:
    raise SystemExit("Could not add the navigation guard.")

old_end_button = '''                <button class="secondary end" @onclick="EndRun">End this run</button>'''

new_end_buttons = '''                <div class="run-controls">
                    <button class="secondary"
                            @onclick="ResetRun">
                        Reset full run
                    </button>

                    <button class="secondary danger-button"
                            @onclick="QuitRun">
                        Quit run
                    </button>
                </div>'''

if old_end_button in component:
    component = component.replace(
        old_end_button,
        new_end_buttons,
        1
    )
elif "Reset full run" not in component:
    raise SystemExit("Could not replace the run-ending control.")

hint_anchor = '''                @if (State.Difficulty == "training")
                {
                    <div class="hint"><span>TRAINING HINT</span><strong>@State.Scenario.Hint</strong></div>
                }

                <label class="editor">'''

hint_replacement = '''                @if (State.Difficulty == "training")
                {
                    <div class="hint">
                        <span>TRAINING GUIDANCE</span>
                        <strong>@State.Scenario.Hint</strong>
                    </div>
                }

                @if (ShowCoaching)
                {
                    <section class="coaching-panel">
                        <div class="coaching-heading">
                            <div>
                                <span>LIVE CODING GUIDANCE</span>
                                <strong>
                                    @Analysis.StructuralAccuracy% structural accuracy
                                </strong>
                            </div>

                            <button type="button"
                                    class="hint-button"
                                    disabled="@State.HintUsed"
                                    @onclick="UseHint">
                                @(State.HintUsed
                                    ? "Hint used"
                                    : $"Reveal {HintRevealPercent}%")
                            </button>
                        </div>

                        <div class="coaching-grid">
                            <div>
                                <span>Current characters</span>
                                <strong>@Analysis.CurrentCharacters</strong>
                            </div>

                            <div>
                                <span>Estimated target</span>
                                <strong>@Analysis.TargetCharacters</strong>
                            </div>

                            <div>
                                <span>Required elements</span>
                                <strong>
                                    @Analysis.MatchedElements /
                                    @Analysis.RequiredElements
                                </strong>
                            </div>

                            <div>
                                <span>Hint cost</span>
                                <strong>@HintCostLabel</strong>
                            </div>
                        </div>

                        <div class="accuracy-track">
                            <i style="width:@Analysis.StructuralAccuracy%"></i>
                        </div>

                        @if (State.HintUsed)
                        {
                            <div class="hint-reveal">
                                <span>
                                    @State.HintPercent% CODE STRUCTURE REVEALED
                                </span>

                                <pre>@State.HintReveal</pre>

                                <small>
                                    This hint reduced the room's possible
                                    score and XP and removed perfect-run
                                    eligibility.
                                </small>
                            </div>
                        }
                    </section>
                }

                <label class="editor">'''

if hint_anchor in component:
    component = component.replace(
        hint_anchor,
        hint_replacement,
        1
    )
elif "LIVE CODING GUIDANCE" not in component:
    raise SystemExit("Could not add the live coaching panel.")

css_marker = '''    .challenge{padding:19px}'''

css_replacement = '''    .run-controls{display:grid;grid-template-columns:1fr 1fr;gap:7px;margin-top:auto}
    .danger-button{color:var(--danger-text);border-color:var(--danger-border);background:var(--danger-surface)}
    .coaching-panel{margin-top:9px;padding:12px;background:var(--surface-soft);border:1px solid var(--accent-border);border-radius:9px}
    .coaching-heading{display:flex;align-items:center;justify-content:space-between;gap:12px}
    .coaching-heading span,.coaching-grid span,.hint-reveal>span{display:block;color:var(--text-muted);font-size:8px;text-transform:uppercase}
    .coaching-heading strong{display:block;margin-top:4px;color:var(--accent);font-size:12px}
    .hint-button{padding:8px 10px;color:var(--accent);background:var(--accent-surface);border:1px solid var(--accent-border);font-size:9px}
    .hint-button:disabled{color:var(--text-muted);background:var(--surface-strong);border-color:var(--border)}
    .coaching-grid{display:grid;grid-template-columns:repeat(4,1fr);gap:6px;margin-top:10px}
    .coaching-grid div{padding:8px;background:var(--surface);border:1px solid var(--border);border-radius:7px}
    .coaching-grid strong{display:block;margin-top:4px;font-size:11px}
    .accuracy-track{height:8px;margin-top:9px;overflow:hidden;background:var(--surface-strong);border-radius:99px}
    .accuracy-track i{display:block;height:100%;background:var(--accent);transition:width .16s ease}
    .hint-reveal{margin-top:10px;padding:10px;color:var(--warning-text);background:var(--warning-surface);border:1px solid var(--warning-border);border-radius:8px}
    .hint-reveal pre{margin:7px 0;padding:9px;overflow:auto;color:var(--code-text,#e9f1f7);background:var(--code-bg,#0b1118);border-radius:7px;font:11px/1.5 Consolas,monospace;white-space:pre-wrap}
    .hint-reveal small{color:var(--warning-text);font-size:8px;line-height:1.45}
'''

if ".coaching-panel{" not in component:
    if css_marker not in component:
        raise SystemExit(
            "Could not find the challenge CSS insertion point."
        )

    component = component.replace(
        css_marker,
        css_replacement + css_marker,
        1
    )

mobile_old = '''@@media(max-width:780px){.mg-header,.game{grid-template-columns:1fr}.stats{grid-template-columns:repeat(2,1fr)}.side{min-height:auto}}'''

mobile_new = '''@@media(max-width:780px){.mg-header,.game{grid-template-columns:1fr}.stats{grid-template-columns:repeat(2,1fr)}.coaching-grid{grid-template-columns:repeat(2,1fr)}.side{min-height:auto}}'''

if mobile_old in component:
    component = component.replace(mobile_old, mobile_new, 1)

fields_anchor = '''    private MinigameCourseState State = new();
    private MinigameCompletionResult? LastReward;'''

fields_replacement = '''    private MinigameCourseState State = new();
    private MinigameCompletionResult? LastReward;
    private MinigameAnalysisResult Analysis = new();'''

if fields_anchor in component:
    component = component.replace(
        fields_anchor,
        fields_replacement,
        1
    )

property_anchor = '''    private string Placeholder => State.Difficulty == "expert" ? "Expert mode begins blank." : "Write your solution here.";
    private int PotentialScore => Math.Max(250, (State.Difficulty switch { "training" => 600, "advanced" => 1000, "expert" => 1250, _ => 800 }) - State.Mistakes * 75);'''

property_replacement = '''    private string Placeholder => State.Difficulty == "expert" ? "Expert mode begins blank." : "Write your solution here.";
    private bool ShowCoaching =>
        State.Difficulty is "training" or "standard";
    private int HintRevealPercent =>
        State.Difficulty == "training" ? 50 : 35;
    private string HintCostLabel =>
        State.Difficulty == "training"
            ? "150 points · 10 XP"
            : "200 points · 15 XP";
    private int PotentialScore => Math.Max(
        250,
        (State.Difficulty switch
        {
            "training" => 600,
            "advanced" => 1000,
            "expert" => 1250,
            _ => 800
        }) - State.Mistakes * 75 - State.HintPenalty);'''

if property_anchor in component:
    component = component.replace(
        property_anchor,
        property_replacement,
        1
    )
elif "private bool ShowCoaching" not in component:
    raise SystemExit("Could not add coaching properties.")

load_old = '''        State = await MinigameService.GetCourseStateAsync(Course);
        LoadCode();
        Ready = true;'''

load_new = '''        State = await MinigameService.GetCourseStateAsync(Course);
        LoadCode();
        await RefreshAnalysis();

        if (State.ActiveRun)
        {
            await JS.InvokeVoidAsync(
                "caveCodeMinigames.bindExitGuard",
                Course);
        }

        Ready = true;'''

if load_old in component:
    component = component.replace(load_old, load_new, 1)

start_old = '''        SystemStatus = "Awaiting validated code";
        LoadCode();
    }

    private void UpdateCode(ChangeEventArgs args) =>
        StudentCode = args.Value?.ToString() ?? "";'''

start_new = '''        SystemStatus = "Awaiting validated code";
        LoadCode();
        await RefreshAnalysis();

        await JS.InvokeVoidAsync(
            "caveCodeMinigames.bindExitGuard",
            Course);
    }

    private async Task UpdateCode(ChangeEventArgs args)
    {
        StudentCode = args.Value?.ToString() ?? "";
        await RefreshAnalysis();
    }'''

if start_old in component:
    component = component.replace(start_old, start_new, 1)
elif "private async Task UpdateCode" not in component:
    raise SystemExit("Could not convert code updates to live analysis.")

reset_code_old = '''        SystemStatus = "Awaiting validated code";
    }

    private async Task RunCode()'''

reset_code_new = '''        SystemStatus = "Awaiting validated code";
        _ = RefreshAnalysis();
    }

    private async Task RunCode()'''

if reset_code_old in component:
    component = component.replace(
        reset_code_old,
        reset_code_new,
        1
    )

continue_old = '''        SystemStatus = State.RunComplete ? "Campaign stabilized" : "Awaiting validated code";
        LoadCode();
    }

    private async Task EndRun()
    {
        State = await MinigameService.EndRunAsync(Course);
        StudentCode = "";
        SystemSuccess = false;
    }

    private void LoadCode() =>
        StudentCode = State.Scenario?.StarterCode ?? "";
}'''

continue_new = '''        SystemStatus = State.RunComplete ? "Campaign stabilized" : "Awaiting validated code";
        LoadCode();

        if (State.RunComplete)
        {
            _ = JS.InvokeVoidAsync(
                "caveCodeMinigames.unbindExitGuard");
        }
        else
        {
            _ = RefreshAnalysis();
        }
    }

    private async Task UseHint()
    {
        MinigameHintResult result =
            await MinigameService.UseHintAsync(Course);

        State = result.State;
        FeedbackClass =
            result.Allowed ? "" : "error";
        FeedbackHeading =
            result.Allowed
                ? $"{result.RevealPercent}% hint revealed"
                : "Hint unavailable";
        FeedbackMessage = result.Message;

        await RefreshAnalysis();
    }

    private async Task ResetRun()
    {
        bool confirmed =
            await JS.InvokeAsync<bool>(
                "confirm",
                "Reset this entire run? Your room progress, score, streak, health or stability, and current scenario will be lost.");

        if (!confirmed)
        {
            return;
        }

        State =
            await MinigameService.ResetRunAsync(
                Course);

        LastReward = null;
        ShowReward = false;
        SystemSuccess = false;
        FeedbackClass = "";
        FeedbackHeading = "Run reset";
        FeedbackMessage =
            "A new randomized Room 1 has been generated using the same difficulty.";
        SystemStatus = "Awaiting validated code";
        LoadCode();
        await RefreshAnalysis();
    }

    private async Task QuitRun()
    {
        bool confirmed =
            await JS.InvokeAsync<bool>(
                "confirm",
                "Quit this minigame run? The run will end immediately and its current progress cannot be resumed.");

        if (!confirmed)
        {
            return;
        }

        await TerminateRun();
    }

    private async ValueTask HandleInternalNavigation(
        LocationChangingContext context)
    {
        if (!State.ActiveRun)
        {
            return;
        }

        bool confirmed =
            await JS.InvokeAsync<bool>(
                "confirm",
                "Leaving this minigame will immediately end the active run. Continue?");

        if (!confirmed)
        {
            context.PreventNavigation();
            return;
        }

        await TerminateRun();
    }

    private async Task TerminateRun()
    {
        State =
            await MinigameService.EndRunAsync(
                Course);

        await JS.InvokeVoidAsync(
            "caveCodeMinigames.unbindExitGuard");

        StudentCode = "";
        SystemSuccess = false;
        ShowReward = false;
        FeedbackClass = "";
        FeedbackHeading = "Run ended";
        FeedbackMessage =
            "The active minigame session has been closed.";
        SystemStatus = "Run inactive";
    }

    private async Task RefreshAnalysis()
    {
        if (!State.ActiveRun ||
            State.Scenario is null ||
            !ShowCoaching)
        {
            Analysis = new();
            return;
        }

        Analysis =
            await MinigameService.AnalyzeAsync(
                Course,
                StudentCode);

        await InvokeAsync(StateHasChanged);
    }

    private void LoadCode() =>
        StudentCode = State.Scenario?.StarterCode ?? "";
}'''

if continue_old in component:
    component = component.replace(
        continue_old,
        continue_new,
        1
    )
elif "private async Task UseHint" not in component:
    raise SystemExit("Could not add reset, quit, hint, and navigation handlers.")

component_path.write_text(component, encoding="utf-8")

# Cache-bust the updated JavaScript.
index = index_path.read_text(encoding="utf-8")
index = index.replace(
    "js/caveCodeMinigames.js?v=3",
    "js/caveCodeMinigames.js?v=4"
)
index = index.replace(
    "js/caveCodeMinigames.js?v=2",
    "js/caveCodeMinigames.js?v=4"
)
index_path.write_text(index, encoding="utf-8")

print("CaveCode minigame rules, exit guard, live guidance, and hint pass installed.")
print()
print("Rules added:")
print("  - Active difficulty remains locked for the current run")
print("  - Reset Full Run generates a new Room 1 using the same difficulty")
print("  - Quit Run immediately closes the active session")
print("  - Internal navigation shows a warning before leaving")
print("  - Refresh, close-tab, and external navigation use the browser leave warning")
print("  - A completed departure records the run as abandoned")
print("  - Returning to the minigame starts from the launch area")
print("  - Multi-word string validation is corrected")
print()
print("Guidance added:")
print("  - Training and Standard only")
print("  - Live non-whitespace character count")
print("  - Estimated target character count")
print("  - Structural accuracy percentage")
print("  - Required-elements matched counter")
print()
print("Hints added:")
print("  - One hint per room")
print("  - Training reveals 50%: -150 possible score and -10 XP")
print("  - Standard reveals 35%: -200 possible score and -15 XP")
print("  - Any hint removes perfect-run eligibility")
print("  - Advanced and Expert have no hint button")
print()
print("Backups saved in .minigame-rules-hints-backup/")
print("Next command: dotnet build")
