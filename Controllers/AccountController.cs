using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TestsBackend.Entities;
using TestsBackend.Interfaces;

namespace TestsBackend.Controllers
{
    public class AccountController : Controller
    {
        readonly IAuthenticationService _authService;
        readonly IUserService _userService;
        public AccountController(IAuthenticationService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("/token")]
        public ActionResult<string> Token(string username, string password)
        {
            var identity = _authService.GetIdentity(username, password);
            if (identity == null)
            {
                return BadRequest(new { message = "Invalid username or password." });
            }
            try
            {
                return (_authService.CreateJWT(identity));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("/user")]
        //[Authorize(Roles = "student")]
        public ActionResult<User> GetUser(int id)
        {
            try
            {
                return _userService.GetUser(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
