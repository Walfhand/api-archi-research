using CarRacingTeam.Buildings.Models;
using CarRacingTeam.Partners.Models;
using CarRacingTeam.Staffs.Models;
using Engine.Mongo;
using Humanizer;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CarRacingTeam.Data;
public class CarRacingTeamDbContext : MongoDbContext
{
    public IMongoCollection<Partner> Partners { get; }
    public IMongoCollection<Building> Buildings { get; }
    public IMongoCollection<Staff> Staffs { get; }
    public IMongoCollection<CarRacingTeams.Models.CarRacingTeam> CarRacingTeams { get; }

    public CarRacingTeamDbContext(IOptions<MongoOptions> options) : base(options)
    {
        Partners = GetCollection<Partner>(nameof(Partner).Underscore());
        Buildings = GetCollection<Building>(nameof(Building).Underscore());
        Staffs = GetCollection<Staff>(nameof(Staff).Underscore());
        CarRacingTeams = GetCollection<CarRacingTeams.Models.CarRacingTeam>(nameof(CarRacingTeam).Underscore());
    }
}
