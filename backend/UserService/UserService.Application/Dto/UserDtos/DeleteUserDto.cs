namespace UserService.Application.Dto;

public class DeleteUserDto
{
    public Guid Id { get; set; }
    
    public DeleteUserDto()
    {
    }

    public DeleteUserDto(Guid id)
    {
        Id = id;
    }
}