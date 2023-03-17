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
        public Repository<Vuelo> repository { get; set; }
        public VuelosController(Sistem21AerolineaDbContext context)
        {
   
            repository = new Repository<Vuelo>(context);
        }
        public IActionResult Get()
        {
            return Ok(repository.Get().OrderBy(x => x.Fecha).ThenBy(x => x.Numerovuelo)
                .Where(x => (x.Estado != Estados.Cancelado && x.Estado != Estados.EnVuelo && x.Estado != Estados.Desviado && x.Estado != Estados.Aterrizo)
                ||
                (x.UltimaEdicionFecha.AddMinutes(1) > DateTime.Now)));
        }
        [HttpPost]
        public IActionResult Post(Vuelo vuelo)
        {
            if (vuelo == null)
            {
                return BadRequest("Proporcione los datos del vuelo");
            }
            if (Validar(vuelo, out List<string> errores))
            {
                vuelo.Estado = Estados.Programado;
                vuelo.Id = 0;
                vuelo.UltimaEdicionFecha= DateTime.Now;
                repository.Insert(vuelo);
                return Ok();
            }
            else
                return BadRequest(errores);
        }
        [HttpPut("Cancelar")]
        public IActionResult CancelarVuelo(Vuelo vuelo)
        {
            Vuelo? v = repository.Get(vuelo.Id);

            if (v == null)
                return NotFound();

            if (vuelo.Estado == Estados.Programado || vuelo.Estado == Estados.Retrasado || vuelo.Estado == Estados.Abordando || vuelo.Estado == Estados.ATiempo)
            {
                v.Estado = Estados.Cancelado;
                v.UltimaEdicionFecha = DateTime.Now;
                v.Puerta = 0;
            }
            else
            {
                return Conflict("El vuelo no puede ser cancelado porque su estado es: " + v.Estado);
            }

            repository.Update(vuelo);
            return Ok();
        }


        [HttpPut("Desviar")]
        public IActionResult Desviar(Vuelo vuelo)
        {
            var v = repository.Get(vuelo.Id);

            if (v == null)
                return NotFound();

            if (v.Estado == Estados.EnVuelo)
            {
                v.Destino = vuelo.Destino;
                v.UltimaEdicionFecha = DateTime.Now;
                v.Estado = Estados.Desviado;
                repository.Update(v);
                return Ok();
            }
            else
            {
                return Conflict("El vuelo no puede ser desviado porque su estado es: " + vuelo.Estado);
            }
        }

        [HttpPut("Despegar")]
        public IActionResult Despegar(Vuelo vuelo)
        {
            var v = repository.Get(vuelo.Id);
            if (v == null)
                return NotFound();

            if (v.Estado == Estados.Abordando)
            {
                vuelo.Estado = Estados.EnVuelo;
                vuelo.Puerta = 0;
                vuelo.UltimaEdicionFecha = DateTime.Now;
                repository.Update(v);
                return Ok();
            }
            else
            {
                return Conflict("El vuelo no puede modificarse porque su estado es: " + vuelo.Estado);
            }


        }

        [HttpPut("CambiarEstado")]
        public IActionResult CambiarEstado(Vuelo v)
        {
            Vuelo? vuelo = repository.Get(v.Id);

            if (vuelo == null)
                return NotFound();


            if ((vuelo.Estado == Estados.Programado && v.Estado == Estados.Retrasado)
                || (vuelo.Estado == Estados.ATiempo && v.Estado == Estados.Retrasado)
                || (vuelo.Estado == Estados.Programado && v.Estado == Estados.ATiempo))
            {
                vuelo.Puerta = v.Puerta;
                vuelo.Estado = v.Estado;
            }
            else if (vuelo.Estado == Estados.EnVuelo && v.Estado == Estados.Aterrizo)
            {
                vuelo.Puerta = v.Puerta;
                vuelo.Estado = Estados.Aterrizo;
            }
            else
                return BadRequest($"El estado del vuelo no puede ser cambiado a " +
                    $"{v.Estado} porque el estado actual es: {vuelo.Estado}");

            vuelo.UltimaEdicionFecha = DateTime.Now;
            repository.Update(vuelo);
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
            if (repository.Get().Any(x => x.Numerovuelo == vuelo.Numerovuelo && x.Id != vuelo.Id))
            {
                errors.Add("Ya existe un vuelo con el mismo numero, agregar otro");
            }
            if (vuelo.Puerta !=0)
            {
                if (repository.Get().Any(x => x.Puerta == vuelo.Puerta && x.Id!=vuelo.Id))
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
