using Moq;
using MyDocs.Features.Alerts.UpdateAlert;
using MyDocs.Models;
using MyDocs.Models.Enums;
using MyDocs.Shared.DTOs;
using MyDocs.Shared.Services.AlertService;
using MyDocs.Shared.Services.EmailTemplateService;
using MyDocs.Shared.Services.ScheduleAlertService;
using MyDocs.Shared.Services.UserCredential;
using MyDocs.Shared.Services.UserService;
using MyDocs.Tests.Shared;

namespace MyDocs.Tests.Features.Alerts.UpdateAlert
{
    public class UpdateAlertServiceTests
    {
        [Fact]
        public async Task UpdateAlert_ShouldUpdateAlertSuccessfully()
        {
            // Arrange
            var alertId = 10;
            var userId = 10;
            var oldName = "Original";
            var oldDescription = "Desc original";
            var oldRecurrence = 1;
            var oldEndDate = DateTime.UtcNow.AddDays(-10);

            var existingAlert = GenerateModelsService.CreateAlert(alertId, userId, oldName, oldDescription, oldRecurrence, oldEndDate);

            using var context = MemoryDatabase.Create();
            context.Alerts.Add(existingAlert);
            context.SaveChanges();

            var newName = "Atualizado";
            var newDescription = "Nova descrição";
            var newRecurrence = AlertRecurrence.Month;
            var newEndDate = DateTime.UtcNow.AddDays(1);

            var request = new UpdateAlertRequest
            {
                IdAlert = alertId,
                IdUser = userId,
                Name = newName,
                Description = newDescription,
                RecurrenceOfSending = newRecurrence,
                EndDate = newEndDate
            };

            // Mocks
            var alertServiceMock = new Mock<IAlertService>();
            alertServiceMock.Setup(x => x.GetAlert(alertId, userId))
                .ReturnsAsync(existingAlert);
            alertServiceMock.Setup(x => x.ConfigureDateSendOfAlert(newRecurrence, It.IsAny<DateTime>()))
                .Returns("0 0 1 * *"); // cron de exemplo

            var userCredentialServiceMock = new Mock<IUserCredentialService>();
            userCredentialServiceMock.Setup(x => x.FindUserCredential(userId))
                .ReturnsAsync(new UserCredentials { Email = "user@email.com" });

            var emailTemplate = new EmailTemplate
            {
                Subject = "Assunto",
                Body = "Conteúdo com {NOME} e {ALERTA}"
            };

            var emailTemplateServiceMock = new Mock<IEmailTemplateService>();
            emailTemplateServiceMock.Setup(x => x.FindEmailTemplate(EmailTemplateEnum.OverdueBill))
                .ReturnsAsync(emailTemplate);
            emailTemplateServiceMock.Setup(x => x.ReplaceEmailTemplateOverdueBill(It.IsAny<string>(), It.IsAny<string>(), emailTemplate))
                .Returns("Email final formatado");

            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.GetUser(userId))
                .ReturnsAsync(new User { Name = "Usuário Teste" });

            var scheduleAlertServiceMock = new Mock<IScheduleAlertService>();
            scheduleAlertServiceMock.Setup(x => x.UpdateAlert(It.IsAny<string>(), It.IsAny<ScheduleJobDTO>(), It.IsAny<EmailRequestDTO>()))
                .Returns(Task.FromResult(""));

            var service = new UpdateAlertService(
                context,
                alertServiceMock.Object,
                scheduleAlertServiceMock.Object,
                userServiceMock.Object,
                userCredentialServiceMock.Object,
                emailTemplateServiceMock.Object
            );

            // Act
            await service.UpdateAlert(request);

            // Assert
            Assert.Equal(newName, existingAlert.Name);
            Assert.Equal(newDescription, existingAlert.Description);
            Assert.Equal((int)newRecurrence, existingAlert.RecurrenceOfSending);
            Assert.Equal(newEndDate.Date, existingAlert.EndDate?.Date);
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

            // Criando mocks para as demais dependências
            var scheduleAlertServiceMock = new Mock<IScheduleAlertService>();
            var userServiceMock = new Mock<IUserService>();
            var userCredentialServiceMock = new Mock<IUserCredentialService>();
            var emailTemplateServiceMock = new Mock<IEmailTemplateService>();

            // Act
            var service = new UpdateAlertService(
                context,
                alertServiceMock.Object,
                scheduleAlertServiceMock.Object,
                userServiceMock.Object,
                userCredentialServiceMock.Object,
                emailTemplateServiceMock.Object);

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateAlert(request));
        }

    }
}
