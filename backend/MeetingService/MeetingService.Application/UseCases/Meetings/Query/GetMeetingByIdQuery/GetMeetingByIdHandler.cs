using Mapster;
using MediatR;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.Application.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingByIdQuery;

public class GetMeetingByIdHandler : IRequestHandler<GetMeetingByIdQuery, MeetingWithParticipantsDto>
{
    public GetMeetingByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    private readonly IUnitOfWork _unitOfWork;
    public async Task<MeetingWithParticipantsDto> Handle(GetMeetingByIdQuery request, CancellationToken cancellationToken)
    {
        var meeting = await _unitOfWork.MeetingRepository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        cancellationToken.ThrowIfCancellationRequested();

        return meeting.Adapt<MeetingWithParticipantsDto>();
    }
}