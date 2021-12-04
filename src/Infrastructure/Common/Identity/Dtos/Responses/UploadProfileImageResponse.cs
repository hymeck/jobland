namespace Jobland.Infrastructure.Common.Identity.Dtos.Responses;

public record UploadProfileImageResponse(bool Success, string ImageUrl, List<string> Errors);
