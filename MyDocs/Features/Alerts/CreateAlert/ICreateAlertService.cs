namespace MyDocs.Features.Alerts.CreateAlert
{
    public interface ICreateAlertService
    {
        public Task AddAlert(CreateAlertRequest request);
    }
}
