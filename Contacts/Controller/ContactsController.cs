using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        [HttpGet]
        [Route("alive")]
        public IActionResult IsAlive()
        {
            return Ok("Contacts API is alive!");
        }
    }
}
