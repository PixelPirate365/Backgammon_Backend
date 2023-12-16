using Auth.EmailService.Interfaces;
using Auth.EmailService.Models;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace Auth.EmailService.Implementation {
    public class EmailSender : IEmailSender {
        private readonly EmailConfiguration _smptpConfigurationModel;
        public EmailSender(EmailConfiguration emailConfig) {
            _smptpConfigurationModel = emailConfig;
        }

        public async Task SendAsync(UserEmailOptions userEmailOptions) {
            await SendEmail(userEmailOptions);
        }
        private async Task SendEmail(UserEmailOptions userEmailOptions) {
            var mail = new MailMessage {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smptpConfigurationModel.SenderAddress, "IdentityServer4"),
                IsBodyHtml = _smptpConfigurationModel.IsBodyHtml

            };
            foreach (var toEmail in userEmailOptions.ToEmails) {
                mail.To.Add(toEmail);
            }
            var networkCredential = new NetworkCredential(_smptpConfigurationModel.UserName, _smptpConfigurationModel.Password);
            var smtpClient = new SmtpClient {
                Host = _smptpConfigurationModel.Host,
                Port = _smptpConfigurationModel.Port,
                EnableSsl = _smptpConfigurationModel.EnableSsl,
                UseDefaultCredentials = _smptpConfigurationModel.UseDefaultCredentials,
                Credentials = networkCredential
            };
            mail.BodyEncoding = Encoding.Default;
            await smtpClient.SendMailAsync(mail);
        }


    }

}
