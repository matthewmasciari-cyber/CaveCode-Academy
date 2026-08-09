namespace CaveCode.CourseEngine;

public sealed record GclRoadmapModule(
    int Number,
    string Chapter,
    string Topic,
    string Title,
    string Summary,
    string ProjectUpgrade,
    string EditorFileName,
    string SystemArea,
    string[] Concepts
);

public static class GclCourseRoadmap
{
    public static IReadOnlyList<GclRoadmapModule> Modules { get; } =
        new[]
        {
            // Chapter 1 · Line Foundations
            new GclRoadmapModule(1, "Chapter 1 · Line Foundations", "Program structure", "Start a Control Sequence", "Understand how a GCL+ program is organized and scanned.", "Open the first control line in the lab.", "Sequence.gcl", "Program shell", new[] { "program body", "scan order", "statements", "comments" }),
            new GclRoadmapModule(2, "Chapter 1 · Line Foundations", "Comments", "Document the Sequence", "Use // comments to explain intent without changing behavior.", "Label the startup block clearly.", "Sequence.gcl", "Documentation", new[] { "// comments", "readability", "intent notes", "safe annotation" }),
            new GclRoadmapModule(3, "Chapter 1 · Line Foundations", "Variables", "Store Operating Values", "Declare Integer, Real, and String variables for live values.", "Hold mode, temperature, and status text.", "Sequence.gcl", "Local storage", new[] { "Variable As Integer", "Variable As Real", "Variable As String", "declaration" }),
            new GclRoadmapModule(4, "Chapter 1 · Line Foundations", "Assignment", "Write Values to Points", "Assign numbers and text to variables and objects.", "Set initial setpoints and status strings.", "Sequence.gcl", "Value writes", new[] { "assignment", "=", "numeric values", "text values" }),
            new GclRoadmapModule(5, "Chapter 1 · Line Foundations", "If conditions", "Gate Actions with If", "Run a block only when a condition is true.", "Enable a stage only when demand is present.", "Sequence.gcl", "Conditional logic", new[] { "If", "Then", "End If", "boolean tests" }),
            new GclRoadmapModule(6, "Chapter 1 · Line Foundations", "Comparisons", "Compare Live Readings", "Use relational operators to test limits and thresholds.", "Check temperature against a setpoint.", "Sequence.gcl", "Threshold tests", new[] { ">", "<", ">=", "<=", "==", "<>" }),
            new GclRoadmapModule(7, "Chapter 1 · Line Foundations", "Logical operators", "Combine Multiple Conditions", "Join tests with And / Or and invert with Not.", "Require both mode and demand before starting.", "Sequence.gcl", "Compound logic", new[] { "And", "Or", "Not", "compound conditions" }),
            new GclRoadmapModule(8, "Chapter 1 · Line Foundations", "Integrated foundations", "Assemble a Minimal Sequence", "Combine variables, assignment, and If into one working line.", "Create a safe manual enable path.", "Sequence.gcl", "Foundation review", new[] { "variables", "If", "assignment", "comments" }),

            // Chapter 2 · Timing and Edges
            new GclRoadmapModule(9, "Chapter 2 · Timing and Edges", "IfOnce", "Act on a Rising Edge", "Run a block only on the transition from false to true.", "Reset an alarm latch once when cleared.", "Sequence.gcl", "Edge detection", new[] { "IfOnce", "rising edge", "one-shot", "transition" }),
            new GclRoadmapModule(10, "Chapter 2 · Timing and Edges", "OnFor", "Require Continuous True Time", "Hold a condition true for a duration before acting.", "Prove a fan failure has lasted long enough.", "Sequence.gcl", "Prove timers", new[] { "OnFor", "duration", "continuous true", "prove" }),
            new GclRoadmapModule(11, "Chapter 2 · Timing and Edges", "OffFor", "Require Continuous False Time", "Wait until a condition has been false for a duration.", "Clear a fault only after a stable recovery.", "Sequence.gcl", "Recovery timers", new[] { "OffFor", "duration", "continuous false", "clear" }),
            new GclRoadmapModule(12, "Chapter 2 · Timing and Edges", "DoEvery", "Run Periodic Tasks", "Execute a block on a fixed time interval.", "Refresh remote data every fifteen minutes.", "Sequence.gcl", "Periodic work", new[] { "DoEvery", "interval", "periodic", "time units" }),
            new GclRoadmapModule(13, "Chapter 2 · Timing and Edges", "Changed", "React to Value Changes", "Detect when a point value differs from the previous scan.", "Trigger logic only when a setpoint is edited.", "Sequence.gcl", "Change detection", new[] { "Changed", "previous scan", "value delta", "event" }),
            new GclRoadmapModule(14, "Chapter 2 · Timing and Edges", "Anti-cycle", "Protect Equipment from Short Cycling", "Combine timers and flags to prevent rapid restart.", "Enforce a minimum off time between stage starts.", "Sequence.gcl", "Cycle protection", new[] { "anti-cycle", "minimum off", "stage flag", "timers" }),
            new GclRoadmapModule(15, "Chapter 2 · Timing and Edges", "Deadbands", "Avoid Chatter Around Setpoints", "Use a band around a setpoint so control does not oscillate.", "Stage on and off with separate thresholds.", "Sequence.gcl", "Stable control", new[] { "deadband", "hysteresis", "on threshold", "off threshold" }),
            new GclRoadmapModule(16, "Chapter 2 · Timing and Edges", "Integrated timing", "Build a Timed Stage Enable", "Combine IfOnce, OnFor, and deadband into one stage.", "Create a protected first-stage enable line.", "Sequence.gcl", "Timing review", new[] { "IfOnce", "OnFor", "deadband", "flags" }),

            // Chapter 3 · Control Math
            new GclRoadmapModule(17, "Chapter 3 · Control Math", "Min and Max", "Clamp to Safe Bounds", "Select the lower or higher of two values.", "Never command a damper below its minimum position.", "Sequence.gcl", "Bounds", new[] { "Min", "Max", "clamp", "safe limits" }),
            new GclRoadmapModule(18, "Chapter 3 · Control Math", "Limit", "Restrict a Value Range", "Force a calculated result into an allowed range.", "Keep a PID output inside 0 to 100.", "Sequence.gcl", "Range limit", new[] { "Limit", "low limit", "high limit", "output range" }),
            new GclRoadmapModule(19, "Chapter 3 · Control Math", "Scale", "Map One Range to Another", "Convert a sensor span into a control percentage.", "Scale a 0-10 signal into 0-100 position.", "Sequence.gcl", "Range mapping", new[] { "Scale", "input span", "output span", "linear map" }),
            new GclRoadmapModule(20, "Chapter 3 · Control Math", "PID bias", "Track and Apply Bias", "Use bias so a loop continues smoothly after mode changes.", "Preserve damper position when switching control sources.", "Sequence.gcl", "Bias tracking", new[] { "bias", "PID", "bumpless", "mode transfer" }),
            new GclRoadmapModule(21, "Chapter 3 · Control Math", "Setpoints", "Hold and Adjust Targets", "Store operator and calculated setpoints cleanly.", "Separate occupied and unoccupied targets.", "Sequence.gcl", "Target values", new[] { "setpoint", "occupied", "unoccupied", "operator value" }),
            new GclRoadmapModule(22, "Chapter 3 · Control Math", "Staging", "Enable Sequential Stages", "Bring stages on in order and drop them safely.", "Stage heating or cooling capacity by demand.", "Sequence.gcl", "Capacity steps", new[] { "stage enable", "stage order", "capacity", "interlock" }),
            new GclRoadmapModule(23, "Chapter 3 · Control Math", "Non-sequential enable", "Allow Flexible Stage Selection", "Enable stages without forcing strict sequential order when allowed.", "Start available stages based on runtime or priority.", "Sequence.gcl", "Flexible staging", new[] { "non-sequential", "available stage", "priority", "runtime" }),
            new GclRoadmapModule(24, "Chapter 3 · Control Math", "Integrated math", "Build a Limited Damper Command", "Combine Scale, Min, Limit, and bias into one command.", "Produce a safe economizer position.", "Sequence.gcl", "Math review", new[] { "Scale", "Min", "Limit", "bias" }),

            // Chapter 4 · Objects and Exchange
            new GclRoadmapModule(25, "Chapter 4 · Objects and Exchange", "Local points", "Read and Write Local Objects", "Work with points that live in the current program.", "Drive a local fan status and command.", "Sequence.gcl", "Local objects", new[] { "local point", "object name", "read", "write" }),
            new GclRoadmapModule(26, "Chapter 4 · Objects and Exchange", "Remote references", "Address Remote Device Objects", "Use device.object notation to reach another controller.", "Pull outdoor air temperature from a remote device.", "Sequence.gcl", "Remote points", new[] { "device.object", "remote read", "remote write", "path" }),
            new GclRoadmapModule(27, "Chapter 4 · Objects and Exchange", "CALL", "Invoke Another Program Block", "Call a named routine or program section when needed.", "Share a common alarm reset routine.", "Sequence.gcl", "Reusable blocks", new[] { "CALL", "routine", "shared logic", "invocation" }),
            new GclRoadmapModule(28, "Chapter 4 · Objects and Exchange", "Data exchange", "Import and Publish Values", "Move values between programs on a schedule.", "Exchange setpoints and status every interval.", "Sequence.gcl", "Data exchange", new[] { "import", "publish", "DoEvery", "shared values" }),
            new GclRoadmapModule(29, "Chapter 4 · Objects and Exchange", "State text", "Present Readable Status", "Assign clear text that operators can understand.", "Show Fan Running, Fan Failed, or Off.", "Sequence.gcl", "Status text", new[] { "state text", "operator message", "status string", "display" }),
            new GclRoadmapModule(30, "Chapter 4 · Objects and Exchange", "Alarms", "Raise and Clear Alarms", "Detect abnormal conditions and latch alarm points.", "Set a supply fan failure alarm with prove time.", "Sequence.gcl", "Alarm logic", new[] { "alarm", "latch", "prove", "clear" }),
            new GclRoadmapModule(31, "Chapter 4 · Objects and Exchange", "Failures and reset", "Handle Failure and Manual Reset", "Require a deliberate reset after a hard failure.", "Use IfOnce to clear a latched failure.", "Sequence.gcl", "Failure handling", new[] { "failure", "reset", "IfOnce", "latch clear" }),
            new GclRoadmapModule(32, "Chapter 4 · Objects and Exchange", "Integrated objects", "Link Local and Remote Points", "Combine local commands with remote sensors and alarms.", "Build a small monitored fan sequence.", "Sequence.gcl", "Object review", new[] { "local", "remote", "alarm", "status text" }),

            // Chapter 5 · Real Sequences
            new GclRoadmapModule(33, "Chapter 5 · Real Sequences", "Fan sequence", "Command a Supply Fan Safely", "Start a fan with interlocks and failure monitoring.", "Run an outdoor-air-aware fan enable line.", "Sequence.gcl", "Fan control", new[] { "fan enable", "interlock", "failure", "OA damper" }),
            new GclRoadmapModule(34, "Chapter 5 · Real Sequences", "Economizer damper", "Modulate an Economizer Position", "Use min position, limit, and bias for free cooling.", "Produce a stable economizer command.", "Sequence.gcl", "Economizer", new[] { "Min", "Limit", "bias", "at-max latch" }),
            new GclRoadmapModule(35, "Chapter 5 · Real Sequences", "DX coil staging", "Stage a Multi-Stage Cooling Coil", "Enable DX stages with anti-cycle and deadbands.", "Bring on up to four cooling stages by demand.", "Sequence.gcl", "DX stages", new[] { "stage enable", "OnFor", "anti-cycle", "deadband" }),
            new GclRoadmapModule(36, "Chapter 5 · Real Sequences", "Gas heat staging", "Stage Dual-Zone Gas Heat", "Manage interior and exterior heat stages together.", "Coordinate zone heat with shared capacity limits.", "Sequence.gcl", "Gas heat", new[] { "zone stage", "interior", "exterior", "capacity" }),
            new GclRoadmapModule(37, "Chapter 5 · Real Sequences", "Baseboard heat", "Control Baseboard Heating", "Drive baseboard output from setpoints and mode.", "Mirror operator setpoints into the control line.", "Sequence.gcl", "Baseboard", new[] { "setpoint", "mode", "heat command", "GUI mirror" }),
            new GclRoadmapModule(38, "Chapter 5 · Real Sequences", "Return air damper", "Position a Return Air Damper", "Combine minimum, scale, and PID bias for RA control.", "Keep return air within safe operating bounds.", "Sequence.gcl", "RA damper", new[] { "Min", "Scale", "bias", "damper position" }),
            new GclRoadmapModule(39, "Chapter 5 · Real Sequences", "Alarms and failures", "Monitor Fans and Raise Alarms", "Detect supply and return fan failures with prove timers.", "Provide a clean reset path for operators.", "Sequence.gcl", "Alarm panel", new[] { "SaFanFalm", "RaFanFalm", "IfOnce", "relay" }),
            new GclRoadmapModule(40, "Chapter 5 · Real Sequences", "Full integrated sequence", "Assemble a Complete Control Line", "Combine timing, math, objects, and staging into one program.", "Deliver a production-ready multi-stage sequence.", "Sequence.gcl", "Capstone", new[] { "If", "OnFor", "Limit", "remote", "staging" })
        };
}
