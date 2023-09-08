using MediatR;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application;

public static class DependencyInjectionCoreApplicationExtension
{
    public static void AddCoreApplication(this IServiceCollection services, Assembly executingAssembly)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddMediatR(executingAssembly);
    }
}