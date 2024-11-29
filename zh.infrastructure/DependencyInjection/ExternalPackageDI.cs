using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using zh.application.Validators;

namespace zh.infrastructure.DependencyInjection;

public static class ExternalPackageDi
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddValidatorsFromAssemblyContaining<ValidateCreditCardQueryValidator>();
        return services;
    }
}