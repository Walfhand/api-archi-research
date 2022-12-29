using MediatR;

namespace CarRacingTeam.CarRacingTeams.Features.CreateCarRacingTeam.Commands.V1;
public class CreateCarRacingTeamCommandHandler : IRequestHandler<CreateCarRacingTeamCommand>
{
    public async Task<Unit> Handle(CreateCarRacingTeamCommand request, CancellationToken cancellationToken)
    {
        await Task.Delay(500, cancellationToken);
        return Unit.Value;
    }
}
