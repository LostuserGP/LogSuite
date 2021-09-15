using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Server.Services.IServices
{
    public interface IMailService
    {
        Task<int> SendForgetPasswordMail();
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
