using MyDocs.Models;

namespace MyDocs.Features.Alerts.GetAlerts
{
    //public class GetAlertsResponse
    //{
    //    public List<AlertDTO> Alerts { get; set; }
    //}

    public class GetAlertsResponse
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RecurrenceOfSending { get; set; }
        public DateTime CreationDate { get; set; }

        public GetAlertsResponse(Alert alert)
        {
            Id = alert.Id;
            IdUser = alert.IdUser;
            Name = alert.Name;
            Description = alert.Description;
            RecurrenceOfSending = alert.RecurrenceOfSending;
            CreationDate = alert.CreationDate;
        }
    }
}
