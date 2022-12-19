using Engine.Core.Model;

namespace CarRacingTeam.CarRacingTeam.Models;
public record CarRacingTeam : AggregateRoot<Guid>
{
    public string Name { get; private set; }
    public static CarRacingTeam Create(string name, bool isDeleted = false)
    {
        return new CarRacingTeam()
        {
            Id = Guid.NewGuid(),
            Name = name,
            IsDeleted = isDeleted
        };
    }
    private CarRacingTeam() { }
}
