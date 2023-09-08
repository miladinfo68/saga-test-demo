using Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stock.Domain.Common;
using Stock.Infrastructure.Persistence;
using Stock.Infrastructure.Repositories;
using Stock.Infrastructure.UnitOfWork;

namespace Stock.Infrastructure;
public static class DependencyInjectionStockInfrastructureExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, DependencyOptions dependencyOptions)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddCoreInfrastructure(configuration, dependencyOptions);

        services.AddDbContext<StockDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("StockDb"));
        });

        services.AddScoped(typeof(IStockRepository), typeof(StockRepository));
        services.AddScoped(typeof(IStockUnitOfWork), typeof(StockUnitOfWork));

    }
}
