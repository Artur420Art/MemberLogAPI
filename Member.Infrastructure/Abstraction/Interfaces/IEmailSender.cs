using System;
namespace Member.Infrastructure.Abstraction.Interfaces
{
	public interface IEmailSender
	{
        public Task SendEmailSender(string email, string subject, string message);

    }
}

