using MauiDocentes.DTos;
using MauiDocentes.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDocentes.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        //Comando
        public Command LoginCommand { get; set; }

        private readonly LoginServices login;
        private readonly AuthService auth;
        public LoginDTO Credenciales { get; set; }= new ();

        public string Mensaje { get; set; }
        public LoginViewModel(LoginServices login)
        {
            this.login = login;
            LoginCommand = new Command(LogIn);
        }
        private async void LogIn()
        {
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    if (!await login.Login(Credenciales))
                    {
                        Mensaje = "Nombre de usuario o contraseña incorrectas";
                    }
                    else
                        Mensaje = "No hay conexion a internet";

                    PropertyChange();
                }
            }
            catch(Exception ex)
            {
                Mensaje = ex.Message;
                PropertyChange();
            }
        }

        public void PropertyChange(string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
