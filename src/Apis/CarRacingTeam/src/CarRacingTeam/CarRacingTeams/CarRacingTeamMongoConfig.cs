using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;

namespace CarRacingTeam.CarRacingTeams
{
    internal static class CarRacingTeamMongoConfig
    {
        public static IServiceCollection ConfigureCarRacingTeam(this IServiceCollection services)
        {
            BsonClassMap.RegisterClassMap<Models.CarRacingTeam>(classMap =>
            {
                classMap.AutoMap();
                classMap.MapField("_staffIds").SetElementName(nameof(Models.CarRacingTeam.StaffIds));
                classMap.MapField("_buildingIds").SetElementName(nameof(Models.CarRacingTeam.BuildingIds));
                classMap.MapField("_partnerIds").SetElementName(nameof(Models.CarRacingTeam.PartnerIds));
            });

            return services;
        }
    }
}
