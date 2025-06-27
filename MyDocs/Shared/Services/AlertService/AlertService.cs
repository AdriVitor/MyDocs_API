using Microsoft.EntityFrameworkCore;
using MyDocs.Infraestructure.Persistence;
using MyDocs.Models;
using MyDocs.Models.Enums;

namespace MyDocs.Shared.Services.AlertService
{
    public class AlertService : IAlertService
    {
        private readonly Context _context;

        public AlertService(Context context)
        {
            _context = context;
        }

        public async Task<Alert> GetAlert(int idAlert, int idUser)
        {
            Alert alert = await _context.Alerts.FirstOrDefaultAsync(a => a.Id == idAlert && a.IdUser == idUser);

            return alert ?? throw new ArgumentNullException("Alerta não encontrado");
        }

        public string ConfigureDateSendOfAlert(AlertRecurrence alertRecurrence, DateTime dateSend)
        {
            switch (alertRecurrence)
            {
                case AlertRecurrence.JustOnce:
                    return dateSend.ToString("o");
                case AlertRecurrence.Week:
                    return $"0 10 * * {dateSend.DayOfWeek}";
                case AlertRecurrence.Month:
                    return $"0 10 {dateSend.Day} * *";
                case AlertRecurrence.Year:
                    return $"0 10 {dateSend.DayOfWeek} {dateSend.Month} *";
                default:
                    throw new ArgumentException("Seleciona uma recorrência para o alerta");
            }
        }
    }
}
