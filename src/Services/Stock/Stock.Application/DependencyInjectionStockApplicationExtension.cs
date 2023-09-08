using System.Reflection;
using Core.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Stock.Application;

public static class DependencyInjectionStockApplicationExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddCoreApplication(Assembly.GetExecutingAssembly());
    }
}