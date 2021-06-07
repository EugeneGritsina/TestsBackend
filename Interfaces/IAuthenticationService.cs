using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TestsBackend.Interfaces
{
    public interface IAuthenticationService
    {
        public (ClaimsIdentity, int?) GetIdentity(string username, string password);
        public string CreateJWT((ClaimsIdentity, int?) identity);
    }
}
