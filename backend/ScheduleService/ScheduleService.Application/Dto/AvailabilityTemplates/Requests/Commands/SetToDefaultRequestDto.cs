namespace ScheduleService.Application.Dto.AvailabilityTemplates.Requests.Commands;

public record SetToDefaultRequestDto
{
    public Guid TemplateId { get; set; }
}