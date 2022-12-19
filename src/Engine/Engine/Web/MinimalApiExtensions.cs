using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Swashbuckle.AspNetCore.Annotations;

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

    public static RouteHandlerBuilder CreateEndpoint(this IEndpointRouteBuilder builder,
        EndpointType endpointType, string path, Delegate handler, string endpointName, string endpointSummary,
        string entityName, double apiVersion = 1.0)
        => builder.CreateConvention(endpointType, path, handler)
            .WithTags(entityName)
            .WithName(endpointName)
            .WithMetadata(new SwaggerOperationAttribute(endpointSummary, ""))
            .WithApiVersionSet(builder.NewApiVersionSet(entityName).Build())
            .HasApiVersion(apiVersion);

    private static RouteHandlerBuilder CreateConvention(this IEndpointRouteBuilder builder, EndpointType endpointType, string path, Delegate handler)
        => endpointType switch
        {
            EndpointType.Get => builder.MapGet(path, handler),
            EndpointType.Post => builder.MapPost(path, handler),
            EndpointType.Put => builder.MapPut(path, handler),
            EndpointType.Patch => builder.MapPatch(path, handler),
            EndpointType.Delete => builder.MapDelete(path, handler),
            _ => throw new NotImplementedException()
        };

    public enum EndpointType
    {
        Get,
        Post,
        Put,
        Patch,
        Delete
    }
}
