using Engine.Mongo;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CarRacingTeam.Extensions.Infrastructure;
internal static class MediatRExtensions
{
    public static IServiceCollection AddCustomMediatR(this IServiceCollection services)
    {
        services.AddMediatR(typeof(MediatRExtensions).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
        return services;
    }
}
