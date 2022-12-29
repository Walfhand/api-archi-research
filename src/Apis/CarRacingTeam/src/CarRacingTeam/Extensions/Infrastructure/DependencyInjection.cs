using CarRacingTeam.Data;
using Engine.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using System.Reflection;

namespace CarRacingTeam.Extensions.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoDbContext<CarRacingTeamDbContext>(configuration);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddCustomMediatR();
        return services;
    }
}
