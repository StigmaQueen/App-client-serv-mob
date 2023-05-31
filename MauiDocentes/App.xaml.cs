
using MauiDocentes.Services;

namespace MauiDocentes
{
    public partial class App : Application
    {
        public App(AuthService auth, LoginServices login)
        {
            InitializeComponent();

            MainPage = new AppShell(auth,login);
        }
    }
}