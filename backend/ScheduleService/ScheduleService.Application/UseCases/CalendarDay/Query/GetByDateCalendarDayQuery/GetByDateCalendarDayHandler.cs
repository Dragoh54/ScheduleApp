using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetDefaultTemplateQuery;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.CalendarDay.Query.GetByDateCalendarDayQuery;

public class GetByDateCalendarDayHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetByDateCalendarDayQuery, CalendarDayDto>
{
    public async Task<CalendarDayDto> Handle(GetByDateCalendarDayQuery request, CancellationToken cancellationToken)
    {
        await unitOfWork.CalendarDays.GetByDateAsync(request.UserId, request.Date, cancellationToken);
        var success = await unitOfWork.Commit(cancellationToken);

        if (!success)
        {
            throw new BadRequestException("Failed to add calendar day");
        }
        
        return request.Adapt<CalendarDayDto>();
    }
}