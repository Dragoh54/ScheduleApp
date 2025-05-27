using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDto.Requests;
using MeetingService.Application.Dtos.ParticipantDto.Responses;
using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.UseCases.Participants.Command.AddParticipantToMeetingCommand;

public record AddParticipantWithGrpcToMeetingCommand : IRequest<ParticipantWithMeetingResponseDto>
{
    public AddParticipantWithGrpcToMeetingCommand()
    {
    }

    public AddParticipantWithGrpcToMeetingCommand(Guid meetingId, Guid userId, string url, string accessToken)
    {
        MeetingId = meetingId;
        UserId = userId;
        CallbackUrl = url;
        AccessToken = accessToken;
    }

    public Guid MeetingId { get; set; }
    public Guid UserId { get; set; }
    public string CallbackUrl { get; set; } = string.Empty; 
    public string AccessToken { get; set; } = string.Empty;
}