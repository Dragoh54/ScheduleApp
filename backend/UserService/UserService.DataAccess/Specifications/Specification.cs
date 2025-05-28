using System.Linq.Expressions;
using UserService.DataAccess.Interfaces.Specifications;

namespace UserService.DataAccess.Specifications;

public abstract class Specification<T> : ISpecification<T>
{
    public abstract Expression<Func<T, bool>> ToExpression();

    public bool IsSatisfiedBy(T entity)
    {
        return ToExpression().Compile().Invoke(entity);
    }
}
