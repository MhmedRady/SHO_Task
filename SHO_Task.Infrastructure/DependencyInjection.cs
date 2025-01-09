using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SHO_Task.Application.Abstractions;
using SHO_Task.Domain;
using SHO_Task.Domain.BuildingBlocks;
using SHO_Task.Infrastructure.Clock;
using SHO_Task.Infrastructure.Interceptors;
using SHO_Task.Infrastructure.Repositories;

namespace SHO_Task.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        AddPersistence(
            services,
            configuration);

        return services;
    }

    private static void AddPersistence(
        IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DatabaseConnectionString") ??
                                  throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(connectionString).UseSnakeCaseNamingConvention()
                .AddInterceptors(new SoftDeleteInterceptor()));

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IShippingOrderRepository, ShippingOrderRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));


    }
}
