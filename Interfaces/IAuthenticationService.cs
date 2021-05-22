using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TestsBackend.Interfaces
{
    public interface IAuthenticationService
    {
        public ClaimsIdentity GetIdentity(string username, string password);
        public string CreateJWT(ClaimsIdentity identity);
    }
}
