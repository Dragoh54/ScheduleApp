using FluentValidation;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantsByMeetingIdQuery;

public class GetParticipantsByMeetingIdQueryValidator : AbstractValidator<GetParticipantsByMeetingIdQuery>
{
    public GetParticipantsByMeetingIdQueryValidator()
    {
        RuleFor(x => x.MeetingId)
            .NotEmpty().WithMessage("MeetingId is required.");
    }
}