using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.CalendarDay.Command.DeleteCalendarDayCommand;

public class DeleteCalendarDayHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteCalendarDayCommand, CalendarDayDto>
{
    public async Task<CalendarDayDto> Handle(DeleteCalendarDayCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.CalendarDays.DeleteAsync(request.Id, cancellationToken);
        var success = await unitOfWork.Commit(cancellationToken);

        if (!success)
        {
            throw new BadRequestException("Failed to delete calendar day");
        }
        
        return request.Adapt<CalendarDayDto>();
    }
}