using MeetingService.DomainModel.Interfaces;

namespace MeetingService.DomainModel.Models;

public class IdEntity : IEntity
{
    public Guid Id { get; set; }
}