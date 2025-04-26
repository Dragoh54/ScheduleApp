using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;

namespace MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingInformationCommand;

public record UpdateMeetingInformationCommand : IRequest<MeetingDto>
{
    public UpdateMeetingInformationCommand()
    {
    }
    
    public UpdateMeetingInformationCommand(Guid meetingId, UpdateMeetingInformationDto dto)
    {
        Id = meetingId;
        Title = dto.Title;
        Description = dto.Description;
    }
    
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}