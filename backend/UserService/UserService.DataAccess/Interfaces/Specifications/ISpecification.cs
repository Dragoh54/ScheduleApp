using System.Linq.Expressions;

namespace UserService.DataAccess.Interfaces.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> ToExpression();
}
