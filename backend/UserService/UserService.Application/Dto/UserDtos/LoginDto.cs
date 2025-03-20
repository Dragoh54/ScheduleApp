namespace UserService.Application.Dto;

public class LoginDto
{
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public LoginDto()
    {
        
    }

    public LoginDto(string email, string password)
    {
        Email = email;
        Password = password;
    }
}