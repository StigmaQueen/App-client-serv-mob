using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VuelosAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddControllers();
builder.Services.AddDbContext<Sistem21AerolineaDbContext>(optionsBuilder=>
optionsBuilder.UseMySql("server=sistemas19.com;user=sistem21_AerolineaDB;password=sistemas19_;database=sistem21_AerolineaDB", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.17-mariadb")));

app.MapControllers();

app.UseRouting();
app.Run();
