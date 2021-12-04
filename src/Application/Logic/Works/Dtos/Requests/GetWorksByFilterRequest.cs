using Jobland.Application.Logic.Works.Dtos.Responses;
using MediatR;

namespace Jobland.Application.Logic.Works.Dtos.Requests;

public sealed record GetWorksByFilterRequest(
    long? LowerPriceBound = null,
    long? UpperPriceBound = null,
    DateTime? Started = null,
    DateTime? Finished = null,
    bool? WithResponses = null,
    HashSet<long>? Subcategories = null) : IRequest<IEnumerable<WorkPlainResponse>>;
