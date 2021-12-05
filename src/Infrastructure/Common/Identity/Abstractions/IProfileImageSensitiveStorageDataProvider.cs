namespace Jobland.Infrastructure.Common.Identity.Abstractions;

public interface IProfileImageSensitiveStorageDataProvider
{
    public string ConnectionString { get; }
    public string BlobContainerName { get; }
}
