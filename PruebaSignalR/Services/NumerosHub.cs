using Microsoft.AspNetCore.SignalR;

namespace PruebaSignalR.Services
{
    public class NumerosHub: Hub
    {
        public async Task Incrementar()
        {
            await Clients.All.SendAsync("incremento");
        }
        public async Task Decrementar()
        {
            await Clients.All.SendAsync("decremento");
        }
    }
}
