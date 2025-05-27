using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDto.Responses;
using MeetingService.Application.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantQuery;

public class GetParticipantQueryHandler : IRequestHandler<GetParticipantQuery, ParticipantWithMeetingResponseDto>
{
    GetParticipantQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    private readonly IUnitOfWork _unitOfWork;
    
    public async Task<ParticipantWithMeetingResponseDto> Handle(GetParticipantQuery request, CancellationToken cancellationToken)
    {
        var participant = await _unitOfWork.ParticipantRepository.GetParticipant(request.MeetingId, request.UserId, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        cancellationToken.ThrowIfCancellationRequested();

        return participant.Adapt<ParticipantWithMeetingResponseDto>();
    }
}