using Elfland.WebApi.Application.Configurations.OpenIddict;
using Elfland.WebApi.Data;
using OpenIddict.Abstractions;

namespace Elfland.WebApi.Infrastructure.DependencyInjection;

public static partial class ServiceCollectionDependencyInjection
{
    public static void AddAppIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenIddict()

                // Register the OpenIddict core components.
                .AddCore(options =>
                {
                    // Configure OpenIddict to use the Entity Framework Core stores and models.
                    // Note: call ReplaceDefaultEntities() to replace the default entities.
                    options.UseEntityFrameworkCore()
                           .UseDbContext<ApplicationDbContext>();
                })

                // Register the OpenIddict server components.
                .AddServer(options =>
                {
                    // Enable the token endpoint.
                    options.SetTokenEndpointUris("/connect/token");

                    // Enable the client credentials flow.
                    options.AllowClientCredentialsFlow();

                    options.RegisterScopes(OpenIddictConstants.Scopes.Email,
                            OpenIddictConstants.Scopes.Profile,
                            OpenIddictConstants.Scopes.Roles,
                            "api"
                            );

                    // Register the signing and encryption credentials.
                    options.AddDevelopmentEncryptionCertificate()
                           .AddDevelopmentSigningCertificate();

                    // Register the ASP.NET Core host and configure the ASP.NET Core options.
                    options.UseAspNetCore()
                           .EnableTokenEndpointPassthrough();
                })

                // Register the OpenIddict validation components.
                .AddValidation(options =>
                {
                    // Import the configuration from the local OpenIddict server instance.
                    options.UseLocalServer();

                    // Register the ASP.NET Core host.
                    options.UseAspNetCore();
                });

        // Register the worker responsible of seeding the database with the sample clients.
        // Note: in a real world application, this step should be part of a setup script.
        services.AddHostedService<ClientExample>();
    }
}
