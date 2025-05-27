using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDto.Requests;
using MeetingService.Application.Dtos.MeetingDto.Responses;

namespace MeetingService.Application.UseCases.Meetings.Command.DeleteMeetingCommand;

public record DeleteMeetingCommand : IRequest<MeetingWithParticipantsResponseDto>
{
    public DeleteMeetingCommand(DeleteMeetingRequestDto requestDto, string token)
    {
        Token = token;
        Id = requestDto.Id;
    }
    public Guid Id { get; set; }
    public string Token { get; set; } = string.Empty; 
}