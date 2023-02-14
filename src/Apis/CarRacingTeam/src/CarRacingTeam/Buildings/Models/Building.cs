using CarRacingTeam.Buildings.Models.ValueObjects;
using Engine.Core.Model;

namespace CarRacingTeam.Buildings.Models;
public class Building : AggregateRoot<Guid>
{
    public Address Address { get; private set; }
    public int Area { get; private set; }
    public string Purpose { get; private set; }

    private Building() { }

    public static Building Create(Address address, int area, string purpose)
        => new()
        {
            Id = Guid.NewGuid(),
            Address = address,
            Area = area,
            Purpose = purpose
        };
}
