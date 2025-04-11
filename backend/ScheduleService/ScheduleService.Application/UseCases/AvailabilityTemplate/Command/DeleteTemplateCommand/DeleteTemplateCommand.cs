using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.DeleteTemplateCommand;

public class DeleteTemplateCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}