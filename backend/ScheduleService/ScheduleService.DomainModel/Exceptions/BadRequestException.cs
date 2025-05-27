namespace ScheduleService.DomainModel.Exceptions;

public class BadRequestException(string message) : Exception(message);