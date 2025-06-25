using Microsoft.EntityFrameworkCore;
using MyDocs.Infraestructure.Persistence;
using MyDocs.Models;
using MyDocs.Models.Enums;

namespace MyDocs.Shared.Services.EmailTemplateService
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly Context _context;
        public EmailTemplateService(Context context)
        {
            _context = context;
        }

        public async Task<EmailTemplate> FindEmailTemplate(EmailTemplateEnum emailTemplateEnum)
        {
            EmailTemplate emailTemplate = await _context.EmailTemplates.FirstOrDefaultAsync(et => et.Id == (int)emailTemplateEnum);

            return emailTemplate ?? throw new ArgumentNullException("Template não localizado");
        }

        public string ReplaceEmailTemplateWelcome(string userName, EmailTemplate emailTemplate)
        {
            ThrowIfExistsNull(userName, emailTemplate);

            return emailTemplate.Body.Replace("#USER_NAME#", userName);
        }

        public string ReplaceEmailTemplateOverdueBill(string userName, string alertName, EmailTemplate emailTemplate)
        {
            ThrowIfExistsNull(userName, emailTemplate);

            return emailTemplate.Body.Replace("#USER_NAME#", userName)
                                     .Replace("#ALERT_NAME#", alertName);
        }

        private void ThrowIfExistsNull(params object[] values)
        {
            if(values.Any(v => v is null))
                throw new Exception("Precisamos de todos os dados do usuário para realizar o envio do template");
        }
    }
}
