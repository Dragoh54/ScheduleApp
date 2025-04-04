namespace UserService.Application.Dto.UserDtos;

public class LoginUserDto
{
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public LoginUserDto()
    {
        
    }

    public LoginUserDto(string email, string password)
    {
        Email = email;
        Password = password;
    }
}