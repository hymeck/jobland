using Jobland.Infrastructure.Common.Identity.Dtos.Responses;
using LanguageExt;
using MediatR;

namespace Jobland.Infrastructure.Common.Identity.Dtos.Requests;

public sealed record CheckCredentialsValidityRequest(string Email, string Password) : IRequest<Option<CheckCredentialsValidityResponse>>;
