using Hangfire;
using System.Linq.Expressions;

namespace MyDocs.Infraestructure.ExternalServices.Hangfire
{
    public class HangfireService : IScheduleJob
    {
        public string AddRecurringJob(Expression<Action> methodCall, string cron, string queue = "default")
        {
            try
            {
                string jobId = BuildJobId();
                RecurringJob.AddOrUpdate(jobId, methodCall, cron, TimeZoneInfo.Local, queue);

                return jobId;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void AddScheduleJob(Expression<Action> methodCall, DateTime dateToSend)
        {
            BackgroundJob.Schedule(methodCall, dateToSend);
        }
        

        public void DeleteRecurringJob(string jobId)
        {
            RecurringJob.RemoveIfExists(jobId);
        }

        public string UpdateRecurringJob(string jobId, Expression<Action> methodCall, string cron, string queue = "default")
        {
            DeleteRecurringJob(jobId);

            string? newJobId = AddRecurringJob(methodCall, cron, queue);

            return newJobId;
        }

        private string BuildJobId()
        {
            return string.Concat(DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss"), Guid.NewGuid());
        }
    }
}
