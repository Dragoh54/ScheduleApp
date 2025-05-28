using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ScheduleService.Application.Interfaces.RabbitMQ;
using ScheduleService.Application.Interfaces.RabbitMQ.Consumers;
using ScheduleService.Application.RabbitMQ.Connection;
using ScheduleService.Application.RabbitMQ.Consumers;
using ScheduleService.Application.RabbitMQ.Options;
using ScheduleService.Application.Validation;

namespace ScheduleService.Application.Extensions;

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
    
    public static void AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IRabbitMQConnection>(provider =>
            {
                var rabbitMQConnectionOptions = provider.GetRequiredService<IOptions<RabbitMQConnectionOptions>>().Value;

                return new RabbitMQConnection(rabbitMQConnectionOptions);
            }
        );

        services.AddTransient<IMessageConsumer, MessageConsumer>();
        services.AddHostedService<SubscriptionConsumer>();
    }
}