using Engine.Core.Model;

namespace CarRacingTeam.CarRacingTeams.Models;
public class CarRacingTeam : AggregateRoot<Guid>
{
    public string Name { get; private set; }
    public string Country { get; private set; }

    private readonly List<Guid> _staffIds = new();
    private readonly List<Guid> _buildingIds = new();
    private readonly List<Guid> _partnerIds = new();

    public IReadOnlyList<Guid>? StaffIds => _staffIds.AsReadOnly();
    public IReadOnlyList<Guid>? BuildingIds => _buildingIds.AsReadOnly();
    public IReadOnlyList<Guid>? PartnerIds => _partnerIds.AsReadOnly();

    public static CarRacingTeam Create(string name, string country)
        => new(name, country)
        {
            Id = Guid.NewGuid(),
        };

    private CarRacingTeam(string name, string country) 
    {
        Name = name;
        Country = country;
    }

    public void AddStaffId(Guid staffId)
    {
        if(!_staffIds.Any(x => x == staffId))
            _staffIds.Add(staffId);
    }

    public void AddBuildingId(Guid buildingId)
    {
        if (!_buildingIds.Any(x => x == buildingId))
            _buildingIds.Add(buildingId);
    }

    public void AddPartnerId(Guid partnerId)
    {
        if (!_partnerIds.Any(x => x == partnerId))
            _partnerIds.Add(partnerId);
    }
}
