namespace MyDocs.Features.Documents.GetDocumentsByUser
{
    public interface IGetDocumentsByUserService
    {
        public Task<List<GetDocumentsByUserResponse>> GetDocumentsByUser(GetDocumentsByUserRequest request);
    }
}
