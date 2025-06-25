using MyDocs.Infraestructure.Persistence;
using MyDocs.Models;
using MyDocs.Shared.Services.AlertService;

namespace MyDocs.Features.Alerts.DeleteAlert
{
    public class DeleteAlertService : IDeleteAlertService
    {
        private readonly Context _context;
        private readonly IAlertService _alertService;

        public DeleteAlertService(Context context, IAlertService alertService)
        {
            _context = context;
            _alertService = alertService;
        }

        public async Task DeleteAlert(DeleteAlertRequest request)
        {
            request.ValidateProperties();

            Alert alert = await _alertService.GetAlert(request.IdAlert, request.IdUser);

            alert.EndDate = DateTime.Now;

            _context.Alerts.Update(alert);
            await _context.SaveChangesAsync();
        }
    }
}
