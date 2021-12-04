using Jobland.Infrastructure.Common.Identity.Dtos.Responses;
using MediatR;

namespace Jobland.Infrastructure.Common.Identity.Dtos.Requests;

public sealed record RegistrationRequest
(
    string FirstName,
    string? LastName,
    string Email,
    string Password,
    Gender? Gender,
    DateTime? BirthDate,
    string? PhoneNumber
) : IRequest<AuthResponse>;
