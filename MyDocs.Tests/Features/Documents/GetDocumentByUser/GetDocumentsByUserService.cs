using Microsoft.EntityFrameworkCore;
using Moq;
using MyDocs.Features.Documents.GetDocumentsByUser;
using MyDocs.Infraestructure.Persistence;
using MyDocs.Models;
using MyDocs.Tests.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocs.Tests.Features.Documents.GetDocumentByUser
{
    public class GetDocumentsByUserServiceTests
    {
        [Fact]
        public async Task GetDocumentsByUser_ReturnsCorrectDocuments()
        {
            var userId = 2;
            var fakeDocuments = new List<Document>
            {
                GenerateModelsService.CreateDocument(5, userId, "arquivo1.pdf", ".pdf", 1024, string.Concat(5, "-", "arquivo1.pdf", "-", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"))),
                GenerateModelsService.CreateDocument(6, userId, "arquivo2.pdf", ".pdf", 1024, string.Concat(6, "-", "arquivo2.pdf", "-", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"))),
            };
            using var context = MemoryDatabase.Create();
            context.Documents.AddRange(fakeDocuments);
            context.SaveChanges();

            var service = new GetDocumentsByUserService(context);

            var request = new GetDocumentsByUserRequest { IdUser = userId };

            var result = await service.GetDocumentsByUser(request);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, r => Assert.Equal(userId, r.IdUser));
        }
    }
}
