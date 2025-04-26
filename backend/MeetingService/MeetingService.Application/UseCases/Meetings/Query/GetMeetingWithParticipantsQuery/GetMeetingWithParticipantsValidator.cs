using FluentValidation;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingWithParticipantsQuery;

public class GetMeetingWithParticipantsValidator : AbstractValidator<GetMeetingWithParticipantsQuery>
{
    public GetMeetingWithParticipantsValidator()
    {
        RuleFor(q => q.MeetingId)
            .NotNull().WithMessage("Id cannot be null.");
    }
}