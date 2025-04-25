using FluentValidation;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingsOrganizedByUserQuery;

public class GetMeetingsOrganizedByUserValidator : AbstractValidator<GetMeetingsOrganizedByUserQuery>
{
    public GetMeetingsOrganizedByUserValidator()
    {
        RuleFor(x => x.OrganizerId)
            .NotNull().WithMessage("OrganizerId is required");
    }
}