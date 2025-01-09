using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SHO_Task.Application.Abstractions.Behaviors;
using SHO_Task.Application.IntegrationEvents.SHO_Task.Application.IntegrationEvents;

namespace SHO_Task.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(
            configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

                configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));

                configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });


        services.AddValidatorsFromAssembly(
        typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        services.AddSingleton<IntegrationEventPublisher>();
        services.AddSingleton<RabbitMQPublisher>();

        return services;
    }
}
