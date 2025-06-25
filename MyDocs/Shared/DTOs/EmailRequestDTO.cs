namespace MyDocs.Shared.DTOs
{
    public class EmailRequestDTO
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public EmailRequestDTO()
        {

        }

        public EmailRequestDTO(string to, string subject, string body)
        {
            if (to is null || subject is null || body is null)
                throw new ArgumentNullException("Não foi possível criar o template de email");

            To = to;
            Subject = subject;
            Body = body; 
        }
    }
}
