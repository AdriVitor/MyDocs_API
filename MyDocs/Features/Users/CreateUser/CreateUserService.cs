using MyDocs.Features.Users.CreateUser;
using MyDocs.Infraestructure.ExternalServices.Email;
using MyDocs.Infraestructure.Persistence;
using MyDocs.Models;
using MyDocs.Models.Enums;
using MyDocs.Shared.Services.EmailTemplateService;

namespace MyDocs.Features.Users.Create
{
    public class CreateUserService : ICreateUserService
    {
        private readonly Context _context;
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateService _emailTemplateService;
        public CreateUserService(Context context, IEmailService emailService, IEmailTemplateService emailTemplateService)
        {
            _context = context;
            _emailService = emailService;
            _emailTemplateService = emailTemplateService;
        }

        public async Task AddUser(CreateUserRequest request)
        {
            request.ValidateProperties();

            int idUser = await CreateUser(request);

            await CreateUserCredential(request.Email, request.Password, idUser);

            EmailTemplate emailTemplate = await _emailTemplateService.FindEmailTemplate(EmailTemplateEnum.Welcome);

            await _emailService.SendEmail(request.Email, emailTemplate.Subject, _emailTemplateService.ReplaceEmailTemplateWelcome(request.Name, emailTemplate));
        }

        private async Task<int> CreateUser(CreateUserRequest request)
        {
            User user = new User
            {
                Name = request.Name,
                CPF = request.CPF,
                DateOfBirth = request.DateOfBirth,
                Phone = request.Phone,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }

        private async Task CreateUserCredential(string email, string password, int userId)
        {
            UserCredentials userCredentials = new UserCredentials
            {
                Email = email,
                Password = password,
                IdUser = userId,
            };

            _context.UsersCredentials.Add(userCredentials);
            await _context.SaveChangesAsync();
        }
    }
}
