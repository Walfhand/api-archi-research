﻿using CarRacingTeam.CarRacingTeams.Features.CreateCarRacingTeam.Commands.V1;
using CarRacingTeam.CarRacingTeams.Features.CreateCarRacingTeam.Dtos;
using Engine.Web;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CarRacingTeam.CarRacingTeams.Features.CreateCarRacingTeam.EndPoints.V1;
public class CreateRacingTeamEndPoint : IMinimalEndpoint
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.CreateEndpoint(MinimalApiExtensions.EndpointType.Post,
            $"{EndpointPath.BaseApiPath}/car-racing-teams",
            CreateCarRacingTeam,
            nameof(CreateCarRacingTeam),
            "Create Car Racing Team",
            "CarRacingTeams")
            .Produces<CreateCarRacingTeamResponseDto>()
            .Produces(StatusCodes.Status400BadRequest);

        return builder;
    }

    public static async Task<IResult> CreateCarRacingTeam([FromBody] CreateCarRacingTeamCommand command,
        IMediator mediator, CancellationToken cancellationToken)
       => Results.Ok(await mediator.Send(command, cancellationToken));
}
