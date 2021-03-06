﻿using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using TestsBackend.Authentication;
using TestsBackend.Entities;
using TestsBackend.Interfaces;

namespace TestsBackend.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        UserContext _userService;
        public AuthenticationService(UserContext userService) => _userService = userService;

        public (ClaimsIdentity, int?) GetIdentity(string username, string password)
        {
            User user = _userService.Users.FirstOrDefault(x => x.Email == username && x.Password == password);
            if (user != null)
            {
                Role role = _userService.Roles.FirstOrDefault(role => role.Id == user.Role);
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return (claimsIdentity, user.Id);
            }
            return (null, null);
        }

        public string CreateJWT((ClaimsIdentity, int?) identity)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Item1.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                    
                    );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Item1.Name,
                userId = identity.Item2,
            };

            return JsonConvert.SerializeObject(response);
        }
    }
}

