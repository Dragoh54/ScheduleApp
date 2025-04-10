using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetDefaultTemplateQuery;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;

namespace ScheduleService.Application.UseCases.CalendarDay.Query.GetByDateCalendarDayQuery;

public class GetByDateCalendarDayHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetByDateCalendarDayQuery, CalendarDayDto>
{
    public Task<CalendarDayDto> Handle(GetByDateCalendarDayQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}