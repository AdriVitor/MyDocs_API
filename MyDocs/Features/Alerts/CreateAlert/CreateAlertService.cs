using Hangfire.Storage.Monitoring;
using Microsoft.EntityFrameworkCore;
using MyDocs.Infraestructure.Persistence;
using MyDocs.Models;
using MyDocs.Models.Enums;
using MyDocs.Shared.DTOs;
using MyDocs.Shared.Services.AlertService;
using MyDocs.Shared.Services.EmailTemplateService;
using MyDocs.Shared.Services.ScheduleAlertService;
using MyDocs.Shared.Services.UserService;
using Xunit;

namespace MyDocs.Features.Alerts.CreateAlert
{
    public class CreateAlertService : ICreateAlertService
    {
        private readonly Context _context;
        private readonly IScheduleAlertService _processAlertService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IUserService _userService;
        private readonly IAlertService _alertService;
        public CreateAlertService(Context context, IScheduleAlertService processAlertService, IEmailTemplateService emailTemplateService, IUserService userService, IAlertService alertService)
        {
            _context = context;
            _processAlertService = processAlertService;
            _emailTemplateService = emailTemplateService;
            _userService = userService;
            _alertService = alertService;
        }

        public async Task AddAlert(CreateAlertRequest request)
        {
            try
            {
                request.ValidateProperties();

                Alert alert = BuildAlertEntity(request);
                _context.Alerts.Add(alert);

                string dateSendingAlert = _alertService.ConfigureDateSendOfAlert(request.RecurrenceOfSending, alert.FirstDaySend);
                ScheduleJobDTO scheduleJobDTO = new ScheduleJobDTO(dateSendingAlert, "alerts");

                EmailRequestDTO emailRequestDTO = await BuildEmailRequest(request.IdUser, alert?.Name);

                if (dateSendingAlert is not null && emailRequestDTO is not null)
                {
                    await ValidateAndScheduleAlertJustOnce(request.RecurrenceOfSending, emailRequestDTO, request.FirstDaySend);

                    string jobId = await ScheduleAndValidateRecurringAlert(scheduleJobDTO, emailRequestDTO);

                    alert.JobId = jobId;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task ValidateAndScheduleAlertJustOnce(AlertRecurrence recurrenceOfSending, EmailRequestDTO emailRequestDTO, DateTime dateSend)
        {
            if (recurrenceOfSending is AlertRecurrence.JustOnce)
                await _processAlertService.ScheduleAlert(emailRequestDTO, dateSend);
        }

        private async Task<string> ScheduleAndValidateRecurringAlert(ScheduleJobDTO scheduleJobDTO, EmailRequestDTO emailRequestDTO)
        {
            return await _processAlertService.ScheduleRecurringAlert(scheduleJobDTO, emailRequestDTO);;
        }

        private async Task<EmailRequestDTO> BuildEmailRequest(int idUser, string alertName)
        {
            string emailUser = await FindEmailUser(idUser);
            var emailTemplate = await _emailTemplateService.FindEmailTemplate(EmailTemplateEnum.OverdueBill);
            var user = await _userService.GetUser(idUser);
            EmailRequestDTO emailRequestDTO = new EmailRequestDTO(emailUser, emailTemplate.Subject, _emailTemplateService.ReplaceEmailTemplateOverdueBill(user?.Name, alertName, emailTemplate));

            return emailRequestDTO;
        }

        private async Task<string> FindEmailUser(int idUser)
        {
            var userCredential =  await _context
                                    .UsersCredentials
                                    .FirstOrDefaultAsync(uc => uc.IdUser == idUser);

            return userCredential?.Email;
        }

        private Alert BuildAlertEntity(CreateAlertRequest request)
        {
            Alert alert = new Alert()
            {
                IdUser = request.IdUser,
                Name = request.Name,
                Description = request.Description,
                RecurrenceOfSending = (int)request.RecurrenceOfSending,
                CreationDate = DateTime.Now,
                EndDate = request.EndDate,
                FirstDaySend = request.FirstDaySend,
            };

            return alert;
        }
    }
}
