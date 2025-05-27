using Mapster;
using MediatR;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.Application.Interfaces.Providers;
using MeetingService.Application.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.UseCases.Meetings.Command.CreateMeetingCommand;

public class CreateMeetingHandler : IRequestHandler<CreateMeetingCommand, MeetingDto>
{
    public CreateMeetingHandler(IUnitOfWork unitOfWork, IJwtProvider jwtProvider)
    {
        _unitOfWork = unitOfWork;
        _jwtProvider = jwtProvider;
    }
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtProvider _jwtProvider;
    
    public async Task<MeetingDto> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
    {
        var organizationUserId = await _jwtProvider.GetUserIdFromToken(request.AccessToken); 
        
        var meeting = request.Adapt<Meeting>();
        meeting.OrganizationUserId = organizationUserId;

        var notifyTime = request.NotifyTime ?? meeting.StartTime.AddDays(-1);
        if (notifyTime < DateTime.Now)
        {
            throw new BadRequestException("Notify time cannot be in past");
        }

        meeting.NotifyTime = notifyTime;
        
        meeting = await _unitOfWork.MeetingRepository.Add(meeting, cancellationToken)
            ?? throw new BadRequestException("Meeting not created");

        await _unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return meeting.Adapt<MeetingDto>();
    }
}