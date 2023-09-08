using Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Domain.Common;
using Order.Infrastructure.Persistence;
using Order.Infrastructure.Repositories;
using Shared.Base;

namespace Order.Infrastructure;

public static class DependencyInjectionOrderInfrastructureExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, DependencyOptions dependencyOptions)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddCoreInfrastructure(configuration, dependencyOptions);

        services.AddDbContext<OrderDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("OrderDb"));
        });

        services.AddScoped(typeof(IOrderRepository) ,typeof(OrderRepository));
        services.AddScoped(typeof(IOrderUnitOfWork), typeof(OrderUnitOfWork));
    }
}