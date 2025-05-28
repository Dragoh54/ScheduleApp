using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDto.Responses;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingByIdQuery;

public record GetMeetingByIdQuery : IRequest<MeetingWithParticipantsResponseDto>
{
    public Guid Id { get; set; }
}