namespace MyDocs.Features.Documents.UploadDocument
{
    public interface IUploadDocumentService
    {
        public Task SaveFile(UploadDocumentRequest request);
    }
}
