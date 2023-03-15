using PantallaVueloMAUI.Models;
using PantallaVueloMAUI.Repositories;
using PantallaVueloMAUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantallaVueloMAUI.ViewModel
{
    public class VuelosViewModel : INotifyPropertyChanged
    {

        //Conbinacion de los dos tablas, la del buffer(local) y en la otra los datos reales. Modifica lo que el usario ve
        //en el VwiewModel
        VueloServices service = new VueloServices();
        VueloRepository<VueloBuffer>BufferRepositoriesVuelos { get; set; }= new VueloRepository<VueloBuffer>();
        public ObservableCollection<Vuelo>  Vuelos { get; set; } = new ObservableCollection<Vuelo>();
        public VuelosViewModel()
        {

        }

        async void Llenar()
        {
            //Recupero los registros de la bd local (proviene de la api)
            var lista = (await service.GetAsync()).ToList();

            //Abrir el buffer de vuelos 
            var pendientes = BufferRepositoriesVuelos.GetAll();

            //Combinar
            foreach (var item in pendientes)
            {
                if (item.Status == State.Agregado)
                {
                    lista.Add(new Vuelo
                    {
                        Id= 0,
                        Destino= item.Destino,
                       Estado= item.Estado, 
                        Fecha= item.Fecha,
                        Numerovuelo= item.Numerovuelo,
                        Puerta= item.Puerta
                    });
                }
                if (item.Status == State.Modificado)
                {
                    var anterior = lista.FirstOrDefault(x=>x.Numerovuelo==item.Numerovuelo);
                    anterior.Destino = item.Destino;
                    anterior.Puerta = item.Puerta;
                    anterior.Estado= item.Estado;
                }

            }
            Vuelos.Clear();
            lista.ForEach(x => Vuelos.Add(x));

        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
