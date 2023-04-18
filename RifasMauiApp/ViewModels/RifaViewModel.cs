using RifasMauiApp.Models;
using RifasMauiApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RifasMauiApp.ViewModels
{
    public class RifaViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<Boleto> Boletos { get; set; } = new ObservableCollection<Boleto>();
        public Boleto Boleto { get; set; }  
        public string Error { get; set; }   
        public ICommand VenderCommand { get; set; }
        public ICommand PagarCommand { get; set; }
        public ICommand NuevaVentaCommand { get; set; } 
        public ICommand CancelarVentaCommand { get; set; }

        RifaServices services= new RifaServices();  
        public RifaViewModel() 
        {
            DescargarBoletos();
        }  
        async void DescargarBoletos()
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                var vendidos = await services.GetAll();
                if (Boletos.Count < 50)
                {
                    for (int i = 1; i < 50; i++)
                    {
                        Boleto b = new Boleto()
                        {
                            NumeroBoleto =(uint) i
                        };
                        Boletos.Add(b); 
                    }
                }
            }
            else
            {
                Error = "No hay conexion a internet";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Error)));
            }
          
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
