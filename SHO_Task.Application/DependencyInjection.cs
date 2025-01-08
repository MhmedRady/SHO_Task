using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SHO_Task.Application.Abstractions.Behaviors;
using SHO_Task.Application.Orders;
using SHO_Task.Application.ShippingOrders;
using SHO_Task.Domain.BuildingBlocks;
using SHO_Task.Domain.ShippingOrders;
using System.Reflection;

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

        AddStrategies(services);

        services.AddScoped<IStatusTransitionStrategyFactory<ShippingOrder, ShippingOrderStatus>, StatusTransitionStrategyFactory>();

        return services;
    }

    private static void AddStrategies(IServiceCollection services)
    {
        Assembly assembly = typeof(IStatusTransitionStrategy<,>).Assembly;

        Type strategyType = typeof(IStatusTransitionStrategy<ShippingOrder, ShippingOrderStatus>);

        IEnumerable<Type> types = assembly.GetTypes().Where(
            t => strategyType.IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false }
        );

        foreach (Type type in types)
        {
            Type? interfaceType = type.GetInterface(strategyType.Name);
            if (interfaceType != null)
            {
                services.AddScoped(
                    interfaceType,
                    type);
            }
        }
    }
}
