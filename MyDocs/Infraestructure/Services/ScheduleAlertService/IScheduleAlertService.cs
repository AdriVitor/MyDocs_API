using MyDocs.Shared.DTOs;

namespace MyDocs.Infraestructure.Services.ScheduleAlertService
{
    public interface IScheduleAlertService
    {
        public Task ScheduleAlert(ScheduleJobDTO scheduleJob, EmailRequestDTO emailRequest);
    }
}
