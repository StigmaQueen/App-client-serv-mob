using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PruebaSignalR.Services;

namespace PruebaSignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumerosController : ControllerBase
    {
        IHubContext<NumerosHub> numeroshub;
        public NumerosController(IHubContext<NumerosHub> context)
        {
            numeroshub = context;
        }
        [HttpGet("Incrementar")]
        public async Task<IActionResult> Incrementar()
        {
          await  numeroshub.Clients.All.SendAsync("incrementar");
            return Ok();
        }
        [HttpGet("Decrementar")]
        public async Task< IActionResult> Decrementar()
        {
           await numeroshub.Clients.All.SendAsync("decrementar");
            return Ok();
        }
    }
}
