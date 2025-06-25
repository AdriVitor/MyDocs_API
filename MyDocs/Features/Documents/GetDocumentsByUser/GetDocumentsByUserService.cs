using Microsoft.EntityFrameworkCore;
using MyDocs.Infraestructure.Persistence;
using MyDocs.Models;

namespace MyDocs.Features.Documents.GetDocumentsByUser
{
    public class GetDocumentsByUserService : IGetDocumentsByUserService
    {
        private readonly Context _context;
        public GetDocumentsByUserService(Context context)
        {
            _context = context;
        }

        public async Task<List<GetDocumentsByUserResponse>> GetDocumentsByUser(GetDocumentsByUserRequest request)
        {
            request.ValidateProperties();

            List<Document> documents = await FindDocuments(request.IdUser);

            List<GetDocumentsByUserResponse> response = documents.ConvertAll(d => new GetDocumentsByUserResponse(d));

            return response;
        }

        private async Task<List<Document>> FindDocuments(int idUser)
        {
            return await _context.Documents.Where(d => d.IdUser == idUser).ToListAsync();
        }
    }
}
