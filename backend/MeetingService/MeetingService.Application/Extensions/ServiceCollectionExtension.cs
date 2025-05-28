using System.Reflection;
using FluentValidation;
using MediatR;
using MeetingService.Application.Interfaces.Providers;
using MeetingService.Application.Interfaces.RabbitMQ;
using MeetingService.Application.Interfaces.RabbitMQ.Producers;
using MeetingService.Application.Interfaces.RabbitMQ.Services;
using MeetingService.Application.Interfaces.Services;
using MeetingService.Application.Providers;
using MeetingService.Application.RabbitMQ;
using MeetingService.Application.RabbitMQ.Options;
using MeetingService.Application.RabbitMQ.Producers;
using MeetingService.Application.RabbitMQ.Services;
using MeetingService.Application.Services;
using MeetingService.Application.Validations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
        services.AddScoped<IEmailTokenService, EmailTokenService>();
        services.AddScoped<IEmailNotificationService, EmailNotificationService>();

        services.AddScoped<IParticipantCacheService, ParticipantCacheService>();
        
        services.AddScoped<IScheduledJobsService, ScheduledJobsService>();
    }

    public static void AddProviders(this IServiceCollection services)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IEmailTokenProvider, EmailTokenProvider>();
    }
    
    public static void AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMQConnectionOptions>(configuration.GetSection("RabbitMQ"));
            
        services.AddSingleton<IRabbitMQConnection>(provider =>
            { 
                var rabbitMQConnectionOptions = provider.GetRequiredService<IOptions<RabbitMQConnectionOptions>>().Value;

                return new RabbitMQConnection(rabbitMQConnectionOptions);
            }
        );

        services.AddScoped<IMessageProducer, MessageProducer>();
        services.AddScoped<IRabbitMQService, RabbitMQService>();
    }
}