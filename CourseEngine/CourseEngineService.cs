using Microsoft.JSInterop;

namespace CaveCode.CourseEngine;

public sealed class CourseEngineService(
    IJSRuntime js,
    CourseCatalogService catalog,
    IEnumerable<ICourseCodeValidator> validators)
{
    public IReadOnlyList<CourseManifest> Courses => catalog.All;

    public CourseManifest GetCourse(string courseId) =>
        catalog.GetRequired(courseId);

    public ValueTask<CourseProgressSnapshot> GetProgressAsync(
        string courseId)
    {
        CourseManifest course = catalog.GetRequired(courseId);

        return js.InvokeAsync<CourseProgressSnapshot>(
            "caveCodeCourseEngine.getProgress",
            course.Id,
            course.ModuleCount,
            course.CourseVersion);
    }

    public ValueTask<CourseProgressSnapshot> SaveProgressAsync(
        string courseId,
        CourseProgressSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        CourseManifest course = catalog.GetRequired(courseId);
        CourseProgressMigrationResult normalized =
            CourseProgressMigrator.Normalize(
                snapshot,
                course.ModuleCount,
                course.CourseVersion);

        return js.InvokeAsync<CourseProgressSnapshot>(
            "caveCodeCourseEngine.saveProgress",
            course.Id,
            normalized.Snapshot,
            course.ModuleCount,
            course.CourseVersion);
    }

    public ValueTask<string> GetStorageKeyAsync(
        string courseId)
    {
        CourseManifest course = catalog.GetRequired(courseId);

        return js.InvokeAsync<string>(
            "caveCodeCourseEngine.getStorageKey",
            course.Id);
    }

    public CourseCodeValidationResult ValidateCode(
        CourseCodeValidationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        string courseId =
            CourseIds.Normalize(request.CourseId);

        ICourseCodeValidator validator =
            validators.FirstOrDefault(
                item => item.Supports(courseId))
            ?? throw new InvalidOperationException(
                $"No code validator supports '{courseId}'.");

        return validator.Validate(
            request with { CourseId = courseId });
    }
}
