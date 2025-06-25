using FastEndpoints;

namespace MyDocs.Features.Documents.GetDocumentsByUser
{
    public class GetDocumentsByUserEndpoint : Endpoint<GetDocumentsByUserRequest, List<GetDocumentsByUserResponse>>
    {
        private readonly IGetDocumentsByUserService _service;
        public GetDocumentsByUserEndpoint(IGetDocumentsByUserService service)
        {
            _service = service;
        }

        public override void Configure()
        {
            Post("Documents");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetDocumentsByUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var documents = await _service.GetDocumentsByUser(request);

                await SendAsync(documents);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
