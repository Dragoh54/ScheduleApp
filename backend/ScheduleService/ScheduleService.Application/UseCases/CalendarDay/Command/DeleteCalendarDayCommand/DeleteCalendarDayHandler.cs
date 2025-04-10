using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;

namespace ScheduleService.Application.UseCases.CalendarDay.Command.DeleteCalendarDayCommand;

public class DeleteCalendarDayHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteCalendarDayCommand, CalendarDayDto>
{
    public Task<CalendarDayDto> Handle(DeleteCalendarDayCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}