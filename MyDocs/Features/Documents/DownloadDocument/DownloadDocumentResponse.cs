namespace MyDocs.Features.Documents.DownloadDocument
{
    public class DownloadDocumentResponse
    {
        public Stream Stream { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }

        public DownloadDocumentResponse(Stream stream, string fileName, long lengthBytes)
        {
            Stream = stream;
            FileName = fileName;
            FileSize = lengthBytes;
        }
    }
}
