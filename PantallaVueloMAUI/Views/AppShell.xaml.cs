namespace PantallaVueloMAUI;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("estado", typeof(Views.EstadosView);

		MainPage = new  Views.AppShell();
	}
}