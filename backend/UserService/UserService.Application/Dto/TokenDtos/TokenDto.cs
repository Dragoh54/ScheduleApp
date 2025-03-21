namespace UserService.Application.Dto.TokenDtos;

public class TokenDto
{
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; } = false;
}