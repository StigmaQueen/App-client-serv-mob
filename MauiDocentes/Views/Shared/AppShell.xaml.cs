using MauiDocentes.Services;
using MauiDocentes.ViewModels;

namespace MauiDocentes
{
    public partial class AppShell : Shell
    {
        public AppShell(AuthService  auth, LoginServices login)
        {
            this.BindingContext = new MainViewModel(auth, login);
            InitializeComponent();
        }

        
    }
}