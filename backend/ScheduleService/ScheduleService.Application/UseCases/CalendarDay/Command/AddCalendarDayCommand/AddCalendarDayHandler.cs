using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.CalendarDay.Command.AddCalendarDayCommand;

public class AddCalendarDayHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<AddCalendarDayCommand, CalendarDayDto>
{
    public async Task<CalendarDayDto> Handle(AddCalendarDayCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.CalendarDays.AddAsync(request.Adapt<DomainModel.Models.CalendarDay>(), cancellationToken);
        var success = await unitOfWork.Commit(cancellationToken);

        if (!success)
        {
            throw new BadRequestException("Failed to add calendar day");
        }
        
        return request.Adapt<CalendarDayDto>();
    }
}