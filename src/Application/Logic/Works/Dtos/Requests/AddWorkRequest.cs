using Jobland.Application.Logic.Works.Dtos.Responses;
using LanguageExt;
using MediatR;

namespace Jobland.Application.Logic.Works.Dtos.Requests;

public sealed record AddWorkRequest(
    string Title,
    string Description,
    DateTime? StartedOn,
    DateTime? FinishedOn,
    string PhoneNumber,
    long? LowerPriceBound,
    long? UpperPriceBound,
    long SubcategoryId) : IRequest<Option<WorkPlainResponse>>;
