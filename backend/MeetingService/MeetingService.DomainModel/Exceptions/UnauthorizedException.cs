namespace MeetingService.DomainModel.Exceptions;

public abstract class UnauthorizedException(string message) : Exception(message);