namespace ScheduleService.Application.Dto.AvailabilityTemplates.Requests;

public record DeleteTemplateRequestDto
{
    public Guid TemplateId { get; set; }
    public Guid UserId { get; set; }
}