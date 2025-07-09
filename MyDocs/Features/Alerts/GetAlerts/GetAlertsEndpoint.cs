using FastEndpoints;

namespace MyDocs.Features.Alerts.GetAlerts
{
    public class GetAlertsEndpoint : Endpoint<GetAlertsRequest, List<GetAlertsResponse>>
    {
        private readonly IGetAlertsService _service;
        public GetAlertsEndpoint(IGetAlertsService service)
        {
            _service = service;
        }

        public override void Configure()
        {
            Post("Alert/List");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetAlertsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _service.GetAlerts(request);

                await SendAsync(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
