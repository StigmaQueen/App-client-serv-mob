using APIDocentes.DTo_s;
using APIDocentes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace APIDocentes.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocentesController : ControllerBase
    {
        private readonly ItesrcneDocentesContext context;
        public DocentesController(ItesrcneDocentesContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var idDepto = User.Claims.Where(x => x.Type == "Id").Select(x => int.Parse(x.Value)).FirstOrDefault();

            var docentes = context.Usuarios.Where(x => x.Eliminado == 0 && x.IdDepartamento==idDepto).Select(x => new DocenteDTO()
            {
                Id= x.Id,
                Correo = x.Correo??"",
                Nombre=x.Nombre,
                Numero=x.NumEmpleado
            }).OrderBy(x=>x.Nombre).ToList();
            return Ok(docentes);    
        }

    }
}
