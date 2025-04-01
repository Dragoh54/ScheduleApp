namespace UserService.Application.Dto;

public class PaginatedPageUsers 
{
    public Filters Filters { get; set; } = new Filters();
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;

    public PaginatedPageUsers()
    {
        
    }
}