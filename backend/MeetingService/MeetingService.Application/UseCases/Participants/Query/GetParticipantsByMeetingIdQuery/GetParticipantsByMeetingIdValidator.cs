using FluentValidation;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantsByMeetingIdQuery;

public class GetParticipantsByMeetingIdValidator : AbstractValidator<GetParticipantsByMeetingIdQuery>
{
    public GetParticipantsByMeetingIdValidator()
    {
        RuleFor(x => x.MeetingId)
            .NotEmpty().WithMessage("MeetingId is required.");
    }
}