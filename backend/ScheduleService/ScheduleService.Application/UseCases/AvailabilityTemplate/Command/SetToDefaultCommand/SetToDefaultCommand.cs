using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.AvailabilityTemplates.Responses;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.SetToDefaultCommand;

public record SetToDefaultCommand : IRequest<AvailabilityTemplateResponseDto>
{
    public Guid TemplateId { get; set; }
    public Guid UserId { get; set; }
}