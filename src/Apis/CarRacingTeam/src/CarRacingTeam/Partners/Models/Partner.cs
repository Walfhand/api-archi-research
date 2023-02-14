using CarRacingTeam.Partners.Models.Enums;
using Engine.Core.Model;

namespace CarRacingTeam.Partners.Models;
public class Partner : AggregateRoot<Guid>
{
    public string Name { get; set; }
    public PartnershipTypes PartnershipType { get; set; }
    public DateTime StartDate { get; set; }
    public int Duration { get; set; }

    private Partner() { }

    public static Partner Create(string name, PartnershipTypes partnershipType, DateTime startDate, int duration)
        => new()
        {
            Id = Guid.NewGuid(),
            Name = name,
            PartnershipType = partnershipType,
            StartDate = startDate,
            Duration = duration
        };
}
