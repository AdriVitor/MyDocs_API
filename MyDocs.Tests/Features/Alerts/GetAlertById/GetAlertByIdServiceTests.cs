using Moq;
using MyDocs.Features.Alerts.GetAlertById;
using MyDocs.Models;
using MyDocs.Shared.Services.AlertService;
using MyDocs.Tests.Shared;

namespace MyDocs.Tests.Features.Alerts.GetAlertById
{
    public class GetAlertByIdServiceTests
    {
        [Fact]
        public async Task GetById_ShouldThrowArgumentNullException_WhenAlertIsNull()
        {
            var alertServiceMock = new Mock<IAlertService>();
            var alertId = 42;
            var userId = 40;

            alertServiceMock.Setup(x => x.GetAlert(alertId, userId))
                .ThrowsAsync(new ArgumentNullException("Alerta não encontrado"));

            var service = new GetAlertByIdService(alertServiceMock.Object);

            var request = new GetAlertByIdRequest
            {
                IdAlert = alertId,
                IdUser = userId
            };

            await Assert.ThrowsAsync<ArgumentNullException>(() => service.GetById(request));
        }

        [Fact]
        public async Task GetById_ShouldReturnAlert_WhenAlertExists()
        {
            var alert = GenerateModelsService.CreateAlert(10, 10, "Alerta de Teste", "Descrição detalhada", 7, new DateTime(2025, 5, 1));

            var alertServiceMock = new Mock<IAlertService>();
            alertServiceMock.Setup(x => x.GetAlert(alert.Id, alert.IdUser))
                .ReturnsAsync(alert);

            var service = new GetAlertByIdService(alertServiceMock.Object);

            var request = new GetAlertByIdRequest
            {
                IdAlert = alert.Id,
                IdUser = alert.IdUser
            };

            var result = await service.GetById(request);

            Assert.Equal(alert.Id, result.Id);
            Assert.Equal(alert.Name, result.Name);
            Assert.Equal(alert.Description, result.Description);
            Assert.Equal(alert.RecurrenceOfSending, result.RecurrenceOfSending);
            Assert.Equal(alert.CreationDate, result.CreationDate);
        }

    }
}
