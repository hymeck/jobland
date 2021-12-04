using Jobland.Application.Logic.Works.Dtos.Responses;
using MediatR;

namespace Jobland.Application.Logic.Works.Dtos.Requests;

public sealed record GetWorksRequest(int Offset = 0, int Limit = 50) : IRequest<IEnumerable<WorkPlainResponse>>;
