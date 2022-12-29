using Engine.Core.Model;

namespace CarRacingTeam.Staffs.Models;
public record Staff : AggregateRoot<Guid>
{
    public string Name { get; private set; }
    public string Position { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string Nationality { get; private set; }

    private Staff() { }

    public static Staff Create(string name, string position, DateTime dateOfBirth, string nationality)
       => new()
       {
           Id = Guid.NewGuid(),
           Name = name,
           Position = position,
           DateOfBirth = dateOfBirth,
           Nationality = nationality
       };
}
