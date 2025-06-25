using MyDocs.Models;

namespace MyDocs.Features.Documents.GetDocumentById
{
    public class GetDocumentByIdResponse
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public DateTime UploadDate { get; set; }

        public GetDocumentByIdResponse(Document document)
        {
            Id = document.Id;
            IdUser = document.IdUser;
            FileName = document.FileName;
            FileType = document.FileType;
            FileSize = document.FileSize;
            UploadDate = document.UploadDate;
        }
    }
}
