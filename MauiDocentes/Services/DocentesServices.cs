using MauiDocentes.DTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MauiDocentes.Services
{
    public class DocentesServices
    {
        public string url = "https://docentes.itesrc.net/";
        private readonly AuthService auth;
        private readonly LoginServices login;
        HttpClient client = new HttpClient();

        public DocentesServices(AuthService auth, LoginServices login)
        {
            client.BaseAddress = new Uri(url);
            this.auth = auth;
            this.login = login;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer" + auth.ReadToken().Result);
        }

        public async Task<List<Docente>> Get()
        {
            var response = await client.GetAsync("api/docentes");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Docente>>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                login.Logout();
            }
            return null;
        }
    }
}
