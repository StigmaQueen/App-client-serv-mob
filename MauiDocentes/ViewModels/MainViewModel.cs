using MauiDocentes.Services;
using MauiDocentes.Views.Docentes;
using MauiDocentes.Views.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDocentes.ViewModels
{
    public class MainViewModel
    {
        private DocentesView viewDocentes = new DocentesView();
        private LoginView loginView;
        private readonly AuthService auth;

        public ContentPage Vista { get; set; }

        public MainViewModel(AuthService auth, LoginServices login)
        {
            this.auth = auth;
            if (auth.IsAuthenticated)
            {
                viewDocentes = new();
                Vista = viewDocentes;

            }
            else
            {
                loginView = new(login);
                Vista = loginView;
            }

            PropertyChange();
        }

        public void PropertyChange(string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
