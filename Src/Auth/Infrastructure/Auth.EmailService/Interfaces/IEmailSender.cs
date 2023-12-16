using Auth.EmailService.Models;

namespace Auth.EmailService.Interfaces {
    public interface IEmailSender {
        Task SendAsync(UserEmailOptions userEmailOptions);
    }
}
