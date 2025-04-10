using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetDefaultTemplateQuery;

public class GetDefaultTemplateHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetDefaultTemplateQuery, AvailabilityTemplateDto>
{
    public Task<AvailabilityTemplateDto> Handle(GetDefaultTemplateQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}