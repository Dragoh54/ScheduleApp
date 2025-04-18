using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Enums;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.Meeting.Command.CreateMeetingCommand;

public class CreateMeetingHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateMeetingCommand, MeetingDto>
{
    public async Task<MeetingDto> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
    {
        var isBusy = await unitOfWork.Meetings.IsUserHasMeetingAsync(request.UserId, request.StartTime, request.EndTime, cancellationToken);
        if (isBusy)
        {
            throw new BadRequestException("User is already busy during this time.");
        }
        
        var isFreeByTemplate = await unitOfWork.AvailabilityTemplates.IsUserFreeAsync(request.UserId, request.StartTime, request.EndTime, cancellationToken);
        if (!isFreeByTemplate)
        {
            throw new BadRequestException("User is not available at this time according to their availability template.");
        }
        
        var meeting = await unitOfWork.Meetings.AddAsync(request.Adapt<DomainModel.Models.Meeting>(), cancellationToken);

        var success = await unitOfWork.Commit(cancellationToken);
        if (!success)
        {
            throw new BadRequestException("Failed to create meeting");
        }
        
        return meeting.Adapt<MeetingDto>();
    }
}