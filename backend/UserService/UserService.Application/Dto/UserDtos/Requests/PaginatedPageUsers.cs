using UserService.DataAccess.Models.Pagination;
using UserService.DataAccess.Pagination;

namespace UserService.Application.Dto.UserDtos;

public class PaginatedPageUsers 
{
    public UserFilters UserFilters { get; set; } = new UserFilters();
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}