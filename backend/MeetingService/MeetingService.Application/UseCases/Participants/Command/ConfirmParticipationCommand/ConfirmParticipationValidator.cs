using FluentValidation;
using MeetingService.DomainModel.Settings;

namespace MeetingService.Application.UseCases.Participants.Command.ConfirmParticipationCommand;

public class ConfirmParticipationValidator : AbstractValidator<ConfirmParticipationCommand>
{
    public ConfirmParticipationValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(ParticipantSettings.EmailMaxLength)
            .WithMessage($"Email length must not exceed {ParticipantSettings.EmailMaxLength} characters.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required.");
        
        RuleFor(x => x.MeetingId)
            .NotNull().WithMessage("Meeting Id cannot be null.");
    }
}