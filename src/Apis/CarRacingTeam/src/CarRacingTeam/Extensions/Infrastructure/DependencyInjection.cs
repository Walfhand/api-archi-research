using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CarRacingTeam.Extensions.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddCustomMediatR();
        return services;
    }
}
