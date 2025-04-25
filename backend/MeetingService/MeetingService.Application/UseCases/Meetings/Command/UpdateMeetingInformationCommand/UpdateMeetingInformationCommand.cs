using MediatR;
using MeetingService.Application.Dtos;

namespace MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingInformationCommand;

public record UpdateMeetingInformationCommand : IRequest<MeetingDto>
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}