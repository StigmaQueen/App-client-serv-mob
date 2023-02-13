
using PandientesApp.Models;
using PandientesApp.Services;
using PandientesApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PandientesApp.ViewModels
{
    public class PendientesViewModel : INotifyPropertyChanged
    {
        PendientesServices ServicePendientes;
        public ICommand NuevoCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand SeleccionarCommand { get; set; }
        public ObservableCollection<Actividad> Actividades { get; set; } = new ObservableCollection<Actividad>();
        public string Error { get; set; }
        public Actividad Actividad { get; set; }
        ActividadView actividadView;
        public PendientesViewModel()
        {
            NuevoCommand = new Command(Nuevo);
            GuardarCommand = new Command(Guardar);
            SeleccionarCommand = new Command<Actividad>(Seleccionar);

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            ServicePendientes= new PendientesServices();
             _= CargarDatos();
            
        }

        private async void Seleccionar(Actividad a)
        {

#if ANDROID
            if (actividadView == null)
                actividadView = new() { BindingContext = this };
#else
            actividadView = new() { BindingContext = this };
#endif

            Error = "";
            Actividad = a;
            Actualizar(nameof(Actividad));
            await Application.Current.MainPage.Navigation.PushAsync(actividadView);

        }

        private async void Guardar()
        {
            try
            {
                Error = "";
                if (string.IsNullOrWhiteSpace(Actividad.Descripcion))
                    Error = "Escriba una descripción para la actividad";

                else if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                {
                    Error = "No hay conexión a internet";
                }
                else
                {
                    if(Connectivity.Current.NetworkAccess!=NetworkAccess.Internet)
                    {
                        await ServicePendientes.Insert(Actividad);
                    }
                    else
                    {
                        await ServicePendientes.Update(Actividad);
                    }
                    await ServicePendientes.Insert(Actividad);
                    _ = CargarDatos();
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                Actualizar();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                Actualizar();

            }
        }

        private async void Nuevo()
        {
            Actividad = new();
            Error = "";

#if ANDROID
            if (actividadView == null)
                actividadView = new() { BindingContext = this };
#else
            actividadView = new() { BindingContext = this };
#endif


            await Application.Current.MainPage.Navigation.PushAsync(actividadView);
            Actualizar();

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
            Actualizar();
        }
        public void Actualizar(string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
