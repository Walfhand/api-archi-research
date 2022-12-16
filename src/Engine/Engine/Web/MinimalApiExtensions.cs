using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Engine.Web;
public static class MinimalApiExtensions
{
    public static IServiceCollection AddMinimalEndpoints(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        services.Scan(scan => scan
            .FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(classes => classes.AssignableTo(typeof(IMinimalEndpoint)))
            .UsingRegistrationStrategy(RegistrationStrategy.Append)
            .As<IMinimalEndpoint>()
            .WithLifetime(lifetime));

        return services;
    }


    public static IEndpointRouteBuilder MapMinimalEndpoints(this IEndpointRouteBuilder builder)
    {
        var scope = builder.ServiceProvider.CreateScope();

        var endpoints = scope.ServiceProvider.GetServices<IMinimalEndpoint>();

        foreach (var endpoint in endpoints)
            endpoint.MapEndpoint(builder);

        return builder;
    }
}
