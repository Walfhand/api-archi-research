using CarRacingTeam.CarRacingTeams.Features.CreateCarRacingTeam.Dtos;
using Mapster;

namespace CarRacingTeam.CarRacingTeams.Features
{
    public class CarRacingTeamMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Models.CarRacingTeam, CreateCarRacingTeamResponseDto>()
                .ConstructUsing(x => new CreateCarRacingTeamResponseDto(x.Id, x.Name, x.Country));
        }
    }
}
