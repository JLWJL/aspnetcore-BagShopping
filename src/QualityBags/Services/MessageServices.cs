using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace QualityBags.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress("Quality Bags Team", "wangj174@myunitec.ac.nz"));
            msg.To.Add(new MailboxAddress("User", email));
            msg.Subject = subject;
            msg.Body = new TextPart("plain")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.office365.com", 587, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate("wangj174@myunitec.ac.nz", "wjl8846160");

                client.Send(msg);
                client.Disconnect(true);
            }


            return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
