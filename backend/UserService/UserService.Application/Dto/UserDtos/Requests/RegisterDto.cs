namespace UserService.Application.Dto.UserDtos;

public class RegisterDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    public RegisterDto()
    {
    }

    public RegisterDto(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = password;
    }
}