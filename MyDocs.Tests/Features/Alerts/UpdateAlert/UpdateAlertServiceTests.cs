using Moq;
using MyDocs.Features.Alerts.UpdateAlert;
using MyDocs.Models;
using MyDocs.Models.Enums;
using MyDocs.Shared.Services.AlertService;
using MyDocs.Tests.Shared;

namespace MyDocs.Tests.Features.Alerts.UpdateAlert
{
    public class UpdateAlertServiceTests
    {
        [Fact]
        public async Task UpdateAlert_ShouldUpdateAlertSuccessfully()
        {
            var alertId = 10;
            var userId = 10;
            var existingAlert = new Alert
            {
                Id = alertId,
                Name = "Original",
                Description = "Desc original",
                RecurrenceOfSending = 1,
                CreationDate = DateTime.Now.AddDays(-10),
                IdUser = userId
            };

            using var context = MemoryDatabase.Create();
            context.Alerts.Add(existingAlert);
            context.SaveChanges();

            var updatedRequest = new UpdateAlertRequest
            {
                IdAlert = alertId,
                IdUser = userId,
                Name = "Atualizado",
                Description = "Nova descrição",
                RecurrenceOfSending = AlertRecurrence.Month,
                EndDate = DateTime.Now.AddDays(1)
            };

            var alerts = new List<Alert> { existingAlert };

            var alertServiceMock = new Mock<IAlertService>();
            alertServiceMock.Setup(x => x.GetAlert(alertId, userId))
                .ReturnsAsync(existingAlert);

            var service = new UpdateAlertService(context, alertServiceMock.Object);

            await service.UpdateAlert(updatedRequest);

            Assert.Equal("Atualizado", existingAlert.Name);
            Assert.Equal("Nova descrição", existingAlert.Description);
            Assert.Equal(2, existingAlert.RecurrenceOfSending);
            Assert.NotNull(existingAlert.EndDate);
        }

        [Fact]
        public async Task UpdateAlert_ShouldThrowArgumentNullException_WhenAlertNotFound()
        {
            var alertId = 99;
            var userId = 99;
            var request = new UpdateAlertRequest
            {
                IdAlert = alertId,
                IdUser = userId,
                Name = "Inexistente",
                Description = "Desc",
                RecurrenceOfSending = AlertRecurrence.JustOnce,
                EndDate = DateTime.Now.AddDays(1)
            };

            var alerts = new List<Alert>();

            using var context = MemoryDatabase.Create();
            var alertServiceMock = new Mock<IAlertService>();
            alertServiceMock.Setup(x => x.GetAlert(alertId, userId))
                .ThrowsAsync(new ArgumentNullException("Alerta não encontrado")); ;

            var service = new UpdateAlertService(context, alertServiceMock.Object);

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateAlert(request));
        }

    }
}
