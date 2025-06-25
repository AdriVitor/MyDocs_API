using Microsoft.EntityFrameworkCore;
using MyDocs.Infraestructure.ExternalServices.AzureBlob;
using MyDocs.Infraestructure.Persistence;
using MyDocs.Models;
using MyDocs.Shared.Services.DocumentService;

namespace MyDocs.Features.Documents.DeleteDocument
{
    public class DeleteDocumentService : IDeleteDocumentService
    {
        private readonly IDocumentService _documentService;
        private readonly IAzureBlobService _azureBlobService;
        private readonly Context _context;
        public DeleteDocumentService(IDocumentService documentService, IAzureBlobService azureBlobService, Context context)
        {
            _documentService = documentService;
            _azureBlobService = azureBlobService;
            _context = context;
        }

        public async Task DeleteDocument(DeleteDocumentRequest request)
        {
            request.ValidateProperties();

            Document document = await _documentService.FindDocument(request.IdUser, request.IdDocument);

            await _azureBlobService.DeleteAsync(document.UniqueFileName);

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
        }
    }
}
