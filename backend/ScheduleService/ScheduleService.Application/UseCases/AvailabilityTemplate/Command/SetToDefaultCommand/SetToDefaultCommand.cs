using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.SetToDefaultCommand;

public record SetToDefaultCommand : IRequest<AvailabilityTemplateDto>
{
    public Guid TemplateId { get; set; }
    public Guid UserId { get; set; }
}