using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;
using LNUbiz.BLL.Interfaces;
using LNUbiz.BLL.Interfaces.Logging;
using LNUbiz.BLL.Settings;

namespace LNUbiz.BLL.Services
{
    public class EmailSendingService : IEmailSendingService
    {
        private readonly ILoggerService<EmailSendingService> _loggerService;

        public EmailSendingService(IOptions<EmailServiceSettings>      settings,
                                   ILoggerService<EmailSendingService> loggerService)
        {
            Settings       = settings;
            _loggerService = loggerService;
        }

        public IOptions<EmailServiceSettings> Settings { get; }

        ///<inheritdoc/>
        public async Task<bool> SendEmailAsync(string email, string subject, string message, string title)
        {
            var SMTPServer         = Settings.Value.SMTPServer;
            var Port               = Settings.Value.Port;
            var SMTPServerLogin    = Settings.Value.SMTPServerLogin;
            var SMTPServerPassword = Settings.Value.SMTPServerPassword;

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(title, SMTPServerLogin));
            emailMessage.To  .Add(new MailboxAddress("", email));

            emailMessage.Subject = subject;
            emailMessage.Body    = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message };
            try
            {
                using var client = new SmtpClient();
                client.CheckCertificateRevocation = false;

                await client.ConnectAsync(SMTPServer, Port, true);
                await client.AuthenticateAsync(SMTPServerLogin, SMTPServerPassword);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
            catch (Exception exс)
            {
                _loggerService.LogError(exс.Message);
                return false;
            }
            return true;
        }
    }
}
