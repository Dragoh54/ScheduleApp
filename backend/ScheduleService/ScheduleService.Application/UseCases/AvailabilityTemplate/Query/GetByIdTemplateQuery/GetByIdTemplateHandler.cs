using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetByIdTemplateQuery;

public class GetByIdTemplateHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetByIdTemplateQuery, AvailabilityTemplateDto>
{
    public Task<AvailabilityTemplateDto> Handle(GetByIdTemplateQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}