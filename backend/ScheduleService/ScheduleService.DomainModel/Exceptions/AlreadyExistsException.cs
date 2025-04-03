namespace ScheduleService.DomainModel.Exceptions;

public class AlreadyExistsException(string message) : Exception(message);