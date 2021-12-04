using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Jobland.Infrastructure.Common.Identity;
using Jobland.Infrastructure.Common.Identity.Abstractions;
using Jobland.Infrastructure.Common.Persistence;
using LanguageExt;
using LanguageExt.Common;

namespace Jobland.Infrastructure.Api.Web.Identity;

public class ProfileImageService : IProfileImageService
{
    private readonly IProfileImageSensitiveStorageDataProvider _provider;
    private readonly ApplicationDbContext _db;

    public ProfileImageService(IProfileImageSensitiveStorageDataProvider provider, ApplicationDbContext db)
    {
        _provider = provider;
        _db = db;
    }

    public async Task<UploadImageResult> UploadImageAsync(IFormFile image, string userId, CancellationToken token = default)
    {
        var (uploadResponse, imageUri) = await UploadImageToBlobAsync(image, userId, token);
        var result = uploadResponse.Match(r => UploadImageResult.Ok(imageUri.ToString()),
            e => UploadImageResult.Fail(new List<string> { e.Message }));

        if (result.Success)
        {
            var imageEntity = new ProfileImage { ImageUrl = result.ImageUrl, OwnerId = userId ?? "" };
            _db.ProfileImages.Add(imageEntity);
            await _db.SaveChangesSafelyAsync(token);
        }
        
        return result;
    }

    private async Task<(Result<Response<BlobContentInfo>> response, Uri imageUri)> UploadImageToBlobAsync(IFormFile image, string userId, CancellationToken token)
    {
        var timestamp = DateTime.UtcNow.ToString("yyyyMMddhhmmss");
        var filename = $"{timestamp}_{userId}_{image.FileName}";
        var blobClient = new BlobClient(_provider.ConnectionString, _provider.BlobContainerName, filename);
        TryAsync<Response<BlobContentInfo>> tryUpload = async () => await blobClient.UploadAsync(image.OpenReadStream(), token);
        return (await tryUpload(), blobClient.Uri);
    }
}
