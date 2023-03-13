using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PantallaVueloMAUI.Models;
using PantallaVueloMAUI.Repositories;


namespace PantallaVueloMAUI.Services
{
    public class VueloServices
    {
        HttpClient client;
        VueloRepository repository = new VueloRepository();

        //Sincronizador de microservicios
        public VueloServices() 
        {
            client = new HttpClient();
            client.BaseAddress= new Uri("https://aerolineatec.sistemas19.com/");
        }

        public async Task<IEnumerable<Vuelo>> GetAsync()
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                //Descargar de la api
                var response = await client.GetAsync("api/vuelos");
                response.EnsureSuccessStatusCode(); //Lanza una exeption si el status code no es un 200
                var json= await response.Content.ReadAsStringAsync();
                var vuelos = JsonConvert.DeserializeObject<List<Vuelo>>(json);

                //Actualizacion en BD local
                foreach (var item in vuelos)
                {
                    var  vuelo = repository.Get(item.Id);
                    if(vuelo != null) 
                    {
                        repository.Insert(item);
                    }
                    else
                        repository.Update(item);
                }
                foreach (var item in vuelos)
                {
                    if (!vuelos.Any(x => x.Id == item.Id))
                    {
                        repository.Delete(item.Id);
                    }
                }
              
            }
            return repository.GetAll();
        }
    }
}
