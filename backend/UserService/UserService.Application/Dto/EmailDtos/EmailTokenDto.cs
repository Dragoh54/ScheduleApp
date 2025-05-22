namespace UserService.Application.Dto.EmailDtos;

public record EmailTokenDto
{
    public string Token { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}