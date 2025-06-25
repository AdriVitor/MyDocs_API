using FastEndpoints;

namespace MyDocs.Features.Alerts.UpdateAlert
{
    public class UpdateAlertEndpoint : Endpoint<UpdateAlertRequest>
    {
        private readonly IUpdateAlertService _service;
        public UpdateAlertEndpoint(IUpdateAlertService service)
        {
            _service = service;
        }
        public override void Configure()
        {
            Patch("Alert/Update");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateAlertRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _service.UpdateAlert(request);

                await SendAsync(new { Message = "Alerta atualizado com sucesso" });
            }
            catch (Exception ex)
            {
                await SendAsync(new { Message = ex.Message });
            }
        }
    }
}
