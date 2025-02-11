using OtoKiralama.Application.Interfaces;
using Resend;

namespace OtoKiralama.Infrastructure.Concretes;

public class EmailService : IEmailService
{
    private readonly IResend _resend;
    public EmailService(IResend resend)
    {
        _resend = resend;
    }
    public Task SendEmailAsync(string to, string subject, string body, bool isHtml = true)
    {
        throw new NotImplementedException();
    }
}