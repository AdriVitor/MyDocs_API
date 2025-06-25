using Moq;
using MyDocs.Features.Alerts.CreateAlert;
using MyDocs.Infraestructure.Services.ScheduleAlertService;
using MyDocs.Models.Enums;
using MyDocs.Models;
using MyDocs.Tests.Shared;
using MyDocs.Shared.DTOs;
using MyDocs.Shared.Services.UserService;
using MyDocs.Shared.Services.EmailTemplateService;

namespace MyDocs.Tests.Features.Alerts.CreateAlert
{
    public class CreateAlertServiceTests
    {
        [Fact]
        public async Task AddAlert_ShouldCreateAlertSuccessfully()
        {
            // Arrange
            var userId = 1;
            var email = "usuario@email.com";
            var userName = "Usuário Teste";
            var request = new CreateAlertRequest
            {
                IdUser = userId,
                Name = "Alerta de Fatura",
                Description = "Alerta mensal para pagamento",
                RecurrenceOfSending = AlertRecurrence.Month,
                FirstDaySend = DateTime.Now.AddDays(1),
            };

            var expectedDate = DateTime.Now;

            using var context = MemoryDatabase.Create();

            context.UsersCredentials.Add(new UserCredentials
            {
                IdUser = userId,
                Email = email,
                Password = "Teste123"
            });

            context.Users.Add(new User
            {
                Id = userId,
                Name = userName,
                CPF = "03699955560",
                Phone = "61998989898"
            });

            context.SaveChanges();

            var processAlertServiceMock = new Mock<IScheduleAlertService>();
            var emailTemplateServiceMock = new Mock<IEmailTemplateService>();
            var userServiceMock = new Mock<IUserService>();

            emailTemplateServiceMock.Setup(x => x.FindEmailTemplate(EmailTemplateEnum.OverdueBill))
                                    .ReturnsAsync(new EmailTemplate
                                    {
                                        Subject = "Alerta de Fatura Vencida",
                                        Body = "Olá {name}, seu alerta {alertName} venceu!"
                                    });

            emailTemplateServiceMock.Setup(x => x.ReplaceEmailTemplateOverdueBill(userName, request.Name, It.IsAny<EmailTemplate>()))
                                    .Returns($"Olá {userName}, seu alerta {request.Name} venceu!");

            userServiceMock.Setup(x => x.GetUser(userId))
                           .ReturnsAsync(new User
                           {
                               Id = userId,
                               Name = userName
                           });

            var service = new CreateAlertService(
                context,
                processAlertServiceMock.Object,
                emailTemplateServiceMock.Object,
                userServiceMock.Object);

            // Act
            await service.AddAlert(request);

            // Assert
            var createdAlert = context.Alerts.FirstOrDefault(a => a.IdUser == userId);
            Assert.NotNull(createdAlert);
            Assert.Equal(request.Name, createdAlert.Name);
            Assert.Equal(request.Description, createdAlert.Description);
            Assert.Equal((int)request.RecurrenceOfSending, createdAlert.RecurrenceOfSending);
            Assert.Equal(userId, createdAlert.IdUser);
            Assert.Equal(expectedDate.Date, createdAlert.CreationDate.Date);

            processAlertServiceMock.Verify(p =>
                p.ScheduleAlert(It.IsAny<ScheduleJobDTO>(), It.Is<EmailRequestDTO>(e =>
                    e.To == email &&
                    e.Subject == "Alerta de Fatura Vencida" &&
                    e.Body.Contains(userName) &&
                    e.Body.Contains(request.Name)
                )),
                Times.Once);

            userServiceMock.Verify(x => x.GetUser(userId), Times.Once);
            emailTemplateServiceMock.Verify(x => x.FindEmailTemplate(EmailTemplateEnum.OverdueBill), Times.Once);
            emailTemplateServiceMock.Verify(x => x.ReplaceEmailTemplateOverdueBill(userName, request.Name, It.IsAny<EmailTemplate>()), Times.Once);
        }
    }
}
