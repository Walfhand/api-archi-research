using CarRacingTeam.Extensions.Infrastructure;
using Engine.Mapping;
using Engine.Swagger;
using Engine.Web;
using Engine.Logging;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;

builder.AddCustomSerilog(env);


builder.Services.AddControllers();

builder.Services.AddCustomSwagger();
builder.Services.AddCustomVersioning();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddMinimalEndpoints();
builder.Services.AddMapping();

var app = builder.Build();

app.UseCustomSwagger();
app.UseInfrastructure();
app.MapMinimalEndpoints();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
