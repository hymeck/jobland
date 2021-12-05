namespace Jobland.Infrastructure.Common.Identity.Dtos.Responses;

public sealed record UpdateUserProfileResponse(bool Succeeded, List<string> Errors);