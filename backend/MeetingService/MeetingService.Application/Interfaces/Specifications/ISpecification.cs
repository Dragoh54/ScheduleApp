using System.Linq.Expressions;

namespace MeetingService.Application.Interfaces.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> ToExpression();
}