namespace RifasMauiApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            Routing.RegisterRoute("//Agregar", typeof(Views.VenderBoletoView));
        }
    }
}