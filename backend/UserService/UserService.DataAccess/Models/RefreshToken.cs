namespace UserService.DataAccess.Models;

public class RefreshToken : IdEntity
{
    public Guid UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    
    public RefreshToken() {}

    public RefreshToken(Guid userId, string token, DateTime createdAt, DateTime expiresAt)
    {
        UserId = userId;
        Token = token;
        CreatedAt = createdAt;
        ExpiresAt = expiresAt;
    }
}