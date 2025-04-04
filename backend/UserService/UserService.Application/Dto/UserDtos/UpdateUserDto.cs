namespace UserService.Application.Dto.UserDtos;

public class UpdateUserDto
{
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public UpdateUserDto()
    {
        
    }

    public UpdateUserDto(string username, string firstName, string lastName)
    {
        Username = username;
        FirstName = firstName;
        LastName = lastName;
    }
}