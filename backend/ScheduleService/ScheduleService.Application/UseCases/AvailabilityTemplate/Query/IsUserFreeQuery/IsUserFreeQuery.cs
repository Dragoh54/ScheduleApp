using MediatR;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Query.IsUserFreeQuery;

public record IsUserFreeQuery : IRequest<bool>
{
    public Guid UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}