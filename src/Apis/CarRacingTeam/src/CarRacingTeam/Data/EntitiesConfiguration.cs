using CarRacingTeam.CarRacingTeams;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace CarRacingTeam.Data
{
    public static class EntitiesConfiguration
    {
        public static IServiceCollection ConfigureEntities(this IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            services.ConfigureCarRacingTeam();
            return services;
        }
    }
}
