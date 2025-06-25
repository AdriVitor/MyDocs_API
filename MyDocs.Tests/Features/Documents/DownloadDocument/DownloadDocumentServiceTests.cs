using Microsoft.EntityFrameworkCore;
using Moq;
using MyDocs.Features.Documents.DownloadDocument;
using MyDocs.Infraestructure.ExternalServices.AzureBlob;
using MyDocs.Infraestructure.Persistence;
using MyDocs.Models;
using MyDocs.Shared.Services.DocumentService;
using MyDocs.Tests.Shared;

namespace MyDocs.Tests.Features.Documents.DownloadDocument
{
    public class DownloadDocumentServiceTests
    {
        [Fact]
        public async Task DownloadDocument_ShouldReturnDownloadResponse_WhenFileExists()
        {
            using var context = MemoryDatabase.Create();

            var document = GenerateModelsService.CreateDocument(1, 1, "arquivo.pdf", ".pdf", 1024, "unique-name.pdf");

            var documentServiceMock = new Mock<IDocumentService>();
            documentServiceMock.Setup(x => x.FindDocument(1, 1))
                .ReturnsAsync(document);

            var azureBlobServiceMock = new Mock<IAzureBlobService>();
            azureBlobServiceMock.Setup(x => x.DownloadAsync("unique-name.pdf"))
                .ReturnsAsync(new MemoryStream(new byte[] { 1, 2, 3 }));

            var service = new DownloadDocumentService(documentServiceMock.Object, azureBlobServiceMock.Object);

            var request = new DownloadDocumentRequest() { IdUser = 1, IdDocument = 1 };
            var result = await service.DownloadDocument(request);

            Assert.NotNull(result);
            Assert.Equal("arquivo.pdf", result.FileName);
            Assert.Equal(1024, result.FileSize);
        }

        [Fact]
        public async Task DownloadDocument_ShouldThrowException_WhenDocumentDoesNotExist()
        {
            using var context = MemoryDatabase.Create();

            var document = GenerateModelsService.CreateDocument(1, 1, "arquivo.pdf", ".pdf", 1024, "unique-name.pdf");

            var documentServiceMock = new Mock<IDocumentService>();
            documentServiceMock.Setup(x => x.FindDocument(2, 2))
                .ThrowsAsync(new ArgumentNullException("O arquivo não foi localizado"));

            var azureBlobServiceMock = new Mock<IAzureBlobService>();
            azureBlobServiceMock.Setup(x => x.DownloadAsync("doc.pdf"))
                .ReturnsAsync(new MemoryStream(new byte[] { 1 }));

            var request = new DownloadDocumentRequest() { IdUser = 2, IdDocument = 2 };

            var service = new DownloadDocumentService(documentServiceMock.Object, azureBlobServiceMock.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => service.DownloadDocument(request));
        }
    }
}
