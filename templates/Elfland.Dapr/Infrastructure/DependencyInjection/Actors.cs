using System.Text.Json;
using Dapr.Actors.Runtime;

namespace Elfland.Dapr.Infrastructure.DependencyInjection;

public static partial class AutomaticDependencyInjectionExtensions
{
    public static void AddDaprActors(this IServiceCollection services)
    {
        services.AddActors(
            options =>
            {
                options.JsonSerializerOptions = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                };

                options.Actors.RegisterActors();
            }
        );
    }

    private static void RegisterActors(this ActorRegistrationCollection actorRegistrationCollection)
    {
        var genericMethodInfo = actorRegistrationCollection.GetType().GetMethod("RegisterActor");

        AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(
                implementationType =>
                    implementationType.IsSubclassOf(typeof(Actor))
                    && implementationType.IsClass
                    && !implementationType.IsAbstract
            )
            .ToList()
            .ForEach(
                implementationType =>
                    genericMethodInfo
                        ?.MakeGenericMethod(implementationType)
                        ?.Invoke(actorRegistrationCollection, new object?[] { null })
            );
    }
}
