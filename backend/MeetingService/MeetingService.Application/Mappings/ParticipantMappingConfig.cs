using Mapster;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.Application.UseCases.Participants.Command.AddParticipantToMeetingCommand;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.Mappings;

public class ParticipantMappingConfig
{
    public static void RegisterMappers()
    {
        TypeAdapterConfig<AddParticipantToMeetingCommand, Participant>.NewConfig()
            .Map(dest => dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.MeetingId, src => src.MeetingId)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName)
            .Map(dest => dest.Status, src => src.Status);
        
        TypeAdapterConfig<Participant, ParticipantDto>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.MeetingId, src => src.MeetingId)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.Meeting, src => src.Meeting.Adapt<MeetingDto>());
    }
}