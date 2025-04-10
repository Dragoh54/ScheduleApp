using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetUserTemplatesQuery;

public class GetUserTemplatesHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetUserTemplatesQuery, IEnumerable<AvailabilityTemplateDto>>
{
    public Task<IEnumerable<AvailabilityTemplateDto>> Handle(GetUserTemplatesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}