namespace MeetingService.Application.Interfaces.Services;

public interface IEmailService
{
    public Task SendEmailAsync(string recipientEmail, string subject, string message, CancellationToken cancellationToken);
}