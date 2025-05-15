using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.AvailabilityTemplates.Responses;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetUserTemplatesQuery;

public record GetUserTemplatesQuery : IRequest<IEnumerable<AvailabilityTemplateResponseDto>>
{
    public Guid UserId { get; set; }
}