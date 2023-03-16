using Engine.Exceptions;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CarRacingTeam.Extensions.Infrastructure
{
    public static class ProblemDetailsExtensions
    {
        public static IServiceCollection AddCustomProblemDetails(this IServiceCollection services)
        {
            services.AddProblemDetails(x =>
            {
                x.IncludeExceptionDetails = (ctx, _) =>
                {
                    var env = ctx.RequestServices.GetRequiredService<IHostEnvironment>();
                    return env.IsDevelopment();
                };
                x.Map<DatabaseException>(ex => new ProblemDetails
                {
                    Title = ex.GetType().Name,
                    Status = (int)ex.StatusCode,
                    Detail = ex.Message
                });
                x.Map<ValidationException>(ex => new ProblemDetails
                {
                    Title = ex.GetType().Name,
                    Status = (int)ex.StatusCode,
                    Detail = ex.Message
                });
                x.MapToStatusCode<ArgumentNullException>(StatusCodes.Status400BadRequest);
            });
            return services;
        }
    }
}
