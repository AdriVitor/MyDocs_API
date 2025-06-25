using MyDocs.Features.Alerts.GetAlerts;
using MyDocs.Models;
using MyDocs.Tests.Shared;

namespace MyDocs.Tests.Features.Alerts.GetAlerts
{
    public class GetAlertsServiceTests
    {
        [Fact]
        public async Task GetAlerts_ShouldReturnOnlyActiveAlerts()
        {
            var userId = 5;
            var alerts = new List<Alert>
            {
                GenerateModelsService.CreateAlert(7, userId, "Ativo 1", "1", 1, DateTime.Now),
                GenerateModelsService.CreateAlert(8, userId, "Ativo 2", "2", 1, DateTime.Now)
            };

            using var context = MemoryDatabase.Create();
            context.Alerts.AddRange(alerts);
            context.SaveChanges();

            var service = new GetAlertsService(context);

            var request = new GetAlertsRequest
            {
                IdUser = userId,
                Status = StatusAlert.NotExpired
            };

            var result = await service.GetAlerts(request);

            Assert.Equal(2, result.Count);
            Assert.True(result.Select(r => r.Name).Contains("Ativo 1"));
            Assert.True(result.Select(r => r.Name).Contains("Ativo 2"));
        }

        [Fact]
        public async Task GetAlerts_ShouldReturnOnlyExpiredAlerts()
        {
            var userId = 1;
            var alerts = new List<Alert>
            {
                GenerateModelsService.CreateAlert(9, userId, "Expirado", "Expirado", 1, DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(-1))
            };

            using var context = MemoryDatabase.Create();
            context.Alerts.AddRange(alerts);
            context.SaveChanges();

            var service = new GetAlertsService(context);

            var request = new GetAlertsRequest
            {
                IdUser = userId,
                Status = StatusAlert.Expired
            };

            var result = await service.GetAlerts(request);

            Assert.Single(result);
            Assert.Equal("Expirado", result[0].Name);
        }

        [Fact]
        public async Task GetAlerts_ShouldReturnEmptyList_WhenNoAlertsFound()
        {
            var userId = 10;
            var alerts = new List<Alert>();

            using var context = MemoryDatabase.Create();
            context.Alerts.AddRange(alerts);
            context.SaveChanges();

            var service = new GetAlertsService(context);

            var request = new GetAlertsRequest
            {
                IdUser = userId,
                Status = StatusAlert.NotExpired
            };

            var result = await service.GetAlerts(request);

            Assert.Empty(result);
        }

    }
}
