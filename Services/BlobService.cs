using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

public class BlobService
{
    private readonly BlobContainerClient _containerClient;

    public BlobService(IConfiguration config)
    {
        // Get the connection string from appsettings.json placeholder
        var connectionStringTemplate = config["AzureStorage:ConnectionString"];

        // Get the secret key from User Secrets
        var key = config["AzureStorage:AccountKey"];

        // Replace the placeholder with the actual key
        var connectionString = connectionStringTemplate.Replace("{AzureStorageKey}", key);

        var containerName = config["AzureStorage:ContainerName"];

        _containerClient = new BlobContainerClient(connectionString, containerName);

        // Ensure container exists and is publicly accessible
        _containerClient.CreateIfNotExists();
        _containerClient.SetAccessPolicy(PublicAccessType.Blob);
    }

    public async Task<string> UploadAsync(IFormFile file)
    {
        // sanitize file name
        var safeFileName = Path.GetFileNameWithoutExtension(file.FileName)
            .Replace(" ", "_")
            .Replace("#", "")
            .Replace("%", "")
            .Replace("&", "")
            + Path.GetExtension(file.FileName);

        var blobName = Guid.NewGuid().ToString() + "_" + safeFileName;
        var blobClient = _containerClient.GetBlobClient(blobName);

        var headers = new BlobHttpHeaders
        {
            ContentType = file.ContentType
        };

        using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, new BlobUploadOptions
            {
                HttpHeaders = headers
            });
        }

        return blobClient.Uri.ToString();
    }
}
