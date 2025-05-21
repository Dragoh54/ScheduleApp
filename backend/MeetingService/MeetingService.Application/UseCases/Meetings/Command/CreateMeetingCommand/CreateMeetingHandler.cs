using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Api.Interfaces.Notifiers;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.Application.Interfaces.Providers;
using MeetingService.Application.Interfaces.Services;
using MeetingService.Application.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.UseCases.Meetings.Command.CreateMeetingCommand;

public class CreateMeetingHandler(
    IUnitOfWork unitOfWork,
    IJwtProvider jwtProvider
    ) : IRequestHandler<CreateMeetingCommand, MeetingDto>
{
    public async Task<MeetingDto> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
    {
        var organizationUserId = await jwtProvider.GetUserIdFromToken(request.AccessToken); 
        
        var meeting = request.Adapt<Meeting>();
        meeting.OrganizationUserId = organizationUserId;

        var notifyTime = request.NotifyTime ?? meeting.StartTime.AddDays(-1);
        if (notifyTime < DateTime.Now)
        {
            throw new BadRequestException("Notify time cannot be in past");
        }

        meeting.NotifyTime = notifyTime;
        
        meeting = await unitOfWork.MeetingRepository.Add(meeting, cancellationToken)
            ?? throw new BadRequestException("Meeting not created");

        await unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return meeting.Adapt<MeetingDto>();
    }
}