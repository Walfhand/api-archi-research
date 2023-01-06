using CarRacingTeam.CarRacingTeams.Features.CreateCarRacingTeam.Dtos;
using MediatR;

namespace CarRacingTeam.CarRacingTeams.Features.CreateCarRacingTeam.Commands.V1;
public record CreateCarRacingTeamCommand(string Name, string Country) : IRequest<CreateCarRacingTeamResponseDto>
{
}
