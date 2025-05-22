using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.AvailabilityTemplates.Requests;
using ScheduleService.Application.Dto.AvailabilityTemplates.Requests.Commands;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.DeleteTemplateCommand;

public record DeleteTemplateCommand : IRequest<bool>
{
    public DeleteTemplateCommand()
    {
    }

    public DeleteTemplateCommand(DeleteTemplateRequestDto dto) => Id = dto.TemplateId;

    public Guid Id { get; set; }
}