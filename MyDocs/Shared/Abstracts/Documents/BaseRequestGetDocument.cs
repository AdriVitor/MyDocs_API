using MyDocs.Shared.Abstracts.Validators;
using MyDocs.Shared.Validators;

namespace MyDocs.Shared.Abstracts.Documents
{
    public abstract class BaseRequestGetDocument : ValidatorBaseAbstract
    {
        public int IdUser { get; set; }
        public int IdDocument { get; set; }

        public override void ValidateProperties()
        {
            ExceptionValidator.When(IdUser < 1, "Informe um IdUser válido");
            ExceptionValidator.When(IdDocument < 1, "Informe um IdDocument válido");
        }
    }
}
