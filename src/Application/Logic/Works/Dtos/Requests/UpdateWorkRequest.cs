using MediatR;

namespace Jobland.Application.Logic.Works.Dtos.Requests;

public sealed record UpdateWorkRequest(long Id,
    string? Title,
    string? Description,
    DateTime? StartedOn, 
    DateTime? FinishedOn,
    string? PhoneNumber,
    long? LowerPriceBound,
    long? UpperPriceBound,
    long? SubcategoryId) : IRequest<bool>;
