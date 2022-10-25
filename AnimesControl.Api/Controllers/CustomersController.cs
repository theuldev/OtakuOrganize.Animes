using Microsoft.AspNetCore.Mvc;

namespace AnimesControl.Api.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class CustomersController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login()
        {
            return Ok();
        }
        [HttpPost("authenticate")]
        public IActionResult Authenticate()
        {
            return Ok();
        }

    }
}