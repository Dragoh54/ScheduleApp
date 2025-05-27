using Mapster;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDto.Responses;
using MeetingService.Application.Dtos.ParticipantDto.Responses;
using MeetingService.Application.UseCases.Meetings.Command.CreateMeetingCommand;
using MeetingService.Application.UseCases.Meetings.Command.RescheduleMeetingCommand;
using MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingInformationCommand;
using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.Mappings;

public class MeetingMappingConfig
{
    public static void RegisterMappers()
    {
        RegisterCommandToCommandMappers();
    }

    private static void RegisterCommandToCommandMappers()
    {
        TypeAdapterConfig<CreateMeetingCommand, Meeting>.NewConfig()
            .Ignore(dest => dest.NotifyTime)
            .Map(dest => dest.Id, _ => Guid.NewGuid())
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.StartTime, src => src.StartTime)
            .Map(dest => dest.EndTime, src => src.EndTime)
            .Map(dest => dest.Status, src => MeetingStatus.Scheduled)
            .Map(dest => dest.CreatedAt, _ => DateTime.UtcNow)
            .Map(dest => dest.Participants, _ => new List<Participant>());

        TypeAdapterConfig<Meeting, MeetingResponseDto>.NewConfig()
            .Map(dest => dest.OrganizationUserId, src => src.OrganizationUserId)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt)
            .Map(dest => dest.StartTime, src => src.StartTime)
            .Map(dest => dest.EndTime, src => src.EndTime)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.NotifyTime , src => src.NotifyTime);
            
        TypeAdapterConfig<Meeting, MeetingWithParticipantsResponseDto>.NewConfig()
            .Map(dest => dest.OrganizationUserId, src => src.OrganizationUserId)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt)
            .Map(dest => dest.StartTime, src => src.StartTime)
            .Map(dest => dest.EndTime, src => src.EndTime)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.NotifyTime , src => src.NotifyTime)
            .Map(dest => dest.Participants, src => src.Participants.Adapt<IEnumerable<ParticipantResponseDto>>());

        TypeAdapterConfig<RescheduleMeetingCommand, Meeting>.NewConfig()
            .Map(dest => dest.StartTime, src => src.StartTime)
            .Map(dest => dest.EndTime, src => src.EndTime)
            .Map(dest => dest.NotifyTime, src => src.NotifyTime)
            .Map(dest => dest.Status, _ => MeetingStatus.Rescheduled);

        TypeAdapterConfig<UpdateMeetingInformationCommand, Meeting>.NewConfig()
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description);
    }
}