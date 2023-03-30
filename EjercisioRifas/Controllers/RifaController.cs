using EjercisioRifas.Models;
using EjercisioRifas.Repositories;
using EjercisioRifas.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EjercisioRifas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RifaController : ControllerBase
    {
        private readonly Sistem21RifasContext context;
        private readonly BoletosHub hubContext;
        BoletosRepository boletosRepository;

        public RifaController(Sistem21RifasContext context, BoletosHub hubContext)
        {
            this.context = context;
            this.hubContext = hubContext;
            boletosRepository = new BoletosRepository(context);
        }

        [HttpGet("vendidos")]
        public IActionResult Get()
        {
            return Ok(boletosRepository.GetAll());
        }

        //Modo fuera de linea
        [HttpGet("actualizar/{hora:DateTime}")]
        public IActionResult Get(DateTime hora)
        {
            return Ok(boletosRepository.GetAllByFecha(hora));
        }


        [HttpPost("vender")]
        public async Task<IActionResult> Post(Boletos boleto)
        {
            if (boletosRepository.Validate(boleto, out List<string> errores))
            {
                boleto.Id = 0;
                boleto.FechaModificacion = DateTime.Now.ToMexicoTime();//Va a causar problemas
                boleto.Eliminado = 0;
                boletosRepository.Insert(boleto);

                await hubContext.Clients.All.SendAsync("agregar", boleto);

                return Ok();
            }
            else
            {
                return BadRequest(errores);
            }
        }
        [HttpPut("pagar")]
        public async Task<IActionResult> Put(Boletos boleto)
        {
            var registro = boletosRepository.GetById(boleto.Id);
            if (registro == null|| registro.Eliminado==0)
            {
                return NotFound("No existe el boleto espesificado");
            }
            else if (registro.Pagado == 1)
            {
                return Conflict("El boleto ya ha sido pegado");
            }
            else
            {
                registro.Eliminado = 1;
                registro.FechaModificacion = DateTime.Now.ToMexicoTime();
                boletosRepository.Update(registro);
                await hubContext.Clients.All.SendAsync("pagar", registro);
            }
            return Ok();
        }
        [HttpPut("cancelar")]
        public async Task<IActionResult> Cancelar(Boletos boleto)
        {
            var registro = boletosRepository.GetById(boleto.Id);
            if (registro == null || registro.Eliminado == 0)
            {
                return NotFound("No existe el boleto espesificado");
            }
            else if (registro.Pagado == 1)
            {
                return Conflict("No se puede cancelar un boleto pagado");
            }
            else
            {
                registro.Eliminado= 1;
                registro.FechaModificacion = DateTime.Now.ToMexicoTime();
                boletosRepository.Update(registro);
                await hubContext.Clients.All.SendAsync("cancelar", registro);

            }
            return Ok();
        }
    }
}

