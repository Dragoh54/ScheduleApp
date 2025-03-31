using UserService.DataAccess.Models;
using UserService.Application.Dto.RoleDto;

namespace UserService.Application.Dto;

public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime LastLoginAt { get; set; }
    public List<RoleDto.RoleDto> Roles { get; set; } = new List<RoleDto.RoleDto>();

    public UserDto()
    {
        
    }
    
    public UserDto(Guid id, string username, string email, string firstName, string lastName, List<RoleDto.RoleDto> roles)
    {
        Id = id;
        Username = username;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Roles = roles;
    }
}