using Moq;
using MyDocs.Features.Alerts.DeleteAlert;
using MyDocs.Infraestructure.ExternalServices.Hangfire;
using MyDocs.Models;
using MyDocs.Shared.Services.AlertService;
using MyDocs.Tests.Shared;

namespace MyDocs.Tests.Features.Alerts.DeleteAlert
{
    public class DeleteAlertServiceTests
    {
        [Fact]
        public async Task DeleteAlert_ShouldThrowArgumentNullException_WhenAlertIsNull()
        {
            using var context = MemoryDatabase.Create();

            var alertServiceMock = new Mock<IAlertService>();
            var alertId = 99;
            var userId = 99;

            alertServiceMock.Setup(x => x.GetAlert(alertId, userId))
                .ThrowsAsync(new ArgumentNullException("Alerta não encontrado"));

            var scheduleJob = new Mock<IScheduleJob>();

            var service = new DeleteAlertService(context, alertServiceMock.Object, scheduleJob.Object);

            var request = new DeleteAlertRequest
            {
                IdAlert = alertId,
                IdUser = userId,
            };

            await Assert.ThrowsAsync<ArgumentNullException>(() => service.DeleteAlert(request));

            scheduleJob.Verify(x => x.DeleteRecurringJob(string.Concat(DateTime.Now, Guid.NewGuid())), Times.Never());
        }

        [Fact]
        public async Task DeleteAlert_ShouldSetEndDate_WhenAlertExists()
        {
            using var context = MemoryDatabase.Create();

            var existingAlert = GenerateModelsService.CreateAlert(1, 10, "Alerta de Teste", "Descrição do alerta", 5, DateTime.UtcNow.AddDays(-3));

            context.Alerts.Add(existingAlert);
            await context.SaveChangesAsync();

            var alertServiceMock = new Mock<IAlertService>();

            var scheduleJob = new Mock<IScheduleJob>();
            scheduleJob.Setup(x => x.DeleteRecurringJob(existingAlert.JobId));

            alertServiceMock.Setup(x => x.GetAlert(1, 10))
                .ReturnsAsync(existingAlert);

            var service = new DeleteAlertService(context, alertServiceMock.Object, scheduleJob.Object);

            var request = new DeleteAlertRequest
            {
                IdAlert = 1,
                IdUser = 10
            };

            await service.DeleteAlert(request);

            var updatedAlert = await context.Alerts.FindAsync(1);
            Assert.NotNull(updatedAlert.EndDate);
            Assert.True(updatedAlert.EndDate.Value.Date <= DateTime.UtcNow.Date);

            scheduleJob.Verify(x => x.DeleteRecurringJob(existingAlert.JobId), Times.Once());
        }
    }
}
