namespace MyDocs.Features.Documents.DownloadDocument
{
    public interface IDownloadDocumentService
    {
        public Task<DownloadDocumentResponse> DownloadDocument(DownloadDocumentRequest request);
    }
}
