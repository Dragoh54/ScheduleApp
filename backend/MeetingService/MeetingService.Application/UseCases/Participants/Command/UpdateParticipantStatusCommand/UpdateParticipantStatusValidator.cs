using FluentValidation;

namespace MeetingService.Application.UseCases.Participants.Command.UpdateParticipantStatusCommand;

public class UpdateParticipantStatusValidator : AbstractValidator<UpdateParticipantStatusCommand>
{
    public UpdateParticipantStatusValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("Id cannot be null.");

        RuleFor(x => x.MeetingId)
            .NotNull().WithMessage("Meeting Id cannot be null.");
        
        RuleFor(x => x.Status)
            .NotNull().WithMessage("Status cannot be null.");
    }
}