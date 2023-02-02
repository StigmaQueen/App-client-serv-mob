using Microsoft.EntityFrameworkCore;
using ToDoListU1_E1.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
builder.Services.AddControllers();

builder.Services.AddDbContext<Sistem21TodolistContext>(optionsBuilder =>

optionsBuilder.UseMySql("server=sistemas19.com;database=sistem21_todolist;user=sistem21_todolist;password=listapendientes", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.17-mariadb")));

app.Run();

app.UseRouting();
app.MapControllers();