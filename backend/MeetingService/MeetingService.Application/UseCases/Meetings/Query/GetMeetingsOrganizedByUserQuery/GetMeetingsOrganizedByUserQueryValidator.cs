using FluentValidation;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingsOrganizedByUserQuery;

public class GetMeetingsOrganizedByUserQueryValidator : AbstractValidator<GetMeetingsOrganizedByUserQuery>
{
    public GetMeetingsOrganizedByUserQueryValidator()
    {
        RuleFor(x => x.OrganizerId)
            .NotNull().WithMessage("OrganizerId is required");
    }
}