using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.AddTemplateCommand;

public class AddTemplateHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<AddTemplateCommand, AvailabilityTemplateDto>
{
    public async Task<AvailabilityTemplateDto> Handle(AddTemplateCommand request, CancellationToken cancellationToken)
    {
        var addedTemplate = await unitOfWork.AvailabilityTemplates.AddAsync(request.Adapt<DomainModel.Models.AvailabilityTemplate>(), cancellationToken);
            
        if (!await unitOfWork.Commit(cancellationToken))
        {
            throw new BadRequestException("Failed to add template to database");
        }
        
        return addedTemplate.Adapt<AvailabilityTemplateDto>();
    }
}