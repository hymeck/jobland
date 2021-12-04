using Jobland.Infrastructure.Common.Identity.Dtos.Responses;
using MediatR;

namespace Jobland.Infrastructure.Common.Identity.Dtos.Requests;

public sealed record LoginRequest(string Email, string Password) : IRequest<AuthResponse>;
