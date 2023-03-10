using Microsoft.AspNetCore.Mvc;

namespace AnimesControl.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public IActionResult Create()
        {

            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
