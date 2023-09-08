using System.Reflection;
using Core.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Order.Application;

public static class DependencyInjectionOrderApplicationExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddCoreApplication(Assembly.GetExecutingAssembly());
    }
}