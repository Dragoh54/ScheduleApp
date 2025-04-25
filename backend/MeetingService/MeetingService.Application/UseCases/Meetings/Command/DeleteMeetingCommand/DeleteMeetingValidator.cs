using FluentValidation;

namespace MeetingService.Application.UseCases.Meetings.Command.DeleteMeetingCommand;

public class DeleteMeetingValidator : AbstractValidator<DeleteMeetingCommand>
{
    public DeleteMeetingValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");
    }
}