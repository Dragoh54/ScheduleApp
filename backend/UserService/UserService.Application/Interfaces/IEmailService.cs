using UserService.Application.Dto.EmailDtos;

namespace UserService.Api.Interfaces;

public interface IEmailService
{
    public Task<bool> SendEmailAsync(ConfirmEmailDto emailDto, CancellationToken cancellationToken);
}