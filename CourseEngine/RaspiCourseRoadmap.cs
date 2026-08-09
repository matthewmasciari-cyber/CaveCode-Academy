namespace CaveCode.CourseEngine;

public sealed record RaspiRoadmapModule(
    int Index,
    string Chapter,
    string Title,
    string Summary,
    bool IsPlayable
);

public static class RaspiCourseRoadmap
{
    public static IReadOnlyList<RaspiRoadmapModule> Modules { get; } =
        new RaspiRoadmapModule[]
        {
            new(0, "Chapter 1 · Pi Script Foundations", "Script entry", "Chapter 1 · Script entry", true),
            new(1, "Chapter 1 · Pi Script Foundations", "Import time", "Chapter 1 · Import time", true),
            new(2, "Chapter 1 · Pi Script Foundations", "sleep", "Chapter 1 · sleep", true),
            new(3, "Chapter 1 · Pi Script Foundations", "Variables", "Chapter 1 · Variables", true),
            new(4, "Chapter 1 · Pi Script Foundations", "Comments", "Chapter 1 · Comments", true),
            new(5, "Chapter 1 · Pi Script Foundations", "while True", "Chapter 1 · while True", true),
            new(6, "Chapter 1 · Pi Script Foundations", "Indentation", "Chapter 1 · Indentation", true),
            new(7, "Chapter 1 · Pi Script Foundations", "Chapter review", "Chapter 1 · Chapter review", true),
            new(8, "Chapter 2 · Digital Output", "GPIO setup idea", "Chapter 2 · GPIO setup idea", true),
            new(9, "Chapter 2 · Digital Output", "LED on", "Chapter 2 · LED on", true),
            new(10, "Chapter 2 · Digital Output", "LED off", "Chapter 2 · LED off", true),
            new(11, "Chapter 2 · Digital Output", "Blink helper", "Chapter 2 · Blink helper", true),
            new(12, "Chapter 2 · Digital Output", "Manual blink", "Chapter 2 · Manual blink", true),
            new(13, "Chapter 2 · Digital Output", "Two LEDs", "Chapter 2 · Two LEDs", true),
            new(14, "Chapter 2 · Digital Output", "Toggle", "Chapter 2 · Toggle", true),
            new(15, "Chapter 2 · Digital Output", "Output lab", "Chapter 2 · Output lab", true),
            new(16, "Chapter 3 · Digital Input", "Button input", "Chapter 3 · Button input", true),
            new(17, "Chapter 3 · Digital Input", "is_pressed", "Chapter 3 · is_pressed", true),
            new(18, "Chapter 3 · Digital Input", "Button to LED", "Chapter 3 · Button to LED", true),
            new(19, "Chapter 3 · Digital Input", "wait_for_press", "Chapter 3 · wait_for_press", true),
            new(20, "Chapter 3 · Digital Input", "when_pressed", "Chapter 3 · when_pressed", true),
            new(21, "Chapter 3 · Digital Input", "Pull-up default", "Chapter 3 · Pull-up default", true),
            new(22, "Chapter 3 · Digital Input", "Hold idea", "Chapter 3 · Hold idea", true),
            new(23, "Chapter 3 · Digital Input", "Input lab", "Chapter 3 · Input lab", true),
            new(24, "Chapter 4 · Timing and State", "State variable", "Chapter 4 · State variable", true),
            new(25, "Chapter 4 · Timing and State", "Flag gate", "Chapter 4 · Flag gate", true),
            new(26, "Chapter 4 · Timing and State", "Phase machine", "Chapter 4 · Phase machine", true),
            new(27, "Chapter 4 · Timing and State", "Timeout idea", "Chapter 4 · Timeout idea", true),
            new(28, "Chapter 4 · Timing and State", "Counter steps", "Chapter 4 · Counter steps", true),
            new(29, "Chapter 4 · Timing and State", "Latch", "Chapter 4 · Latch", true),
            new(30, "Chapter 4 · Timing and State", "Duty pattern", "Chapter 4 · Duty pattern", true),
            new(31, "Chapter 4 · Timing and State", "State lab", "Chapter 4 · State lab", true),
            new(32, "Chapter 5 · Sensors and Projects", "Read a value", "Chapter 5 · Read a value", true),
            new(33, "Chapter 5 · Sensors and Projects", "Threshold", "Chapter 5 · Threshold", true),
            new(34, "Chapter 5 · Sensors and Projects", "Loop threshold", "Chapter 5 · Loop threshold", true),
            new(35, "Chapter 5 · Sensors and Projects", "Night light logic", "Chapter 5 · Night light logic", true),
            new(36, "Chapter 5 · Sensors and Projects", "Alarm blink", "Chapter 5 · Alarm blink", true),
            new(37, "Chapter 5 · Sensors and Projects", "Button override", "Chapter 5 · Button override", true),
            new(38, "Chapter 5 · Sensors and Projects", "Named constants", "Chapter 5 · Named constants", true),
            new(39, "Chapter 5 · Sensors and Projects", "Maker project", "Chapter 5 · Maker project", true),
        };

    public static RaspiRoadmapModule Get(int index) => Modules[index];
}
