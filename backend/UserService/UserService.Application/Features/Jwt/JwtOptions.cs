namespace UserService.Application.Features.Jwt;

public class JwtOptions
{
    public int AccessExpiresMinutes { get; set; }
    public int RefreshExpiresDays { get; set; }
    public int EmailConfirmationExpiresHours { get; set; }
    public int RecoverAccountExpiresHours { get; set; }
}
