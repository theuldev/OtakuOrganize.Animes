using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using AnimesControl.Domain.Models;
namespace AnimesControl.Api.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class AnimeController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get()
        {
            return Ok();
        }
        [HttpGet("/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }
        [HttpPost()]
        public IActionResult Post(AnimeDetail anime)
        {
            return CreatedAtAction("GetById", new { id = anime.Id} ,anime);
        }
        [HttpPut()]
        public IActionResult Put()
        {
            return Ok();
        }
        [HttpDelete()]
        public IActionResult Delete()
        {
            return Ok();
        }
 

    }
}