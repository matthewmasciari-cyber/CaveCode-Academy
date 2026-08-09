namespace CaveCode.CourseEngine;

public sealed class CourseCatalogService
{
    private readonly IReadOnlyList<CourseManifest> courses;
    private readonly IReadOnlyDictionary<string, CourseManifest> byId;

    public CourseCatalogService()
    {
        courses = new[]
        {
            new CourseManifest
            {
                Id = CourseIds.CSharp,
                DisplayName = "C# Cave Adventure",
                ShortMark = "C#",
                LanguageName = "C#",
                Route = "/csharp",
                Description = "Build a growing cave-exploration game while mastering C#.",
                ProjectName = "Cave Explorer",
                EditorFileName = "PlayerTraining.cs",
                EditorLanguageLabel = "C#",
                PreviewKind = "cave-game",
                IsAvailable = true,
                HasMinigame = true
            },
            new CourseManifest
            {
                Id = CourseIds.Python,
                DisplayName = "Python Automation Quest",
                ShortMark = "Py",
                LanguageName = "Python",
                Route = "/python",
                Description = "Restore an underground facility while learning Python automation.",
                ProjectName = "Crystal Cavern Control Room",
                EditorFileName = "control_training.py",
                EditorLanguageLabel = "PYTHON",
                PreviewKind = "automation-facility",
                IsAvailable = true,
                HasMinigame = true
            },
            new CourseManifest
            {
                Id = CourseIds.Cpp,
                DisplayName = "C++ Engine Foundry",
                ShortMark = "C++",
                LanguageName = "C++",
                Route = "/cpp",
                Description = "Build a real-time engine workshop while learning high-performance C++.",
                ProjectName = "Engine Foundry",
                EditorFileName = "EngineTraining.cpp",
                EditorLanguageLabel = "C++",
                PreviewKind = "engine-workshop",
                IsAvailable = true,
                HasMinigame = true
            },
            new CourseManifest
            {
                Id = CourseIds.HtmlCss,
                DisplayName = "HTML & CSS Workshop",
                ShortMark = "HTML",
                LanguageName = "HTML & CSS",
                Route = "/html-css",
                Description = "Build a polished responsive website and game interface.",
                ProjectName = "Interface Workshop",
                EditorFileName = "index.html",
                EditorLanguageLabel = "HTML / CSS",
                PreviewKind = "live-web-preview",
                IsAvailable = true,
                HasMinigame = true
            },
            new CourseManifest
            {
                Id = CourseIds.Gcl,
                DisplayName = "GCL+ Control Line Lab",
                ShortMark = "GCL+",
                LanguageName = "GCL+",
                Route = "/gcl",
                Description = "Build control sequences, timing logic, staging, and point-driven automation programs.",
                ProjectName = "Control Line Lab",
                EditorFileName = "Sequence.gcl",
                EditorLanguageLabel = "GCL+",
                PreviewKind = "control-line-lab",
                IsAvailable = true,
                HasMinigame = true
            },

            new CourseManifest
            {
                Id = CourseIds.Arduino,
                DisplayName = "Arduino C++",
                ShortMark = "INO",
                LanguageName = "Arduino C++",
                Route = "/arduino",
                Description = "Applied C++ for Arduino-style boards: sketches, pins, LEDs, and sensors in a growing lab.",
                ProjectName = "Maker Lab",
                EditorFileName = "Sketch.ino",
                EditorLanguageLabel = "ARDUINO",
                PreviewKind = "arduino-lab",
                IsAvailable = true,
                HasMinigame = false
            }

        };

        byId = courses.ToDictionary(
            item => item.Id,
            StringComparer.OrdinalIgnoreCase);
    }

    public IReadOnlyList<CourseManifest> All => courses;

    public CourseManifest GetRequired(string courseId)
    {
        string normalized = CourseIds.Normalize(courseId);

        return byId.TryGetValue(
            normalized,
            out CourseManifest? course)
            ? course
            : throw new KeyNotFoundException(
                $"Course '{courseId}' is not registered.");
    }

    public bool TryGet(
        string courseId,
        out CourseManifest? course)
    {
        string normalized = CourseIds.Normalize(courseId);
        return byId.TryGetValue(normalized, out course);
    }
}
