using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VuelosAPI.Models;
using VuelosAPI.Respositories;

namespace VuelosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VuelosController : ControllerBase
    {
        public Sistem21AerolineaDbContext context { get; set; }
        public Repository<Vuelo> Repository { get; set; }
        public VuelosController(Sistem21AerolineaDbContext context)
        {
            this.context = context;
            Repository = new Repository<Vuelo>(context);
        }
        public IActionResult Get()
        {
            var vuelos = Repository.GetAll().OrderBy(x => x.Fecha).ThenBy(x => x.Numerovuelo);
            return Ok(vuelos);
        }
        [HttpPost]
        public IActionResult Post(Vuelo vuelo)
        {
            if (vuelo == null)
            {
                return BadRequest("Proporcione los datos del vuelo");
            }
            return Ok();
        }
        private bool Validar(Vuelo vuelo, out List<string> errors)
        {
            errors = new List<string>();
        
            if (string.IsNullOrEmpty(vuelo.Destino))
            {
                errors.Add("El destino del vuelo no puede ir vacio");
            }
            if (string.IsNullOrWhiteSpace(vuelo.Numerovuelo))
            {
                errors.Add("El numero de vuelo no puede ir vacio");
            }
            if (Repository.GetAll().Any(x => x.Numerovuelo == vuelo.Numerovuelo && x.Id != vuelo.Id))
            {
                errors.Add("Ya existe un vuelo con el mismo numero, agregar otro");
            }
            if (vuelo.Puerta !=0)
            {
                if (Repository.GetAll().Any(x => x.Puerta == vuelo.Puerta && x.Id!=vuelo.Id))
                {
                errors.Add("Ya existe un vuelo con el mismo numero, agregar otro");
                }
            }
            if (vuelo.Fecha <= DateTime.Now)
            {
                errors.Add("La fecha y hora no puede ser menor e igual a la actual");
            }
            return errors.Count==0; 
        }
    }
}
