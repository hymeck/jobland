using Jobland.Infrastructure.Common.Identity.Dtos.Responses;
using LanguageExt;
using MediatR;

namespace Jobland.Infrastructure.Common.Identity.Dtos.Requests;

public sealed record GetUserProfileRequest(string UserId) : IRequest<Option<GetUserProfileResponse>>;
