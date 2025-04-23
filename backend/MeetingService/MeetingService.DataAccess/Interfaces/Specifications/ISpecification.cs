using System.Linq.Expressions;

namespace MeetingService.DataAccess.Interfaces.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> ToExpression();
}