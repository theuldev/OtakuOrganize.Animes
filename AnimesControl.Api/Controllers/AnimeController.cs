using Microsoft.AspNetCore.Mvc;
using AnimesControl.Application.Common.Interfaces.Services;
using AnimesControl.Application.Models.InputModels;
using AutoMapper;
using AnimesControl.Core.Entities;

namespace AnimesControl.Api.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class AnimeController : ControllerBase
    {
        private readonly IAnimeService animeservice;
        private readonly IAnime_CustomerService animecustomerService;

        public AnimeController(IAnimeService _animeservice, IAnime_CustomerService _animecustomerService)
        {
            animeservice = _animeservice;
            animecustomerService = _animecustomerService;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            try
            {
                var clients = await animeservice.GetAnimes();
                return Ok(clients);

            }
            catch (NullReferenceException ex)
            {
                return NotFound("Nenhum objeto foi retornado: " + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex.Message);
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var client = await animeservice.GetByIdAnimeDetails(id);
                return Ok(client);
            }
            catch (NullReferenceException ex)
            {
                return NotFound("Nenhum objeto com o Id indicado foi retornado: " + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex.Message);
            }
        }
        [HttpPost()]
        public IActionResult Post(AnimeInputModel anime)
        {
            try
            {
                animeservice.PostAnime(anime);
                return CreatedAtAction("GetById", new { id = anime.Id }, anime);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("Ocorreu um erro de natureza nula: " + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex);
            }


        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, AnimeInputModel anime)
        {
            if (id.Equals(anime.Id)) return BadRequest("O id passado não é o mesmo do usuário");

            try
            {
                animeservice.PutAnime(id, anime);
                return NoContent();
            }
            catch (NullReferenceException ex)
            {
                return NotFound("Ocorreu o seguinte erro na validação das informações: " + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                animeservice.DeleteAnime(id);
                return Ok("Anime excluido com sucesso");
            }
            catch (NullReferenceException ex)
            {
                return NotFound("Id não encontrado: " + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex);
            }
        }
        [HttpGet("GetAnimeWithCustomerId/{id}")]
        public async Task<IActionResult> GetAnimeWithCustomerId(int id)
        {

            try
            {
                var customers = await animecustomerService.GetAnimeWithCustomerId(id);
                return Ok(customers);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("Nenhum anime foi encontrado: " + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Ocorreu o seguinte erro" + ex.Message);
            }
        }


    }
}