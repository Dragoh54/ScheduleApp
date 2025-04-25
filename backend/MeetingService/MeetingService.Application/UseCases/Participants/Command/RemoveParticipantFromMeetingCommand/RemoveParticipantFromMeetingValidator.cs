using FluentValidation;

namespace MeetingService.Application.UseCases.Participants.Command.RemoveParticipantFromMeetingCommand;

public class RemoveParticipantFromMeetingValidator : AbstractValidator<RemoveParticipantFromMeetingCommand>
{
    public RemoveParticipantFromMeetingValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");
        
        RuleFor(x => x.MeetingId).NotEmpty().WithMessage("Meeting Id cannot be empty");
    }
}