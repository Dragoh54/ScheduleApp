using System.Reflection;
using FluentValidation;
using MediatR;
using MeetingService.Application.Interfaces.Providers;
using MeetingService.Application.Interfaces.Services;
using MeetingService.Application.Providers;
using MeetingService.Application.Services;
using MeetingService.Application.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace MeetingService.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddMediatRServices(this IServiceCollection services)
    {

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IEmailCacheService, EmailCacheService>();
        services.AddScoped<ITokenService, TokenService>();
    }

    public static void AddProviders(this IServiceCollection services)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IEmailTokenProvider, EmailTokenProvider>();
    }
}