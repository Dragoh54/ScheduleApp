using FluentValidation;

namespace ScheduleService.Application.UseCases.Meeting.Command.DeleteMeetingCommand;

public class DeleteMeetingCommandValidator : AbstractValidator<DeleteMeetingCommand>
{
    public DeleteMeetingCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id cannot be empty");
    }
}