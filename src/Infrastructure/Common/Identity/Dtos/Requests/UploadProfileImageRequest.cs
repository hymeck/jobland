using Jobland.Infrastructure.Common.Identity.Dtos.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Jobland.Infrastructure.Common.Identity.Dtos.Requests;

public sealed record UploadProfileImageRequest(IFormFile Image, string UserId) : IRequest<UploadProfileImageResponse>;
