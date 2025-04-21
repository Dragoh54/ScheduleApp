using MediatR;

namespace ScheduleService.Application.UseCases.Meeting.Query.IsUserHasMeetingQuery;

public record IsUserHasMeetingQuery : IRequest<bool>
{
    public Guid UserId { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
}