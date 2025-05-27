using FluentValidation;

namespace MeetingService.Application.UseCases.Meetings.Command.RescheduleMeetingCommand;

public class RescheduleMeetingCommandValidator : AbstractValidator<RescheduleMeetingCommand>
{
    public RescheduleMeetingCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id cannot be null.");
        
        RuleFor(x => x.StartTime)
            .NotNull().WithMessage("StartTime is required.")
            .LessThan(x => x.EndTime).WithMessage("StartTime must be before EndTime.");

        RuleFor(x => x.EndTime)
            .NotNull().WithMessage("EndTime is required.")
            .GreaterThan(x => x.StartTime).WithMessage("EndTime must be after StartTime.");
    }
}