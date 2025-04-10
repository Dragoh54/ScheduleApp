namespace UserService.DataAccess.Exceptions;

public class NotFoundException(string message) : Exception(message);