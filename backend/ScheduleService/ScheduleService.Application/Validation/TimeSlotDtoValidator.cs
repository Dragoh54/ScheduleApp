using FluentValidation;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.TimeSlots;

namespace ScheduleService.Application.Validation;

public class TimeSlotDtoValidator : AbstractValidator<TimeSlotDto>
{
    public TimeSlotDtoValidator()
    {
        RuleFor(x => x.StartTime)
            .LessThan(x => x.EndTime)
            .WithMessage("Start time must be before end time");
            
        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage("End time must be after start time");
    }
}