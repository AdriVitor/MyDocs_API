using Microsoft.EntityFrameworkCore;
using MimeKit.Cryptography;
using MyDocs.Infraestructure.Persistence;
using MyDocs.Models;
using MyDocs.Shared.Services.AlertService;

namespace MyDocs.Features.Alerts.UpdateAlert
{
    public class UpdateAlertService : IUpdateAlertService
    {
        private readonly Context _context;
        private readonly IAlertService _alertService;
        public UpdateAlertService(Context context, IAlertService alertService)
        {
            _context = context;
            _alertService = alertService;
        }


        public async Task UpdateAlert(UpdateAlertRequest request)
        {
            request.ValidateProperties();

            Alert alert = await _alertService.GetAlert(request.IdAlert, request.IdUser);

            alert.Name = request.Name;
            alert.Description = request.Description;
            alert.RecurrenceOfSending = (int)request.RecurrenceOfSending;
            alert.EndDate = request.EndDate;

            _context.Alerts.Update(alert);
            await _context.SaveChangesAsync();
        }
    }
}
