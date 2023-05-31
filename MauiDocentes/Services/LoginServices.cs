using MauiDocentes.DTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MauiDocentes.Services
{
    public class LoginServices
    {
        private readonly AuthService auth;
        private string url = "https://docentes.itesrc.net/";
        private HttpClient client = new HttpClient();
        public LoginServices(AuthService auth)
        {
            client.BaseAddress = new Uri(url);
            this.auth = auth;
        }
        public async Task<bool> Login(LoginDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Contraseña) || string.IsNullOrEmpty(dto.Usuario))
            {
                throw new ArgumentException("Escriba el nombre de usuario y contraseña");
            }
            var response = await client.PostAsJsonAsync("api/login", dto);
            if (response.IsSuccessStatusCode)
            {
                ///Inicie sesión  y me va a devolver el token 
                var token = await response.Content.ReadAsStringAsync();

                auth.WriteToken(token);
                return true;
            }
            else
            {
                var message = response.Content.ReadAsStringAsync();
                return false;
            }
        }
        public void Logout()
        {
            auth.RemoveToken();
            Shell.Current.GoToAsync("/login");
        }
    }
}
