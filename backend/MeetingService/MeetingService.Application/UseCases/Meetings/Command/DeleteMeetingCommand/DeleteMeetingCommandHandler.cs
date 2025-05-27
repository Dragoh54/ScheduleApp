using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Api.Interfaces.Notifiers;
using MeetingService.Application.Dtos.MeetingDto.Responses;
using MeetingService.Application.Handlers.Email;
using MeetingService.Application.Interfaces.Providers;
using MeetingService.Application.Interfaces.Services;
using MeetingService.Application.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.UseCases.Meetings.Command.DeleteMeetingCommand;

public class DeleteMeetingCommandHandler : IRequestHandler<DeleteMeetingCommand, MeetingWithParticipantsResponseDto>
{
    public DeleteMeetingCommandHandler(
        IUnitOfWork unitOfWork, 
        IEmailService emailService,
        IJwtProvider jwtProvider, 
        IMeetingNotifier notifier)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
        _jwtProvider = jwtProvider;
        _notifier = notifier;
    }

    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    private readonly IJwtProvider _jwtProvider;
    private readonly IMeetingNotifier _notifier;
    public async Task<MeetingWithParticipantsResponseDto> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = await _unitOfWork.MeetingRepository.GetMeetingWithParticipants(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        var idFromToken = await _jwtProvider.GetUserIdFromToken(request.Token);
        
        if (meeting.OrganizationUserId != idFromToken)
        {
            throw new BadRequestException("You are not an organization user");
        }
        
        var success = await _unitOfWork.MeetingRepository.Delete(meeting, cancellationToken);

        if (!success)
        {
            throw new BadRequestException("Failed to delete meeting");
        }
        
        await _unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
        
        var meetingTitle = meeting.Title!;
        
        await Parallel.ForEachAsync(
            meeting.Participants,
            cancellationToken,
            async (participant, ct) =>
            {
                await SendEmailAsync(participant, meetingTitle, ct);
            });
        
        await _notifier.NotifyMeetingDeletedAsync(meeting.Id, meetingTitle);
        
        return meeting.Adapt<MeetingWithParticipantsResponseDto>();
    }

    private Task SendEmailAsync(Participant participant, string meetingTitle, CancellationToken ct)
    {
        return Task.Run(() =>
        {
            BackgroundJob.Enqueue(() =>
                _emailService.SendEmailAsync(
                    participant.Email,
                    "Meeting Deleted",
                    MeetingEmailMessageHandler.MeetingDeletedBody(meetingTitle),
                    ct
                ));
        }, ct);
    }
}