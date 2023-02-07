using Newtonsoft.Json;
using PandientesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PandientesApp.Services
{
    public class PendientesServices
    {
        HttpClient cliet;
        public PendientesServices()
        {
            cliet = new HttpClient()
            {
                BaseAddress = new Uri("https://pendintes.sistemas19.com")
            };

        }
        public async Task<List<Actividad>> GetAll()
        {
            var result = await cliet.GetAsync("api/pendientes");//peticion regresa un response
            if(result.IsSuccessStatusCode)//verificar si la respuesta fue existosa
            {
                var json= await result.Content.ReadAsStringAsync();
                List<Actividad> lista= JsonConvert.DeserializeObject<List<Actividad>>(json);    
                return lista;
            }
            return new List<Actividad>();
        }
    }
}
