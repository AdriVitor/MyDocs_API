namespace MyDocs.Shared.DTOs
{
    //public class ScheduleJobDTO
    //{
    //    public string Cron { get; private set; }
    //    public string Queue { get; private set; }

    //    public ScheduleJobDTO(string cron, string queue)
    //    {
    //        Cron = cron;
    //        Queue = queue;
    //    }
    //}

    public record ScheduleJobDTO(string Cron, string Queue);
}
