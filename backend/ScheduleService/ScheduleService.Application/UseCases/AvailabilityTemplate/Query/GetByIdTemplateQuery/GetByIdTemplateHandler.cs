using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetByIdTemplateQuery;

public class GetByIdTemplateHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetByIdTemplateQuery, AvailabilityTemplateDto>
{
    public async Task<AvailabilityTemplateDto> Handle(GetByIdTemplateQuery request, CancellationToken cancellationToken)
    {
        var template = await unitOfWork.AvailabilityTemplates.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Template not found");
        
        // var success = await unitOfWork.Commit(cancellationToken);
        //
        // if (!success)
        // {
        //     throw new BadRequestException("Failed to get availability template");
        // }

        return template.Adapt<AvailabilityTemplateDto>();
    }
}