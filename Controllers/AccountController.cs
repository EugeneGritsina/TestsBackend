using Microsoft.AspNetCore.Mvc;
using System;
using TestsBackend.Interfaces;

namespace TestsBackend.Controllers
{
    public class AccountController : Controller
    {
        readonly IAuthenticationService _authService;
        public AccountController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("/token")]
        public ActionResult<string> Token(string username, string password)
        {
            var identity = _authService.GetIdentity(username, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
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

    }
}
