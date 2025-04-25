using FluentValidation;
using MeetingService.DomainModel.Settings;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantByEmailQuery;

public class GetParticipantByEmailValidator : AbstractValidator<GetParticipantByEmailQuery>
{
    public GetParticipantByEmailValidator()
    {
        RuleFor(x => x.MeetingId)
            .NotEmpty().WithMessage("MeetingId is required.");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(ParticipantSettings.EmailMaxLength)
            .WithMessage($"Email length must not exceed {ParticipantSettings.EmailMaxLength} characters.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}