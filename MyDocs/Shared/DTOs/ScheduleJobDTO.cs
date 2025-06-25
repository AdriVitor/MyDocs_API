namespace MyDocs.Shared.DTOs
{
    public class ScheduleJobDTO
    {
        public string Cron { get; set; }
        public string Queue { get; set; }

        public ScheduleJobDTO()
        {

        }

        public ScheduleJobDTO(string cron, string queue)
        {
            Cron = cron;
            Queue = queue;
        }
    }
}
