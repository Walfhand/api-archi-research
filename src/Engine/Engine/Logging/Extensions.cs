using Engine.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.SpectreConsole;
using System.Text;

namespace Engine.Logging;
public static class Extensions
{
    public static WebApplicationBuilder AddCustomSerilog(this WebApplicationBuilder builder, IWebHostEnvironment webHostEnvironment)
    {
        builder.Host.UseSerilog((context, services, loggerConfiguration) =>
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var logOptions = context.Configuration.GetSection(nameof(LogOptions)).Get<LogOptions>();
            var appOptions = context.Configuration.GetSection(nameof(AppOptions)).Get<AppOptions>();

            var logLevel = Enum.TryParse<LogEventLevel>(logOptions?.Level, true, out var level)
                    ? level
                    : LogEventLevel.Information;

            loggerConfiguration
                    .MinimumLevel.Is(logLevel)
                    .WriteTo.SpectreConsole(logOptions?.LogTemplate, logLevel)
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error)
                    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                    .Enrich.WithExceptionDetails()
                    .Enrich.FromLogContext()
                    .ReadFrom.Configuration(context.Configuration);

            if (logOptions.Elastic.Enabled)
            {
                loggerConfiguration.WriteTo.Elasticsearch(
                    new ElasticsearchSinkOptions(new Uri(logOptions.Elastic.ElasticServiceUrl))
                    {
                        AutoRegisterTemplate = true,
                        IndexFormat = $"{appOptions.Name}-{env?.ToLower()}"
                    });
            }

            if (logOptions.File.Enabled)
            {
                var root = webHostEnvironment.ContentRootPath;
                Directory.CreateDirectory(Path.Combine(root, "logs"));

                var path = string.IsNullOrWhiteSpace(logOptions.File.Path) ? "logs/.txt" : logOptions.File.Path;
                if (!Enum.TryParse<RollingInterval>(logOptions.File.Interval, true, out var interval))
                {
                    interval = RollingInterval.Day;
                }

                loggerConfiguration.WriteTo.File(path, rollingInterval: interval, encoding: Encoding.UTF8, outputTemplate: logOptions.LogTemplate);
            }
        });

        return builder;
    }
}
