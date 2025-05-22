namespace UserService.Application.Dto;

public class PaginatedPageUsers 
{
    public UserFilters UserFilters { get; set; } = new UserFilters();
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;

    public PaginatedPageUsers()
    {
        
    }
}