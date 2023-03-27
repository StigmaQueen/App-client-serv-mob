namespace PruebaSignalRMaui;
using Microsoft.AspNetCore.SignalR.Client;
public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }
    int numero = 0;
    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        var connection = new HubConnectionBuilder().WithUrl("https://pruebas.sistemas19.com/numerosHub").Build();
        connection.On("incrementar", () =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                numero++;
                lblValor.Text = numero.ToString();
            });

        });
        connection.On("decrementar", () =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                numero--;
                lblValor.Text = numero.ToString();
            });

        });
        await connection.StartAsync();
    }
    HttpClient client= new HttpClient();
    private async void btnIncrementar_Clicked(object sender, EventArgs e)
    {
        var result = await client.GetAsync("https://pruebas.sistemas19.com/api/numeros/incrementar");
        result.EnsureSuccessStatusCode();
    }

    private async void btnDecrementar_Clicked(object sender, EventArgs e)
    {
        var result = await client.GetAsync("https://pruebas.sistemas19.com/api/numeros/decrementar");
        result.EnsureSuccessStatusCode();
    }
}