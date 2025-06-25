using MyDocs.Models;

namespace MyDocs.Shared.Services.AlertService
{
    public interface IAlertService
    {
        public Task<Alert> GetAlert(int alertId, int idUser);
    }
}
