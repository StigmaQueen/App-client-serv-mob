using EjercisioRifas.Models;
using EjercisioRifas.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSignalR();
string? cadena = builder.Configuration.GetConnectionString("RifasConnectionStrings");

builder.Services.AddDbContext<Sistem21RifasContext>(optionsBuilder
    => optionsBuilder.UseMySql(cadena, ServerVersion.AutoDetect(cadena)));


var app = builder.Build();
app.MapHub<BoletosHub>("/BoletosHub");
app.MapControllers();
app.Run();
