using System;
using System.Net.Mail;
using System.Net;
using Member.Infrastructure.Abstraction.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Member.Infrastructure.Abstraction.Repostory
{
	public class EmailSender : IEmailSender
	{
        public Task SendEmailSender(string email, string subject, string message)
        {
            var mail = "art.gasparyan.420@gmail.com";
            var password = "cbgykptgbehvazat";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, password)
            };

            return client.SendMailAsync(
                    new MailMessage(from: mail,
                                    to: email,
                                    subject,
                                    message
                ));
         
        }

        
	}
}

