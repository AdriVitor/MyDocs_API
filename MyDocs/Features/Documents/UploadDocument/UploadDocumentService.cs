using MyDocs.Infraestructure.ExternalServices.AzureBlob;
using MyDocs.Infraestructure.Persistence;
using MyDocs.Models;

namespace MyDocs.Features.Documents.UploadDocument
{
    public class UploadDocumentService : IUploadDocumentService
    {
        private readonly Context _context;
        private readonly IAzureBlobService _azureBlobService;
        public UploadDocumentService(Context context, IAzureBlobService azureBlobService)
        {
            _context = context;
            _azureBlobService = azureBlobService;
        }

        public async Task SaveFile(UploadDocumentRequest request)
        {
            request.FileInfo.ValidateProperties();

            var extension = Path.GetExtension(request.FileInfo.File.FileName).ToLowerInvariant();
            string uniqueFileName = string.Concat(request.FileInfo.IdUser, "-", request.FileInfo.File.FileName, "-", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));

            Document document = new Document()
            {
                IdUser = request.FileInfo.IdUser,
                FileName = request.FileInfo.File.FileName,
                UniqueFileName = uniqueFileName,
                FileType = extension,
                FileSize = request.FileInfo.File.Length,
                UploadDate = DateTime.Now
            };

            using (var stream = request.FileInfo.File.OpenReadStream())
            {
                await _azureBlobService.UploadAsync(stream, uniqueFileName, extension);
            }

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
        }
    }
}
