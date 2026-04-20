using Contacts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IJwt _jwt;

        public LoginController(IJwt jwt)
        {
            _jwt = jwt;
        }

        [HttpGet]
        public ActionResult<string> Login()
        {
            var token = _jwt.GenerateJwtToken("testuser");
            return Ok(token);
        }
    }
}
