namespace UserService.DataAccess.Exceptions;

public abstract class UnauthorizedException(string message) : Exception(message);