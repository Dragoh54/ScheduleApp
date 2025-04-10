using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.UpdateTemplateCommand;

public class UpdateTemplateHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<UpdateTemplateCommand, AvailabilityTemplateDto>
{
    public async Task<AvailabilityTemplateDto> Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.AvailabilityTemplates.UpdateAsync(request.Adapt<DomainModel.Models.AvailabilityTemplate>(), cancellationToken);
        var success = await unitOfWork.Commit(cancellationToken);

        if (!success)
        {
            throw new BadRequestException("Failed to update availability template");
        }
        
        return request.Adapt<AvailabilityTemplateDto>();
    }
}