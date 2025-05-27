namespace ScheduleService.Application.Dto.AvailabilityTemplates.Requests.Commands;

public record DeleteTemplateRequestDto
{
    public Guid TemplateId { get; set; }
}