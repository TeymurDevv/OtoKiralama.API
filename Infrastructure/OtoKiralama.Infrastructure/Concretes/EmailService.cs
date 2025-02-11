using OtoKiralama.Application.Interfaces;

namespace OtoKiralama.Infrastructure.Concretes;

public class EmailService : IEmailService
{
    public Task SendEmailAsync(string to, string subject, string body, bool isHtml = true)
    {
        throw new NotImplementedException();
    }
}