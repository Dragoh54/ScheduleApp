using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using UserService.DataAccess.Exceptions;

namespace UserService.Api.Middlewares;

public class ExceptionHandlerMiddleware(
    RequestDelegate next, 
    ILogger<ExceptionHandlerMiddleware> logger
    )
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context); 
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception switch
        {
            BadRequestException or ArgumentException or ValidationException => StatusCodes.Status400BadRequest,
            UnauthorizedException  => StatusCodes.Status401Unauthorized,
            UnauthorizedAccessException => StatusCodes.Status403Forbidden,
            NotFoundException => StatusCodes.Status404NotFound,
            AlreadyExistsException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        var response = new
        {
            statusCode = context.Response.StatusCode,
            message = exception.Message,
            details = exception.InnerException?.Message
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}