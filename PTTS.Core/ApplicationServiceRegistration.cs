using System.Reflection;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;

namespace PTTS.Core;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.NotificationPublisherType = typeof(TaskWhenAllPublisher);
            cfg.Lifetime = ServiceLifetime.Transient;
        });

        return services;
    }
}