using Jobland.Application.Logic.Works.Dtos.Responses;
using LanguageExt;
using MediatR;

namespace Jobland.Application.Logic.Works.Dtos.Requests;

public sealed record RespondWorkRequest(long Id) : IRequest<Option<RespondWorkResponse>>
{
    public string ResponderId { get; set; } = "";
};
