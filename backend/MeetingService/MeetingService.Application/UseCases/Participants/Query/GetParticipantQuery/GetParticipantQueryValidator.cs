using FluentValidation;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantQuery;

public class GetParticipantQueryValidator : AbstractValidator<GetParticipantQuery>
{
    public GetParticipantQueryValidator()
    {
        RuleFor(x => x.MeetingId)
            .NotEmpty().WithMessage("MeetingId is required.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");
    }
}