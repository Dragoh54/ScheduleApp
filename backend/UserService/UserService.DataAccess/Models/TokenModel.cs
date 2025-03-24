using UserService.DataAccess.Enums;

namespace UserService.DataAccess.Models;

public class TokenModel : IdEntity
{
    public Guid UserId { get; set; }
    public Token Token { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; } = false;
    
    public TokenModel() {}

    public TokenModel(Guid id, Token token,  Guid userId, DateTime createdAt, DateTime expiresAt)
    {
        Id = id;
        Token = token;
        UserId = userId;
        CreatedAt = createdAt;
        ExpiresAt = expiresAt;
    }
}