using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MyDocs.Models;

namespace MyDocs.Features.Documents.GetDocumentsByUser
{
    public class GetDocumentsByUserResponse
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public DateTime UploadDate { get; set; }

        public GetDocumentsByUserResponse(Document document)
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
