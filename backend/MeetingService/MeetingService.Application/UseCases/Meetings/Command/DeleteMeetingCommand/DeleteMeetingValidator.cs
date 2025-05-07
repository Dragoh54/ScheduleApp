using FluentValidation;

namespace MeetingService.Application.UseCases.Meetings.Command.DeleteMeetingCommand;

public class DeleteMeetingValidator : AbstractValidator<DeleteMeetingCommand>
{
    public DeleteMeetingValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");
        
        RuleFor(x => x.Token).NotEmpty().WithMessage("Token cannot be empty");
    }
}