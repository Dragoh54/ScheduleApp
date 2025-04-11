using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.CalendarDay.Command.UpdateCalendarDayCommand;

public class UpdateCalendarDayHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateCalendarDayCommand, CalendarDayDto>
{
    public async Task<CalendarDayDto> Handle(UpdateCalendarDayCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.CalendarDays.UpdateAsync(request.Adapt<DomainModel.Models.CalendarDay>(), cancellationToken);
        var success = await unitOfWork.Commit(cancellationToken);

        if (!success)
        {
            throw new BadRequestException("Failed to update calendar day");
        }
        
        return request.Adapt<CalendarDayDto>();
    }
}