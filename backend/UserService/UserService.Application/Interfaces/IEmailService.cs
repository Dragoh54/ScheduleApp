using UserService.Application.Dto.EmailDtos;

namespace UserService.Api.Interfaces;

public interface IEmailService
{
    public Task SendEmailAsync(string recipientEmail, string subject, string message, CancellationToken cancellationToken);
}