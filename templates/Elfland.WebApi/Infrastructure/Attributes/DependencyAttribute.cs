namespace Elfland.WebApi.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class AutomaticDependencyInjectionAttribute : Attribute
{
    public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Transient;

    public AutomaticDependencyInjectionAttribute() { }

    public AutomaticDependencyInjectionAttribute(ServiceLifetime serviceLifetime)
    {
        Lifetime = serviceLifetime;
    }
}
