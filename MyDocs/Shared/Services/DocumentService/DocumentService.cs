using Microsoft.EntityFrameworkCore;
using MyDocs.Features.Documents.GetDocumentById;
using MyDocs.Infraestructure.Persistence;
using MyDocs.Models;

namespace MyDocs.Shared.Services.DocumentService
{
    public class DocumentService : IDocumentService
    {
        private readonly Context _context;
        public DocumentService(Context context)
        {
            _context = context;
        }

        public async Task<Document> FindDocument(int idUser, int idDocument)
        {
            Document? document =  await _context
                                        .Documents
                                        .FirstOrDefaultAsync(d => d.IdUser == idUser && d.Id == idDocument);

            return document ?? throw new ArgumentNullException("O arquivo não foi localizado");
        }
    }
}
