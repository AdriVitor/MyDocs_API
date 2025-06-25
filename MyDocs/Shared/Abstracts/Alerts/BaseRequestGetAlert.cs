using MyDocs.Shared.Abstracts.Validators;
using MyDocs.Shared.Validators;

namespace MyDocs.Shared.Abstracts.Alerts
{
    public abstract class BaseRequestGetAlert : ValidatorBaseAbstract
    {
        public int IdAlert { get; set; }
        public int IdUser { get; set; }

        public override void ValidateProperties()
        {
            ExceptionValidator.When(IdAlert < 1, "Informe um IdAlert válido");
            ExceptionValidator.When(IdUser < 1, "Informe um IdUser válido");
        }
    }
}
