namespace MeetingService.Application.Dtos.MeetingDto.Requests;

public record UpdateMeetingInformationRequestDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
}