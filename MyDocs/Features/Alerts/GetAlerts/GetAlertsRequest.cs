using MyDocs.Shared.Abstracts.Validators;
using MyDocs.Shared.Validators;

namespace MyDocs.Features.Alerts.GetAlerts
{
    public class GetAlertsRequest : ValidatorBaseAbstract
    {
        public int IdUser { get; set; }
        public StatusAlert Status { get; set; }

        public override void ValidateProperties()
        {
            ExceptionValidator.When(IdUser < 1, "Informe um IdUser válido");
            ExceptionValidator.When(!Enum.IsDefined(typeof(StatusAlert), Status), "Status do alerta inválido");
        }
    }

    public enum StatusAlert
    {
        Expired = 1,
        NotExpired = 2
    }
}
