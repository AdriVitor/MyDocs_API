namespace MyDocs.Features.Alerts.GetAlertById
{
    public class GetAlertByIdResponse
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RecurrenceOfSending { get; set; }
        public DateTime CreationDate { get; set; }
        public string Configuration { get; set; }
    }
}
