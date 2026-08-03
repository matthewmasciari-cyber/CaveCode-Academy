using System.Net;
using System.Text.RegularExpressions;

namespace CaveCode.CourseEngine;

public sealed class HtmlCourseCodeValidator : ICourseCodeValidator
{
    private static readonly Regex TagRegex = new(
        @"<\s*(?<closing>/)?\s*(?<name>[A-Za-z][A-Za-z0-9:-]*)(?<attrs>[^>]*)>",
        RegexOptions.Compiled);

    private static readonly Regex AttributeRegex = new(
        @"(?<name>[^\s=/>]+)(?:\s*=\s*(?:""(?<double>[^""]*)""|'(?<single>[^']*)'|(?<bare>[^\s>]+)))?",
        RegexOptions.Compiled);

    private static readonly Regex CommentRegex = new(
        @"<!--.*?-->",
        RegexOptions.Compiled | RegexOptions.Singleline);

    private static readonly Regex DoctypeRegex = new(
        @"<!DOCTYPE\s+html\s*>",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private static readonly Regex UnsafeMarkupRegex = new(
        @"<\s*script\b|\son[a-z]+\s*=|javascript\s*:",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private static readonly HashSet<string> VoidTags = new(
        StringComparer.OrdinalIgnoreCase)
    {
        "area", "base", "br", "col", "embed", "hr", "img",
        "input", "link", "meta", "param", "source", "track", "wbr"
    };

    public bool Supports(string courseId) =>
        string.Equals(
            courseId,
            CourseIds.HtmlCss,
            StringComparison.OrdinalIgnoreCase);

    public CourseCodeValidationResult Validate(
        CourseCodeValidationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        string submitted = NormalizeLineEndings(request.SubmittedCode);
        string expected = NormalizeLineEndings(request.ExpectedCode);
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(submitted))
        {
            return Result(
                false,
                0,
                1,
                "Add the requested HTML before checking.",
                submitted,
                expected);
        }

        if (UnsafeMarkupRegex.IsMatch(submitted))
        {
            errors.Add(
                "Scripts and inline event handlers are not part of this HTML lesson.");
        }

        HtmlAnalysis submittedAnalysis = Analyze(submitted);
        HtmlAnalysis expectedAnalysis = Analyze(expected);

        if (!submittedAnalysis.IsBalanced)
        {
            errors.Add(
                submittedAnalysis.BalanceMessage ??
                "Opening and closing tags are not balanced.");
        }

        int totalRequirements = 1;
        int completedRequirements = submittedAnalysis.IsBalanced ? 1 : 0;

        if (expectedAnalysis.HasDoctype)
        {
            totalRequirements++;

            if (submittedAnalysis.HasDoctype)
            {
                completedRequirements++;
            }
            else
            {
                errors.Add("Add <!DOCTYPE html> at the beginning of the page.");
            }
        }

        foreach ((string tag, int expectedCount) in expectedAnalysis.TagCounts)
        {
            totalRequirements++;
            submittedAnalysis.TagCounts.TryGetValue(tag, out int actualCount);

            if (actualCount >= expectedCount)
            {
                completedRequirements++;
            }
            else
            {
                errors.Add(
                    $"The solution needs {expectedCount} <{tag}> element" +
                    (expectedCount == 1 ? "." : "s."));
            }
        }

        foreach (HtmlAttribute attribute in expectedAnalysis.Attributes)
        {
            totalRequirements++;

            bool found = submittedAnalysis.Attributes.Any(candidate =>
                string.Equals(
                    candidate.Tag,
                    attribute.Tag,
                    StringComparison.OrdinalIgnoreCase) &&
                string.Equals(
                    candidate.Name,
                    attribute.Name,
                    StringComparison.OrdinalIgnoreCase) &&
                string.Equals(
                    candidate.Value,
                    attribute.Value,
                    StringComparison.OrdinalIgnoreCase));

            if (found)
            {
                completedRequirements++;
            }
            else
            {
                errors.Add(
                    $"Add {attribute.Name}=\"{attribute.Value}\" to a <{attribute.Tag}> element.");
            }
        }

        string submittedText = NormalizeVisibleText(
            submittedAnalysis.VisibleText);

        foreach (string textChunk in expectedAnalysis.TextChunks)
        {
            totalRequirements++;
            string requiredText = NormalizeVisibleText(textChunk);

            if (submittedText.Contains(
                requiredText,
                StringComparison.OrdinalIgnoreCase))
            {
                completedRequirements++;
            }
            else
            {
                errors.Add($"Include the text: {requiredText}");
            }
        }

        bool valid = errors.Count == 0;
        int accuracy = totalRequirements <= 0
            ? 100
            : Math.Clamp(
                (int)Math.Round(
                    completedRequirements * 100d / totalRequirements),
                0,
                100);

        string message = valid
            ? "The required elements, attributes, text, and tag nesting are present."
            : string.Join(" ", errors.Take(3));

        return Result(
            valid,
            accuracy,
            errors.Count,
            message,
            submitted,
            expected);
    }

    private static CourseCodeValidationResult Result(
        bool valid,
        int accuracy,
        int errors,
        string message,
        string submitted,
        string expected) =>
        new(
            valid,
            accuracy,
            Math.Max(0, errors),
            valid ? "HTML accepted" : "Keep repairing the HTML",
            message,
            CollapseWhitespace(submitted),
            CollapseWhitespace(expected));

    private static HtmlAnalysis Analyze(string html)
    {
        string withoutComments = CommentRegex.Replace(html, "");
        var tagCounts = new Dictionary<string, int>(
            StringComparer.OrdinalIgnoreCase);
        var attributes = new List<HtmlAttribute>();
        var stack = new Stack<string>();
        bool balanced = true;
        string? balanceMessage = null;

        foreach (Match match in TagRegex.Matches(withoutComments))
        {
            string name = match.Groups["name"].Value.ToLowerInvariant();
            bool closing = match.Groups["closing"].Success;
            string attributeText = match.Groups["attrs"].Value;
            bool selfClosing = attributeText.TrimEnd().EndsWith(
                "/",
                StringComparison.Ordinal);

            if (closing)
            {
                if (VoidTags.Contains(name))
                {
                    balanced = false;
                    balanceMessage = $"<{name}> is a void element and should not have a closing tag.";
                    break;
                }

                if (stack.Count == 0)
                {
                    balanced = false;
                    balanceMessage = $"Found </{name}> without a matching opening tag.";
                    break;
                }

                string openName = stack.Pop();

                if (!string.Equals(
                    openName,
                    name,
                    StringComparison.OrdinalIgnoreCase))
                {
                    balanced = false;
                    balanceMessage =
                        $"Close <{openName}> before closing <{name}>.";
                    break;
                }

                continue;
            }

            tagCounts[name] = tagCounts.TryGetValue(name, out int count)
                ? count + 1
                : 1;

            foreach (Match attributeMatch in AttributeRegex.Matches(attributeText))
            {
                string attributeName = attributeMatch.Groups["name"].Value;

                if (string.IsNullOrWhiteSpace(attributeName) ||
                    attributeName == "/")
                {
                    continue;
                }

                string value = attributeMatch.Groups["double"].Success
                    ? attributeMatch.Groups["double"].Value
                    : attributeMatch.Groups["single"].Success
                        ? attributeMatch.Groups["single"].Value
                        : attributeMatch.Groups["bare"].Value;

                attributes.Add(
                    new HtmlAttribute(name, attributeName, value));
            }

            if (!VoidTags.Contains(name) && !selfClosing)
            {
                stack.Push(name);
            }
        }

        if (balanced && stack.Count > 0)
        {
            string openName = stack.Peek();
            balanced = false;
            balanceMessage = $"Add the missing </{openName}> closing tag.";
        }

        string visibleText = TagRegex.Replace(withoutComments, "\n");
        visibleText = DoctypeRegex.Replace(visibleText, "\n");
        visibleText = WebUtility.HtmlDecode(visibleText);

        string[] chunks = visibleText
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(NormalizeVisibleText)
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        return new HtmlAnalysis(
            tagCounts,
            attributes,
            chunks,
            visibleText,
            DoctypeRegex.IsMatch(withoutComments),
            balanced,
            balanceMessage);
    }

    private static string NormalizeLineEndings(string? value) =>
        (value ?? "")
            .Replace("\r\n", "\n", StringComparison.Ordinal)
            .Replace('\r', '\n')
            .Trim();

    private static string NormalizeVisibleText(string? value) =>
        Regex.Replace(value ?? "", @"\s+", " ").Trim();

    private static string CollapseWhitespace(string? value) =>
        Regex.Replace(
            NormalizeLineEndings(value),
            @"\s+",
            " ");

    private sealed record HtmlAttribute(
        string Tag,
        string Name,
        string Value);

    private sealed record HtmlAnalysis(
        IReadOnlyDictionary<string, int> TagCounts,
        IReadOnlyList<HtmlAttribute> Attributes,
        IReadOnlyList<string> TextChunks,
        string VisibleText,
        bool HasDoctype,
        bool IsBalanced,
        string? BalanceMessage);
}
