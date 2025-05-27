using MediatR;
using ScheduleService.Application.Interfaces.UnitOfWork;

namespace ScheduleService.Application.UseCases.Meeting.Query.IsUserHasMeetingQuery;

public class IsUserHasMeetingQueryHandler : IRequestHandler<IsUserHasMeetingQuery, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public IsUserHasMeetingQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<bool> Handle(IsUserHasMeetingQuery request, CancellationToken cancellationToken)
    {
        var meetings = await _unitOfWork.Meetings.GetMeetingsForUserInRangeAsync(request.UserId, request.StartDate,
            request.EndDate, cancellationToken);
        
        var isUserHasAnyMeeting = meetings.Any(m => 
            m.StartTime < request.EndDate && 
            m.EndTime > request.StartDate);
        
        return !isUserHasAnyMeeting;
    }
}