using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.AvailabilityTemplates.Responses;
using ScheduleService.Application.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.AvailabilityTemplate.Command.UpdateTemplateCommand;

public class UpdateTemplateCommandHandler : IRequestHandler<UpdateTemplateCommand, AvailabilityTemplateResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateTemplateCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AvailabilityTemplateResponseDto> Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = await _unitOfWork.AvailabilityTemplates.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Template not found");

        request.Adapt(template);
        
        await _unitOfWork.AvailabilityTemplates.UpdateAsync(template, cancellationToken);
        
        return template.Adapt<AvailabilityTemplateResponseDto>();
    }
}