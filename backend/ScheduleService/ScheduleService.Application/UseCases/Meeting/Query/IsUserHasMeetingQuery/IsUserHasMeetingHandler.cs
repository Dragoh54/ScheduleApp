using MediatR;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;

namespace ScheduleService.Application.UseCases.Meeting.Query.IsUserHasMeetingQuery;

public class IsUserHasMeetingHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<IsUserHasMeetingQuery, bool>
{
    public async Task<bool> Handle(IsUserHasMeetingQuery request, CancellationToken cancellationToken)
    {
        var meetings = await unitOfWork.Meetings
            .GetMeetingsForUserInRangeAsync(request.UserId, request.StartTime, request.EndTime, cancellationToken);

        return meetings.Any();
    }
}