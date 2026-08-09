namespace CaveCode.CourseEngine;

public sealed record ArduinoRoadmapModule(
    int Index,
    string Chapter,
    string Title,
    string Summary,
    bool IsPlayable
);

public static class ArduinoCourseRoadmap
{
    public static IReadOnlyList<ArduinoRoadmapModule> Modules { get; } =
        new ArduinoRoadmapModule[]
        {
            new(0, "Chapter 1 · Sketch Foundations", "Board power-up", "Chapter 1 · Board power-up", true),
            new(1, "Chapter 1 · Sketch Foundations", "setup and loop", "Chapter 1 · setup and loop", true),
            new(2, "Chapter 1 · Sketch Foundations", "pinMode output", "Chapter 1 · pinMode output", true),
            new(3, "Chapter 1 · Sketch Foundations", "digitalWrite levels", "Chapter 1 · digitalWrite levels", true),
            new(4, "Chapter 1 · Sketch Foundations", "delay timing", "Chapter 1 · delay timing", true),
            new(5, "Chapter 1 · Sketch Foundations", "Blink pattern", "Chapter 1 · Blink pattern", true),
            new(6, "Chapter 1 · Sketch Foundations", "Named pins", "Chapter 1 · Named pins", true),
            new(7, "Chapter 1 · Sketch Foundations", "Sketch review", "Chapter 1 · Sketch review", true),
            new(8, "Chapter 2 · Digital Output", "Steady LED", "Chapter 2 · Steady LED", true),
            new(9, "Chapter 2 · Digital Output", "Blink control", "Chapter 2 · Blink control", true),
            new(10, "Chapter 2 · Digital Output", "Multi-step pattern", "Chapter 2 · Multi-step pattern", true),
            new(11, "Chapter 2 · Digital Output", "Active-low LED", "Chapter 2 · Active-low LED", true),
            new(12, "Chapter 2 · Digital Output", "Two outputs", "Chapter 2 · Two outputs", true),
            new(13, "Chapter 2 · Digital Output", "Duty feel", "Chapter 2 · Duty feel", true),
            new(14, "Chapter 2 · Digital Output", "Status LED", "Chapter 2 · Status LED", true),
            new(15, "Chapter 2 · Digital Output", "Output lab", "Chapter 2 · Output lab", true),
            new(16, "Chapter 3 · Digital Input", "Read a pin", "Chapter 3 · Read a pin", true),
            new(17, "Chapter 3 · Digital Input", "Button level", "Chapter 3 · Button level", true),
            new(18, "Chapter 3 · Digital Input", "Pull-up idea", "Chapter 3 · Pull-up idea", true),
            new(19, "Chapter 3 · Digital Input", "Edge vs level", "Chapter 3 · Edge vs level", true),
            new(20, "Chapter 3 · Digital Input", "Debounce idea", "Chapter 3 · Debounce idea", true),
            new(21, "Chapter 3 · Digital Input", "Button to LED", "Chapter 3 · Button to LED", true),
            new(22, "Chapter 3 · Digital Input", "Hold detect", "Chapter 3 · Hold detect", true),
            new(23, "Chapter 3 · Digital Input", "Input lab", "Chapter 3 · Input lab", true),
            new(24, "Chapter 4 · Timing and State", "State variable", "Chapter 4 · State variable", true),
            new(25, "Chapter 4 · Timing and State", "Mode flags", "Chapter 4 · Mode flags", true),
            new(26, "Chapter 4 · Timing and State", "Non-blocking idea", "Chapter 4 · Non-blocking idea", true),
            new(27, "Chapter 4 · Timing and State", "Two-phase blink", "Chapter 4 · Two-phase blink", true),
            new(28, "Chapter 4 · Timing and State", "Sequence steps", "Chapter 4 · Sequence steps", true),
            new(29, "Chapter 4 · Timing and State", "Timeout", "Chapter 4 · Timeout", true),
            new(30, "Chapter 4 · Timing and State", "Latch", "Chapter 4 · Latch", true),
            new(31, "Chapter 4 · Timing and State", "State lab", "Chapter 4 · State lab", true),
            new(32, "Chapter 5 · Analog and Projects", "analogRead", "Chapter 5 · analogRead", true),
            new(33, "Chapter 5 · Analog and Projects", "Map range", "Chapter 5 · Map range", true),
            new(34, "Chapter 5 · Analog and Projects", "Threshold", "Chapter 5 · Threshold", true),
            new(35, "Chapter 5 · Analog and Projects", "Sensor bar", "Chapter 5 · Sensor bar", true),
            new(36, "Chapter 5 · Analog and Projects", "Night light", "Chapter 5 · Night light", true),
            new(37, "Chapter 5 · Analog and Projects", "Alarm pattern", "Chapter 5 · Alarm pattern", true),
            new(38, "Chapter 5 · Analog and Projects", "Combined lab", "Chapter 5 · Combined lab", true),
            new(39, "Chapter 5 · Analog and Projects", "Maker project", "Chapter 5 · Maker project", true),
        };

    public static ArduinoRoadmapModule Get(int index) => Modules[index];
}
