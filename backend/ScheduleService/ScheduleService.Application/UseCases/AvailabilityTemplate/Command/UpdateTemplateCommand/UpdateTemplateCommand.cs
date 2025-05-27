using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.AvailabilityTemplates.Requests;
using ScheduleService.Application.Dto.AvailabilityTemplates.Requests.Commands;
using ScheduleService.Application.Dto.AvailabilityTemplates.Responses;
using ScheduleService.Application.Dto.DayOfWeekSchedules;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.UpdateTemplateCommand;

public record UpdateTemplateCommand : IRequest<AvailabilityTemplateResponseDto>
{
    public UpdateTemplateCommand()
    {
    }

    public UpdateTemplateCommand(UpdateTemplateRequestDto dto)
    {
        Id = dto.Id;
        Name = dto.Name;
        Schedule = dto.Schedule;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<DayOfWeekScheduleDto> Schedule { get; set; } = [];
}