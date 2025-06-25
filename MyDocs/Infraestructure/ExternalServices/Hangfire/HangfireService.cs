using Hangfire;
using System.Linq.Expressions;

namespace MyDocs.Infraestructure.ExternalServices.Hangfire
{
    public class HangfireService : IScheduleJob
    {
        public void ScheduleRecurringJob(Expression<Action> methodCall, string cron, string queue = "default")
        {
            RecurringJob.AddOrUpdate(methodCall, cron, TimeZoneInfo.Local, queue);
        }
    }
}
