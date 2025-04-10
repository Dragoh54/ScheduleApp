using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.SetToDefaultCommand;

public class SetToDefaultCommand : IRequest<AvailabilityTemplateDto>
{
    public Guid TemplateId { get; set; }
    public Guid UserId { get; set; }
}