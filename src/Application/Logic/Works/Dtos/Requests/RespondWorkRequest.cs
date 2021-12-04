using MediatR;

namespace Jobland.Application.Logic.Works.Dtos.Requests;

public sealed record RespondWorkRequest(long Id) : IRequest<bool>;
