namespace MyDocs.Infraestructure.ExternalServices.AzureBlob
{
    public interface IAzureBlobService
    {
        public Task UploadAsync(Stream strem, string fileName, string contentType);
        public Task<Stream> DownloadAsync(string fileName);
        public Task DeleteAsync(string fileName);
    }
}
