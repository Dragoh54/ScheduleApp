namespace MeetingService.Application.Dtos.MeetingDto.Requests;

public record DeleteMeetingRequestDto
{
    public Guid Id { get; set; }
}