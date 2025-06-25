using MyDocs.Shared.Abstracts.Validators;
using MyDocs.Shared.Validators;

namespace MyDocs.Shared.Abstracts.Users
{
    public abstract class BaseRequestGetUser : ValidatorBaseAbstract
    {
        public int IdUser { get; set; }
        public override void ValidateProperties()
        {
            ExceptionValidator.When(IdUser < 1, "Informe um IdUser válido");
        }
    }
}
