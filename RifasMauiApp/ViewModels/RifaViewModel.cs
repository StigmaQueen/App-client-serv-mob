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

        public IEnumerable<uint> BoletosSinVender =>Boletos.Where(x=>x.Id==0).Select(x=>x.NumeroBoleto).ToList();
        public ICommand VenderCommand { get; set; }
        public ICommand PagarCommand { get; set; }
        public ICommand NuevaVentaCommand { get; set; } 
        public ICommand CancelarVentaCommand { get; set; }

        RifaServices services= new RifaServices();  
        public RifaViewModel() 
        {
            DescargarBoletos();
            NuevaVentaCommand = new Command<Boleto>(NuevaVenta);
            VenderCommand = new Command(Vender);
        }

        private async void Vender(object obj)
        {
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    BoletoDTO dto = new BoletoDTO()
                    {
                        NombrePersona = Boleto.NombrePersona,
                        NumeroBoleto = Boleto.NumeroBoleto,
                        Pagado = Boleto.Pagado ? 1ul : 0ul,
                    };
                    services.Post(dto);
                    await Shell.Current.GoToAsync("//Main");
                }
            }
            catch(Exception ex) 
            {
                Error = ex.Message; 
            }
        }

        private async void NuevaVenta(Boleto b)
        {
            if (b.Id == 0)
            {
                Boleto= b;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
                await Shell.Current.GoToAsync("//Agregar");
            }
           
        }

        async void DescargarBoletos()
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                var vendidos = await services.GetAll();
                //Genera los boletos en blanco
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

                //
                foreach (var bv in vendidos)
                {
                    Boleto bvendido = new Boleto
                    {
                        Id= bv.Id,  
                        NombrePersona = bv.NombrePersona,   
                        NumeroBoleto= bv.NumeroBoleto,
                        NumeroTelefono= bv.NumeroTelefono,
                        Pagado=bv.Pagado==1
                    };
                    Boletos[(int)(bvendido.NumeroBoleto-1)]=bvendido;  
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
