using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDocentes.Services
{
    public class AuthService
    {
        string token;
        public async void WriteToken(string token)
        {
            this.token = token;
            await SecureStorage.SetAsync("JwtToken", token);
        }
        public async Task<string> ReadToken()
        {
            token = await SecureStorage.GetAsync("JwtToken");
            return token;
        }
        public void RemoveToken()
        {
            SecureStorage.Remove("JwtToken");
            token = null;
        }
    }
}
