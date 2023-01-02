using MessageConsumer.Application.Interfaces;
using MessageConsumer.Application.Models;
using MessageConsumer.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MessageConsumer.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private  MailMessage mailMessage;
        private  SmtpClient smtpClient;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            GetConfigurationEmail();
        }
        private void GetConfigurationEmail()
        {
            try
            {
                var emailConfiguration = _configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
                mailMessage = new MailMessage { From = new MailAddress(emailConfiguration.From) };
                smtpClient = new SmtpClient(emailConfiguration.SmtpClient)
                {
                    EnableSsl = emailConfiguration.EnableSsl,
                    Port = emailConfiguration.Port,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = emailConfiguration.UseDefaultCredentials,
                    Credentials = new NetworkCredential(
                           emailConfiguration.UserName,
                           emailConfiguration.Password
                      )
                };
            }
            catch (Exception)
            {
                //Do something such as log;
            }      
        }

        public async Task SendEmailAsync(InvitationMessage invitation)
        {
            try
            {
                Email email = new Email(MailFake.To, MailFake.Title, MailFake.Body);

                mailMessage.To.Add(email.To);
                mailMessage.Subject = $"{email.Title}: {invitation.ContactFullName} - Price: {invitation.InvitationPrice}";
                mailMessage.Body = email.Body;
                mailMessage.IsBodyHtml = true;
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception e)
            {
               //The credentials are invalid and this method will always be an exception

            }
        }
    }

  
}
