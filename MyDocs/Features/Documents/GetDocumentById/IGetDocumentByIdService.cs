namespace MyDocs.Features.Documents.GetDocumentById
{
    public interface IGetDocumentByIdService
    {
        public Task<GetDocumentByIdResponse> GetDocumentById(GetDocumentByIdRequest request);
    }
}
