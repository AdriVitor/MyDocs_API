using FastEndpoints;
using MyDocs.Features.Documents.DeleteDocument;

namespace MyDocs.Features.Authentication.Login
{
    public class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
    {
        private readonly ILoginService _service;
        public LoginEndpoint(ILoginService service)
        {
            _service = service;
        }

        public override void Configure()
        {
            Post("v1/Authentication");
            AllowAnonymous();
        }

        public override async Task HandleAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            try
            {
                LoginResponse response = await _service.GenerateToken(request);

                await SendAsync(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
