using System.Reflection;
using FluentValidation;
using MediatR;
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
}