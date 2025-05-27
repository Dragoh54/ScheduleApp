using MediatR;
using ScheduleService.Application.Dto.Meetings.Requests.Queries;

namespace ScheduleService.Application.UseCases.Meeting.Query.IsUserHasMeetingQuery;

public class IsUserHasMeetingQuery : IRequest<bool>
{
    public IsUserHasMeetingQuery()
    {
    }

    public IsUserHasMeetingQuery(Guid userId, IsUserHasMeetingsRequestDto dto)
    {
        UserId = userId;
        StartDate = dto.StartDate;
        EndDate = dto.EndDate;
    }

    public Guid UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}