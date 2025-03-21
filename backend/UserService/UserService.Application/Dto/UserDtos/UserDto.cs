using UserService.DataAccess.Models;

namespace UserService.Application.Dto;

public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public List<UserRoles> Roles { get; set; } = new List<UserRoles>();

    public UserDto()
    {
        
    }

    public UserDto(Guid id, string username, string email, string firstName, string lastName)
    {
        Id = id;
        Username = username;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }
    
    public UserDto(Guid id, string username, string email, string firstName, string lastName, List<UserRoles> roles)
    {
        Id = id;
        Username = username;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Roles = roles;
    }
}