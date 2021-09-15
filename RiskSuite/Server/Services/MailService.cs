using Microsoft.Extensions.Configuration;
using LogSuite.Server.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LogSuite.Server.Services
{
    public class MailService : IMailService
    {
        private readonly string _from;
        private readonly string _siteAddress;
        private readonly string _host;
        private readonly int _port;

        private SmtpClient _client;
        private readonly IConfiguration _config;

        public MailService(IConfiguration configuration)
        {
            _from = configuration.GetValue<string>("Email:From");
            _siteAddress = configuration.GetValue<string>("SiteAddress");
            _host = configuration.GetValue<string>("Email:Smtp:Host");
            _port = configuration.GetValue<int>("Email:Smtp:Port");
            _client = CreateClient();
        }

        private SmtpClient CreateClient()
        {
            SmtpClient client = new SmtpClient();
            NetworkCredential nc = CredentialCache.DefaultNetworkCredentials;
            client.Host = _host;
            client.Port = _port;
            client.UseDefaultCredentials = true;
            client.Credentials = (System.Net.ICredentialsByHost)nc.GetCredential(
                        _host,
                        _port,
                        "Basic");
            return client;
        }

        public async Task<int> SendForgetPasswordMail()
        {
            throw new NotImplementedException();
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                using (var smtpClient = _client)
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(_from);
                    mailMessage.To.Add(new MailAddress(email));
                    mailMessage.IsBodyHtml = true;
                    mailMessage.SubjectEncoding = Encoding.UTF8;
                    mailMessage.BodyEncoding = Encoding.UTF8;
                    mailMessage.Subject = subject;
                    mailMessage.Body = htmlMessage;

                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
