using System.Linq.Expressions;
using UserService.DataAccess.Interfaces.Specifications;
using UserService.DataAccess.Models;
using UserService.DataAccess.Models.Pagination;

namespace UserService.DataAccess.Specifications;

public class UserFilterSpecification : ISpecification<UserEntity>
{
    private readonly UserFilters _filters;
    
    public UserFilterSpecification(UserFilters filters)
    {
        _filters = filters;
    }
    
    public Expression<Func<UserEntity, bool>> Criteria => user =>
        (string.IsNullOrEmpty(_filters.Username) || user.Username.Contains(_filters.Username)) &&
        (string.IsNullOrEmpty(_filters.Email) || user.Email.Contains(_filters.Email)) &&
        (string.IsNullOrEmpty(_filters.FirstName) || user.FirstName.Contains(_filters.FirstName)) &&
        (string.IsNullOrEmpty(_filters.LastName) || user.LastName.Contains(_filters.LastName)) &&
        (string.IsNullOrEmpty(_filters.LastLoginAt) || user.LastLoginAt.Date == DateTime.Parse(_filters.LastLoginAt).Date);
    
    public List<Expression<Func<UserEntity, object>>> Includes =>
        [u => u.UserRoles, u => u.UserRoles.Select(ur => ur.Role)];
}