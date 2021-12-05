using Jobland.Infrastructure.Common.Identity.Dtos.Responses;
using MediatR;

namespace Jobland.Infrastructure.Common.Identity.Dtos.Requests;

public sealed record UpdateUserProfileRequest(
    string? FirstName,
    string? LastName,
    Gender? Gender,
    DateTime? BirthDate,
    string? Email,
    string? Password) : IRequest<UpdateUserProfileResponse>
{
    public string UserId { get; set; } = "";
}
