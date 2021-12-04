using Jobland.Application.Logic.Works.Dtos.Responses;
using MediatR;

namespace Jobland.Application.Logic.Works.Dtos.Requests;

public sealed record GetWorksRequest(int Offset, int Limit) : IRequest<IEnumerable<WorkPlainResponse>>;
