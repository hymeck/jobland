using Jobland.Application.Logic.Works.Dtos.Responses;
using LanguageExt;
using MediatR;

namespace Jobland.Infrastructure.Common.Logic.Works.Dtos.Requests;

public sealed record GetUserAddedWorksRequest(string UserId = ""): IRequest<Option<IEnumerable<WorkPlainResponse>>>;
