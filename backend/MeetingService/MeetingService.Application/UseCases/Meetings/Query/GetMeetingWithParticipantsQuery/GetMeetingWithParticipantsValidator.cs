using FluentValidation;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingWithParticipantsQuery;

public class GetMeetingWithParticipantsValidator : AbstractValidator<GetMeetingWithParticipantsQuery>
{
    public GetMeetingWithParticipantsValidator()
    {
        RuleFor(q => q.Id)
            .NotNull().WithMessage("Id cannot be null.");
    }
}