using MyDocs.Models.Enums;
using MyDocs.Shared.Abstracts.Validators;
using MyDocs.Shared.Validators;

namespace MyDocs.Features.Alerts.CreateAlert
{
    public class CreateAlertRequest : ValidatorBaseAbstract
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AlertRecurrence RecurrenceOfSending { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime FirstDaySend { get; set; }

        public override void ValidateProperties()
        {
            ExceptionValidator.When(string.IsNullOrWhiteSpace(Name) || Name?.Length < 3, "Informe um nome válido para o alerta");
            ExceptionValidator.When(!Enum.IsDefined(typeof(AlertRecurrence), RecurrenceOfSending), "Recorrência de envio inválida");
            if (EndDate.HasValue)
                ExceptionValidator.When(EndDate.Value.Date < DateTime.Today, "Data de término não pode ser anterior a hoje");

            ExceptionValidator.When(FirstDaySend.Date < DateTime.Today, "Data de envio não pode ser anterior a hoje");
        }
    }
}
