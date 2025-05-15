using MediatR;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;
using ScheduleService.DomainModel.Models;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Query.IsUserFreeQuery;

public class IsUserFreeQueryHandler : IRequestHandler<IsUserFreeQuery, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public IsUserFreeQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<bool> Handle(IsUserFreeQuery request, CancellationToken cancellationToken)
    {
        var template = await _unitOfWork.AvailabilityTemplates.GetDefaultTemplateAsync(request.UserId, cancellationToken)
                       ?? throw new NotFoundException("Default template not found");
        
        var dayOfWeek = request.StartDate.DayOfWeek;
        
        var busySlots = template.Schedule
                            .FirstOrDefault(s => s.DayOfWeek == dayOfWeek)?.TimeSlots 
                        ?? new List<TimeSlot>();

        var start = TimeOnly.FromDateTime(request.StartDate);
        var end = TimeOnly.FromDateTime(request.EndDate);

        foreach (var slot in busySlots)
        {
            var slotStart = slot.StartTime;
            var slotEnd = slot.EndTime;

            var isOverlap = start < slotEnd && end > slotStart;
            if (isOverlap)
                return false;
        }

        return true;
    }
}