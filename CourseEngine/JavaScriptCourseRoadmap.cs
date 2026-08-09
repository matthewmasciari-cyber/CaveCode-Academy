namespace CaveCode.CourseEngine;

public sealed record JavaScriptRoadmapModule(int Index, string Chapter, string Title, string Summary, bool IsPlayable);

public static class JavaScriptCourseRoadmap
{
    public static IReadOnlyList<JavaScriptRoadmapModule> Modules { get; } =
        new JavaScriptRoadmapModule[]
        {
            new(0, "Chapter 1 · Forge Boot-Up", "console.log", "Chapter 1 · console.log", true),
            new(1, "Chapter 1 · Forge Boot-Up", "Strings", "Chapter 1 · Strings", true),
            new(2, "Chapter 1 · Forge Boot-Up", "Variables let", "Chapter 1 · Variables let", true),
            new(3, "Chapter 1 · Forge Boot-Up", "const", "Chapter 1 · const", true),
            new(4, "Chapter 1 · Forge Boot-Up", "Numbers", "Chapter 1 · Numbers", true),
            new(5, "Chapter 1 · Forge Boot-Up", "Booleans", "Chapter 1 · Booleans", true),
            new(6, "Chapter 1 · Forge Boot-Up", "Comments", "Chapter 1 · Comments", true),
            new(7, "Chapter 1 · Forge Boot-Up", "Chapter review", "Chapter 1 · Chapter review", true),
            new(8, "Chapter 2 · Values and Decisions", "if true", "Chapter 2 · if true", true),
            new(9, "Chapter 2 · Values and Decisions", "else", "Chapter 2 · else", true),
            new(10, "Chapter 2 · Values and Decisions", "Comparison", "Chapter 2 · Comparison", true),
            new(11, "Chapter 2 · Values and Decisions", "Greater than", "Chapter 2 · Greater than", true),
            new(12, "Chapter 2 · Values and Decisions", "else if", "Chapter 2 · else if", true),
            new(13, "Chapter 2 · Values and Decisions", "Logical and", "Chapter 2 · Logical and", true),
            new(14, "Chapter 2 · Values and Decisions", "Logical or", "Chapter 2 · Logical or", true),
            new(15, "Chapter 2 · Values and Decisions", "Decision lab", "Chapter 2 · Decision lab", true),
            new(16, "Chapter 3 · Functions and Events", "Function declare", "Chapter 3 · Function declare", true),
            new(17, "Chapter 3 · Functions and Events", "Parameters", "Chapter 3 · Parameters", true),
            new(18, "Chapter 3 · Functions and Events", "Return", "Chapter 3 · Return", true),
            new(19, "Chapter 3 · Functions and Events", "Arrow feel", "Chapter 3 · Arrow feel", true),
            new(20, "Chapter 3 · Functions and Events", "Event idea", "Chapter 3 · Event idea", true),
            new(21, "Chapter 3 · Functions and Events", "Multiple calls", "Chapter 3 · Multiple calls", true),
            new(22, "Chapter 3 · Functions and Events", "Score helper", "Chapter 3 · Score helper", true),
            new(23, "Chapter 3 · Functions and Events", "Function lab", "Chapter 3 · Function lab", true),
            new(24, "Chapter 4 · DOM Arcade", "getElementById", "Chapter 4 · getElementById", true),
            new(25, "Chapter 4 · DOM Arcade", "textContent", "Chapter 4 · textContent", true),
            new(26, "Chapter 4 · DOM Arcade", "click listener", "Chapter 4 · click listener", true),
            new(27, "Chapter 4 · DOM Arcade", "Toggle text", "Chapter 4 · Toggle text", true),
            new(28, "Chapter 4 · DOM Arcade", "classList add", "Chapter 4 · classList add", true),
            new(29, "Chapter 4 · DOM Arcade", "Create element", "Chapter 4 · Create element", true),
            new(30, "Chapter 4 · DOM Arcade", "querySelector", "Chapter 4 · querySelector", true),
            new(31, "Chapter 4 · DOM Arcade", "DOM lab", "Chapter 4 · DOM lab", true),
            new(32, "Chapter 5 · Mini Games and Polish", "Click counter", "Chapter 5 · Click counter", true),
            new(33, "Chapter 5 · Mini Games and Polish", "Win at 5", "Chapter 5 · Win at 5", true),
            new(34, "Chapter 5 · Mini Games and Polish", "Disable button", "Chapter 5 · Disable button", true),
            new(35, "Chapter 5 · Mini Games and Polish", "Reset idea", "Chapter 5 · Reset idea", true),
            new(36, "Chapter 5 · Mini Games and Polish", "Random flavor", "Chapter 5 · Random flavor", true),
            new(37, "Chapter 5 · Mini Games and Polish", "Coin flip text", "Chapter 5 · Coin flip text", true),
            new(38, "Chapter 5 · Mini Games and Polish", "HUD polish", "Chapter 5 · HUD polish", true),
            new(39, "Chapter 5 · Mini Games and Polish", "Forge finale", "Chapter 5 · Forge finale", true),
        };
    public static JavaScriptRoadmapModule Get(int index) => Modules[index];
}
