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
        var meeting = await unitOfWork.MeetingRepository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");

        var organizationUser =
            await unitOfWork.ParticipantRepository.GetParticipant(meeting.Id, meeting.OrganizationUserId, cancellationToken)
            ?? throw new NotFoundException("Organization user not found");
        
        var success = await unitOfWork.MeetingRepository.Delete(meeting, cancellationToken);

        if (!success)
        {
            throw new BadRequestException("Failed to delete meeting");
        }
        
        await unitOfWork.SaveChangesAsync();

        //TODO: THINK ABOUT NOTIFY THROUGH SIGNALR
        BackgroundJob.Enqueue(() =>
            emailService.SendEmailAsync(
                organizationUser.Email,
                "Meeting deleted",
                $"Meeting {meeting.Title} was successfully deleted.",
                cancellationToken
            ));
        
        cancellationToken.ThrowIfCancellationRequested();
        
        
        
        return meeting.Adapt<MeetingDto>();
    }
}