using AutoMapper;
using OtakuOrganize.Application.Common.Interfaces.Services;
using OtakuOrganize.Application.Models.InputModels;
using OtakuOrganize.Application.Models.ViewModels;
using OtakuOrganize.Core.Entities;
using OtakuOrganize.Core.Interfaces.Repositories;

namespace OtakuOrganize.Application.Services
{
    public class Anime_CustomerService : IAnime_CustomerService
    {
        private readonly ICustomerRepository  customerRepository;
        private readonly IAnimeRepository animeRepository;
        private readonly IAnime_CustomerRepository anime_customerRepository;
        private readonly IMapper mapper;
        public Anime_CustomerService(ICustomerRepository _customerRepository, IAnimeRepository _animeRepository, IAnime_CustomerRepository _anime_CustomerRepository, IMapper _mapper)
        {
            customerRepository = _customerRepository;
            animeRepository = _animeRepository;
            anime_customerRepository = _anime_CustomerRepository;
            mapper = _mapper;
        }

        public void AddAnimeCustomer(Anime_CustomerInputModel model)
        {
            var exist = anime_customerRepository.ExistAnimeAndCustomer(model.AnimeId, model.CustomerId);

            var modelMap = (exist.Result) ? mapper.Map<Anime_Customer>(model) : throw new NullReferenceException();


            anime_customerRepository.AddAnime_Customer(modelMap);
        }

        public async Task<List<Anime_CustomerViewModel>> GetCustomerWithAnimeId(Guid? id)
        {

            if (id.Equals(null)) throw new ArgumentNullException();
            var clients = await anime_customerRepository.GetCustomersWithAnimeId(id);

            var clientsMap = clients != null ? mapper.Map<List<Anime_CustomerViewModel>>(clients) : throw new NullReferenceException();



            return clientsMap;
        }
        public async Task<List<Anime_CustomerViewModel>> GetAnimeWithCustomerId(Guid? id)
        {
            if (id.Equals(null)) throw new ArgumentNullException();
            var animes = await anime_customerRepository.GetAnimesWithCustomerId(id);
            var animeMap = animes != null ? mapper.Map<List<Anime_CustomerViewModel>>(animes) : throw new NullReferenceException();
            return animeMap;
        }
        public void RemoveAnimeCustomer(Anime_CustomerInputModel anime)
        {
            var animeMap =  anime.AnimeId.Equals(null) || anime.CustomerId.Equals(null) ? mapper.Map<Anime_Customer>(anime) :  throw new NullReferenceException();
    
            anime_customerRepository.RemoveAnimeCustomer(animeMap);

        }
    }
}
