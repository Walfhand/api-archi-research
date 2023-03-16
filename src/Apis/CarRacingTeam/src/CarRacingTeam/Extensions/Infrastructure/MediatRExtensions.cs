using Engine.Logging;
using Engine.Mongo;
using Engine.Validation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CarRacingTeam.Extensions.Infrastructure;
internal static class MediatRExtensions
{
    public static IServiceCollection AddCustomMediatR(this IServiceCollection services)
    {
        services.AddMediatR(typeof(MediatRExtensions).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
        return services;
    }
}
