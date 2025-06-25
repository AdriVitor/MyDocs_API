using System.Linq.Expressions;

namespace MyDocs.Infraestructure.ExternalServices.Hangfire
{
    public interface IScheduleJob
    {
        void ScheduleRecurringJob(Expression<Action> methodCall, string cron, string queue = "default");
    }
}
