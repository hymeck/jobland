using Jobland.Application.Logic.Works.Dtos.Responses;
using LanguageExt;
using MediatR;

namespace Jobland.Application.Logic.Works.Dtos.Requests;

public sealed record GetWorkByIdRequest(long Id) : IRequest<Option<WorkPlainResponse>>;
