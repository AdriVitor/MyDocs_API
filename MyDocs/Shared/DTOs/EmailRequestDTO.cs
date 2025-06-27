namespace MyDocs.Shared.DTOs
{
    public record EmailRequestDTO
    {
        public string To { get; init; }
        public string Subject { get; init; }
        public string Body { get; init; }

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
