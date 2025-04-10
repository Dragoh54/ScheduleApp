using UserService.DataAccess.Enums;

namespace UserService.DataAccess.Models;

public class TokenEntity : IdEntity
{
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public string Token { get; set; } = string.Empty;
    public TokenTypes TokenType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; } = false;
    
    public TokenEntity() {}

    public TokenEntity(Guid id, string token, TokenTypes tokenType,  Guid userId, DateTime createdAt, DateTime expiresAt)
    {
        Id = id;
        Token = token;
        TokenType = tokenType;
        UserId = userId;
        CreatedAt = createdAt;
        ExpiresAt = expiresAt;
    }
}