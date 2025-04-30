using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Meetings.Command.DeleteMeetingCommand;

public class DeleteMeetingHandler(
    IUnitOfWork unitOfWork,
    IEmailService emailService
    ) : IRequestHandler<DeleteMeetingCommand, MeetingDto>
{
    public async Task<MeetingDto> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
    {
        //TODO: GET WITH PARTICIPANTS TO NOTIFY THEM
        var meeting = await unitOfWork.MeetingRepository.GetMeetingWithParticipants(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");

        if (meeting.OrganizationUserId != request.UserId)
        {
            throw new BadRequestException("You are not an organization user");
        }
        
        var success = await unitOfWork.MeetingRepository.Delete(meeting, cancellationToken);

        if (!success)
        {
            throw new BadRequestException("Failed to delete meeting");
        }
        
        await unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
        
        //TODO: THINK ABOUT NOTIFY ALL PARTICIPANTS THROUGH SIGNALR
        
        return meeting.Adapt<MeetingDto>();
    }
}