using MauiDocentes.Services;
using MauiDocentes.ViewModels;

namespace MauiDocentes.Views.Login;

public partial class LoginView : ContentPage
{
	public LoginView(LoginServices login)
	{

		InitializeComponent();
		this.BindingContext = new LoginViewModel(login);
	}
}