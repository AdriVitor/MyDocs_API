using Microsoft.EntityFrameworkCore;
using MyDocs.Infraestructure.Persistence;
using MyDocs.Infraestructure.Services.ScheduleAlertService;
using MyDocs.Models;
using MyDocs.Models.Enums;
using MyDocs.Shared.DTOs;
using MyDocs.Shared.Services.EmailTemplateService;
using MyDocs.Shared.Services.UserService;

namespace MyDocs.Features.Alerts.CreateAlert
{
    public class CreateAlertService : ICreateAlertService
    {
        private readonly Context _context;
        private readonly IScheduleAlertService _processAlertService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IUserService _userService;
        public CreateAlertService(Context context, IScheduleAlertService processAlertService, IEmailTemplateService emailTemplateService, IUserService userService)
        {
            _context = context;
            _processAlertService = processAlertService;
            _emailTemplateService = emailTemplateService;
            _userService = userService;
        }

        public async Task AddAlert(CreateAlertRequest request)
        {
            try
            {
                request.ValidateProperties();

                Alert alert = BuildAlertEntity(request);

                _context.Alerts.Add(alert);

                string dateSendingAlert = await ConfigureDateSendOfAlert(request.RecurrenceOfSending, alert.FirstDaySend);
                ScheduleJobDTO scheduleJobDTO = new ScheduleJobDTO(dateSendingAlert, "alerts");

                string emailUser = await FindEmailUser(request.IdUser);
                var emailTemplate = await _emailTemplateService.FindEmailTemplate(EmailTemplateEnum.OverdueBill);
                var user = await _userService.GetUser(request.IdUser);
                EmailRequestDTO emailRequestDTO = new EmailRequestDTO(emailUser, emailTemplate.Subject, _emailTemplateService.ReplaceEmailTemplateOverdueBill(user?.Name, alert.Name, emailTemplate));

                await ValidateAndScheduleAlert(dateSendingAlert, emailUser, scheduleJobDTO, emailRequestDTO);

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível cadastrar o alerta, tente novamente mais tarde");
            }
        }

        private async Task<string> FindEmailUser(int idUser)
        {
            var userCredential =  await _context
                                    .UsersCredentials
                                    .FirstOrDefaultAsync(uc => uc.IdUser == idUser);

            return userCredential?.Email;
        }

        private async Task<string> ConfigureDateSendOfAlert(AlertRecurrence alertRecurrence, DateTime dateSend)
        {
            switch (alertRecurrence)
            {
                case AlertRecurrence.JustOnce:
                    return dateSend.ToString();
                case AlertRecurrence.Week:
                    return $"0 10 * * {dateSend.DayOfWeek}";
                case AlertRecurrence.Month:
                    return $"0 10 {dateSend.Day} * *";
                case AlertRecurrence.Year:
                    return $"0 10 {dateSend.DayOfWeek} {dateSend.Month} *";
                default:
                    throw new ArgumentException("Seleciona uma recorrência para o alerta");
            }
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

        private async Task ValidateAndScheduleAlert(string dateSendingAlert, string emailUser, ScheduleJobDTO scheduleJobDTO, EmailRequestDTO emailRequestDTO)
        {
            if (dateSendingAlert is not null && emailUser is not null)
                await _processAlertService.ScheduleAlert(scheduleJobDTO, emailRequestDTO);
        }
    }
}
