using MyDocs.Models.Enums;
using MyDocs.Shared.Abstracts.Alerts;
using MyDocs.Shared.Validators;

namespace MyDocs.Features.Alerts.UpdateAlert
{
    public class UpdateAlertRequest : BaseRequestGetAlert
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AlertRecurrence RecurrenceOfSending { get; set; }
        public DateTime? EndDate { get; set; }

        public override void ValidateProperties()
        {
            base.ValidateProperties();
            ExceptionValidator.When(string.IsNullOrWhiteSpace(Name) || Name?.Length < 3, "Informe um nome válido para o alerta");
            ExceptionValidator.When(!Enum.IsDefined(typeof(AlertRecurrence), RecurrenceOfSending), "Recorrência de envio inválida");
            if (EndDate.HasValue)
                ExceptionValidator.When(EndDate.Value.Date < DateTime.Today, "Data de término não pode ser anterior a hoje");
        }
    }
}
