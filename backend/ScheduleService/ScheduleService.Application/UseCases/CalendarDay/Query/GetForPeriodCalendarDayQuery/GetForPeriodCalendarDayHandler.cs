using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;

namespace ScheduleService.Application.UseCases.CalendarDay.Query.GetForPeriodCalendarDayQuery;

public class GetForPeriodCalendarDayHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetForPeriodCalendarDayQuery, IEnumerable<CalendarDayDto>>
{
    public Task<IEnumerable<CalendarDayDto>> Handle(GetForPeriodCalendarDayQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}