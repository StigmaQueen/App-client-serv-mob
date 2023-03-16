using PantallaVueloMAUI.ViewModel;

namespace PantallaVueloMAUI
{
    public partial class App : Application
    {
        VuelosViewModel v = new();

        public App()
        {
            InitializeComponent();

            Routing.RegisterRoute("estado",typeof(Views.EstadosView));

            MainPage=new AppShell();
        }
    }
}