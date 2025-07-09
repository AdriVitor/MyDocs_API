using FastEndpoints;

namespace MyDocs.Features.Alerts.DeleteAlert
{
    public class DeleteAlertEndpoint : Endpoint<DeleteAlertRequest>
    {
        private readonly IDeleteAlertService _service;
        public DeleteAlertEndpoint(IDeleteAlertService service)
        {
            _service = service;
        }
        public override void Configure()
        {
            Patch("Alert/Delete");
            AuthSchemes("Bearer");
        }

        public override async Task HandleAsync(DeleteAlertRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _service.DeleteAlert(request);

                await SendAsync(new { Message = "Alerta deletado com sucesso" });
            }
            catch(Exception ex)
            {
                await SendAsync(new { Message = "Erro ao deletar alerta", Erro = ex.Message });
            }
            
        }
    }
}
