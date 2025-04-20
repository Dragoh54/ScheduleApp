using FluentValidation;

namespace ScheduleService.Application.UseCases.Meeting.Command.DeleteMeetingCommand;

public class DeleteMeetingValidator : AbstractValidator<DeleteMeetingCommand>
{
    public DeleteMeetingValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id cannot be empty");
    }
}