using FastEndpoints;
using MyDocs.Shared.Abstracts.Validators;
using MyDocs.Shared.Validators;

namespace MyDocs.Features.Documents.UploadDocument
{
    public class UploadDocumentRequest
    {
        [FromForm]
        public FileInfo FileInfo { get; set; }
    }

    public class FileInfo : ValidatorBaseAbstract
    {
        public int IdUser { get; set; }
        public IFormFile File { get; set; }

        public override void ValidateProperties()
        {
            ExceptionValidator.When(IdUser < 1, "Informe um IdUser válido");
            ExceptionValidator.When(File == null, "O arquivo é obrigatório");
            ExceptionValidator.When(File?.Length == 0, "O arquivo não pode estar vazio");
            ExceptionValidator.When(File?.Length > 5 * 1024 * 1024, "O arquivo não pode ter mais de 5MB");
        }
    }
}
