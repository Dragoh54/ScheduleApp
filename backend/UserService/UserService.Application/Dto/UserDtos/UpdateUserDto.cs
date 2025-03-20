namespace UserService.Application.Dto;

public class UpdateUserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public UpdateUserDto()
    {
        
    }

    public UpdateUserDto(Guid id, string username, string firstName, string lastName)
    {
        Id = id;
        Username = username;
        FirstName = firstName;
        LastName = lastName;
    }
}