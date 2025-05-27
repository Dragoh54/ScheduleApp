using FluentValidation;
using MeetingService.DomainModel.Settings;

namespace MeetingService.Application.UseCases.Participants.Command.AddParticipantToMeetingCommand;

public class AddParticipantToMeetingCommandValidator : AbstractValidator<AddParticipantToMeetingCommand>
{
    public AddParticipantToMeetingCommandValidator()
    {
        RuleFor(x => x.MeetingId)
            .NotEmpty().WithMessage("MeetingId is required.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(ParticipantSettings.EmailMaxLength)
            .WithMessage($"Email length must not exceed {ParticipantSettings.EmailMaxLength} characters.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(ParticipantSettings.UsernameMaxLength)
            .WithMessage($"Username must not exceed {ParticipantSettings.UsernameMaxLength} characters.");

        RuleFor(x => x.FirstName)
            .NotNull().WithMessage("FirstName is required.")
            .MaximumLength(ParticipantSettings.FirstNameMaxLength)
            .WithMessage($"First name must not exceed {ParticipantSettings.FirstNameMaxLength} characters.");

        RuleFor(x => x.LastName)
            .NotNull().WithMessage("LastName is required.")
            .MaximumLength(ParticipantSettings.LastNameMaxLength)
            .WithMessage($"Last name must not exceed {ParticipantSettings.LastNameMaxLength} characters.");
    }
}