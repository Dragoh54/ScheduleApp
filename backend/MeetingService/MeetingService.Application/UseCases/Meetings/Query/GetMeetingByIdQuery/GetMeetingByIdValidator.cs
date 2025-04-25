using FluentValidation;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingByIdQuery;

public class GetMeetingByIdValidator : AbstractValidator<GetMeetingByIdQuery>
{
    public GetMeetingByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id cannot be null");
    }
}