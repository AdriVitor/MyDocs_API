using MyDocs.Models;
using MyDocs.Models.Enums;

namespace MyDocs.Shared.Services.AlertService
{
    public interface IAlertService
    {
        public Task<Alert> GetAlert(int alertId, int idUser);
        public string ConfigureDateSendOfAlert(AlertRecurrence alertRecurrence, DateTime dateSend);
    }
}
