using Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payment.Domain.Common;
using Payment.Infrastructure.Persistence;
using Payment.Infrastructure.Repositories;
using Shared.Base;

namespace Payment.Infrastructure;
public static class DependencyInjectionPaymentInfrastructureExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, DependencyOptions dependencyOptions)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddCoreInfrastructure(configuration, dependencyOptions);

        services.AddDbContext<PaymentDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PaymentDb"));
        });

        services.AddScoped(typeof(IPaymentRepository), typeof(PaymentRepository));
        services.AddScoped(typeof(IPaymentUnitOfWork), typeof(PaymentUnitOfWork));

    }
}
