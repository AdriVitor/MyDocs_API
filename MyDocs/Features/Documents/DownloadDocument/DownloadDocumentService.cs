using MyDocs.Infraestructure.ExternalServices.AzureBlob;
using MyDocs.Models;
using MyDocs.Shared.Services.DocumentService;

namespace MyDocs.Features.Documents.DownloadDocument
{
    public class DownloadDocumentService : IDownloadDocumentService
    {
        private readonly IDocumentService _documentService;
        private readonly IAzureBlobService _azureBlobService;
        public DownloadDocumentService(IDocumentService documentService, IAzureBlobService azureBlobService)
        {
            _documentService = documentService;
            _azureBlobService = azureBlobService;
        }

        public async Task<DownloadDocumentResponse> DownloadDocument(DownloadDocumentRequest request)
        {
            request.ValidateProperties();

            Document document = await _documentService.FindDocument(request.IdUser, request.IdDocument);

            Stream content = await _azureBlobService.DownloadAsync(document.UniqueFileName);

            return new DownloadDocumentResponse(content, document.FileName, document.FileSize);
        }
    }
}
