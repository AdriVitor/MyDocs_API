using System.Linq.Expressions;

namespace MyDocs.Infraestructure.ExternalServices.Hangfire
{
    public interface IScheduleJob
    {
        string AddRecurringJob(Expression<Action> methodCall, string cron, string queue = "default");
        public void AddScheduleJob(Expression<Action> methodCall, DateTime dateToSend);
        public void DeleteRecurringJob(string jobId);
        public string UpdateRecurringJob(string jobId, Expression<Action> methodCall, string cron, string queue = "default");
    }
}
