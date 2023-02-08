
using PandientesApp.Models;
using PandientesApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandientesApp.ViewModels
{
    public class PendientesViewModel : INotifyPropertyChanged
    {
        PendientesServices ServicePendientes;
        public ObservableCollection<Actividad> Actividades { get; set; } = new ObservableCollection<Actividad>();
        public string Error { get; set; }
        public PendientesViewModel()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            ServicePendientes= new PendientesServices();
             _= CargarDatos();
            
        }

        private async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            await CargarDatos();
        }

        async Task CargarDatos()
        {
            if(Connectivity.Current.NetworkAccess==NetworkAccess.Internet) //Verficar si hay conexión a internet
            {
                var lista= await ServicePendientes.GetAll();
                Actividades.Clear();
                lista.ForEach(a => Actividades.Add(a));
                Error = "";
            }
            else
            {
                Error = "No hay conexión internet";
             
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
