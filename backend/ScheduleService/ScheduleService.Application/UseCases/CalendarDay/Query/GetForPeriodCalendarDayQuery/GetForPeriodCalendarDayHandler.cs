using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.CalendarDay.Query.GetForPeriodCalendarDayQuery;

public class GetForPeriodCalendarDayHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetForPeriodCalendarDayQuery, IEnumerable<CalendarDayDto>>
{
    public async Task<IEnumerable<CalendarDayDto>> Handle(GetForPeriodCalendarDayQuery request, CancellationToken cancellationToken)
    {
        await unitOfWork.CalendarDays.GetForPeriodAsync(request.UserId, request.StartDate, request.EndDate, cancellationToken);
        var success = await unitOfWork.Commit(cancellationToken);

        if (!success)
        {
            throw new BadRequestException("Failed to get calendar days");
        }
        
        return request.Adapt< IEnumerable<CalendarDayDto>>();
    }
}