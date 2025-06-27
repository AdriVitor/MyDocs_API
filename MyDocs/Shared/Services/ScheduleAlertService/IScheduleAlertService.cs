using MyDocs.Shared.DTOs;

namespace MyDocs.Shared.Services.ScheduleAlertService
{
    public interface IScheduleAlertService
    {
        public Task ScheduleAlert(EmailRequestDTO emailRequest, DateTime dateToSend);
        public Task<string?> ScheduleRecurringAlert(ScheduleJobDTO scheduleJob, EmailRequestDTO emailRequest);
        public Task<string?> UpdateAlert(string jobId, ScheduleJobDTO scheduleJob, EmailRequestDTO emailRequest);
    }
}
