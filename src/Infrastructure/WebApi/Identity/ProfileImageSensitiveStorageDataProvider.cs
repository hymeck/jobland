using Jobland.Infrastructure.Common.Identity.Abstractions;

namespace Jobland.Infrastructure.Api.Web.Identity;

public sealed class ProfileImageSensitiveStorageDataProvider : IProfileImageSensitiveStorageDataProvider
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;

    public ProfileImageSensitiveStorageDataProvider(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _env = env;
    }

    public string ConnectionString => _env.IsDevelopment()
        ? _configuration["ProfileImagesStorageAccountConnectionString"]
        : Environment.GetEnvironmentVariable("ProfileImagesStorageAccountConnectionString") ?? "";

    public string BlobContainerName => _env.IsDevelopment()
        ? _configuration["ProfileImagesBlobContainerName"]
        : Environment.GetEnvironmentVariable("ProfileImagesBlobContainerName") ?? "";
}
