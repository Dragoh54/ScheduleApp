using FluentValidation;

namespace ScheduleService.Application.UseCases.Meeting.Command.CreateMeetingCommand;

public class CreateMeetingCommandValidator : AbstractValidator<CreateMeetingCommand>
{
    public CreateMeetingCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required");

        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("StartTime is required");

        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage("EndTime is required");

        RuleFor(x => x)
            .Must(x => x.StartTime < x.EndTime)
            .WithMessage("StartTime must be earlier than EndTime");
    }
}