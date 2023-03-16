using CarRacingTeam.Data;
using Engine.Logging;
using Engine.Mongo;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;

namespace CarRacingTeam.Extensions.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoDbContext<CarRacingTeamDbContext>(configuration);
        services.ConfigureEntities();
        services.AddCustomMediatR();
        services.AddCustomProblemDetails();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication webApplication)
    {
        webApplication.UseProblemDetails();
        webApplication.UseSerilogRequestLogging(options =>
        {
            options.EnrichDiagnosticContext = LogEnrichHelper.EnrichFromRequest;
        });
        return webApplication;
    }
}
