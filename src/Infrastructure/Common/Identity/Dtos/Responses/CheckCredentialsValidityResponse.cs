namespace Jobland.Infrastructure.Common.Identity.Dtos.Responses;

public sealed record CheckCredentialsValidityResponse(bool Permissible, IReadOnlyList<string> Warnings);
