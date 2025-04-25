using FluentValidation;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantQuery;

public class GetParticipantValidator : AbstractValidator<GetParticipantQuery>
{
    public GetParticipantValidator()
    {
        RuleFor(x => x.MeetingId)
            .NotEmpty().WithMessage("MeetingId is required.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");
    }
}