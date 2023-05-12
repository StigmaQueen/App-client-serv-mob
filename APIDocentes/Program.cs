using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddControllers();

//datos para jwt
//Issuer, Audience, Secret
//Emisor de quien o qué app emite el token url de la aplicacion, A quien está emitiendo el token con el nombre de la aplicacion, Pasword con la cual vamos a firmar el token de jwt

string isser = "docentes.itesrc.net";
string audience = "mauidocentes";


builder.Services.AddAuthentication();

app.MapControllers();


app.Run();
