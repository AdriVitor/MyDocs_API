using Microsoft.EntityFrameworkCore;
using MyDocs.Infraestructure.Persistence;
using MyDocs.Models;

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
    }
}
