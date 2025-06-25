namespace MyDocs.Features.Alerts.GetAlertById
{
    public interface IGetAlertByIdService
    {
        public Task<GetAlertByIdResponse> GetById(GetAlertByIdRequest request);
    }
}
