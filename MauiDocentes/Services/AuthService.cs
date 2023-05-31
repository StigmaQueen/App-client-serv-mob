using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MauiDocentes.Services
{
    public class AuthService
    {
        string token;

        public bool IsAuthenticated => ReadToken().Result != null;
        public IEnumerable<Claim> Claims
        {
            get
            {
                JwtSecurityTokenHandler handler = new();
                var tkn = handler.ReadJwtToken(token);
                return tkn.Claims;
            }
        }

        public bool IsValid
        {
            get
            {
                JwtSecurityTokenHandler handler = new();
                var tkn = handler.ReadJwtToken(token);
                return DateTime.UtcNow <= tkn.ValidTo;
            }
        }

        //Write
        public async void WriteToken(string token)
        {
            this.token = token;
            await SecureStorage.SetAsync("JwtToken", token);
        }
        //Read
        public async Task<string> ReadToken()
        {
            token = await SecureStorage.GetAsync("JwtToken");
            return token;
        }

        /// Remove
        public void RemoveToken()
        {
            SecureStorage.Remove("JwtToken");
            token = null;
        }
    }
}
