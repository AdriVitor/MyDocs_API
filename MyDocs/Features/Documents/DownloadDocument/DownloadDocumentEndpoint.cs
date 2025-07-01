using FastEndpoints;

namespace MyDocs.Features.Documents.DownloadDocument
{
    public class DownloadDocumentEndpoint : Endpoint<DownloadDocumentRequest>
    {
        private readonly IDownloadDocumentService _service;
        public DownloadDocumentEndpoint(IDownloadDocumentService service)
        {
            _service = service;
        }

        public override void Configure()
        {
            Post("v1/Document/Download");
            AllowAnonymous();
        }

        public override async Task HandleAsync(DownloadDocumentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                DownloadDocumentResponse response = await _service.DownloadDocument(request);
                //var file = File(stream, "application/octet-stream", "");
                await SendStreamAsync(
                        response.Stream,
                        response.FileName,
                        response.FileSize,
                         "application/octet-stream"
                         );
            }
            catch (Exception ex)
            {
                await SendAsync(new { Message = ex.Message });
            }
        }
    }
}
