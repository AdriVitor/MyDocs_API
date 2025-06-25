using MyDocs.Models;
using MyDocs.Models.Enums;

namespace MyDocs.Shared.Services.EmailTemplateService
{
    public interface IEmailTemplateService
    {
        public Task<EmailTemplate> FindEmailTemplate(EmailTemplateEnum emailTemplateEnum);
        public string ReplaceEmailTemplateWelcome(string userName, EmailTemplate emailTemplate);
        public string ReplaceEmailTemplateOverdueBill(string userName, string alertName, EmailTemplate emailTemplate);
    }
}
