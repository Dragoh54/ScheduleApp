namespace UserService.Api.Interfaces;

public interface IEmailService
{
    public Task<bool> SendEmail();
}