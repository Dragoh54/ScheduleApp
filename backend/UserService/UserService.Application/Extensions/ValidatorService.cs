using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Dto;
using UserService.Application.Validator;
using UserService.Application.Validator.UserValidators;

namespace UserService.Application.Extensions;

public static class ValidatorService
{
    public static void AddValidation(this IServiceCollection services)
    {
        services.AddControllersWithViews(options =>
        {
            options.Filters.Add<ValidationFilter>();
        });
        services.AddScoped<IValidator<RegisterDto>, RegisterUserValidator>();
    }
}