using CarRacingTeam.CarRacingTeams.Models;
using CarRacingTeam.Data;
using Engine.Mongo;
using MediatR;

namespace CarRacingTeam.CarRacingTeams.Features.CreateCarRacingTeam.Commands.V1;
public class CreateCarRacingTeamCommandHandler : IRequestHandler<CreateCarRacingTeamCommand>
{
    private readonly CarRacingTeamDbContext _carRacingTeamDb;
    private readonly IMongoRepository<Models.CarRacingTeam, Guid> _repository;

    public CreateCarRacingTeamCommandHandler(CarRacingTeamDbContext carRacingTeamDb, IMongoRepository<Models.CarRacingTeam, Guid> repository)
    {
        _carRacingTeamDb = carRacingTeamDb;
        _repository = repository;
    }
    public async Task<Unit> Handle(CreateCarRacingTeamCommand request, CancellationToken cancellationToken)
    {
        await Task.Delay(500, cancellationToken);
        var carRacingTeam = Models.CarRacingTeam.Create("Test", "Test");
        carRacingTeam.AddBuildingId(Guid.NewGuid());
        await _repository.AddAsync(carRacingTeam, cancellationToken);
        return Unit.Value;
    }
}
