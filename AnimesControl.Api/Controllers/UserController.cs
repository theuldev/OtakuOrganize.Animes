using AnimesControl.Application.Common.Interfaces.Services;
using AnimesControl.Application.Models.InputModels;
using AnimesControl.Application.Services;
using AnimesControl.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AnimesControl.Api.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService _userService)
        {
            userService = _userService;

        }
        [HttpPost("create")]
        public IActionResult Create(UserInputModel userInputModel)
        {
            try
            {
                userService.PostUser(userInputModel);
                return Ok(userInputModel);
            }
            catch (NullReferenceException ex)
            {
                return NotFound("Objeto não encontrado: " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                return NotFound("O objeto passado é nulo " + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Ocorreu um erro desconhecido " + ex.Message);
            }
        }
        [HttpPost("login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}
