using Auth.EmailService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.EmailService.Interfaces
{
    public interface IEmailSender
    {
        Task SendAsync(UserEmailOptions userEmailOptions);
    }
}
