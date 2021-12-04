using Jobland.Infrastructure.Common.Identity.Abstractions;
using Jobland.Infrastructure.Common.Identity.Dtos.Requests;
using Jobland.Infrastructure.Common.Identity.Dtos.Responses;
using MediatR;

namespace Jobland.Infrastructure.Common.Identity.Commands;

public sealed class UploadProfileImageRequestHandler : IRequestHandler<UploadProfileImageRequest, UploadProfileImageResponse>
{
    private readonly IProfileImageService _imageService;

    public UploadProfileImageRequestHandler(IProfileImageService imageService) => _imageService = imageService;

    public async Task<UploadProfileImageResponse> Handle(UploadProfileImageRequest request, CancellationToken cancellationToken)
    {
        var result = await _imageService.UploadImageAsync(request.Image, request.UserId, cancellationToken);
        return new UploadProfileImageResponse(result.Success, result.ImageUrl, result.Errors);
    }
}
