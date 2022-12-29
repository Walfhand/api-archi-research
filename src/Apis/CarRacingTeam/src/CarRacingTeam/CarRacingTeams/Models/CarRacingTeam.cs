using Engine.Core.Model;

namespace CarRacingTeam.CarRacingTeams.Models;
public record CarRacingTeam : AggregateRoot<Guid>
{
    public string Name { get; private set; }
    public string Country { get; private set; }
    public IReadOnlyList<Guid> StaffIds { get; private set; }
    public IReadOnlyList<Guid> BuildingIds { get; private set; }
    public IReadOnlyList<Guid> PartnerIds { get; private set; }
    public static CarRacingTeam Create(string name, string country, IEnumerable<Guid> staffIds, IEnumerable<Guid> buildingIds, IEnumerable<Guid> partnerIds)
    {
        return new CarRacingTeam()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Country = country,
            StaffIds = staffIds.ToList().AsReadOnly(),
            BuildingIds= buildingIds.ToList().AsReadOnly(),
            PartnerIds= partnerIds.ToList().AsReadOnly()
        };
    }
    private CarRacingTeam() { }
}
