namespace CaveCode.CourseEngine;

/// <summary>
/// Full GCL+ Control Line Lab curriculum: 5 chapters × 8 modules = 40 lessons.
/// Language syntax and semantics only — no vendor, brand, or origin references.
/// </summary>
public static class GclCourseLessons
{
    public const int PlayableModuleCount = 40;
    public const int ChapterCount = 5;
    public const int ModulesPerChapter = 8;

    public static IReadOnlyList<CourseLesson> All { get; } =
        new[]
        {
            // ========== Chapter 1 · Line Foundations ==========
            new CourseLesson(
                "Chapter 1 · Line Foundations",
                "Program structure",
                "Start a Control Sequence",
                "A GCL+ program is a list of statements that the controller evaluates in scan order. Each statement ends when its logic is complete. Comments begin with // and are ignored by the runtime. Programs typically mix variable declarations, assignments, and conditional blocks.",
                "// Control Line Lab — startup shell\nVariable Mode As Integer\nMode = 1",
                "// Control Line Lab — startup shell\nVariable Mode As Integer\nMode = 1",
                "// Control Line Lab — startup shell\nVariable Mode As ___\nMode = ___",
                "What does the controller do with lines that begin with // ?",
                new[] { "Treats them as errors", "Executes them as commands", "Ignores them as comments", "Stores them as variables" },
                2,
                "Lines that start with // are comments. The runtime skips them while scanning the program.",
                "Variable Mode As Integer\nMode = 1",
                "Add a comment line that documents the program purpose, and keep the variable declaration.",
                "Declare an Integer variable named Mode and assign it the value 1. Include a short // comment above the declaration.",
                "Declare an Integer variable named State and assign it the value 0. Include a short // comment above the declaration.",
                "// Sequence state\nVariable State As Integer\nState = 0",
                "The first control line is online and ready for values."
            )
            {
                ConceptPoints = new[] { "Programs are scanned top to bottom.", "// starts a comment that the runtime ignores.", "Statements perform declarations, assignments, or control flow." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 1 · Line Foundations",
                "Comments",
                "Document the Sequence",
                "Comments explain why a block exists. Place them above the logic they describe. Keep comments short and focused on intent so future edits stay safe. Never put executable code on the same line after // if you still need that code to run.",
                "// Enable path for occupied mode\nVariable OccEnable As Integer\nOccEnable = 1",
                "// Enable path for occupied mode\nVariable OccEnable As Integer\nOccEnable = 1",
                "// ___ path for occupied mode\nVariable OccEnable As Integer\nOccEnable = 1",
                "Where should a clarifying comment usually be placed?",
                new[] { "After the End If only", "Above the block it describes", "Inside a variable name", "Instead of the assignment" },
                1,
                "Place comments above the logic they document so the next reader sees intent before the code.",
                "Variable OccEnable As Integer\nOccEnable = 1",
                "Add a // comment that states this is the occupied enable path.",
                "Write a // comment that says Enable path for occupied mode, then declare OccEnable as Integer and set it to 1.",
                "Write a // comment that says Manual override flag, then declare ManualOvrd as Integer and set it to 0.",
                "// Manual override flag\nVariable ManualOvrd As Integer\nManualOvrd = 0",
                "Operators can now read the intent of each block."
            )
            {
                ConceptPoints = new[] { "Comments document intent, not every token.", "Place comments above the related block.", "Commented text is never executed." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 1 · Line Foundations",
                "Variables",
                "Store Operating Values",
                "Declare storage with Variable Name As Type. Integer holds whole numbers, Real holds floating-point values, and String holds text. Declarations usually appear near the top of a program or section so later statements can use the names.",
                "Variable SaTemp As Real\nVariable FanCmd As Integer\nVariable StatusTxt As String",
                "Variable SaTemp As Real\nVariable FanCmd As Integer\nVariable StatusTxt As String",
                "Variable SaTemp As ___\nVariable FanCmd As ___\nVariable StatusTxt As ___",
                "Which type is appropriate for a temperature reading such as 72.5?",
                new[] { "Integer", "Real", "String", "IfOnce" },
                1,
                "Real stores fractional numeric values such as temperatures and percentages.",
                "Variable SaTemp As Integer\nVariable FanCmd As Real",
                "SaTemp should be Real for fractional degrees. FanCmd is typically Integer for on/off or stage counts.",
                "Declare SaTemp as Real, FanCmd as Integer, and StatusTxt as String.",
                "Declare RaTemp as Real, HeatStage as Integer, and ModeTxt as String.",
                "Variable RaTemp As Real\nVariable HeatStage As Integer\nVariable ModeTxt As String",
                "Local storage for live operating values is ready."
            )
            {
                ConceptPoints = new[] { "Variable Name As Type creates storage.", "Integer for whole numbers, Real for fractions, String for text.", "Declare before use in the scan." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 1 · Line Foundations",
                "Assignment",
                "Write Values to Points",
                "Use = to write a value into a variable or object. The left side is the destination; the right side is the value or expression. Assignments execute every scan unless guarded by a condition.",
                "Variable Sp As Real\nSp = 72.0\nVariable ModeTxt As String\nModeTxt = \"Occupied\"",
                "Variable Sp As Real\nSp = 72.0\nVariable ModeTxt As String\nModeTxt = \"Occupied\"",
                "Variable Sp As Real\nSp = ___\nVariable ModeTxt As String\nModeTxt = \"___\"",
                "In the statement Sp = 72.0, what is Sp?",
                new[] { "The comment marker", "The destination being written", "A duration timer", "A remote device name" },
                1,
                "The left side of = is the destination that receives the value from the right side.",
                "Variable Sp As Real\nSp == 72.0",
                "Assignment uses a single =. Double equals is for comparison in conditions.",
                "Declare Sp as Real and assign 72.0. Declare ModeTxt as String and assign the text Occupied.",
                "Declare MinPos as Real and assign 20.0. Declare StateTxt as String and assign the text Ready.",
                "Variable MinPos As Real\nMinPos = 20.0\nVariable StateTxt As String\nStateTxt = \"Ready\"",
                "Setpoints and status text can now be written each scan."
            )
            {
                ConceptPoints = new[] { "Left side receives the value.", "Right side may be a literal or expression.", "Assignments run every scan unless guarded." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 1 · Line Foundations",
                "If conditions",
                "Gate Actions with If",
                "If Condition Then ... End If runs the enclosed statements only while the condition is true. Use If to protect equipment commands, stage enables, and writes that must not happen in the wrong mode.",
                "Variable Demand As Integer\nVariable Stage1 As Integer\nDemand = 1\nIf Demand = 1 Then\n  Stage1 = 1\nEnd If",
                "Variable Demand As Integer\nVariable Stage1 As Integer\nDemand = 1\nIf Demand = 1 Then\n  Stage1 = 1\nEnd If",
                "Variable Demand As Integer\nVariable Stage1 As Integer\nDemand = 1\nIf Demand = 1 ___\n  Stage1 = 1\nEnd If",
                "What keyword closes an If block?",
                new[] { "End Program", "End If", "Stop", "Return" },
                1,
                "Every If ... Then block is closed with End If.",
                "If Demand = 1\n  Stage1 = 1\nEnd If",
                "The Then keyword is required after the condition.",
                "When Demand equals 1, set Stage1 to 1 inside an If ... Then ... End If block.",
                "When Enable equals 1, set FanCmd to 1 inside an If ... Then ... End If block.",
                "Variable Enable As Integer\nVariable FanCmd As Integer\nEnable = 1\nIf Enable = 1 Then\n  FanCmd = 1\nEnd If",
                "Conditional gates now protect stage and command writes."
            )
            {
                ConceptPoints = new[] { "If Condition Then starts a guarded block.", "End If closes the block.", "Statements inside run only while the condition is true." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 1 · Line Foundations",
                "Comparisons",
                "Compare Live Readings",
                "Relational operators test values: > < >= <= = and <>. In GCL+ conditions, a single = often tests equality. Use comparisons inside If to decide when setpoints are satisfied or limits are crossed.",
                "Variable SaTemp As Real\nVariable Sp As Real\nSaTemp = 75.0\nSp = 72.0\nIf SaTemp > Sp Then\n  // cooling demand present\nEnd If",
                "Variable SaTemp As Real\nVariable Sp As Real\nSaTemp = 75.0\nSp = 72.0\nIf SaTemp > Sp Then\n  // cooling demand present\nEnd If",
                "Variable SaTemp As Real\nVariable Sp As Real\nSaTemp = 75.0\nSp = 72.0\nIf SaTemp ___ Sp Then\n  // cooling demand present\nEnd If",
                "Which operator tests whether the left value is greater than the right value?",
                new[] { "<", ">", "<>", "=" },
                1,
                "The > operator is true when the left operand is greater than the right operand.",
                "If SaTemp > Sp\nEnd If",
                "Include Then after the comparison and keep a body or comment inside the block.",
                "If SaTemp is greater than Sp, enter an If block (you may leave a comment inside).",
                "If RaTemp is less than Sp, enter an If block with a short comment inside.",
                "Variable RaTemp As Real\nVariable Sp As Real\nRaTemp = 68.0\nSp = 70.0\nIf RaTemp < Sp Then\n  // heating demand present\nEnd If",
                "Threshold tests can now drive demand decisions."
            )
            {
                ConceptPoints = new[] { "Use >, <, >=, <=, =, <> inside conditions.", "Comparisons produce true or false for If.", "Single = is commonly used for equality tests in this language." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 1 · Line Foundations",
                "Logical operators",
                "Combine Multiple Conditions",
                "And requires every part to be true. Or is true if any part is true. Not inverts a condition. Combine them to express precise enable rules such as mode and demand together.",
                "Variable Mode As Integer\nVariable Demand As Integer\nVariable Stage1 As Integer\nMode = 1\nDemand = 1\nIf Mode = 1 And Demand = 1 Then\n  Stage1 = 1\nEnd If",
                "Variable Mode As Integer\nVariable Demand As Integer\nVariable Stage1 As Integer\nMode = 1\nDemand = 1\nIf Mode = 1 And Demand = 1 Then\n  Stage1 = 1\nEnd If",
                "Variable Mode As Integer\nVariable Demand As Integer\nVariable Stage1 As Integer\nMode = 1\nDemand = 1\nIf Mode = 1 ___ Demand = 1 Then\n  Stage1 = 1\nEnd If",
                "Which operator is true only when both sides are true?",
                new[] { "Or", "Not", "And", "Changed" },
                2,
                "And is true only when every joined condition is true.",
                "If Mode = 1 Or Demand = 1 Then\n  Stage1 = 1\nEnd If",
                "For a strict enable that needs both mode and demand, use And.",
                "Enable Stage1 only when Mode equals 1 And Demand equals 1.",
                "Enable FanCmd only when Auto equals 1 And Prove equals 1.",
                "Variable Auto As Integer\nVariable Prove As Integer\nVariable FanCmd As Integer\nAuto = 1\nProve = 1\nIf Auto = 1 And Prove = 1 Then\n  FanCmd = 1\nEnd If",
                "Compound enable rules are now expressible."
            )
            {
                ConceptPoints = new[] { "And requires all parts true.", "Or is true if any part is true.", "Not inverts a condition." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 1 · Line Foundations",
                "Integrated foundations",
                "Assemble a Minimal Sequence",
                "Combine a comment, variable declarations, assignment, and a guarded If into one small safe program. This pattern is the foundation of every later sequence.",
                "// Manual enable path\nVariable Enable As Integer\nVariable OutCmd As Integer\nEnable = 1\nIf Enable = 1 Then\n  OutCmd = 1\nEnd If",
                "// Manual enable path\nVariable Enable As Integer\nVariable OutCmd As Integer\nEnable = 1\nIf Enable = 1 Then\n  OutCmd = 1\nEnd If",
                "// Manual enable path\nVariable Enable As ___\nVariable OutCmd As ___\nEnable = 1\nIf Enable = 1 Then\n  OutCmd = ___\nEnd If",
                "What is the safest way to prevent OutCmd from writing when Enable is off?",
                new[] { "Delete the variable", "Guard the assignment with If", "Use only comments", "Assign twice" },
                1,
                "Place the write inside an If so it only runs while the enable condition is true.",
                "Variable Enable As Integer\nOutCmd = 1",
                "Declare both variables and wrap the OutCmd assignment in If Enable = 1 Then ... End If.",
                "Write a commented manual enable path that sets OutCmd to 1 only when Enable equals 1.",
                "Write a commented auto path that sets HeatCmd to 1 only when HeatEnable equals 1.",
                "// Auto heat path\nVariable HeatEnable As Integer\nVariable HeatCmd As Integer\nHeatEnable = 1\nIf HeatEnable = 1 Then\n  HeatCmd = 1\nEnd If",
                "Foundation sequences are complete. Timing tools come next."
            )
            {
                ConceptPoints = new[] { "Comments, declarations, assignment, and If form the core pattern.", "Always guard equipment commands.", "Keep names clear and consistent." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            // ========== Chapter 2 · Timing and Edges ==========
            new CourseLesson(
                "Chapter 2 · Timing and Edges",
                "IfOnce",
                "Act on a Rising Edge",
                "IfOnce runs its block only on the scan where the condition becomes true. It does not repeat on later scans while the condition stays true. Use it for resets, one-shot latches, and edge-triggered actions.",
                "Variable AlarmClr As Integer\nVariable AlarmLatched As Integer\nAlarmClr = 1\nAlarmLatched = 1\nIfOnce AlarmClr = 1 Then\n  AlarmLatched = 0\nEnd If",
                "Variable AlarmClr As Integer\nVariable AlarmLatched As Integer\nAlarmClr = 1\nAlarmLatched = 1\nIfOnce AlarmClr = 1 Then\n  AlarmLatched = 0\nEnd If",
                "Variable AlarmClr As Integer\nVariable AlarmLatched As Integer\nAlarmClr = 1\nAlarmLatched = 1\n___ AlarmClr = 1 Then\n  AlarmLatched = 0\nEnd If",
                "How many times does IfOnce execute its block while the condition remains true after the first true scan?",
                new[] { "Every scan", "Twice per second", "Only on the rising edge", "Never" },
                2,
                "IfOnce fires once when the condition transitions to true, then stays quiet until the condition goes false and true again.",
                "If AlarmClr = 1 Then\n  AlarmLatched = 0\nEnd If",
                "Use IfOnce instead of If when the clear must happen only on the rising edge.",
                "Clear AlarmLatched to 0 with IfOnce when AlarmClr equals 1.",
                "Clear FailLatched to 0 with IfOnce when Reset equals 1.",
                "Variable Reset As Integer\nVariable FailLatched As Integer\nReset = 1\nFailLatched = 1\nIfOnce Reset = 1 Then\n  FailLatched = 0\nEnd If",
                "One-shot resets are now available."
            )
            {
                ConceptPoints = new[] { "IfOnce reacts to the false-to-true transition.", "It does not retrigger while the condition stays true.", "Ideal for reset and latch-clear actions." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 2 · Timing and Edges",
                "OnFor",
                "Require Continuous True Time",
                "Condition OnFor Duration is true only after the condition has stayed true continuously for the given time. Use it to prove alarms, filter noise, and delay stage starts.",
                "Variable FanStatus As Integer\nVariable FanFail As Integer\nFanStatus = 0\nIf FanStatus = 0 OnFor 30S Then\n  FanFail = 1\nEnd If",
                "Variable FanStatus As Integer\nVariable FanFail As Integer\nFanStatus = 0\nIf FanStatus = 0 OnFor 30S Then\n  FanFail = 1\nEnd If",
                "Variable FanStatus As Integer\nVariable FanFail As Integer\nFanStatus = 0\nIf FanStatus = 0 ___ 30S Then\n  FanFail = 1\nEnd If",
                "What does OnFor 30S require before the If body can run?",
                new[] { "A single true scan", "Thirty true scans only", "The condition true continuously for 30 seconds", "A remote device reply" },
                2,
                "OnFor measures continuous true time. The body runs only after that duration has elapsed without interruption.",
                "If FanStatus = 0 Then\n  FanFail = 1\nEnd If",
                "Add OnFor 30S so the failure must be proven for thirty seconds.",
                "Set FanFail to 1 only when FanStatus equals 0 OnFor 30S.",
                "Set HeatFail to 1 only when HeatStatus equals 0 OnFor 45S.",
                "Variable HeatStatus As Integer\nVariable HeatFail As Integer\nHeatStatus = 0\nIf HeatStatus = 0 OnFor 45S Then\n  HeatFail = 1\nEnd If",
                "Prove timers now filter fleeting status glitches."
            )
            {
                ConceptPoints = new[] { "OnFor requires continuous true time.", "Common units include S for seconds and M for minutes.", "Use for alarm prove and delayed enables." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 2 · Timing and Edges",
                "OffFor",
                "Require Continuous False Time",
                "Condition OffFor Duration becomes true only after the condition has stayed false for the full duration. Use it for recovery delays and minimum off times.",
                "Variable StageCmd As Integer\nVariable CanRestart As Integer\nStageCmd = 0\nIf StageCmd = 0 OffFor 5M Then\n  CanRestart = 1\nEnd If",
                "Variable StageCmd As Integer\nVariable CanRestart As Integer\nStageCmd = 0\nIf StageCmd = 0 OffFor 5M Then\n  CanRestart = 1\nEnd If",
                "Variable StageCmd As Integer\nVariable CanRestart As Integer\nStageCmd = 0\nIf StageCmd = 0 ___ 5M Then\n  CanRestart = 1\nEnd If",
                "OffFor is most often used to enforce which kind of delay?",
                new[] { "Minimum on time only", "Minimum off or recovery time", "Scan rate changes", "String formatting" },
                1,
                "OffFor waits until a condition has been false long enough, which is ideal for minimum off and recovery timers.",
                "If StageCmd = 0 Then\n  CanRestart = 1\nEnd If",
                "Add OffFor 5M so restart is allowed only after five minutes off.",
                "Set CanRestart to 1 when StageCmd equals 0 OffFor 5M.",
                "Set Ready to 1 when CompCmd equals 0 OffFor 3M.",
                "Variable CompCmd As Integer\nVariable Ready As Integer\nCompCmd = 0\nIf CompCmd = 0 OffFor 3M Then\n  Ready = 1\nEnd If",
                "Recovery and anti-cycle off delays are in place."
            )
            {
                ConceptPoints = new[] { "OffFor measures continuous false time.", "Useful for minimum off and clear delays.", "Pairs with OnFor in anti-cycle designs." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 2 · Timing and Edges",
                "DoEvery",
                "Run Periodic Tasks",
                "DoEvery Interval ... End Do runs the block on a repeating schedule. Use it for data exchange, slow housekeeping, and any work that should not run every scan.",
                "Variable RemoteTemp As Real\nDoEvery 15M\n  RemoteTemp = OaSensor\nEnd Do",
                "Variable RemoteTemp As Real\nDoEvery 15M\n  RemoteTemp = OaSensor\nEnd Do",
                "Variable RemoteTemp As Real\n___ 15M\n  RemoteTemp = OaSensor\nEnd Do",
                "How often does a DoEvery 15M block evaluate its body?",
                new[] { "Every scan", "Once only at startup", "Approximately every 15 minutes", "Only on alarm" },
                2,
                "DoEvery schedules the block to run at the stated interval rather than every scan.",
                "If 15M Then\n  RemoteTemp = OaSensor\nEnd If",
                "Use DoEvery 15M ... End Do for periodic work.",
                "Every 15M, assign OaSensor into RemoteTemp inside a DoEvery block.",
                "Every 5M, assign ZoneTemp into LoggedTemp inside a DoEvery block.",
                "Variable LoggedTemp As Real\nDoEvery 5M\n  LoggedTemp = ZoneTemp\nEnd Do",
                "Periodic exchange and logging blocks are ready."
            )
            {
                ConceptPoints = new[] { "DoEvery runs on a time interval.", "End Do closes the block.", "Ideal for import/export and slow tasks." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 2 · Timing and Edges",
                "Changed",
                "React to Value Changes",
                "Changed(Point) is true on the scan where the point value differs from the previous scan. Use it to react to operator edits or sudden sensor jumps without polling constantly.",
                "Variable Sp As Real\nVariable SpEdited As Integer\nSp = 72.0\nIf Changed(Sp) Then\n  SpEdited = 1\nEnd If",
                "Variable Sp As Real\nVariable SpEdited As Integer\nSp = 72.0\nIf Changed(Sp) Then\n  SpEdited = 1\nEnd If",
                "Variable Sp As Real\nVariable SpEdited As Integer\nSp = 72.0\nIf ___(Sp) Then\n  SpEdited = 1\nEnd If",
                "When is Changed(Sp) true?",
                new[] { "Every scan while Sp is non-zero", "Only on the scan Sp differs from its previous value", "Only at midnight", "Whenever Sp equals the setpoint" },
                1,
                "Changed is an edge detector for value differences between consecutive scans.",
                "If Sp = 72.0 Then\n  SpEdited = 1\nEnd If",
                "Wrap the reaction in If Changed(Sp) Then so it fires only when the value changes.",
                "Set SpEdited to 1 inside If Changed(Sp) Then ... End If.",
                "Set ModeEdited to 1 inside If Changed(Mode) Then ... End If.",
                "Variable Mode As Integer\nVariable ModeEdited As Integer\nMode = 1\nIf Changed(Mode) Then\n  ModeEdited = 1\nEnd If",
                "Change-driven reactions are available for operator inputs."
            )
            {
                ConceptPoints = new[] { "Changed detects a difference from the prior scan.", "Fires for one scan on the change.", "Useful for setpoint and mode edits." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 2 · Timing and Edges",
                "Anti-cycle",
                "Protect Equipment from Short Cycling",
                "Anti-cycle logic combines a stage flag with OffFor (or OnFor) so a stage cannot restart until a minimum off time has passed. This protects compressors and burners from rapid cycling.",
                "Variable Stage1 As Integer\nVariable Stage1Ready As Integer\nStage1 = 0\nIf Stage1 = 0 OffFor 5M Then\n  Stage1Ready = 1\nEnd If\nIf Stage1Ready = 1 And Demand = 1 Then\n  Stage1 = 1\nEnd If",
                "Variable Stage1 As Integer\nVariable Stage1Ready As Integer\nVariable Demand As Integer\nStage1 = 0\nDemand = 1\nIf Stage1 = 0 OffFor 5M Then\n  Stage1Ready = 1\nEnd If\nIf Stage1Ready = 1 And Demand = 1 Then\n  Stage1 = 1\nEnd If",
                "Variable Stage1 As Integer\nVariable Stage1Ready As Integer\nStage1 = 0\nIf Stage1 = 0 ___ 5M Then\n  Stage1Ready = 1\nEnd If",
                "Why is a minimum off time used before allowing a stage to restart?",
                new[] { "To change the scan rate", "To protect equipment from short cycling", "To format status text", "To declare variables" },
                1,
                "Short cycling stresses mechanical equipment. A forced off time reduces that risk.",
                "If Stage1 = 0 Then\n  Stage1Ready = 1\nEnd If",
                "Require OffFor 5M before Stage1Ready becomes true.",
                "Set Stage1Ready when Stage1 has been 0 OffFor 5M, then allow Stage1 to start when ready and Demand is 1.",
                "Set CompReady when Comp has been 0 OffFor 4M, then allow Comp to start when ready and CoolDemand is 1.",
                "Variable Comp As Integer\nVariable CompReady As Integer\nVariable CoolDemand As Integer\nComp = 0\nCoolDemand = 1\nIf Comp = 0 OffFor 4M Then\n  CompReady = 1\nEnd If\nIf CompReady = 1 And CoolDemand = 1 Then\n  Comp = 1\nEnd If",
                "Anti-cycle protection is now part of the staging toolkit."
            )
            {
                ConceptPoints = new[] { "Pair stage flags with OffFor for minimum off time.", "Only enable the stage when the ready flag and demand are both true.", "Protects compressors and heat stages." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 2 · Timing and Edges",
                "Deadbands",
                "Avoid Chatter Around Setpoints",
                "A deadband uses different thresholds for on and off so the command does not chatter when the process variable hovers near the setpoint. Stage on above Sp + OnBand and off below Sp - OffBand (or the reverse for heating).",
                "Variable SaTemp As Real\nVariable Sp As Real\nVariable CoolCmd As Integer\nSaTemp = 76.0\nSp = 72.0\nIf SaTemp > Sp + 2.0 Then\n  CoolCmd = 1\nEnd If\nIf SaTemp < Sp - 1.0 Then\n  CoolCmd = 0\nEnd If",
                "Variable SaTemp As Real\nVariable Sp As Real\nVariable CoolCmd As Integer\nSaTemp = 76.0\nSp = 72.0\nIf SaTemp > Sp + 2.0 Then\n  CoolCmd = 1\nEnd If\nIf SaTemp < Sp - 1.0 Then\n  CoolCmd = 0\nEnd If",
                "Variable SaTemp As Real\nVariable Sp As Real\nVariable CoolCmd As Integer\nSaTemp = 76.0\nSp = 72.0\nIf SaTemp > Sp + ___ Then\n  CoolCmd = 1\nEnd If\nIf SaTemp < Sp - 1.0 Then\n  CoolCmd = 0\nEnd If",
                "What problem does a deadband primarily prevent?",
                new[] { "Missing comments", "Rapid on/off chatter near the setpoint", "Remote path syntax errors", "Integer overflow" },
                1,
                "Separate on and off thresholds stop the output from oscillating when the value sits near the target.",
                "If SaTemp > Sp Then\n  CoolCmd = 1\nEnd If",
                "Use an on threshold above Sp and an off threshold below Sp.",
                "Turn CoolCmd on when SaTemp > Sp + 2.0 and off when SaTemp < Sp - 1.0.",
                "Turn HeatCmd on when RaTemp < Sp - 2.0 and off when RaTemp > Sp + 1.0.",
                "Variable RaTemp As Real\nVariable Sp As Real\nVariable HeatCmd As Integer\nRaTemp = 66.0\nSp = 70.0\nIf RaTemp < Sp - 2.0 Then\n  HeatCmd = 1\nEnd If\nIf RaTemp > Sp + 1.0 Then\n  HeatCmd = 0\nEnd If",
                "Stable on/off decisions around setpoints are in place."
            )
            {
                ConceptPoints = new[] { "Use different thresholds for on and off.", "Deadband reduces chatter and wear.", "Apply to stages, valves, and binary commands." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 2 · Timing and Edges",
                "Integrated timing",
                "Build a Timed Stage Enable",
                "Combine deadband, OffFor anti-cycle, and a demand check into one protected stage enable. This is the standard pattern for mechanical stages.",
                "Variable Temp As Real\nVariable Sp As Real\nVariable Stage1 As Integer\nVariable Ready As Integer\nTemp = 76.0\nSp = 72.0\nIf Stage1 = 0 OffFor 5M Then\n  Ready = 1\nEnd If\nIf Ready = 1 And Temp > Sp + 2.0 Then\n  Stage1 = 1\nEnd If\nIf Temp < Sp - 1.0 Then\n  Stage1 = 0\nEnd If",
                "Variable Temp As Real\nVariable Sp As Real\nVariable Stage1 As Integer\nVariable Ready As Integer\nTemp = 76.0\nSp = 72.0\nIf Stage1 = 0 OffFor 5M Then\n  Ready = 1\nEnd If\nIf Ready = 1 And Temp > Sp + 2.0 Then\n  Stage1 = 1\nEnd If\nIf Temp < Sp - 1.0 Then\n  Stage1 = 0\nEnd If",
                "Variable Temp As Real\nVariable Sp As Real\nVariable Stage1 As Integer\nVariable Ready As Integer\nIf Stage1 = 0 ___ 5M Then\n  Ready = 1\nEnd If\nIf Ready = 1 And Temp > Sp + 2.0 Then\n  Stage1 = 1\nEnd If",
                "Which three ideas appear together in a protected stage enable?",
                new[] { "Comments only", "Anti-cycle timer, deadband, and demand check", "String formatting only", "Remote path only" },
                1,
                "A robust stage uses a ready timer, a demand threshold with deadband, and a clear off path.",
                "If Temp > Sp Then\n  Stage1 = 1\nEnd If",
                "Add OffFor readiness and separate on/off thresholds.",
                "Implement Stage1 with 5M OffFor ready, on when Temp > Sp + 2.0, off when Temp < Sp - 1.0.",
                "Implement Stage2 with 4M OffFor ready, on when Temp > Sp + 3.0, off when Temp < Sp - 0.5.",
                "Variable Temp As Real\nVariable Sp As Real\nVariable Stage2 As Integer\nVariable Ready2 As Integer\nTemp = 78.0\nSp = 72.0\nIf Stage2 = 0 OffFor 4M Then\n  Ready2 = 1\nEnd If\nIf Ready2 = 1 And Temp > Sp + 3.0 Then\n  Stage2 = 1\nEnd If\nIf Temp < Sp - 0.5 Then\n  Stage2 = 0\nEnd If",
                "Timed, protected staging is complete. Control math is next."
            )
            {
                ConceptPoints = new[] { "Merge OffFor, deadband, and demand.", "Keep on and off paths explicit.", "This pattern scales to multiple stages." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            // ========== Chapter 3 · Control Math ==========
            new CourseLesson(
                "Chapter 3 · Control Math",
                "Min and Max",
                "Clamp to Safe Bounds",
                "Min(A, B) returns the smaller value. Max(A, B) returns the larger value. Use them to enforce minimum damper positions, maximum commands, and other hard limits.",
                "Variable PidOut As Real\nVariable MinPos As Real\nVariable Cmd As Real\nPidOut = 10.0\nMinPos = 20.0\nCmd = Min(PidOut, 100.0)\nCmd = Max(Cmd, MinPos)",
                "Variable PidOut As Real\nVariable MinPos As Real\nVariable Cmd As Real\nPidOut = 10.0\nMinPos = 20.0\nCmd = Min(PidOut, 100.0)\nCmd = Max(Cmd, MinPos)",
                "Variable PidOut As Real\nVariable MinPos As Real\nVariable Cmd As Real\nPidOut = 10.0\nMinPos = 20.0\nCmd = ___(PidOut, 100.0)\nCmd = ___(Cmd, MinPos)",
                "If PidOut is 10 and MinPos is 20, what does Max(PidOut, MinPos) return?",
                new[] { "10", "20", "30", "0" },
                1,
                "Max returns the larger of the two arguments, so 20 is selected.",
                "Cmd = PidOut",
                "Clamp with Min against the high limit and Max against the minimum position.",
                "Set Cmd to Min(PidOut, 100.0) then Max(Cmd, MinPos) where MinPos is 20.",
                "Set Out to Min(Raw, 90.0) then Max(Out, 15.0).",
                "Variable Raw As Real\nVariable Out As Real\nRaw = 5.0\nOut = Min(Raw, 90.0)\nOut = Max(Out, 15.0)",
                "Hard minimum and maximum bounds are enforceable."
            )
            {
                ConceptPoints = new[] { "Min selects the smaller value.", "Max selects the larger value.", "Use them to enforce equipment limits." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 3 · Control Math",
                "Limit",
                "Restrict a Value Range",
                "Limit(Value, Low, High) forces the result into the inclusive range Low..High. It is a concise way to clamp PID outputs and calculated positions.",
                "Variable PidOut As Real\nVariable Cmd As Real\nPidOut = 120.0\nCmd = Limit(PidOut, 0.0, 100.0)",
                "Variable PidOut As Real\nVariable Cmd As Real\nPidOut = 120.0\nCmd = Limit(PidOut, 0.0, 100.0)",
                "Variable PidOut As Real\nVariable Cmd As Real\nPidOut = 120.0\nCmd = ___(PidOut, 0.0, 100.0)",
                "What does Limit(120.0, 0.0, 100.0) return?",
                new[] { "120.0", "0.0", "100.0", "20.0" },
                2,
                "Limit clamps the value into the stated low and high bounds, so 120 becomes 100.",
                "Cmd = PidOut",
                "Wrap the assignment with Limit(..., 0.0, 100.0).",
                "Set Cmd to Limit(PidOut, 0.0, 100.0).",
                "Set Valve to Limit(Calc, 10.0, 90.0).",
                "Variable Calc As Real\nVariable Valve As Real\nCalc = 95.0\nValve = Limit(Calc, 10.0, 90.0)",
                "Output ranges can be constrained in one expression."
            )
            {
                ConceptPoints = new[] { "Limit(Value, Low, High) clamps inclusively.", "Common for 0-100 percent commands.", "Prefer Limit when both ends must be enforced together." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 3 · Control Math",
                "Scale",
                "Map One Range to Another",
                "Scale maps a value from an input span to an output span. Use it to convert sensor signals or normalized PID results into engineering units or percent commands.",
                "Variable Input As Real\nVariable Cmd As Real\nInput = 5.0\nCmd = Scale(Input, 0.0, 10.0, 0.0, 100.0)",
                "Variable Input As Real\nVariable Cmd As Real\nInput = 5.0\nCmd = Scale(Input, 0.0, 10.0, 0.0, 100.0)",
                "Variable Input As Real\nVariable Cmd As Real\nInput = 5.0\nCmd = ___(Input, 0.0, 10.0, 0.0, 100.0)",
                "Scale(5.0, 0.0, 10.0, 0.0, 100.0) produces which result?",
                new[] { "5.0", "10.0", "50.0", "100.0" },
                2,
                "Halfway through a 0-10 input maps to halfway through a 0-100 output, which is 50.",
                "Cmd = Input * 10",
                "Prefer the Scale function with explicit input and output spans.",
                "Map Input from 0-10 into Cmd 0-100 using Scale.",
                "Map Signal from 4-20 into Out 0-100 using Scale.",
                "Variable Signal As Real\nVariable Out As Real\nSignal = 12.0\nOut = Scale(Signal, 4.0, 20.0, 0.0, 100.0)",
                "Linear range mapping is available for sensors and commands."
            )
            {
                ConceptPoints = new[] { "Scale converts between numeric spans.", "Provide input low/high and output low/high.", "Keeps mapping intent explicit." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 3 · Control Math",
                "PID bias",
                "Track and Apply Bias",
                "Bias lets a control loop resume smoothly after a mode or source change. Track the current command into a bias variable while in the alternate mode, then add bias when returning to automatic control so the output does not jump.",
                "Variable PidOut As Real\nVariable Bias As Real\nVariable ManualCmd As Real\nVariable Auto As Integer\nVariable Cmd As Real\nAuto = 0\nManualCmd = 40.0\nIf Auto = 0 Then\n  Bias = ManualCmd - PidOut\n  Cmd = ManualCmd\nEnd If\nIf Auto = 1 Then\n  Cmd = PidOut + Bias\nEnd If",
                "Variable PidOut As Real\nVariable Bias As Real\nVariable ManualCmd As Real\nVariable Auto As Integer\nVariable Cmd As Real\nAuto = 0\nManualCmd = 40.0\nPidOut = 25.0\nIf Auto = 0 Then\n  Bias = ManualCmd - PidOut\n  Cmd = ManualCmd\nEnd If\nIf Auto = 1 Then\n  Cmd = PidOut + Bias\nEnd If",
                "Variable PidOut As Real\nVariable Bias As Real\nVariable ManualCmd As Real\nVariable Auto As Integer\nVariable Cmd As Real\nIf Auto = 0 Then\n  Bias = ManualCmd - PidOut\n  Cmd = ManualCmd\nEnd If\nIf Auto = 1 Then\n  Cmd = PidOut + ___\nEnd If",
                "Why is bias added to the PID output when returning to automatic?",
                new[] { "To delete the setpoint", "To keep the command continuous (bumpless)", "To force a zero output", "To change the scan order" },
                1,
                "Adding the tracked bias prevents a sudden jump when control authority switches back to the PID.",
                "Cmd = PidOut",
                "While manual, track Bias = ManualCmd - PidOut. While auto, Cmd = PidOut + Bias.",
                "Implement bias tracking so manual mode stores Bias and auto mode applies PidOut + Bias.",
                "Implement bias so Override mode tracks Bias = OvrdCmd - LoopOut and Auto applies LoopOut + Bias.",
                "Variable LoopOut As Real\nVariable Bias As Real\nVariable OvrdCmd As Real\nVariable Auto As Integer\nVariable Cmd As Real\nAuto = 0\nOvrdCmd = 55.0\nLoopOut = 30.0\nIf Auto = 0 Then\n  Bias = OvrdCmd - LoopOut\n  Cmd = OvrdCmd\nEnd If\nIf Auto = 1 Then\n  Cmd = LoopOut + Bias\nEnd If",
                "Bumpless transfer between modes is supported."
            )
            {
                ConceptPoints = new[] { "Track bias while in the alternate mode.", "Apply bias when returning to automatic.", "Prevents output jumps at mode change." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 3 · Control Math",
                "Setpoints",
                "Hold and Adjust Targets",
                "Keep operator setpoints and calculated setpoints in clearly named variables. Separate occupied and unoccupied targets so mode logic can select the active value without overwriting the operator entry.",
                "Variable OccSp As Real\nVariable UnoccSp As Real\nVariable ActiveSp As Real\nVariable Occupied As Integer\nOccSp = 72.0\nUnoccSp = 78.0\nOccupied = 1\nIf Occupied = 1 Then\n  ActiveSp = OccSp\nEnd If\nIf Occupied = 0 Then\n  ActiveSp = UnoccSp\nEnd If",
                "Variable OccSp As Real\nVariable UnoccSp As Real\nVariable ActiveSp As Real\nVariable Occupied As Integer\nOccSp = 72.0\nUnoccSp = 78.0\nOccupied = 1\nIf Occupied = 1 Then\n  ActiveSp = OccSp\nEnd If\nIf Occupied = 0 Then\n  ActiveSp = UnoccSp\nEnd If",
                "Variable OccSp As Real\nVariable UnoccSp As Real\nVariable ActiveSp As Real\nVariable Occupied As Integer\nOccSp = 72.0\nUnoccSp = 78.0\nOccupied = 1\nIf Occupied = 1 Then\n  ActiveSp = ___\nEnd If",
                "Why store OccSp and UnoccSp separately from ActiveSp?",
                new[] { "So mode can switch targets without erasing operator values", "Because Real cannot hold two numbers", "To force comments", "To disable IfOnce" },
                0,
                "Keeping source setpoints intact lets the program switch ActiveSp by mode without losing the operator entries.",
                "ActiveSp = 72.0",
                "Select ActiveSp from OccSp or UnoccSp based on Occupied.",
                "When Occupied is 1 use OccSp; when 0 use UnoccSp for ActiveSp.",
                "When DayMode is 1 use DaySp; when 0 use NightSp for ActiveSp.",
                "Variable DaySp As Real\nVariable NightSp As Real\nVariable ActiveSp As Real\nVariable DayMode As Integer\nDaySp = 70.0\nNightSp = 64.0\nDayMode = 1\nIf DayMode = 1 Then\n  ActiveSp = DaySp\nEnd If\nIf DayMode = 0 Then\n  ActiveSp = NightSp\nEnd If",
                "Setpoint selection by mode is clean and reversible."
            )
            {
                ConceptPoints = new[] { "Keep source setpoints separate from the active target.", "Switch ActiveSp by mode.", "Protect operator-entered values from overwrite." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 3 · Control Math",
                "Staging",
                "Enable Sequential Stages",
                "Sequential staging turns stage N on only when stage N-1 is already on and additional demand exists. Turn stages off from the highest first. This matches capacity to load smoothly.",
                "Variable Demand As Integer\nVariable S1 As Integer\nVariable S2 As Integer\nDemand = 2\nIf Demand >= 1 Then\n  S1 = 1\nEnd If\nIf Demand >= 2 And S1 = 1 Then\n  S2 = 1\nEnd If\nIf Demand < 2 Then\n  S2 = 0\nEnd If\nIf Demand < 1 Then\n  S1 = 0\nEnd If",
                "Variable Demand As Integer\nVariable S1 As Integer\nVariable S2 As Integer\nDemand = 2\nIf Demand >= 1 Then\n  S1 = 1\nEnd If\nIf Demand >= 2 And S1 = 1 Then\n  S2 = 1\nEnd If\nIf Demand < 2 Then\n  S2 = 0\nEnd If\nIf Demand < 1 Then\n  S1 = 0\nEnd If",
                "Variable Demand As Integer\nVariable S1 As Integer\nVariable S2 As Integer\nDemand = 2\nIf Demand >= 1 Then\n  S1 = 1\nEnd If\nIf Demand >= 2 And S1 = ___ Then\n  S2 = 1\nEnd If",
                "In sequential staging, when may stage 2 start?",
                new[] { "Anytime Demand is non-zero", "Only when stage 1 is already on and demand requires more capacity", "Only at midnight", "Only after IfOnce" },
                1,
                "Stage 2 is interlocked behind stage 1 so capacity increases in order.",
                "S2 = 1",
                "Require S1 = 1 and sufficient Demand before setting S2.",
                "Enable S1 when Demand >= 1 and S2 when Demand >= 2 And S1 = 1; clear in reverse.",
                "Enable H1 when HeatDem >= 1 and H2 when HeatDem >= 2 And H1 = 1; clear in reverse.",
                "Variable HeatDem As Integer\nVariable H1 As Integer\nVariable H2 As Integer\nHeatDem = 2\nIf HeatDem >= 1 Then\n  H1 = 1\nEnd If\nIf HeatDem >= 2 And H1 = 1 Then\n  H2 = 1\nEnd If\nIf HeatDem < 2 Then\n  H2 = 0\nEnd If\nIf HeatDem < 1 Then\n  H1 = 0\nEnd If",
                "Ordered capacity staging is working."
            )
            {
                ConceptPoints = new[] { "Higher stages require lower stages to be on.", "Drop stages from the top first.", "Match capacity to measured demand." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 3 · Control Math",
                "Non-sequential enable",
                "Allow Flexible Stage Selection",
                "When the equipment allows it, stages may start based on availability, runtime, or priority rather than strict order. Still protect each stage with its own anti-cycle timer.",
                "Variable Demand As Integer\nVariable S1Avail As Integer\nVariable S2Avail As Integer\nVariable S1 As Integer\nVariable S2 As Integer\nDemand = 1\nS1Avail = 0\nS2Avail = 1\nIf Demand >= 1 And S1Avail = 1 Then\n  S1 = 1\nEnd If\nIf Demand >= 1 And S1 = 0 And S2Avail = 1 Then\n  S2 = 1\nEnd If",
                "Variable Demand As Integer\nVariable S1Avail As Integer\nVariable S2Avail As Integer\nVariable S1 As Integer\nVariable S2 As Integer\nDemand = 1\nS1Avail = 0\nS2Avail = 1\nIf Demand >= 1 And S1Avail = 1 Then\n  S1 = 1\nEnd If\nIf Demand >= 1 And S1 = 0 And S2Avail = 1 Then\n  S2 = 1\nEnd If",
                "Variable Demand As Integer\nVariable S1Avail As Integer\nVariable S2Avail As Integer\nVariable S1 As Integer\nVariable S2 As Integer\nIf Demand >= 1 And S1 = 0 And S2Avail = ___ Then\n  S2 = 1\nEnd If",
                "What does non-sequential staging allow?",
                new[] { "Skipping unavailable stages when another stage can serve demand", "Deleting setpoints", "Removing OffFor forever", "Ignoring all limits" },
                0,
                "If stage 1 is unavailable, an allowed design can start stage 2 when demand exists and stage 2 is ready.",
                "S2 = 1",
                "Only start S2 when demand exists, S1 is not already on, and S2 is available.",
                "Prefer S1 when available; otherwise allow S2 when Demand >= 1 and S2Avail = 1.",
                "Prefer A when AAvail = 1; otherwise allow B when Demand >= 1 and BAvail = 1.",
                "Variable Demand As Integer\nVariable AAvail As Integer\nVariable BAvail As Integer\nVariable A As Integer\nVariable B As Integer\nDemand = 1\nAAvail = 0\nBAvail = 1\nIf Demand >= 1 And AAvail = 1 Then\n  A = 1\nEnd If\nIf Demand >= 1 And A = 0 And BAvail = 1 Then\n  B = 1\nEnd If",
                "Flexible stage selection is available when the plant design allows it."
            )
            {
                ConceptPoints = new[] { "Availability flags can override strict order when permitted.", "Still apply per-stage anti-cycle protection.", "Document the plant rule that allows non-sequential starts." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 3 · Control Math",
                "Integrated math",
                "Build a Limited Damper Command",
                "Combine Scale or PID output with Min/Limit and bias into one safe damper command. This is the core of economizer and mixed-air positioning.",
                "Variable PidOut As Real\nVariable MinPos As Real\nVariable Bias As Real\nVariable Cmd As Real\nPidOut = 35.0\nMinPos = 20.0\nBias = 0.0\nCmd = Limit(PidOut + Bias, MinPos, 100.0)",
                "Variable PidOut As Real\nVariable MinPos As Real\nVariable Bias As Real\nVariable Cmd As Real\nPidOut = 35.0\nMinPos = 20.0\nBias = 0.0\nCmd = Limit(PidOut + Bias, MinPos, 100.0)",
                "Variable PidOut As Real\nVariable MinPos As Real\nVariable Bias As Real\nVariable Cmd As Real\nCmd = ___(PidOut + Bias, MinPos, 100.0)",
                "Which function simultaneously enforces a low and high bound on the damper command?",
                new[] { "Changed", "Limit", "IfOnce", "DoEvery" },
                1,
                "Limit applies both the minimum position and the 100 percent ceiling in one step.",
                "Cmd = PidOut",
                "Add Bias and wrap with Limit(..., MinPos, 100.0).",
                "Set Cmd to Limit(PidOut + Bias, MinPos, 100.0) with MinPos at 20.",
                "Set Pos to Limit(Loop + Bias, 15.0, 100.0).",
                "Variable Loop As Real\nVariable Bias As Real\nVariable Pos As Real\nLoop = 40.0\nBias = 5.0\nPos = Limit(Loop + Bias, 15.0, 100.0)",
                "Safe, biased damper commands are ready. Objects and exchange are next."
            )
            {
                ConceptPoints = new[] { "Add bias then clamp with Limit or Min/Max.", "Always enforce a minimum position when required.", "One expression keeps the command path clear." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            // ========== Chapter 4 · Objects and Exchange ==========
            new CourseLesson(
                "Chapter 4 · Objects and Exchange",
                "Local points",
                "Read and Write Local Objects",
                "Local points live in the current program context. Read them in conditions and write them with assignment. Keep names consistent with the point list so operators and the sequence stay aligned.",
                "Variable FanCmd As Integer\nVariable FanStatus As Integer\nFanCmd = 1\nIf FanStatus = 1 Then\n  // fan proven\nEnd If",
                "Variable FanCmd As Integer\nVariable FanStatus As Integer\nFanCmd = 1\nIf FanStatus = 1 Then\n  // fan proven\nEnd If",
                "Variable FanCmd As Integer\nVariable FanStatus As Integer\nFanCmd = ___\nIf FanStatus = 1 Then\n  // fan proven\nEnd If",
                "What is a local point in this context?",
                new[] { "A comment only", "A value that belongs to the current program context", "A remote path only", "A timer unit" },
                1,
                "Local points are addressed by name inside the program that owns them.",
                "FanCmd == 1",
                "Use a single = for assignment to the local command point.",
                "Set FanCmd to 1 and test FanStatus inside an If.",
                "Set HeatCmd to 1 and test HeatStatus inside an If.",
                "Variable HeatCmd As Integer\nVariable HeatStatus As Integer\nHeatCmd = 1\nIf HeatStatus = 1 Then\n  // heat proven\nEnd If",
                "Local command and status points are under sequence control."
            )
            {
                ConceptPoints = new[] { "Local points are named objects in the current program.", "Read them in conditions; write them with =.", "Match names to the point list." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 4 · Objects and Exchange",
                "Remote references",
                "Address Remote Device Objects",
                "Remote values use device.object notation. Read outdoor conditions, shared setpoints, or other controller points this way. Writes to remote objects follow the same path form when permitted.",
                "Variable OaTemp As Real\nOaTemp = OA.Temp\nVariable SharedSp As Real\nSharedSp = Plant.CoolSp",
                "Variable OaTemp As Real\nOaTemp = OA.Temp\nVariable SharedSp As Real\nSharedSp = Plant.CoolSp",
                "Variable OaTemp As Real\nOaTemp = ___ .Temp\nVariable SharedSp As Real\nSharedSp = Plant.___",
                "In the reference OA.Temp, what does the part before the dot identify?",
                new[] { "A comment", "The remote device or program context", "A timer", "An IfOnce flag" },
                1,
                "The qualifier before the dot selects the remote device or context; the name after selects the object.",
                "OaTemp = Temp",
                "Use the device.object form such as OA.Temp.",
                "Read OA.Temp into OaTemp and Plant.CoolSp into SharedSp.",
                "Read RA.Temp into RaTemp and Plant.HeatSp into HeatSp.",
                "Variable RaTemp As Real\nRaTemp = RA.Temp\nVariable HeatSp As Real\nHeatSp = Plant.HeatSp",
                "Remote sensor and setpoint paths are usable in the sequence."
            )
            {
                ConceptPoints = new[] { "device.object addresses a remote point.", "Use remote reads for shared sensors and setpoints.", "Keep path names stable and documented." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 4 · Objects and Exchange",
                "CALL",
                "Invoke Another Program Block",
                "CALL runs a named routine or program section. Use it to share alarm reset logic, common interlocks, or repeated calculations without copying the same lines into every sequence.",
                "Variable NeedReset As Integer\nNeedReset = 1\nIf NeedReset = 1 Then\n  CALL AlarmReset\nEnd If",
                "Variable NeedReset As Integer\nNeedReset = 1\nIf NeedReset = 1 Then\n  CALL AlarmReset\nEnd If",
                "Variable NeedReset As Integer\nNeedReset = 1\nIf NeedReset = 1 Then\n  ___ AlarmReset\nEnd If",
                "What does CALL AlarmReset do?",
                new[] { "Deletes the alarm point", "Invokes the named routine AlarmReset", "Changes the scan rate", "Declares a variable" },
                1,
                "CALL transfers execution to the named block so shared logic can be reused.",
                "AlarmReset",
                "Prefix the routine name with CALL.",
                "When NeedReset equals 1, CALL AlarmReset.",
                "When NeedSync equals 1, CALL DataSync.",
                "Variable NeedSync As Integer\nNeedSync = 1\nIf NeedSync = 1 Then\n  CALL DataSync\nEnd If",
                "Shared routines can be invoked on demand."
            )
            {
                ConceptPoints = new[] { "CALL invokes a named routine.", "Use it to reuse reset and interlock logic.", "Keeps sequences shorter and consistent." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 4 · Objects and Exchange",
                "Data exchange",
                "Import and Publish Values",
                "Exchange data on a schedule with DoEvery. Read remote values into local variables and publish local results back out so other programs stay synchronized without scanning every point every cycle.",
                "Variable OaTemp As Real\nVariable LocalSp As Real\nDoEvery 15M\n  OaTemp = OA.Temp\n  Plant.UnitSp = LocalSp\nEnd Do",
                "Variable OaTemp As Real\nVariable LocalSp As Real\nLocalSp = 72.0\nDoEvery 15M\n  OaTemp = OA.Temp\n  Plant.UnitSp = LocalSp\nEnd Do",
                "Variable OaTemp As Real\nVariable LocalSp As Real\n___ 15M\n  OaTemp = OA.Temp\n  Plant.UnitSp = LocalSp\nEnd Do",
                "Why place import and publish assignments inside DoEvery?",
                new[] { "To run them only at a chosen interval instead of every scan", "To disable comments", "To force Integer types", "To remove remote paths" },
                0,
                "Periodic exchange reduces unnecessary traffic and keeps slow data updates intentional.",
                "OaTemp = OA.Temp\nPlant.UnitSp = LocalSp",
                "Wrap the exchange lines in DoEvery 15M ... End Do.",
                "Every 15M read OA.Temp into OaTemp and write LocalSp to Plant.UnitSp.",
                "Every 10M read RA.Temp into RaTemp and write ActiveSp to Plant.ZoneSp.",
                "Variable RaTemp As Real\nVariable ActiveSp As Real\nActiveSp = 70.0\nDoEvery 10M\n  RaTemp = RA.Temp\n  Plant.ZoneSp = ActiveSp\nEnd Do",
                "Scheduled data exchange is in place."
            )
            {
                ConceptPoints = new[] { "DoEvery schedules import and publish work.", "Read remote sensors; write shared setpoints.", "Choose an interval that matches how often the data must move." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 4 · Objects and Exchange",
                "State text",
                "Present Readable Status",
                "Assign clear String values that operators can read on graphics and reports. Update the text whenever the underlying state changes so the display stays truthful.",
                "Variable FanCmd As Integer\nVariable FanFail As Integer\nVariable FanTxt As String\nFanCmd = 1\nFanFail = 0\nIf FanFail = 1 Then\n  FanTxt = \"Failed\"\nEnd If\nIf FanFail = 0 And FanCmd = 1 Then\n  FanTxt = \"Running\"\nEnd If\nIf FanCmd = 0 And FanFail = 0 Then\n  FanTxt = \"Off\"\nEnd If",
                "Variable FanCmd As Integer\nVariable FanFail As Integer\nVariable FanTxt As String\nFanCmd = 1\nFanFail = 0\nIf FanFail = 1 Then\n  FanTxt = \"Failed\"\nEnd If\nIf FanFail = 0 And FanCmd = 1 Then\n  FanTxt = \"Running\"\nEnd If\nIf FanCmd = 0 And FanFail = 0 Then\n  FanTxt = \"Off\"\nEnd If",
                "Variable FanCmd As Integer\nVariable FanFail As Integer\nVariable FanTxt As String\nIf FanFail = 1 Then\n  FanTxt = \"___\"\nEnd If",
                "What should state text reflect?",
                new[] { "Only the programmer name", "The current operating condition in plain language", "Only numeric setpoints", "Scan order" },
                1,
                "Operators rely on short, accurate phrases such as Running, Failed, or Off.",
                "FanTxt = 1",
                "Assign String literals that describe the state.",
                "Set FanTxt to Failed, Running, or Off based on FanFail and FanCmd.",
                "Set HeatTxt to Fault, On, or Off based on HeatFail and HeatCmd.",
                "Variable HeatCmd As Integer\nVariable HeatFail As Integer\nVariable HeatTxt As String\nHeatCmd = 1\nHeatFail = 0\nIf HeatFail = 1 Then\n  HeatTxt = \"Fault\"\nEnd If\nIf HeatFail = 0 And HeatCmd = 1 Then\n  HeatTxt = \"On\"\nEnd If\nIf HeatCmd = 0 And HeatFail = 0 Then\n  HeatTxt = \"Off\"\nEnd If",
                "Readable status text is available for graphics."
            )
            {
                ConceptPoints = new[] { "Use String variables for operator-facing status.", "Update text when the underlying state changes.", "Prefer short, unambiguous phrases." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 4 · Objects and Exchange",
                "Alarms",
                "Raise and Clear Alarms",
                "Detect abnormal conditions with proven timers, latch an alarm point, and provide a clear path. Alarms should be sticky until acknowledged or until the condition and reset rules are satisfied.",
                "Variable FanStatus As Integer\nVariable FanCmd As Integer\nVariable FanAlarm As Integer\nFanCmd = 1\nFanStatus = 0\nIf FanCmd = 1 And FanStatus = 0 OnFor 30S Then\n  FanAlarm = 1\nEnd If\nIf FanCmd = 0 OffFor 10S Then\n  FanAlarm = 0\nEnd If",
                "Variable FanStatus As Integer\nVariable FanCmd As Integer\nVariable FanAlarm As Integer\nFanCmd = 1\nFanStatus = 0\nIf FanCmd = 1 And FanStatus = 0 OnFor 30S Then\n  FanAlarm = 1\nEnd If\nIf FanCmd = 0 OffFor 10S Then\n  FanAlarm = 0\nEnd If",
                "Variable FanStatus As Integer\nVariable FanCmd As Integer\nVariable FanAlarm As Integer\nIf FanCmd = 1 And FanStatus = 0 ___ 30S Then\n  FanAlarm = 1\nEnd If",
                "Why prove a fan failure with OnFor before setting the alarm?",
                new[] { "To avoid nuisance trips from brief status glitches", "To change variable types", "To remove remote paths", "To force sequential staging" },
                0,
                "A short status drop should not immediately raise a hard alarm; the prove time filters noise.",
                "If FanStatus = 0 Then\n  FanAlarm = 1\nEnd If",
                "Require FanCmd = 1 And FanStatus = 0 OnFor 30S before latching the alarm.",
                "Latch FanAlarm when commanded on but status is false OnFor 30S; clear after FanCmd has been off OffFor 10S.",
                "Latch PumpAlarm when PumpCmd = 1 And PumpStatus = 0 OnFor 20S; clear after PumpCmd = 0 OffFor 5S.",
                "Variable PumpCmd As Integer\nVariable PumpStatus As Integer\nVariable PumpAlarm As Integer\nPumpCmd = 1\nPumpStatus = 0\nIf PumpCmd = 1 And PumpStatus = 0 OnFor 20S Then\n  PumpAlarm = 1\nEnd If\nIf PumpCmd = 0 OffFor 5S Then\n  PumpAlarm = 0\nEnd If",
                "Proven alarm raise and clear paths are working."
            )
            {
                ConceptPoints = new[] { "Prove abnormal conditions with OnFor.", "Latch the alarm point.", "Define a clear recovery path." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 4 · Objects and Exchange",
                "Failures and reset",
                "Handle Failure and Manual Reset",
                "Hard failures often stay latched until an operator reset. Use IfOnce on the reset signal so a held reset button does not continuously retrigger logic.",
                "Variable FailLatched As Integer\nVariable ResetBtn As Integer\nVariable FanAlarm As Integer\nFailLatched = 1\nResetBtn = 1\nIfOnce ResetBtn = 1 Then\n  FailLatched = 0\n  FanAlarm = 0\nEnd If",
                "Variable FailLatched As Integer\nVariable ResetBtn As Integer\nVariable FanAlarm As Integer\nFailLatched = 1\nResetBtn = 1\nIfOnce ResetBtn = 1 Then\n  FailLatched = 0\n  FanAlarm = 0\nEnd If",
                "Variable FailLatched As Integer\nVariable ResetBtn As Integer\nVariable FanAlarm As Integer\n___ ResetBtn = 1 Then\n  FailLatched = 0\n  FanAlarm = 0\nEnd If",
                "Why use IfOnce for an operator reset button?",
                new[] { "So the clear runs only on the rising edge of the reset", "To declare strings", "To force DoEvery", "To disable Min" },
                0,
                "IfOnce ensures a single clear action even if the operator holds the reset input true.",
                "If ResetBtn = 1 Then\n  FailLatched = 0\nEnd If",
                "Use IfOnce so the reset is edge-triggered.",
                "On IfOnce ResetBtn = 1, clear FailLatched and FanAlarm.",
                "On IfOnce ClearBtn = 1, clear Lockout and HeatAlarm.",
                "Variable Lockout As Integer\nVariable ClearBtn As Integer\nVariable HeatAlarm As Integer\nLockout = 1\nClearBtn = 1\nIfOnce ClearBtn = 1 Then\n  Lockout = 0\n  HeatAlarm = 0\nEnd If",
                "Manual reset paths are safe and edge-triggered."
            )
            {
                ConceptPoints = new[] { "Latch hard failures until reset.", "Clear with IfOnce on the reset input.", "Reset may clear both the latch and related alarms." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 4 · Objects and Exchange",
                "Integrated objects",
                "Link Local and Remote Points",
                "Combine a local fan command, a remote outdoor temperature read, status text, and an alarm into one small monitored sequence.",
                "Variable FanCmd As Integer\nVariable FanStatus As Integer\nVariable FanTxt As String\nVariable FanAlarm As Integer\nVariable OaTemp As Real\nOaTemp = OA.Temp\nFanCmd = 1\nIf FanCmd = 1 And FanStatus = 0 OnFor 30S Then\n  FanAlarm = 1\n  FanTxt = \"Failed\"\nEnd If\nIf FanAlarm = 0 And FanCmd = 1 Then\n  FanTxt = \"Running\"\nEnd If",
                "Variable FanCmd As Integer\nVariable FanStatus As Integer\nVariable FanTxt As String\nVariable FanAlarm As Integer\nVariable OaTemp As Real\nOaTemp = OA.Temp\nFanCmd = 1\nIf FanCmd = 1 And FanStatus = 0 OnFor 30S Then\n  FanAlarm = 1\n  FanTxt = \"Failed\"\nEnd If\nIf FanAlarm = 0 And FanCmd = 1 Then\n  FanTxt = \"Running\"\nEnd If",
                "Variable FanCmd As Integer\nVariable FanStatus As Integer\nVariable FanTxt As String\nVariable FanAlarm As Integer\nVariable OaTemp As Real\nOaTemp = ___ .Temp\nFanCmd = 1\nIf FanCmd = 1 And FanStatus = 0 OnFor 30S Then\n  FanAlarm = 1\n  FanTxt = \"Failed\"\nEnd If",
                "Which elements belong in a minimal monitored fan sequence?",
                new[] { "Only comments", "Command, status, prove timer, alarm, and status text", "Only Scale", "Only DoEvery at 1S" },
                1,
                "A practical monitored line commands the fan, proves status, latches an alarm, and shows text.",
                "FanCmd = 1",
                "Add remote OA read, prove-based alarm, and FanTxt updates.",
                "Read OA.Temp, command the fan, latch FanAlarm on prove failure, and set FanTxt accordingly.",
                "Read RA.Temp, command a pump, latch PumpAlarm on prove failure, and set PumpTxt accordingly.",
                "Variable PumpCmd As Integer\nVariable PumpStatus As Integer\nVariable PumpTxt As String\nVariable PumpAlarm As Integer\nVariable RaTemp As Real\nRaTemp = RA.Temp\nPumpCmd = 1\nIf PumpCmd = 1 And PumpStatus = 0 OnFor 20S Then\n  PumpAlarm = 1\n  PumpTxt = \"Failed\"\nEnd If\nIf PumpAlarm = 0 And PumpCmd = 1 Then\n  PumpTxt = \"Running\"\nEnd If",
                "Local and remote points now work together. Real sequences are next."
            )
            {
                ConceptPoints = new[] { "Blend local commands with remote sensors.", "Always include prove, alarm, and text for monitored equipment.", "Keep the path names consistent." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            // ========== Chapter 5 · Real Sequences ==========
            new CourseLesson(
                "Chapter 5 · Real Sequences",
                "Fan sequence",
                "Command a Supply Fan Safely",
                "A supply fan sequence enables the fan under interlocks, monitors status with a prove timer, and may position an outdoor-air damper when the fan is on. Keep failure handling explicit.",
                "Variable FanCmd As Integer\nVariable FanStatus As Integer\nVariable FanAlarm As Integer\nVariable OaDpr As Real\nVariable Enable As Integer\nEnable = 1\nIf Enable = 1 Then\n  FanCmd = 1\n  OaDpr = 20.0\nEnd If\nIf FanCmd = 1 And FanStatus = 0 OnFor 30S Then\n  FanAlarm = 1\nEnd If",
                "Variable FanCmd As Integer\nVariable FanStatus As Integer\nVariable FanAlarm As Integer\nVariable OaDpr As Real\nVariable Enable As Integer\nEnable = 1\nIf Enable = 1 Then\n  FanCmd = 1\n  OaDpr = 20.0\nEnd If\nIf FanCmd = 1 And FanStatus = 0 OnFor 30S Then\n  FanAlarm = 1\nEnd If",
                "Variable FanCmd As Integer\nVariable FanStatus As Integer\nVariable FanAlarm As Integer\nVariable OaDpr As Real\nVariable Enable As Integer\nIf Enable = 1 Then\n  FanCmd = 1\n  OaDpr = ___\nEnd If\nIf FanCmd = 1 And FanStatus = 0 OnFor 30S Then\n  FanAlarm = 1\nEnd If",
                "When the fan is enabled, what is a common related action for outdoor air?",
                new[] { "Delete the setpoint", "Move the OA damper to a minimum open position", "Force all stages on", "Disable comments" },
                1,
                "Many sequences open the outdoor-air damper to a minimum when the fan starts.",
                "FanCmd = 1",
                "Guard with Enable, set a minimum OaDpr, and prove FanStatus.",
                "When Enable is 1, set FanCmd and OaDpr to a minimum; latch FanAlarm if status fails OnFor 30S.",
                "When SysEnable is 1, set SaFan and MinOa; latch SaFanAlarm if status fails OnFor 25S.",
                "Variable SaFan As Integer\nVariable SaFanStatus As Integer\nVariable SaFanAlarm As Integer\nVariable MinOa As Real\nVariable SysEnable As Integer\nSysEnable = 1\nIf SysEnable = 1 Then\n  SaFan = 1\n  MinOa = 15.0\nEnd If\nIf SaFan = 1 And SaFanStatus = 0 OnFor 25S Then\n  SaFanAlarm = 1\nEnd If",
                "A safe supply-fan enable line is complete."
            )
            {
                ConceptPoints = new[] { "Interlock the fan enable.", "Prove status with OnFor.", "Coordinate related dampers when the fan starts." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 5 · Real Sequences",
                "Economizer damper",
                "Modulate an Economizer Position",
                "Economizer logic applies a minimum position, limits the command, and often tracks bias so transfers stay smooth. An at-max latch can signal that free cooling is exhausted.",
                "Variable PidOut As Real\nVariable MinPos As Real\nVariable Bias As Real\nVariable EconCmd As Real\nVariable EconAtMax As Integer\nPidOut = 80.0\nMinPos = 20.0\nBias = 0.0\nEconCmd = Limit(PidOut + Bias, MinPos, 100.0)\nIf EconCmd >= 99.0 Then\n  EconAtMax = 1\nEnd If\nIf EconCmd < 95.0 Then\n  EconAtMax = 0\nEnd If",
                "Variable PidOut As Real\nVariable MinPos As Real\nVariable Bias As Real\nVariable EconCmd As Real\nVariable EconAtMax As Integer\nPidOut = 80.0\nMinPos = 20.0\nBias = 0.0\nEconCmd = Limit(PidOut + Bias, MinPos, 100.0)\nIf EconCmd >= 99.0 Then\n  EconAtMax = 1\nEnd If\nIf EconCmd < 95.0 Then\n  EconAtMax = 0\nEnd If",
                "Variable PidOut As Real\nVariable MinPos As Real\nVariable Bias As Real\nVariable EconCmd As Real\nVariable EconAtMax As Integer\nEconCmd = ___(PidOut + Bias, MinPos, 100.0)\nIf EconCmd >= 99.0 Then\n  EconAtMax = 1\nEnd If",
                "What does an economizer at-max flag typically indicate?",
                new[] { "The sequence has no variables", "Free cooling is at maximum and more cooling may need mechanical stages", "Comments are disabled", "Remote paths are invalid" },
                1,
                "When the economizer is wide open, additional cooling demand usually requires mechanical stages.",
                "EconCmd = PidOut",
                "Limit to MinPos..100 and latch EconAtMax near 100 percent.",
                "Compute EconCmd with Limit(PidOut + Bias, MinPos, 100) and latch EconAtMax when near fully open.",
                "Compute FreeCool with Limit(Loop + Bias, 15, 100) and latch AtMax when near fully open.",
                "Variable Loop As Real\nVariable Bias As Real\nVariable FreeCool As Real\nVariable AtMax As Integer\nLoop = 90.0\nBias = 0.0\nFreeCool = Limit(Loop + Bias, 15.0, 100.0)\nIf FreeCool >= 99.0 Then\n  AtMax = 1\nEnd If\nIf FreeCool < 95.0 Then\n  AtMax = 0\nEnd If",
                "Economizer positioning with at-max signaling is ready."
            )
            {
                ConceptPoints = new[] { "Enforce minimum position and 100 percent limit.", "Track bias for smooth control.", "At-max latch informs higher-level cooling decisions." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 5 · Real Sequences",
                "DX coil staging",
                "Stage a Multi-Stage Cooling Coil",
                "DX stages use demand thresholds, deadbands, and anti-cycle timers. Stages may be sequential or allow non-sequential enable when the design permits. Each stage should have its own ready flag.",
                "Variable Temp As Real\nVariable Sp As Real\nVariable S1 As Integer\nVariable S1Ready As Integer\nVariable S2 As Integer\nVariable S2Ready As Integer\nTemp = 78.0\nSp = 72.0\nIf S1 = 0 OffFor 5M Then\n  S1Ready = 1\nEnd If\nIf S1Ready = 1 And Temp > Sp + 2.0 Then\n  S1 = 1\nEnd If\nIf S2 = 0 OffFor 5M Then\n  S2Ready = 1\nEnd If\nIf S2Ready = 1 And S1 = 1 And Temp > Sp + 3.5 Then\n  S2 = 1\nEnd If\nIf Temp < Sp + 0.5 Then\n  S2 = 0\n  S1 = 0\nEnd If",
                "Variable Temp As Real\nVariable Sp As Real\nVariable S1 As Integer\nVariable S1Ready As Integer\nVariable S2 As Integer\nVariable S2Ready As Integer\nTemp = 78.0\nSp = 72.0\nIf S1 = 0 OffFor 5M Then\n  S1Ready = 1\nEnd If\nIf S1Ready = 1 And Temp > Sp + 2.0 Then\n  S1 = 1\nEnd If\nIf S2 = 0 OffFor 5M Then\n  S2Ready = 1\nEnd If\nIf S2Ready = 1 And S1 = 1 And Temp > Sp + 3.5 Then\n  S2 = 1\nEnd If\nIf Temp < Sp + 0.5 Then\n  S2 = 0\n  S1 = 0\nEnd If",
                "Variable Temp As Real\nVariable Sp As Real\nVariable S1 As Integer\nVariable S1Ready As Integer\nIf S1 = 0 ___ 5M Then\n  S1Ready = 1\nEnd If\nIf S1Ready = 1 And Temp > Sp + 2.0 Then\n  S1 = 1\nEnd If",
                "What protects each DX stage from short cycling?",
                new[] { "String status only", "Per-stage OffFor ready timers", "Removing Limit", "Deleting setpoints" },
                1,
                "Each stage waits for its own minimum off time before it is allowed to start again.",
                "S1 = 1",
                "Add OffFor readiness and stepped demand thresholds.",
                "Implement two DX stages with 5M anti-cycle, sequential enable, and a shared off threshold.",
                "Implement two cool stages with 4M anti-cycle, sequential enable, and a shared off threshold.",
                "Variable Temp As Real\nVariable Sp As Real\nVariable C1 As Integer\nVariable C1Ready As Integer\nVariable C2 As Integer\nVariable C2Ready As Integer\nTemp = 79.0\nSp = 73.0\nIf C1 = 0 OffFor 4M Then\n  C1Ready = 1\nEnd If\nIf C1Ready = 1 And Temp > Sp + 1.5 Then\n  C1 = 1\nEnd If\nIf C2 = 0 OffFor 4M Then\n  C2Ready = 1\nEnd If\nIf C2Ready = 1 And C1 = 1 And Temp > Sp + 3.0 Then\n  C2 = 1\nEnd If\nIf Temp < Sp Then\n  C2 = 0\n  C1 = 0\nEnd If",
                "Multi-stage DX enable logic is in place."
            )
            {
                ConceptPoints = new[] { "Per-stage anti-cycle timers.", "Stepped demand thresholds with deadband.", "Clear stages from the top when demand falls." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 5 · Real Sequences",
                "Gas heat staging",
                "Stage Dual-Zone Gas Heat",
                "Interior and exterior zones may share capacity limits while still staging independently. Coordinate enables so total stages respect plant limits and each zone meets its own demand.",
                "Variable IntDem As Integer\nVariable ExtDem As Integer\nVariable IntS1 As Integer\nVariable ExtS1 As Integer\nVariable MaxStages As Integer\nMaxStages = 2\nIntDem = 1\nExtDem = 1\nIf IntDem >= 1 Then\n  IntS1 = 1\nEnd If\nIf ExtDem >= 1 And IntS1 + ExtS1 < MaxStages Then\n  ExtS1 = 1\nEnd If",
                "Variable IntDem As Integer\nVariable ExtDem As Integer\nVariable IntS1 As Integer\nVariable ExtS1 As Integer\nVariable MaxStages As Integer\nMaxStages = 2\nIntDem = 1\nExtDem = 1\nIf IntDem >= 1 Then\n  IntS1 = 1\nEnd If\nIf ExtDem >= 1 And IntS1 + ExtS1 < MaxStages Then\n  ExtS1 = 1\nEnd If",
                "Variable IntDem As Integer\nVariable ExtDem As Integer\nVariable IntS1 As Integer\nVariable ExtS1 As Integer\nVariable MaxStages As Integer\nIf ExtDem >= 1 And IntS1 + ExtS1 < ___ Then\n  ExtS1 = 1\nEnd If",
                "Why track a MaxStages limit across zones?",
                new[] { "To keep total fired capacity within plant design", "To remove deadbands", "To disable remote reads", "To force String types" },
                0,
                "Shared capacity limits prevent the combined zones from exceeding what the plant can support.",
                "ExtS1 = 1",
                "Only start the exterior stage when demand exists and the total stage count is below MaxStages.",
                "Enable interior stage on IntDem; enable exterior stage only if total stages stay below MaxStages.",
                "Enable ZoneA stage on ADem; enable ZoneB stage only if total stages stay below CapLimit.",
                "Variable ADem As Integer\nVariable BDem As Integer\nVariable ASt As Integer\nVariable BSt As Integer\nVariable CapLimit As Integer\nCapLimit = 2\nADem = 1\nBDem = 1\nIf ADem >= 1 Then\n  ASt = 1\nEnd If\nIf BDem >= 1 And ASt + BSt < CapLimit Then\n  BSt = 1\nEnd If",
                "Coordinated multi-zone heat staging is working."
            )
            {
                ConceptPoints = new[] { "Zones can stage independently within a shared capacity cap.", "Check total active stages before enabling another.", "Keep each zone's demand logic readable." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 5 · Real Sequences",
                "Baseboard heat",
                "Control Baseboard Heating",
                "Baseboard sequences often mirror operator setpoints into the control line, select occupied or standby targets, and command heat when the zone is below the active setpoint with an appropriate deadband.",
                "Variable OccSp As Real\nVariable ActiveSp As Real\nVariable ZoneTemp As Real\nVariable HeatCmd As Integer\nVariable Occupied As Integer\nOccSp = 70.0\nOccupied = 1\nActiveSp = OccSp\nZoneTemp = 66.0\nIf Occupied = 1 And ZoneTemp < ActiveSp - 1.0 Then\n  HeatCmd = 1\nEnd If\nIf ZoneTemp > ActiveSp + 1.0 Then\n  HeatCmd = 0\nEnd If",
                "Variable OccSp As Real\nVariable ActiveSp As Real\nVariable ZoneTemp As Real\nVariable HeatCmd As Integer\nVariable Occupied As Integer\nOccSp = 70.0\nOccupied = 1\nActiveSp = OccSp\nZoneTemp = 66.0\nIf Occupied = 1 And ZoneTemp < ActiveSp - 1.0 Then\n  HeatCmd = 1\nEnd If\nIf ZoneTemp > ActiveSp + 1.0 Then\n  HeatCmd = 0\nEnd If",
                "Variable OccSp As Real\nVariable ActiveSp As Real\nVariable ZoneTemp As Real\nVariable HeatCmd As Integer\nVariable Occupied As Integer\nActiveSp = OccSp\nIf Occupied = 1 And ZoneTemp < ActiveSp - ___ Then\n  HeatCmd = 1\nEnd If",
                "What role does ActiveSp play in a baseboard sequence?",
                new[] { "It is only a comment", "It is the target temperature currently used for control", "It disables OnFor", "It stores remote device names only" },
                1,
                "ActiveSp is the live target selected from occupied, standby, or other source setpoints.",
                "HeatCmd = 1",
                "Compare ZoneTemp to ActiveSp with a deadband and respect Occupied.",
                "When occupied and ZoneTemp is below ActiveSp - 1, turn HeatCmd on; turn off above ActiveSp + 1.",
                "When occupied and SpaceTemp is below Target - 0.5, turn BbCmd on; turn off above Target + 0.5.",
                "Variable Target As Real\nVariable SpaceTemp As Real\nVariable BbCmd As Integer\nVariable Occupied As Integer\nTarget = 71.0\nOccupied = 1\nSpaceTemp = 67.0\nIf Occupied = 1 And SpaceTemp < Target - 0.5 Then\n  BbCmd = 1\nEnd If\nIf SpaceTemp > Target + 0.5 Then\n  BbCmd = 0\nEnd If",
                "Baseboard heat control with mirrored setpoints is complete."
            )
            {
                ConceptPoints = new[] { "Mirror operator setpoints into ActiveSp.", "Apply deadband around the active target.", "Respect occupancy or mode before heating." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 5 · Real Sequences",
                "Return air damper",
                "Position a Return Air Damper",
                "Return-air damper commands often take a minimum position, scale or limit a loop output, and apply bias so the damper does not jump when control authority changes.",
                "Variable LoopOut As Real\nVariable MinRa As Real\nVariable Bias As Real\nVariable RaCmd As Real\nLoopOut = 40.0\nMinRa = 30.0\nBias = 0.0\nRaCmd = Max(MinRa, Limit(LoopOut + Bias, 0.0, 100.0))",
                "Variable LoopOut As Real\nVariable MinRa As Real\nVariable Bias As Real\nVariable RaCmd As Real\nLoopOut = 40.0\nMinRa = 30.0\nBias = 0.0\nRaCmd = Max(MinRa, Limit(LoopOut + Bias, 0.0, 100.0))",
                "Variable LoopOut As Real\nVariable MinRa As Real\nVariable Bias As Real\nVariable RaCmd As Real\nRaCmd = ___(MinRa, Limit(LoopOut + Bias, 0.0, 100.0))",
                "Why apply both Limit and Max(MinRa, ...) to a return-air command?",
                new[] { "To document comments only", "To keep the command inside 0-100 and never below the minimum position", "To remove variables", "To force IfOnce" },
                1,
                "Limit enforces the overall range; Max with MinRa guarantees the damper never closes below its required minimum.",
                "RaCmd = LoopOut",
                "Clamp with Limit then enforce MinRa with Max.",
                "Set RaCmd to Max(MinRa, Limit(LoopOut + Bias, 0, 100)).",
                "Set RetCmd to Max(15.0, Limit(Pid + Bias, 0, 100)).",
                "Variable Pid As Real\nVariable Bias As Real\nVariable RetCmd As Real\nPid = 25.0\nBias = 0.0\nRetCmd = Max(15.0, Limit(Pid + Bias, 0.0, 100.0))",
                "Return-air damper positioning follows the same safe math pattern."
            )
            {
                ConceptPoints = new[] { "Minimum position is mandatory for many RA dampers.", "Limit the loop output, then enforce the minimum.", "Bias keeps transfers smooth." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 5 · Real Sequences",
                "Alarms and failures",
                "Monitor Fans and Raise Alarms",
                "Supply and return fans each need prove timers, latched alarms, and a shared or individual reset path. Keep the alarm text clear for operators.",
                "Variable SaFanCmd As Integer\nVariable SaFanStatus As Integer\nVariable SaFanAlarm As Integer\nVariable RaFanCmd As Integer\nVariable RaFanStatus As Integer\nVariable RaFanAlarm As Integer\nVariable ResetBtn As Integer\nSaFanCmd = 1\nRaFanCmd = 1\nIf SaFanCmd = 1 And SaFanStatus = 0 OnFor 30S Then\n  SaFanAlarm = 1\nEnd If\nIf RaFanCmd = 1 And RaFanStatus = 0 OnFor 30S Then\n  RaFanAlarm = 1\nEnd If\nIfOnce ResetBtn = 1 Then\n  SaFanAlarm = 0\n  RaFanAlarm = 0\nEnd If",
                "Variable SaFanCmd As Integer\nVariable SaFanStatus As Integer\nVariable SaFanAlarm As Integer\nVariable RaFanCmd As Integer\nVariable RaFanStatus As Integer\nVariable RaFanAlarm As Integer\nVariable ResetBtn As Integer\nSaFanCmd = 1\nRaFanCmd = 1\nSaFanStatus = 0\nRaFanStatus = 0\nResetBtn = 0\nIf SaFanCmd = 1 And SaFanStatus = 0 OnFor 30S Then\n  SaFanAlarm = 1\nEnd If\nIf RaFanCmd = 1 And RaFanStatus = 0 OnFor 30S Then\n  RaFanAlarm = 1\nEnd If\nIfOnce ResetBtn = 1 Then\n  SaFanAlarm = 0\n  RaFanAlarm = 0\nEnd If",
                "Variable SaFanCmd As Integer\nVariable SaFanStatus As Integer\nVariable SaFanAlarm As Integer\nVariable ResetBtn As Integer\nIf SaFanCmd = 1 And SaFanStatus = 0 OnFor 30S Then\n  SaFanAlarm = 1\nEnd If\n___ ResetBtn = 1 Then\n  SaFanAlarm = 0\nEnd If",
                "How should a hard fan alarm typically be cleared?",
                new[] { "Automatically every scan", "Through an explicit reset path such as IfOnce on a reset input", "By deleting the variable", "By changing Scale spans" },
                1,
                "Hard alarms stay latched until an operator reset is accepted, often with IfOnce.",
                "SaFanAlarm = 0",
                "Prove both fans and clear alarms only on IfOnce ResetBtn.",
                "Latch SaFanAlarm and RaFanAlarm on 30S prove failures; clear both with IfOnce ResetBtn.",
                "Latch SfAlarm and RfAlarm on 25S prove failures; clear both with IfOnce ClearBtn.",
                "Variable SfCmd As Integer\nVariable SfStatus As Integer\nVariable SfAlarm As Integer\nVariable RfCmd As Integer\nVariable RfStatus As Integer\nVariable RfAlarm As Integer\nVariable ClearBtn As Integer\nSfCmd = 1\nRfCmd = 1\nIf SfCmd = 1 And SfStatus = 0 OnFor 25S Then\n  SfAlarm = 1\nEnd If\nIf RfCmd = 1 And RfStatus = 0 OnFor 25S Then\n  RfAlarm = 1\nEnd If\nIfOnce ClearBtn = 1 Then\n  SfAlarm = 0\n  RfAlarm = 0\nEnd If",
                "Dual-fan alarm monitoring with reset is complete."
            )
            {
                ConceptPoints = new[] { "Prove each fan independently.", "Latch alarms until reset.", "Use IfOnce for the operator reset edge." },
                EditorFileNameOverride = "Sequence.gcl"
            },

            new CourseLesson(
                "Chapter 5 · Real Sequences",
                "Full integrated sequence",
                "Assemble a Complete Control Line",
                "Bring together enable logic, a fan, economizer minimum, one DX stage with anti-cycle, alarm prove, and status text into a single cohesive program. This is the capstone pattern for the Control Line Lab.",
                "// Capstone control line\nVariable Enable As Integer\nVariable FanCmd As Integer\nVariable FanStatus As Integer\nVariable FanAlarm As Integer\nVariable FanTxt As String\nVariable EconCmd As Real\nVariable MinPos As Real\nVariable Temp As Real\nVariable Sp As Real\nVariable Stage1 As Integer\nVariable Stage1Ready As Integer\nEnable = 1\nMinPos = 20.0\nSp = 72.0\nTemp = 76.0\nIf Enable = 1 Then\n  FanCmd = 1\n  EconCmd = MinPos\nEnd If\nIf FanCmd = 1 And FanStatus = 0 OnFor 30S Then\n  FanAlarm = 1\n  FanTxt = \"Failed\"\nEnd If\nIf FanAlarm = 0 And FanCmd = 1 Then\n  FanTxt = \"Running\"\nEnd If\nIf Stage1 = 0 OffFor 5M Then\n  Stage1Ready = 1\nEnd If\nIf Stage1Ready = 1 And Temp > Sp + 2.0 And FanCmd = 1 Then\n  Stage1 = 1\nEnd If\nIf Temp < Sp - 1.0 Then\n  Stage1 = 0\nEnd If",
                "// Capstone control line\nVariable Enable As Integer\nVariable FanCmd As Integer\nVariable FanStatus As Integer\nVariable FanAlarm As Integer\nVariable FanTxt As String\nVariable EconCmd As Real\nVariable MinPos As Real\nVariable Temp As Real\nVariable Sp As Real\nVariable Stage1 As Integer\nVariable Stage1Ready As Integer\nEnable = 1\nMinPos = 20.0\nSp = 72.0\nTemp = 76.0\nIf Enable = 1 Then\n  FanCmd = 1\n  EconCmd = MinPos\nEnd If\nIf FanCmd = 1 And FanStatus = 0 OnFor 30S Then\n  FanAlarm = 1\n  FanTxt = \"Failed\"\nEnd If\nIf FanAlarm = 0 And FanCmd = 1 Then\n  FanTxt = \"Running\"\nEnd If\nIf Stage1 = 0 OffFor 5M Then\n  Stage1Ready = 1\nEnd If\nIf Stage1Ready = 1 And Temp > Sp + 2.0 And FanCmd = 1 Then\n  Stage1 = 1\nEnd If\nIf Temp < Sp - 1.0 Then\n  Stage1 = 0\nEnd If",
                "// Capstone control line\nVariable Enable As Integer\nVariable FanCmd As Integer\nVariable FanStatus As Integer\nVariable FanAlarm As Integer\nVariable FanTxt As String\nVariable EconCmd As Real\nVariable MinPos As Real\nVariable Temp As Real\nVariable Sp As Real\nVariable Stage1 As Integer\nVariable Stage1Ready As Integer\nIf Enable = 1 Then\n  FanCmd = 1\n  EconCmd = ___\nEnd If\nIf FanCmd = 1 And FanStatus = 0 OnFor 30S Then\n  FanAlarm = 1\n  FanTxt = \"Failed\"\nEnd If\nIf Stage1 = 0 ___ 5M Then\n  Stage1Ready = 1\nEnd If",
                "Which of the following best describes a complete control line?",
                new[] { "Only variable declarations", "Enable, equipment command, prove/alarm, related damper or stage logic, and status text working together", "Only remote paths", "Only comments" },
                1,
                "A production sequence coordinates enable, safety, related outputs, staging, and operator feedback.",
                "FanCmd = 1",
                "Include economizer minimum, fan prove/alarm/text, and a protected Stage1 enable.",
                "Build the capstone line: enable fan and min economizer, prove fan alarm with text, and protect Stage1 with OffFor and deadband.",
                "Build a similar line with pump enable, min valve position, prove alarm with text, and one protected heat stage.",
                "// Capstone heat line\nVariable Enable As Integer\nVariable PumpCmd As Integer\nVariable PumpStatus As Integer\nVariable PumpAlarm As Integer\nVariable PumpTxt As String\nVariable VlvCmd As Real\nVariable MinVlv As Real\nVariable Temp As Real\nVariable Sp As Real\nVariable Heat1 As Integer\nVariable Heat1Ready As Integer\nEnable = 1\nMinVlv = 10.0\nSp = 70.0\nTemp = 65.0\nIf Enable = 1 Then\n  PumpCmd = 1\n  VlvCmd = MinVlv\nEnd If\nIf PumpCmd = 1 And PumpStatus = 0 OnFor 20S Then\n  PumpAlarm = 1\n  PumpTxt = \"Failed\"\nEnd If\nIf PumpAlarm = 0 And PumpCmd = 1 Then\n  PumpTxt = \"Running\"\nEnd If\nIf Heat1 = 0 OffFor 4M Then\n  Heat1Ready = 1\nEnd If\nIf Heat1Ready = 1 And Temp < Sp - 2.0 And PumpCmd = 1 Then\n  Heat1 = 1\nEnd If\nIf Temp > Sp + 1.0 Then\n  Heat1 = 0\nEnd If",
                "Control Line Lab complete. You can now read and write full GCL+ sequences with timing, math, objects, and real equipment patterns."
            )
            {
                ConceptPoints = new[] { "Integrate enable, safety, related outputs, and staging.", "Keep operator text accurate.", "Anti-cycle and deadband protect every mechanical stage." },
                EditorFileNameOverride = "Sequence.gcl"
            }
        };
}
