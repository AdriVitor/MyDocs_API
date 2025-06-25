namespace MyDocs.Features.Alerts.DeleteAlert
{
    public interface IDeleteAlertService
    {
        public Task DeleteAlert(DeleteAlertRequest request);
    }
}
