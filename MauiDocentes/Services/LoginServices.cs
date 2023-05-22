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
        private string url = "https://docentes.itesrc.net/";
        private HttpClient client= new HttpClient();    
        public LoginServices()
        {
            client.BaseAddress = new Uri(url);
        }
        public async Task <bool> login(LoginDTO dto)
        {
            if(string.IsNullOrEmpty(dto.Contraseña) || string.IsNullOrEmpty(dto.Usuario))
            {
                throw new ArgumentException("Escriba el nombre de usuario y contraseña");
            }
            var response = await client.PostAsJsonAsync("api/login", dto);
            if (response.IsSuccessStatusCode)
            {
                ///Inicie sesión  y me va a devolver el token 
                var token = await response.Content.ReadFromJsonAsync<string>();
            }
            else
            {
                return false;   
            }
        }
    }
}
