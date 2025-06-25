
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using MyDocs.Settings;

namespace MyDocs.Infraestructure.ExternalServices.AzureBlob
{
    public class AzureBlobService : IAzureBlobService
    {
        private readonly AzureBlobStorageSettings _azureBlobStorageSettings;
        private readonly BlobContainerClient _blobContainerClient;
        public AzureBlobService(IOptions<AzureBlobStorageSettings> azureBlobStorageSettings)
        {
            _azureBlobStorageSettings = azureBlobStorageSettings.Value;

            var blobServiceClient = new BlobServiceClient(_azureBlobStorageSettings.ConnectionString);
            _blobContainerClient = blobServiceClient.GetBlobContainerClient(_azureBlobStorageSettings.ContainerName);
            
        }

        public async Task DeleteAsync(string fileName)
        {
            var blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<Stream> DownloadAsync(string fileName)
        {
            var blobClient = _blobContainerClient.GetBlobClient(fileName);
            var response = await blobClient.DownloadAsync();
            return response.Value.Content;
        }

        public async Task UploadAsync(Stream content, string fileName, string contentType)
        {
            var httpHeaders = new BlobHttpHeaders()
            {
                ContentType = contentType,
            };

            var blobClient = _blobContainerClient.GetBlobClient(fileName);
            var result = await blobClient.UploadAsync(content, httpHeaders);
        }
    }
}
