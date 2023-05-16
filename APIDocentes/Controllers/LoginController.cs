using APIDocentes.DTo_s;
using APIDocentes.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Text;

namespace APIDocentes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ItesrcneDocentesContext Context;
        public LoginController(ItesrcneDocentesContext context)
        {
           Context = context;
        }
        [HttpPost]
        public IActionResult Login(LoginDTO datos)
        {
            //No olvidar encriptar password por seguridad
            var dpto = Context.Departamentos.SingleOrDefault(x => x.Clave == datos.Usuario && x.Contraseña == datos.Contraseña);
            if (dpto == null)
            {
               return Unauthorized("Clave de departamento ó contraseña incorrecto");
            }
            else
            {
                //1. Crear claims
                //2. Crear Token
                //3. Regresar el token

                List<Claim> claims = new()
                {
                    new Claim("Id", dpto.Id.ToString()),
                    new Claim("Clave", dpto.Clave??""),
                    new Claim(ClaimTypes.Name,dpto.Nombre),
                    new Claim(ClaimTypes.Email,dpto.Correo??""),
                    new Claim(ClaimTypes.Role,"Departamento") //Imperzonalizar
                };

                SecurityTokenDescriptor tokenDescriptor = new()
                {
                    Issuer = "docentes.itesrc.net",
                    Audience = "mauidocentes",
                    IssuedAt = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddHours(.5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DocentesKeyMoviles83G")), SecurityAlgorithms.HmacSha256),
                    Subject = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme)
                };
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                var token = handler.CreateToken(tokenDescriptor);
                return Ok(handler.WriteToken(token));
            }
        }
    }
}
