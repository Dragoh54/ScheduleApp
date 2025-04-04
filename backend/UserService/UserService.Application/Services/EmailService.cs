using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using UserService.Application.Handlers.Email;
using UserService.Application.Interfaces.Services;

namespace UserService.Application.Services;

public class EmailService(
    IOptions<EmailSettings> settings
    ) : IEmailService
{
    private readonly EmailSettings _settings = settings.Value;
    public async Task SendEmailAsync(string mailTo, string subject, string message, CancellationToken cancellationToken)
    {
        var emailMessage = new MimeMessage();
 
        emailMessage.From.Add(new MailboxAddress(_settings.FromName, _settings.FromAddress));
        emailMessage.To.Add(new MailboxAddress("", mailTo));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(TextFormat.Html)
        {
            Text = message
        };
             
        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, _settings.EnableSsl, cancellationToken);
            await client.AuthenticateAsync(_settings.SmtpUsername, _settings.SmtpPassword, cancellationToken);
            await client.SendAsync(emailMessage, cancellationToken);
            
            await client.DisconnectAsync(true, cancellationToken);
        }
    }
}