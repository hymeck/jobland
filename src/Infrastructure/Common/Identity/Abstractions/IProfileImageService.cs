using Microsoft.AspNetCore.Http;

namespace Jobland.Infrastructure.Common.Identity.Abstractions;

public interface IProfileImageService
{
    public Task<UploadImageResult> UploadImageAsync(IFormFile image, string userId, CancellationToken token = default);
}
