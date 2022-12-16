using MediatR;

namespace CarRacingTeam.CarRacingTeam.Features.CreateCarRacingTeam.Commands.V1;
public record CreateCarRacingTeamCommand(string Name) : IRequest
{
}
