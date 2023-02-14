using CarRacingTeam.Data;
using Engine.Mongo;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRacingTeam.Extensions.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoDbContext<CarRacingTeamDbContext>(configuration);
        services.ConfigureEntities();
        services.AddCustomMediatR();
        services.AddCustomProblemDetails();
        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication webApplication)
    {
        webApplication.UseProblemDetails();
        return webApplication;
    }
}
