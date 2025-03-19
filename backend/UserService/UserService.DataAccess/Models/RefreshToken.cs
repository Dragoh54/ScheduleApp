namespace UserService.DataAccess.Models;

public class RefreshToken : IdEntity
{
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; } = false;
    
    public RefreshToken() {}

    public RefreshToken(Guid id, Guid userId, DateTime createdAt, DateTime expiresAt)
    {
        Id = id;
        UserId = userId;
        CreatedAt = createdAt;
        ExpiresAt = expiresAt;
    }
}