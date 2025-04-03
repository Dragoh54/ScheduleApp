namespace UserService.DataAccess.Exceptions;

public class AlreadyExistsException(string message) : Exception(message);