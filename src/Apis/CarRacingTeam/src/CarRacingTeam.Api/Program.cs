using CarRacingTeam.Extensions.Infrastructure;
using Engine.Swagger;
using Engine.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCustomSwagger();
builder.Services.AddCustomVersioning();
builder.Services.AddInfrastructure();
builder.Services.AddMinimalEndpoints();

var app = builder.Build();

app.UseCustomSwagger();
app.MapMinimalEndpoints();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
