namespace CaveCode.CourseEngine;

public static class CourseIds
{
    public const string CSharp = "csharp";
    public const string Python = "python";
    public const string Cpp = "cpp";
    public const string HtmlCss = "htmlcss";
    public const string Gcl = "gcl";
    public const string Arduino = "arduino";

    public static IReadOnlySet<string> Known { get; } =
        new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            CSharp, Python, Cpp, HtmlCss, Gcl, Arduino
        };

    public static bool IsKnown(string? courseId) =>
        !string.IsNullOrWhiteSpace(courseId) &&
        Known.Contains(courseId.Trim());

    public static string Normalize(string? courseId)
    {
        if (string.IsNullOrWhiteSpace(courseId))
        {
            return string.Empty;
        }

        string source = courseId.Trim().ToLowerInvariant();

        return source switch
        {
            "c#" or "cs" or "csharp" => CSharp,
            "py" or "python" => Python,
            "c++" or "cplusplus" or "cpp" => Cpp,
            "html" or "css" or "html-css" or "htmlcss" => HtmlCss,
            "gcl" or "gcl+" or "cgline" or "cgl" or "cgline+" => Gcl,
            "arduino" or "arduino-cpp" or "arduinocpp" => Arduino,
            _ => source
        };
    }
}
