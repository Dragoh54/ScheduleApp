using MediatR;
using MeetingService.Application.Dtos;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingByIdQuery;

public record GetMeetingByIdQuery : IRequest<MeetingDto>
{
    
}