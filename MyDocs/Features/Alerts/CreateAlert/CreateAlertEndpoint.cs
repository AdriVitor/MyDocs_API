using FastEndpoints;

namespace MyDocs.Features.Alerts.CreateAlert
{
    public class CreateAlertEndpoint : Endpoint<CreateAlertRequest>
    {
        private readonly ICreateAlertService _service;
        public CreateAlertEndpoint(ICreateAlertService service)
        {
            _service = service;
        }
        public override void Configure()
        {
            Post("Alert/Create");
            AuthSchemes("Bearer");
        }

        public override async Task HandleAsync(CreateAlertRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _service.AddAlert(request);

                await SendAsync(new { Message = "Alerta criado com sucesso" });
            }
            catch (Exception ex)
            {
                await SendAsync(new { Message = ex.Message });
            }
        }
    }
}
