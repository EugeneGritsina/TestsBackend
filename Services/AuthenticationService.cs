using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using TestsBackend.Authentication;
using TestsBackend.Entities;
using TestsBackend.Interfaces;
using TestsBackend.Models;

namespace TestsBackend.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        TestsContext _testsContext;
        public AuthenticationService(TestsContext testsContext)
        {
            _testsContext = testsContext;
        }
        public ClaimsIdentity GetIdentity(string username, string password)
        {
            User user = _testsContext.Users.FirstOrDefault(x => x.Email == username && x.Password == password);
            if (user != null)
            {
                Role role = _testsContext.Roles.FirstOrDefault(role => role.Id == user.Role);
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }

        public string CreateJWT(ClaimsIdentity identity)
        {

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return JsonConvert.SerializeObject(response);
        }
    }
}

