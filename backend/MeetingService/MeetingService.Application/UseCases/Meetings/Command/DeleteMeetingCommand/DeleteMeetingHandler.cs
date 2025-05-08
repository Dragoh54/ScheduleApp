using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.Application.Interfaces.Providers;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.UseCases.Meetings.Command.DeleteMeetingCommand;

public class DeleteMeetingHandler(
    IUnitOfWork unitOfWork,
    IEmailService emailService,
    IJwtProvider jwtProvider
    ) : IRequestHandler<DeleteMeetingCommand, MeetingWithParticipantsDto>
{
    public async Task<MeetingWithParticipantsDto> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = await unitOfWork.MeetingRepository.GetMeetingWithParticipants(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        var idFromToken = await jwtProvider.GetUserIdFromToken(request.Token);
        
        if (meeting.OrganizationUserId != idFromToken)
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
        
        var meetingTitle = meeting.Title!;
        
        await Parallel.ForEachAsync(
            meeting.Participants,
            cancellationToken,
            async (participant, ct) =>
            {
                await SendEmailAsync(participant, meetingTitle, ct);
            });
        
        return meeting.Adapt<MeetingWithParticipantsDto>();
    }

    private Task SendEmailAsync(Participant participant, string meetingTitle, CancellationToken ct)
    {
        return Task.Run(() =>
        {
            BackgroundJob.Enqueue(() =>
                emailService.SendEmailAsync(
                    participant.Email,
                    "Meeting Deleted",
                    $"Meeting {meetingTitle} was deleted! ",
                    ct
                ));
        }, ct);
    }
}