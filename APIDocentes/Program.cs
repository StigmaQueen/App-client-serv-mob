using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using APIDocentes.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddControllers();
builder.Services.AddDbContext<ItesrcneDocentesContext>(optionsBuilder => optionsBuilder.UseMySql("server=204.93.216.11;database=itesrcne_docentes;user=itesrcne_docente;password=docentes1", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.29-mariadb")));

//datos para jwt
//Issuer, Audience, Secret
//Emisor de quien o qué app emite el token en una url de la aplicacion, A quien está emitiendo el token con el nombre de la aplicacion, Pasword con la cual vamos a firmar el token de jwt

string issur = "docentes.itesrc.net";
string audience = "mauidocentes";
var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DocentesKey"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(jwt =>
{
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = issur,
        ValidAudience = audience,
        IssuerSigningKey = secret,
        ValidateAudience = true,
        ValidateIssuer = true
    };

});


builder.Services.AddAuthentication();

app.MapControllers();
app.UseAuthentication();
app.Run();
