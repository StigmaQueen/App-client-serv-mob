using Newtonsoft.Json;
using PandientesApp.Models;
using System.Text;

namespace PandientesApp.Services
{
    public class PendientesServices
    {
        HttpClient client;
        public PendientesServices()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://pendientes.sistemas19.com/")
            };

        }
        public async Task<List<Actividad>> GetAll()
        {
            HttpResponseMessage result = await client.GetAsync("api/pendientes");//peticion regresa un response
            if(result.IsSuccessStatusCode)//verificar si la respuesta fue existosa
            {
                var json= await result.Content.ReadAsStringAsync();
                List<Actividad> lista= JsonConvert.DeserializeObject<List<Actividad>>(json);    
                return lista;
            }
            return new List<Actividad>();
        }
        public async Task Insert(Actividad a)
        {
            var json = JsonConvert.SerializeObject(a);
            StringContent scontent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("api/pendientes",scontent);
            if (!result.IsSuccessStatusCode)
            {
                json = await result.Content.ReadAsStringAsync();
                string s = JsonConvert.DeserializeObject<string>(json);
                throw new Exception($"Ha ocurrido un error: {result.StatusCode}\n{s}");
            }
        }
        public async Task Update(Actividad a)
        {
            var json = JsonConvert.SerializeObject(a);
            StringContent scontent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await client.PutAsync("api/pendientes", scontent);
            if (!result.IsSuccessStatusCode)
            {
                json = await result.Content.ReadAsStringAsync();
                string s = JsonConvert.DeserializeObject<string>(json);
                throw new Exception($"Ha ocurrido un error: {result.StatusCode}\n{s}");
            }
        }
        public async Task Delete(Actividad a)
        {
         
            var result = await client.DeleteAsync("api/pendientes"+ a.Id);
            if (!result.IsSuccessStatusCode)
            {
                var json = await result.Content.ReadAsStringAsync();
                string s = JsonConvert.DeserializeObject<string>(json);
                throw new Exception($"Ha ocurrido un error: {result.StatusCode}\n{s}");
            }
        }
    }
}
