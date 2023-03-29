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


        [HttpPost("vender")]
        public async Task<IActionResult> Post(Boletos boleto)
        {
            if (boletosRepository.Validate(boleto, out List<string> errores))
            {
                boleto.Id = 0;
                boleto.FechaModificacion = DateTime.Now;//Va a causar problemas
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
    }
}
}
