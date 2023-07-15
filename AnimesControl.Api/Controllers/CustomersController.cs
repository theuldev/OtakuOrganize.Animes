
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AnimesControl.Application.Models.InputModels;
using AnimesControl.Application.Services;
using AnimesControl.Application.Common.Interfaces.Services;

namespace AnimesControl.Api.Controllers
{
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerservice;

        IAnime_CustomerService anime_CustomerService;
        public CustomersController(ICustomerService _customerservice, IAnime_CustomerService _anime_CustomerService)
        {
            customerservice = _customerservice;
            anime_CustomerService = _anime_CustomerService;

        }
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            try
            {
                var clients = await customerservice.GetCustomers();
                return Ok(clients);
            }
            catch (NullReferenceException ex)
            {
                return NotFound("Não foi encontrado nenhum objeto: " + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Ocorreu um erro: " + ex.Message);
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var client = await customerservice.GetByIdCustomer(id);
                return Ok(client);
            }
            catch (NullReferenceException ex)
            {
                return NotFound("Não foi encontrado nenhum cliente: " + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Ocorreu o erro: " + ex.Message);
            }
        }
        [HttpPost()]
        public IActionResult Post(CustomerInputModel customer)
        {
            try
            {
                customerservice.PostCustomer(customer);
                return CreatedAtAction("GetById", new { customer.Id }, customer);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest("O objeto é nulo" + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Ocorreu um erro: " + ex.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, CustomerInputModel customer)
        {
            try
            {

                customerservice.PutCustomer(id, customer);
                return NoContent();
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("Ocorreu o seguinte erro durante a validação das informações: " + ex.Message);

            }
            catch (Exception ex)
            {
                return BadRequest("Ocorreu o seguinte erro: " + ex.Message);

            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                customerservice.DeleteCustomer(id);
                return Ok("Cliente excluido com êxito");

            }
            catch (NullReferenceException ex)
            {
                return NotFound("Cliente não encontrado" + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex.Message);
            }
        }
        [HttpPost("Add")]
        public IActionResult Add(Anime_CustomerInputModel model)
        {
            try
            {

                if (model == null) throw new NullReferenceException("Model é invalido" + model);
                anime_CustomerService.AddAnimeCustomer(model);
                return Ok(model);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("O objeto passado é nulo" + ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Objeto já existente na coleção");
            }
            catch (Exception ex)
            {
                return BadRequest("Cliente ou Anime não encontrado :" + ex.Message);
            }

        }
        [HttpGet("GetCustomerWithAnimeId/{id}")]
        public IActionResult GetCustomerWithAnimeId(Guid id)
        {
            try
            {
                var customers = anime_CustomerService.GetCustomerWithAnimeId(id);
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
        [HttpDelete("DeleteRelationshipAnimeCustomer")]
        public IActionResult DeleteAnimeCustomer(Anime_CustomerInputModel model)
        {
            try
            {

                anime_CustomerService.RemoveAnimeCustomer(model);
                return Ok(model);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("Objeto não encontrado " + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Ocorreu o seguinte erro" + ex.Message);
            }

        }

    }
}
