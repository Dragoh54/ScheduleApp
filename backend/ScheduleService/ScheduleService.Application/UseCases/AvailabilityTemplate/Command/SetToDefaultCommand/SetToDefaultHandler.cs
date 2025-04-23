using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.SetToDefaultCommand;

public class SetToDefaultHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<SetToDefaultCommand, AvailabilityTemplateDto>
{
    public async Task<AvailabilityTemplateDto> Handle(SetToDefaultCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.AvailabilityTemplates.SetDefaultTemplateAsync(request.UserId, request.TemplateId, cancellationToken);
        
        var success = await unitOfWork.Commit(cancellationToken);
        if (!success)
        {
            throw new BadRequestException("Failed to set to default availability template");
        }
        
        var updatedTemplate = await unitOfWork.AvailabilityTemplates.GetByIdAsync(request.TemplateId, cancellationToken);
        
        return updatedTemplate.Adapt<AvailabilityTemplateDto>();
    }
}