using Jobland.Application.Logic.Works.Dtos.Responses;
using MediatR;

namespace Jobland.Application.Logic.Works.Dtos.Requests;

public sealed record GetWorkCountRequest : IRequest<WorkCountResponse>;
