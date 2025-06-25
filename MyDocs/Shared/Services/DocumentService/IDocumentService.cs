using MyDocs.Features.Documents.GetDocumentById;
using MyDocs.Models;

namespace MyDocs.Shared.Services.DocumentService
{
    public interface IDocumentService
    {
        public Task<Document> FindDocument(int idUser, int idDocument);
    }
}
