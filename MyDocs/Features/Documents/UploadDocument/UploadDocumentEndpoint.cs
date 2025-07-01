using FastEndpoints;

namespace MyDocs.Features.Documents.UploadDocument
{
    public class UploadDocumentEndpoint : Endpoint<UploadDocumentRequest>
    {
        private readonly IUploadDocumentService _service;
        public UploadDocumentEndpoint(IUploadDocumentService service)
        {
            _service = service;
        }

        public override void Configure()
        {
            Post("v1/Document/Upload");
            AllowAnonymous();
            AllowFileUploads();
            AllowFormData();
        }

        public override async Task HandleAsync(UploadDocumentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _service.SaveFile(request);

                await SendAsync(new { Message = "Arquivo salvo com sucesso" });
            }
            catch (Exception ex)
            {
                await SendAsync(new { Message = ex.Message });
            }
        }
    }
}
