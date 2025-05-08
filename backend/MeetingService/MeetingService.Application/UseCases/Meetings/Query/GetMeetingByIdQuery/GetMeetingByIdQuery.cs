using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingByIdQuery;

public record GetMeetingByIdQuery : IRequest<MeetingWithParticipantsDto>
{
    public Guid Id { get; set; }
}