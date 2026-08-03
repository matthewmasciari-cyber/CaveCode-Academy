namespace CaveCode.CourseEngine;

public sealed record CourseDefinitionValidationResult(
    IReadOnlyList<string> Errors,
    IReadOnlyList<string> Warnings
)
{
    public bool IsValid => Errors.Count == 0;
}

public static class CourseDefinitionValidator
{
    public static CourseDefinitionValidationResult Validate(
        CourseDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        var errors = new List<string>();
        var warnings = new List<string>();
        CourseManifest manifest = definition.Manifest;

        if (!CourseIds.IsKnown(manifest.Id))
        {
            warnings.Add(
                $"Course ID '{manifest.Id}' is not in the initial registry.");
        }

        if (manifest.ModuleCount <= 0)
        {
            errors.Add("ModuleCount must be greater than zero.");
        }

        if (manifest.ChapterCount * manifest.ModulesPerChapter !=
            manifest.ModuleCount)
        {
            errors.Add(
                "ChapterCount multiplied by ModulesPerChapter must equal ModuleCount.");
        }

        if (manifest.IsAvailable &&
            definition.Lessons.Count != manifest.ModuleCount)
        {
            errors.Add(
                $"{manifest.DisplayName} is available but contains " +
                $"{definition.Lessons.Count} of {manifest.ModuleCount} lessons.");
        }

        var titles = new HashSet<string>(
            StringComparer.OrdinalIgnoreCase);

        for (int index = 0; index < definition.Lessons.Count; index++)
        {
            CourseLesson lesson = definition.Lessons[index];
            string location = $"Module {index + 1}";

            Require(lesson.Chapter, $"{location}: Chapter is required.", errors);
            Require(lesson.Topic, $"{location}: Topic is required.", errors);
            Require(lesson.Title, $"{location}: Title is required.", errors);
            Require(lesson.Teaching, $"{location}: Teaching is required.", errors);
            Require(lesson.TargetCode, $"{location}: TargetCode is required.", errors);
            Require(lesson.TransferCode, $"{location}: TransferCode is required.", errors);

            if (!string.IsNullOrWhiteSpace(lesson.Title) &&
                !titles.Add(lesson.Title.Trim()))
            {
                warnings.Add(
                    $"{location}: duplicate title '{lesson.Title}'.");
            }

            if (lesson.PredictionOptions is null ||
                lesson.PredictionOptions.Length < 2)
            {
                errors.Add(
                    $"{location}: at least two prediction choices are required.");
            }
            else if (lesson.PredictionCorrect < 0 ||
                     lesson.PredictionCorrect >=
                     lesson.PredictionOptions.Length)
            {
                errors.Add(
                    $"{location}: PredictionCorrect is outside the choices.");
            }
        }

        return new CourseDefinitionValidationResult(errors, warnings);
    }

    private static void Require(
        string? value,
        string message,
        List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add(message);
        }
    }
}

public sealed record CourseCodeValidationRequest(
    string CourseId,
    int ModuleIndex,
    CourseTrainingStage Stage,
    string SubmittedCode,
    string ExpectedCode,
    bool IgnoreWhitespace = true,
    bool IgnoreLineEndings = true
);

public sealed record CourseCodeValidationResult(
    bool IsValid,
    int Accuracy,
    int ErrorCount,
    string Heading,
    string Message,
    string NormalizedSubmittedCode,
    string NormalizedExpectedCode
);

public interface ICourseCodeValidator
{
    bool Supports(string courseId);

    CourseCodeValidationResult Validate(
        CourseCodeValidationRequest request);
}

public sealed class StructuralCourseCodeValidator :
    ICourseCodeValidator
{
    public bool Supports(string courseId) => true;

    public CourseCodeValidationResult Validate(
        CourseCodeValidationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        string submitted = Normalize(
            request.SubmittedCode,
            request.IgnoreWhitespace,
            request.IgnoreLineEndings);

        string expected = Normalize(
            request.ExpectedCode,
            request.IgnoreWhitespace,
            request.IgnoreLineEndings);

        int matched = 0;
        int comparisonLength = Math.Max(
            submitted.Length,
            expected.Length);

        for (int index = 0;
             index < Math.Min(
                 submitted.Length,
                 expected.Length);
             index++)
        {
            if (submitted[index] == expected[index])
            {
                matched++;
            }
        }

        int errors = comparisonLength - matched;
        int accuracy = comparisonLength == 0
            ? 100
            : Math.Clamp(
                (int)Math.Round(
                    matched * 100d / comparisonLength),
                0,
                100);

        bool valid = string.Equals(
            submitted,
            expected,
            StringComparison.Ordinal);

        return new CourseCodeValidationResult(
            valid,
            accuracy,
            Math.Max(0, errors),
            valid ? "Code accepted" : "Keep debugging",
            valid
                ? "The submitted structure matches the expected solution."
                : "The code does not match the expected structure yet.",
            submitted,
            expected);
    }

    private static string Normalize(
        string? value,
        bool ignoreWhitespace,
        bool ignoreLineEndings)
    {
        string result = value ?? "";

        if (ignoreLineEndings)
        {
            result = result
                .Replace("\r\n", "\n", StringComparison.Ordinal)
                .Replace('\r', '\n');
        }

        result = result.Trim();

        if (ignoreWhitespace)
        {
            result = System.Text.RegularExpressions.Regex.Replace(
                result,
                @"\s+",
                " ");
        }

        return result;
    }
}
