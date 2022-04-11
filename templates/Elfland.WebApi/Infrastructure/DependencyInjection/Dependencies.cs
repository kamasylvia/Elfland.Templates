﻿using System.Reflection;
using Elfland.WebApi.Infrastructure.Attributes;

namespace Elfland.WebApi.Infrastructure.Extensions;

public static partial class AutomaticDependencyInjectionExtensions
{
    public static void AddDependencies(this IServiceCollection services)
    {
        services.RegisterLifetimesByAttribute(ServiceLifetime.Transient);
        services.RegisterLifetimesByAttribute(ServiceLifetime.Scoped);
        services.RegisterLifetimesByAttribute(ServiceLifetime.Singleton);
    }

    private static void RegisterLifetimesByAttribute(
        this IServiceCollection services,
        ServiceLifetime serviceLifetime
    ) =>
        AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(
                implementationType =>
                    implementationType
                        .GetCustomAttribute<AutomaticDependencyInjectionAttribute>()
                        ?.Lifetime == serviceLifetime
                    && implementationType.IsClass
                    && !implementationType.IsAbstract
            )
            .ToList()
            .ForEach(
                implementationType =>
                    implementationType
                        .GetInterfaces()
                        .ToList()
                        .ForEach(
                            serviceType =>
                            {
                                switch (serviceLifetime)
                                {
                                    case ServiceLifetime.Transient:
                                        services.AddTransient(serviceType, implementationType);
                                        break;
                                    case ServiceLifetime.Scoped:
                                        services.AddScoped(serviceType, implementationType);
                                        break;
                                    case ServiceLifetime.Singleton:
                                        services.AddSingleton(serviceType, implementationType);
                                        break;
                                }
                            }
                        )
            );
}
