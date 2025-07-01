using FastEndpoints;

namespace MyDocs.Features.Documents.DeleteDocument
{
    public class DeleteDocumentEndpoint : Endpoint<DeleteDocumentRequest>
    {
        private readonly IDeleteDocumentService _service;
        public DeleteDocumentEndpoint(IDeleteDocumentService service)
        {
            _service = service;
        }

        public override void Configure()
        {
            Post("v1/Document/Delete");
            AllowAnonymous();
        }

        public override async Task HandleAsync(DeleteDocumentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _service.DeleteDocument(request);

                await SendAsync(new { Message = "Arquivo excluído com sucesso" });
            }
            catch (Exception ex)
            {
                await SendAsync(new { Message = "Erro ao excluir arquivo" }); ;
            }
        }
    }
}
