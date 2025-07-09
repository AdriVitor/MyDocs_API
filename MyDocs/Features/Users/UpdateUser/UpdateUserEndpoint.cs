using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace MyDocs.Features.Users.UpdateUser
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateUserEndpoint : Endpoint<UpdateUserRequest>
    {
        private readonly IUpdateUserService _service;
        public UpdateUserEndpoint(IUpdateUserService service)
        {
            _service = service;
        }
        public override void Configure()
        {
            Patch("User/Update");
            AuthSchemes("Bearer");
        }

        public override async Task HandleAsync(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _service.Update(request);

                await SendAsync(new { Message = "Usuário Atualizado com Sucesso" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
