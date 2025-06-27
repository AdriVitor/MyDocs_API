using MyDocs.Infraestructure.ExternalServices.Hangfire;
using MyDocs.Infraestructure.Persistence;
using MyDocs.Models;
using MyDocs.Shared.Services.AlertService;

namespace MyDocs.Features.Alerts.DeleteAlert
{
    public class DeleteAlertService : IDeleteAlertService
    {
        private readonly Context _context;
        private readonly IAlertService _alertService;
        private readonly IScheduleJob _scheduleJob;
        public DeleteAlertService(Context context, IAlertService alertService, IScheduleJob scheduleJob)
        {
            _context = context;
            _alertService = alertService;
            _scheduleJob = scheduleJob;
        }

        public async Task DeleteAlert(DeleteAlertRequest request)
        {
            request.ValidateProperties();

            Alert alert = await _alertService.GetAlert(request.IdAlert, request.IdUser);

            alert.EndDate = DateTime.Now;

            _context.Alerts.Update(alert);

            _scheduleJob.DeleteRecurringJob(alert.JobId);

            await _context.SaveChangesAsync();
        }
    }
}
