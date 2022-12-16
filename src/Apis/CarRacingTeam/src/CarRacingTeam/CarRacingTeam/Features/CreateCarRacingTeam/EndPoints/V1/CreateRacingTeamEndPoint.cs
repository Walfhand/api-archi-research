using AutoMapper;
using CarRacingTeam.CarRacingTeam.Features.CreateCarRacingTeam.Commands.V1;
using Engine.Web;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Swashbuckle.AspNetCore.Annotations;

namespace CarRacingTeam.CarRacingTeam.Features.CreateCarRacingTeam.EndPoints.V1;
public class CreateRacingTeamEndPoint : IMinimalEndpoint
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost($"{EndpointPath.BaseApiPath}/car-racing-team", CreateCarRacingTeam)
            .WithTags("CarRacingTeam")
            .WithName(nameof(CreateCarRacingTeam))
            .WithMetadata(new SwaggerOperationAttribute("Create Car Racing Team", ""))
            .WithApiVersionSet(builder.NewApiVersionSet("CarRacingTeam").Build())
            .Produces<CreateCarRacingTeamCommand>()
            .Produces(StatusCodes.Status400BadRequest)
            .HasApiVersion(1.0);

        return builder;
    }

    public static async Task<IResult> CreateCarRacingTeam(CreateCarRacingTeamCommand command, IMediator mediator, IMapper mapper, CancellationToken cancellationToken)
    {
        return Results.Ok(await mediator.Send(command, cancellationToken));
    }
}
