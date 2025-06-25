namespace MyDocs.Features.Documents.DeleteDocument
{
    public interface IDeleteDocumentService
    {
        public Task DeleteDocument(DeleteDocumentRequest request);
    }
}
