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
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetUserRequest request, CancellationToken cancellationToken)
        {
            await SendAsync(await _getUserService.GetById(request));
        }
    }
}
