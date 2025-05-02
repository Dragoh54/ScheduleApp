using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.UseCases.Participants.Command.ConfirmParticipationCommand;

public class ConfirmParticipationHandler(
    IUnitOfWork unitOfWork,
    IEmailService emailService
    ) : IRequestHandler<ConfirmParticipationCommand, ParticipantDto>
{
    public async Task<ParticipantDto> Handle(ConfirmParticipationCommand request, CancellationToken cancellationToken)
    {
        // var participant = await unitOfWork.ParticipantRepository.GetParticipantWithMeeting(request.MeetingId, request.UserId, cancellationToken)
        //                   ?? throw new NullReferenceException("Participant not found");
        //
        // //TODO: THINK ABOUT DELETING IN THIS HANDLER
        // if (request.Status == ParticipationStatus.Declined)
        // {
        //     await DeleteParticipant(participant, cancellationToken);
        //
        //     return participant.Adapt<ParticipantDto>();
        // }
        //
        // participant.Status = request.Status;
        //
        // var updatedParticipant = await unitOfWork.ParticipantRepository.Update(participant, cancellationToken)
        //     ?? throw new BadRequestException("Participant not updated");
        //
        // await unitOfWork.SaveChangesAsync();
        //
        // cancellationToken.ThrowIfCancellationRequested();
        //
        // BackgroundJob.Enqueue(() =>
        //     emailService.SendEmailAsync(
        //         participant.Email,
        //         $"Your status of participating updated",
        //         $"Your status for meeting {participant.Meeting.Title} is {participant.Status}!",
        //         cancellationToken
        //     ));
        //
        // return updatedParticipant.Adapt<ParticipantDto>();
        
        throw new NotImplementedException();
    }

    private async Task DeleteParticipant(Participant participant, CancellationToken cancellationToken)
    {
        var success = await unitOfWork.ParticipantRepository.Delete(participant, cancellationToken);
        
        if (!success)
        {
            throw new BadRequestException("Failed to delete declined participant");
        }
        
        await unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
    }
    
    private void SendNotifications(Meeting meeting, Participant participant, CancellationToken cancellationToken)
    {
        var notifyBeforeDay = meeting.StartTime.AddDays(-1);
        var notifyBeforeHour = meeting.StartTime.AddHours(-1);
        
        BackgroundJob.Schedule(() => 
            emailService.SendEmailAsync(
                participant.Email,
                $"Reminder",
                $"Meeting {meeting.Title} will be next day at {meeting.StartTime:hh:mm}!",
                cancellationToken
            ), notifyBeforeDay);
        
        BackgroundJob.Schedule(() => 
            emailService.SendEmailAsync(
                participant.Email,
                $"Reminder",
                $"Meeting {meeting.Title} starts soon: {meeting.StartTime:hh:mm}!",
                cancellationToken
            ), notifyBeforeHour);
    }
}