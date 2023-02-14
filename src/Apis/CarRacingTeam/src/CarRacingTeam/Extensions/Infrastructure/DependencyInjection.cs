using CarRacingTeam.Data;
using Engine.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRacingTeam.Extensions.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoDbContext<CarRacingTeamDbContext>(configuration);
        services.ConfigureEntities();
        services.AddCustomMediatR();
        return services;
    }
}
