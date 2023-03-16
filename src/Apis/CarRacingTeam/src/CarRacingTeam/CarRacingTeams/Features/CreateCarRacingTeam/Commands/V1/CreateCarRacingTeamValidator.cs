using FluentValidation;

namespace CarRacingTeam.CarRacingTeams.Features.CreateCarRacingTeam.Commands.V1;
public class CreateCarRacingTeamValidator : AbstractValidator<CreateCarRacingTeamCommand>
{
    public CreateCarRacingTeamValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required!");
    }
}
