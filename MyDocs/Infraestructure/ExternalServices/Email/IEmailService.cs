namespace MyDocs.Infraestructure.ExternalServices.Email
{
    public interface IEmailService
    {
        public Task SendEmail(string to, string subject, string body);
    }
}
