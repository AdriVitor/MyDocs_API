using FastEndpoints;
using MyDocs.Features.Users.GetUser;

namespace MyDocs.Features.Users.GetById
{
    public class GetUserEndpoint : Endpoint<GetUserRequest, GetUserResponse>
    {
        private readonly IGetUserService _getUserService;
        public GetUserEndpoint(IGetUserService getUserService)
        {
            _getUserService = getUserService;
        }
        public override void Configure()
        {
            Post("User/Find");
            AuthSchemes("Bearer");
        }

        public override async Task HandleAsync(GetUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await SendAsync(await _getUserService.GetById(request));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
