using MyDocs.Infraestructure.ExternalServices.Email;
using MyDocs.Infraestructure.ExternalServices.Hangfire;
using MyDocs.Infraestructure.Services.ScheduleAlertService;
using MyDocs.Shared.DTOs;

namespace MyDocs.Infraestructure.Services.ProcessAlerts
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

        public async Task ScheduleAlert(ScheduleJobDTO scheduleJob, EmailRequestDTO emailRequest)
        {
            _scheduleJob.ScheduleRecurringJob(
                () =>
                 _emailService.SendEmail(emailRequest.To, emailRequest.Subject, emailRequest.Body)
                , scheduleJob.Cron
                , scheduleJob.Queue);
        }
    }
}
