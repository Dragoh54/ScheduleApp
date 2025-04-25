using FluentValidation;

namespace MeetingService.Application.UseCases.Participants.Command.AddParticipantToMeetingCommand;

public class AddParticipantToMeetingValidator : AbstractValidator<AddParticipantToMeetingCommand>
{
    public AddParticipantToMeetingValidator()
    {
        RuleFor(x => x.MeetingId)
            .NotEmpty().WithMessage("MeetingId is required.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(100).WithMessage("Username must not exceed 100 characters.");

        RuleFor(x => x.FirstName)
            .NotNull().WithMessage("FirstName is required.")
            .MaximumLength(256).WithMessage("First name must not exceed 256 characters.");

        RuleFor(x => x.LastName)
            .NotNull().WithMessage("LastName is required.")
            .MaximumLength(256).WithMessage("Last name must not exceed 256 characters.");
        
        RuleFor(x => x.Status)
            .NotNull().WithMessage("Status is required.");
    }
}