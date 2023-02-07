
using PandientesApp.Models;
using PandientesApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandientesApp.ViewModels
{
    public class PendientesViewModel : INotifyCollectionChanged
    {
        PendientesServices ServicePendientes;
        public ObservableCollection<Actividad> Actividades { get; set; }
        public PendientesViewModel()
        {
            ServicePendientes= new PendientesServices();
            CargarDatos();
        }

        async void CargarDatos()
        {
            if(Connectivity.Current.NetworkAccess==NetworkAccess.Internet) //Verficar si hay conexión a internet
            {
                Actividades.Clear();
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
