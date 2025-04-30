using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.UseCases.Meetings.Command.CreateMeetingCommand;

public class CreateMeetingHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateMeetingCommand, MeetingDto>
{
    public async Task<MeetingDto> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
    {
        //TODO: CHECK IF USER FREE FROM SCHEDULE SERVICE

        var meeting = await unitOfWork.MeetingRepository.Add(request.Adapt<Meeting>(), cancellationToken)
            ?? throw new BadRequestException("Meeting not created");

        await unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
        
        //TODO: THINK ABOUT NOTIFY THROUGH SIGNALR
        
        return meeting.Adapt<MeetingDto>();
    }
}