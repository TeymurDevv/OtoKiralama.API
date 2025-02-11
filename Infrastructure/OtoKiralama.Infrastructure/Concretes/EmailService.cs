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
    public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = true)
    {
        var message = new EmailMessage();
        message.From = "teymursuleymanli2008@gmail.com";
        message.To.Add(to);
        message.Subject =subject;
        message.HtmlBody = body;

        await _resend.EmailSendAsync(message);    }
}