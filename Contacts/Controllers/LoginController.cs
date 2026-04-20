using Contacts.Interfaces;
using Contacts.Security;
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
        public ActionResult<JwtToken> Login()
        {
            var token = _jwt.GenerateJwtToken("user");
            if (string.IsNullOrEmpty(token.Token))
            {
                return BadRequest("Error generating JWT token");
            }
            return Ok(token);
        }
    }
}
