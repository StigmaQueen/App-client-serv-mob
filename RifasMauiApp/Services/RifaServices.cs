using Newtonsoft.Json;
using RifasMauiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task <IEnumerable<BoletoDTO>> GetAll()
        {
            var response = await Client.GetAsync("api/rifas/vendidos");
            response.EnsureSuccessStatusCode();//Verificar que regreso un 200
            var json= await response.Content.ReadAsStringAsync();
            var datos= JsonConvert.DeserializeObject<List<BoletoDTO>>(json);  
            return datos;   
        }
        public void Post( BoletoDTO bo)
        {
           //validar 
        }
        public void Put() 
        { 

        }
        public void Delete() 
        {

        }
    }
}
