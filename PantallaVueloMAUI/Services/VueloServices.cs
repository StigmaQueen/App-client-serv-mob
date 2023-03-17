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
        VueloRepository<Vuelo> repository = new();
        VueloRepository<VueloBuffer> bufferRep = new();
        public string Errors { get; private set; }


        //Sincronizador de microservicios
        public VueloServices()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://aerolineatec.sistemas19.com/");
        }

        public async Task<IEnumerable<Vuelo>> GetAsync()
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {

                //Descargar de la api
                var response = await client.GetAsync("api/vuelos");
                response.EnsureSuccessStatusCode(); //Lanza una exeption si el status code no es un 200
                var json = await response.Content.ReadAsStringAsync();
                var vuelos = JsonConvert.DeserializeObject<List<Vuelo>>(json);

                //Actualizacion en BD local
                foreach (var item in vuelos)
                {
                    var vuelo = repository.Get(item.Id);
                    if (vuelo == null)
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
        public async Task<bool> PostAsync(Vuelo v)
        {
            Errors = "";
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                var json = JsonConvert.SerializeObject(v);
                var response = await client.PostAsync("api/vuelos", new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Errors = await response.Content.ReadAsStringAsync();
                    return false;
                }
            }
            else
            {
                //Si no hay internet agregamos al buffer
                VueloBuffer vb = new VueloBuffer
                {
                    Destino = v.Destino,
                    Estado = v.Estado,
                    Fecha = v.Fecha,
                    Numerovuelo = v.Numerovuelo,
                    Puerta = v.Puerta,
                    Status = State.Agregado

                };
                bufferRep.Insert(vb);
                return true;
            }
        }
        public async Task<bool> PutAsync(Vuelo v)
        {
            Errors = "";
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                var json = JsonConvert.SerializeObject(v);
                var response = await client.PutAsync("api/vuelos/cambiarestado", new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Errors = await response.Content.ReadAsStringAsync();
                    return false;
                }
            }
            else
            {
                //Si no hay internet agregamos al buffer
                VueloBuffer vb = new VueloBuffer
                {
                    Destino = v.Destino,
                    Estado = v.Estado,
                    Fecha = v.Fecha,
                    Numerovuelo = v.Numerovuelo,
                    Puerta = v.Puerta,
                    Status = State.Modificado

                };
                bufferRep.Insert(vb);
                return true;
            }
        }
    }
}
