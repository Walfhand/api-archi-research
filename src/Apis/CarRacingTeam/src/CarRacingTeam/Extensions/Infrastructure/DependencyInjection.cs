using CarRacingTeam.Data;
using Engine.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Reflection;

namespace CarRacingTeam.Extensions.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoDbContext<CarRacingTeamDbContext>(configuration);

        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        BsonClassMap.RegisterClassMap<CarRacingTeams.Models.CarRacingTeam>(classMap =>
        {
            classMap.AutoMap();
            classMap.MapField("_staffIds").SetElementName(nameof(CarRacingTeams.Models.CarRacingTeam.StaffIds));
            classMap.MapField("_buildingIds").SetElementName(nameof(CarRacingTeams.Models.CarRacingTeam.BuildingIds));
            classMap.MapField("_partnerIds").SetElementName(nameof(CarRacingTeams.Models.CarRacingTeam.PartnerIds));
        });

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddCustomMediatR();
        return services;
    }
}
