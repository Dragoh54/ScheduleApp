using FluentValidation;

namespace MeetingService.Application.UseCases.Participants.Command.RemoveParticipantFromMeetingCommand;

public class RemoveParticipantFromMeetingCommandValidator : AbstractValidator<RemoveParticipantFromMeetingCommand>
{
    public RemoveParticipantFromMeetingCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("Id cannot be empty");
        
        RuleFor(x => x.MeetingId).NotEmpty().WithMessage("Meeting Id cannot be empty");
        
        RuleFor(x => x.AccessToken).NotEmpty().WithMessage("AccessToken cannot be empty");
    }
}