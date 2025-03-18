namespace UserService.DataAccess.Models;

public class UserEntity : IdEntity
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
    
    public ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
    
    public UserEntity() {}

    public UserEntity(string username, string email, string passwordHash, string firstName, string lastName, DateTime createdAt)
    {
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        FirstName = firstName;
        LastName = lastName;
        CreatedAt = createdAt;
        UpdatedAt = createdAt;
    }
}