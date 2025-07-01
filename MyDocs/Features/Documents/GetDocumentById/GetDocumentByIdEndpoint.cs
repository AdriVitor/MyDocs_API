using FastEndpoints;

namespace MyDocs.Features.Documents.GetDocumentById
{
    public class GetDocumentByIdEndpoint : Endpoint<GetDocumentByIdRequest, GetDocumentByIdResponse>
    {
        private readonly IGetDocumentByIdService _service;
        public GetDocumentByIdEndpoint(IGetDocumentByIdService service)
        {
            _service = service;
        }

        public override void Configure()
        {
            Post("v1/Document/Find");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetDocumentByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                GetDocumentByIdResponse document = await _service.GetDocumentById(request);

                await SendAsync(document);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
