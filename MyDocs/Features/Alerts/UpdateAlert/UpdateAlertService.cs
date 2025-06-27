using Microsoft.EntityFrameworkCore;
using MimeKit.Cryptography;
using MyDocs.Infraestructure.ExternalServices.Email;
using MyDocs.Infraestructure.ExternalServices.Hangfire;
using MyDocs.Infraestructure.Persistence;
using MyDocs.Models;
using MyDocs.Models.Enums;
using MyDocs.Shared.DTOs;
using MyDocs.Shared.Services.AlertService;
using MyDocs.Shared.Services.EmailTemplateService;
using MyDocs.Shared.Services.ScheduleAlertService;
using MyDocs.Shared.Services.UserCredential;
using MyDocs.Shared.Services.UserService;

namespace MyDocs.Features.Alerts.UpdateAlert
{
    public class UpdateAlertService : IUpdateAlertService
    {
        private readonly Context _context;
        private readonly IAlertService _alertService;
        private readonly IScheduleAlertService _scheduleAlertService;
        private readonly IUserService _userService;
        private readonly IUserCredentialService _userCredentialService;
        private readonly IEmailTemplateService _emailTemplateService;
        public UpdateAlertService(Context context,
                                  IAlertService alertService,
                                  IScheduleAlertService scheduleAlertService,
                                  IUserService userService,
                                  IUserCredentialService userCredentialService,
                                  IEmailTemplateService emailTemplateService)
        {
            _context = context;
            _alertService = alertService;
            _scheduleAlertService = scheduleAlertService;
            _userService = userService;
            _userCredentialService = userCredentialService;
            _emailTemplateService = emailTemplateService;
        }


        public async Task UpdateAlert(UpdateAlertRequest request)
        {
            request.ValidateProperties();

            Alert alert = await _alertService.GetAlert(request.IdAlert, request.IdUser);

            alert.Name = request.Name;
            alert.Description = request.Description;
            alert.RecurrenceOfSending = (int)request.RecurrenceOfSending;
            alert.EndDate = request.EndDate;

            _context.Alerts.Update(alert);

            string cron = _alertService.ConfigureDateSendOfAlert(request.RecurrenceOfSending, request.FirstDaySend);

            UserCredentials credentials = await _userCredentialService.FindUserCredential(request.IdUser);

            var template = await _emailTemplateService.FindEmailTemplate(EmailTemplateEnum.OverdueBill);

            User user = await _userService.GetUser(request.IdUser);

            await _scheduleAlertService.UpdateAlert(alert.JobId,
                                              new ScheduleJobDTO(cron, "alerts"),
                                              new EmailRequestDTO(credentials.Email, 
                                                                  template.Subject, 
                                                                  _emailTemplateService.ReplaceEmailTemplateOverdueBill(user.Name, alert.Name, template)));

            await _context.SaveChangesAsync();
        }
    }
}
