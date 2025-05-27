using MediatR;
using ScheduleService.Application.Dto.AvailabilityTemplates.Requests.Queries;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Query.IsUserFreeQuery;

public record IsUserFreeQuery : IRequest<bool>
{
    public IsUserFreeQuery()
    {
    }

    public IsUserFreeQuery(Guid userId, IsUserFreeRequestDto dto)
    {
        UserId = userId;
        StartDate = dto.StartDate;
        EndDate = dto.EndDate;
    }

    public Guid UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}