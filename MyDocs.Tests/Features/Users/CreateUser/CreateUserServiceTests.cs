using Moq;
using MyDocs.Features.Users.Create;
using MyDocs.Infraestructure.ExternalServices.Email;
using MyDocs.Models.Enums;
using MyDocs.Models;
using MyDocs.Tests.Shared;
using MyDocs.Shared.Services.EmailTemplateService;

namespace MyDocs.Tests.Features.Users.CreateUser
{
    public class CreateUserServiceTests
    {
        [Fact]
        public async Task AddUser_ShouldCreateUserAndCredentialsAndSendEmail_WhenRequestIsValid()
        {
            using var context = MemoryDatabase.Create();
            var request = new CreateUserRequest
            {
                Name = "Test Create User",
                CPF = "83590820063",
                DateOfBirth = new DateTime(1995, 1, 1),
                Phone = "11988991166",
                Email = "testUser@email.com",
                Password = "password123"
            };

            string subject = "Criação de conta MyDocs";
            string template = $"<h1> Ola {request.Name}, Parabéns pela sua criação de conta no MyDocs </h1>";

            var emailServiceMock = new Mock<IEmailService>();
            var emailTemplateServiceMock = new Mock<IEmailTemplateService>();
            emailTemplateServiceMock.Setup(x => x.FindEmailTemplate(EmailTemplateEnum.Welcome))
                        .ReturnsAsync(new EmailTemplate
                        {
                            Name = "Welcome",
                            Subject = subject,
                            Body = "<h1> Ola {userName}, Parabéns pela sua criação de conta no MyDocs </h1>"
                        });

            emailTemplateServiceMock.Setup(x => x.ReplaceEmailTemplateWelcome(request.Name, It.IsAny<EmailTemplate>()))
                                    .Returns(template);


            var service = new CreateUserService(context, emailServiceMock.Object, emailTemplateServiceMock.Object);


            emailServiceMock.Setup(x => x.SendEmail(request.Email, subject, template));

            await service.AddUser(request);

            var user = context.Users.FirstOrDefault(u => u.CPF == request.CPF);
            Assert.NotNull(user);
            Assert.Equal(request.Name, user.Name);
            Assert.Equal(request.CPF, user.CPF);

            var credentials = context.UsersCredentials.FirstOrDefault(c => c.IdUser == user.Id);
            Assert.NotNull(credentials);
            Assert.Equal(request.Email, credentials.Email);
            Assert.Equal(request.Password, credentials.Password);
            Assert.Equal(user.Id, credentials.IdUser);

            emailServiceMock.Verify(e => e.SendEmail(
                request.Email,
                subject,
                template), Times.Once);
        }

    }
}
