using System.Reflection;
using Core.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Payment.Application;

public static class DependencyInjectionPaymentApplicationExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddCoreApplication(Assembly.GetExecutingAssembly());
    }
}