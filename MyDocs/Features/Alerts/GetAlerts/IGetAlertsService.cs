namespace MyDocs.Features.Alerts.GetAlerts
{
    public interface IGetAlertsService
    {
        public Task<List<GetAlertsResponse>> GetAlerts(GetAlertsRequest request);
    }
}
