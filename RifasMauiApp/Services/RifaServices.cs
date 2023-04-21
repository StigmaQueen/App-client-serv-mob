using Newtonsoft.Json;
using RifasMauiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RifasMauiApp.Services
{
    public class RifaServices
    {
        HttpClient Client;
        public RifaServices()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri("http://rifas.sistemas19.com/");
        }
        public async Task<IEnumerable<BoletoDTO>> GetAll()
        {
            var response = await Client.GetAsync("api/rifa/vendidos");
            response.EnsureSuccessStatusCode();//Verificar que regreso un 200
            var json = await response.Content.ReadAsStringAsync();
            var datos = JsonConvert.DeserializeObject<List<BoletoDTO>>(json);
            return datos;
        }
        public async Task Post(BoletoDTO bo)
        {
            //validar 

            await Send("api/rifa/vender", bo, HttpMethod.Post);
        }
        public async Task Put(BoletoDTO bo)
        {
            await Send("api/rifa/pagar", bo, HttpMethod.Put);
        }


        public async Task Delete(BoletoDTO bo)
        {
            await Send("api/rifa/cancelar", bo, HttpMethod.Put);
            //en cancelar no eliminamos, solo editamos para que los cancelados no salgan en el get all
        }
        async Task Send(string url, object dto, HttpMethod method)
        {
            
            var json = JsonConvert.SerializeObject(dto);
            HttpRequestMessage httpRequest = new HttpRequestMessage(method, url);
            httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();
        }
    }
}
