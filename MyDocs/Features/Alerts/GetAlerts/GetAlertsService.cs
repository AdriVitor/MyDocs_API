using Microsoft.EntityFrameworkCore;
using MyDocs.Infraestructure.Persistence;

namespace MyDocs.Features.Alerts.GetAlerts
{
    public class GetAlertsService : IGetAlertsService
    {
        private readonly Context _context;

        public GetAlertsService(Context context)
        {
            _context = context;
        }

        public async Task<List<GetAlertsResponse>> GetAlerts(GetAlertsRequest request)
        {
            DateTime? endDate = request.Status is StatusAlert.Expired ? DateTime.Now : null;

            var alerts = await _context.Alerts.Where(a => a.IdUser == request.IdUser && (endDate == null ? a.EndDate == endDate : a.EndDate <= endDate)).ToListAsync();

            var response = alerts.ConvertAll(a => new GetAlertsResponse(a));

            return response;
        }
    }
}
