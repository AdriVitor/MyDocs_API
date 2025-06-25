using MyDocs.Shared.Abstracts.Validators;
using MyDocs.Shared.Validators;

namespace MyDocs.Features.Documents.GetDocumentsByUser
{
    public class GetDocumentsByUserRequest : ValidatorBaseAbstract
    {
        public int IdUser { get; set; }

        public override void ValidateProperties()
        {
            ExceptionValidator.When(IdUser < 1, "Informe um IdUser válido");
        }
    }
}
