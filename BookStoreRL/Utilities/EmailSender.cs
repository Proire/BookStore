﻿using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using UserModelLayer;

namespace UserRLL.Utilities
{
    public class EmailSender 
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(EmailDTO dto)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["EmailSettings:SenderEmail"]));
            email.To.Add(MailboxAddress.Parse(dto.To));
            email.Subject = dto.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = dto.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_configuration["EmailSettings:SmtpServer"], Convert.ToInt32(_configuration["EmailSettings:SmtpPort"]), SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration["EmailSettings:SenderEmail"], Environment.GetEnvironmentVariable("MailPassword")); 
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}