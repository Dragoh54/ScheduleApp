using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDto.Requests;
using MeetingService.Application.Dtos.MeetingDto.Responses;

namespace MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingInformationCommand;

public record UpdateMeetingInformationCommand : IRequest<MeetingWithParticipantsResponseDto>
{
    public UpdateMeetingInformationCommand()
    {
    }
    
    public UpdateMeetingInformationCommand(Guid meetingId, UpdateMeetingInformationRequestDto requestDto)
    {
        Id = meetingId;
        Title = requestDto.Title;
        Description = requestDto.Description;
    }
    
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}