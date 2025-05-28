using FluentValidation;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingWithParticipantsQuery;

public class GetMeetingWithParticipantsQueryValidator : AbstractValidator<GetMeetingWithParticipantsQuery>
{
    public GetMeetingWithParticipantsQueryValidator()
    {
        RuleFor(q => q.MeetingId)
            .NotNull().WithMessage("Id cannot be null.");
    }
}