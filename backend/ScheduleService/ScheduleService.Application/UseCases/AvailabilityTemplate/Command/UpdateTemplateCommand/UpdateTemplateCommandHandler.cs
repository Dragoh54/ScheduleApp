using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.UpdateTemplateCommand;

public class UpdateTemplateCommandHandler : IRequestHandler<UpdateTemplateCommand, AvailabilityTemplateDto>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateTemplateCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AvailabilityTemplateDto> Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = await _unitOfWork.AvailabilityTemplates.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Template not found");

        request.Adapt(template);
        
        await _unitOfWork.AvailabilityTemplates.UpdateAsync(template, cancellationToken);
        
        var success = await _unitOfWork.Commit(cancellationToken);
        if (!success)
        {
            throw new BadRequestException("Failed to update availability template");
        }
        
        return template.Adapt<AvailabilityTemplateDto>();
    }
}