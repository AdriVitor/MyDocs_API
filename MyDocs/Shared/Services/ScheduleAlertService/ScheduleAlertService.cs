using MyDocs.Infraestructure.ExternalServices.Email;
using MyDocs.Infraestructure.ExternalServices.Hangfire;
using MyDocs.Shared.DTOs;

namespace MyDocs.Shared.Services.ScheduleAlertService
{
    public class ScheduleAlertService : IScheduleAlertService
    {
        private readonly IScheduleJob _scheduleJob;
        private readonly IEmailService _emailService;
        public ScheduleAlertService(IScheduleJob scheduleJob, IEmailService emailService)
        {
            _scheduleJob = scheduleJob;
            _emailService = emailService;
        }

        public async Task ScheduleAlert(EmailRequestDTO emailRequest, DateTime dateToSend)
        {
            _scheduleJob.AddScheduleJob(() => _emailService.SendEmail(emailRequest.To, emailRequest.Subject, emailRequest.Body), dateToSend);
        }


        public async Task<string?> ScheduleRecurringAlert(ScheduleJobDTO scheduleJob, EmailRequestDTO emailRequest)
        {
            string? jobId = _scheduleJob.AddRecurringJob(
                                        () => _emailService.SendEmail(emailRequest.To, emailRequest.Subject, emailRequest.Body)
                                        , scheduleJob.Cron
                                        , scheduleJob.Queue);

            return jobId;
        }

        public async Task<string?> UpdateAlert(string jobId, ScheduleJobDTO scheduleJob, EmailRequestDTO emailRequest)
        {
            string? newJobId = _scheduleJob.UpdateRecurringJob(
                                        jobId,
                                        () => _emailService.SendEmail(emailRequest.To, emailRequest.Subject, emailRequest.Body)
                                        , scheduleJob.Cron
                                        , scheduleJob.Queue);

            return newJobId;
        }
    }
}
