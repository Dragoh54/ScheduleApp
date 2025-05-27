using FluentValidation;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingByIdQuery;

public class GetMeetingByIdQueryValidator : AbstractValidator<GetMeetingByIdQuery>
{
    public GetMeetingByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id cannot be null");
    }
}