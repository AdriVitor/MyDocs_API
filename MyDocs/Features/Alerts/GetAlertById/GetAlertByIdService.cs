using Microsoft.EntityFrameworkCore;
using MyDocs.Infraestructure.Persistence;
using MyDocs.Models;
using MyDocs.Shared.Services.AlertService;

namespace MyDocs.Features.Alerts.GetAlertById
{
    public class GetAlertByIdService : IGetAlertByIdService
    {
        private readonly IAlertService _alertService;
        public GetAlertByIdService(IAlertService alertService)
        {
            _alertService = alertService;
        }

        public async Task<GetAlertByIdResponse> GetById(GetAlertByIdRequest request)
        {
            request.ValidateProperties();

            Alert alert = await _alertService.GetAlert(request.IdAlert, request.IdUser);

            GetAlertByIdResponse response = new GetAlertByIdResponse()
            {
                Id = alert.Id,
                Name = alert.Name,
                Description = alert.Description,
                CreationDate = alert.CreationDate,
                RecurrenceOfSending = alert.RecurrenceOfSending
            };

            return response;
        }
    }
}
