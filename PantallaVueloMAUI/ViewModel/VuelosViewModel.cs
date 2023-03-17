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
using System.Windows.Input;

namespace PantallaVueloMAUI.ViewModel
{
    public class VuelosViewModel : INotifyPropertyChanged
    {

        //Conbinacion de los dos tablas, la del buffer(local) y en la otra los datos reales. Modifica lo que el usario ve
        //en el VwiewModel
        VueloServices service;
        VueloRepository<VueloBuffer> BufferRepositoriesVuelos = new();
        public string Error { get; set; } = "";
        public ObservableCollection<Vuelo> Vuelos { get; set; } = new();
        public Vuelo Vuelo { get; set; } = new();
        public ICommand AgregarCommand { get; set; }

        public VuelosViewModel()
        {
            service = new VueloServices();
            AgregarCommand = new Command(Agregar);
            Llenar();
        }

        private async void Agregar()
        {
            //Validar
            var agg = await service.PostAsync(Vuelo);
            if (agg)
            {
                Llenar();
                Vuelo = new();
                await Shell.Current.GoToAsync("//principal");
            }
            else
            {
                Error = service.Errors;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Error)));
            }

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
                        Id = 0,
                        Destino = item.Destino,
                        Estado = item.Estado,
                        Fecha = item.Fecha,
                        Numerovuelo = item.Numerovuelo,
                        Puerta = item.Puerta
                    });
                }
                if (item.Status == State.Modificado)
                {
                    var anterior = lista.FirstOrDefault(x => x.Numerovuelo == item.Numerovuelo);
                    anterior.Destino = item.Destino;
                    anterior.Puerta = item.Puerta;
                    anterior.Estado = item.Estado;
                }

            }
            Vuelos.Clear();
            lista.ForEach(x => Vuelos.Add(x));

        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
