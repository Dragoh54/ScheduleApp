namespace UserService.Application.Dto;

public class Filters
{
    public string? Username { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? LastLoginAt { get; set; }
}