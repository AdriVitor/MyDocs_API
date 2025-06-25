using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyDocs.Features.Alerts.GetAlertById
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAlertByIdEndpoint : Endpoint<GetAlertByIdRequest, GetAlertByIdResponse>
    {
        private readonly IGetAlertByIdService _service;
        public GetAlertByIdEndpoint(IGetAlertByIdService service)
        {
            _service = service;
        }
        public override void Configure()
        {
            Post("Alert");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetAlertByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _service.GetById(request);

                await SendAsync(response);
            }
            catch (Exception ex)
            {
                //await SendAsync(ex.Message);
            }
        }
    }
}
