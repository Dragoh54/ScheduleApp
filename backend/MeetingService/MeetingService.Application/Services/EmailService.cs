using MailKit.Net.Smtp;
using MeetingService.Application.Interfaces.Services;
using MeetingService.Application.Settings;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace MeetingService.Application.Services;

public class EmailService : IEmailService
{
    public EmailService(IOptions<EmailSettings> settings)
    {
        _settings = settings.Value;
    }

    private readonly EmailSettings _settings;
    
    public async Task SendEmailAsync(string mailTo, string subject, string message, CancellationToken cancellationToken)
    {
        var emailMessage = BuildMimeMessage(mailTo, subject, message);
             
        using (var client = new SmtpClient())
        {
            await SendEmailThroughClientAsync(client, emailMessage, cancellationToken);
        }
    }

    private MimeMessage BuildMimeMessage(string mailTo, string subject, string message)
    {
        var emailMessage = new MimeMessage();
 
        emailMessage.From.Add(new MailboxAddress(_settings.FromName, _settings.FromAddress));
        emailMessage.To.Add(new MailboxAddress("", mailTo));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(TextFormat.Html)
        {
            Text = message
        };
        
        return emailMessage;
    }

    private async Task SendEmailThroughClientAsync(SmtpClient client, MimeMessage emailMessage, CancellationToken cancellationToken)
    {
        await client.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, _settings.EnableSsl, cancellationToken);
    
        await client.AuthenticateAsync(_settings.SmtpUsername, _settings.SmtpPassword, cancellationToken);
    
        await client.SendAsync(emailMessage, cancellationToken);
        await client.DisconnectAsync(true, cancellationToken);
    }
}