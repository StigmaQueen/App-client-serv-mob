using Microsoft.AspNetCore.SignalR;
using PruebaSignalR.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSignalR();
var app = builder.Build();



app.UseRouting();
app.MapControllers();
app.MapHub<NumerosHub>("/numerosHub");
app.Run();
