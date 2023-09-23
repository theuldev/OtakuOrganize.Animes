using OtakuOrganize.Application.Common.Interfaces.Services;
using OtakuOrganize.Application.Models.InputModels;
using OtakuOrganize.Application.Services;
using OtakuOrganize.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OtakuOrganize.Api.Controllers
{
    [AllowAnonymous]
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
        public async Task<IActionResult> Login(LoginInputModel user)
        {
            try
            {
                var token = await userService.Login(user);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }


        }
    }
}
