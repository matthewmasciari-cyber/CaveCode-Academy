namespace CaveCode.CourseEngine;

public static class CourseIds
{
    public const string CSharp = "csharp";
    public const string Python = "python";
    public const string Cpp = "cpp";
    public const string HtmlCss = "htmlcss";

    public static IReadOnlySet<string> Known { get; } =
        new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            CSharp, Python, Cpp, HtmlCss
        };

    public static bool IsKnown(string? courseId) =>
        !string.IsNullOrWhiteSpace(courseId) &&
        Known.Contains(courseId.Trim());

    public static string Normalize(string courseId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(courseId);
        string value = courseId.Trim().ToLowerInvariant();

        return value switch
        {
            "c#" or "cs" or "csharp" => CSharp,
            "py" or "python" => Python,
            "c++" or "cplusplus" or "cpp" => Cpp,
            "html" or "css" or "html-css" or "htmlcss" => HtmlCss,
            _ => value
        };
    }
}
