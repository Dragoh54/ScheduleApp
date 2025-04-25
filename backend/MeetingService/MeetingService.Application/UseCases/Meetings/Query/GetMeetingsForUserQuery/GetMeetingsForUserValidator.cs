using FluentValidation;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingsForUserQuery;

public class GetMeetingsForUserValidator : AbstractValidator<GetMeetingsForUserQuery>
{
    public GetMeetingsForUserValidator()
    {
        RuleFor(x => x.OrganizerId)
            .NotNull().WithMessage("OrganizerId is required");
    }
}