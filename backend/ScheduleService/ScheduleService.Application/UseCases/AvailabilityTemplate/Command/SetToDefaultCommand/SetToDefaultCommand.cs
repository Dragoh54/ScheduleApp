using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.AvailabilityTemplates.Requests.Commands;
using ScheduleService.Application.Dto.AvailabilityTemplates.Responses;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.SetToDefaultCommand;

public record SetToDefaultCommand : IRequest<AvailabilityTemplateResponseDto>
{
    public SetToDefaultCommand()
    {
    }

    public SetToDefaultCommand(SetToDefaultRequestDto dto)
    {
        TemplateId = dto.TemplateId;
        UserId = dto.UserId;
    }

    public Guid TemplateId { get; set; }
    public Guid UserId { get; set; }
}