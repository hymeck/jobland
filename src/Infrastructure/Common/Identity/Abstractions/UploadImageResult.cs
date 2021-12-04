namespace Jobland.Infrastructure.Common.Identity.Abstractions;

public sealed class UploadImageResult
{
    public UploadImageResult()
    {
    }
    public UploadImageResult(bool success, string imageUrl, List<string> errors)
    {
        Success = success;
        ImageUrl = imageUrl;
        Errors = errors;
    }

    public bool Success { get; init; } = true;
    public string ImageUrl { get; init; } = "";
    public List<string> Errors { get; init; } = new();

    public static UploadImageResult Ok(string imageUrl) => new() { ImageUrl = imageUrl };
    public static UploadImageResult Fail(List<string> errors) => new() { Errors = errors };
}
