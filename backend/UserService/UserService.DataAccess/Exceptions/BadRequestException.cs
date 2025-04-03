namespace UserService.DataAccess.Exceptions;

public class BadRequestException(string message) : Exception(message);