using Engine.Mongo;
using MediatR;

namespace CarRacingTeam.CarRacingTeams.Features.CreateCarRacingTeam.Commands.V1;
public class CreateCarRacingTeamCommandHandler : IRequestHandler<CreateCarRacingTeamCommand>
{
    private readonly IMongoRepository<Models.CarRacingTeam, Guid> _repository;

    public CreateCarRacingTeamCommandHandler(IMongoRepository<Models.CarRacingTeam, Guid> repository)
    {
        _repository = repository;
    }
    public async Task<Unit> Handle(CreateCarRacingTeamCommand request, CancellationToken cancellationToken)
    {
        var carRacingTeam = Models.CarRacingTeam.Create("Test", "Test");
        carRacingTeam.AddBuildingId(Guid.NewGuid());
        await _repository.AddAsync(carRacingTeam, cancellationToken);
        return Unit.Value;
    }
}
