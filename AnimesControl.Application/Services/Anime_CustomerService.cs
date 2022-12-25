using AnimesControl.Core.Entities;
using AnimesControl.Core.Interfaces.Repostories;
using AnimesControl.Application.Common.Interfaces.Services;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimesControl.Application.Models.InputModels;

namespace AnimesControl.Domain.Services
{
    public class Anime_CustomerService : IAnime_CustomerService
    {
        private readonly ICustomerService customerService;
        private readonly IAnimeService animeService;
        private readonly IAnime_CustomerRepository anime_customerRepository;
        private readonly IMapper mapper;
        public Anime_CustomerService(ICustomerService _customerService, IAnimeService _animeService,IAnime_CustomerRepository _anime_CustomerRepository, IMapper _mapper)
        {
            customerService = _customerService;
            animeService = _animeService;
            anime_customerRepository = _anime_CustomerRepository;
            mapper = _mapper;
        }

        public void AddAnimeCustomer(Anime_CustomerInputModel model)
        {
            if (model.AnimeId == null || model.CustomerId == null) throw new NullReferenceException();

            var client = customerService.GetByIdCustomer(model.CustomerId);
            var anime = animeService.GetByIdAnimeDetails(model.AnimeId);
            anime_customerRepository.AddAnime_Customer(anime, client);
        }

        public List<Anime_Customer> GetCustomerWithAnimeId(int id)
        {
            if (id == null) throw new NullReferenceException();
            var clients = anime_customerRepository.GetCustomerWithAnimeId(id);
            return clients;
        }
        public List<Anime_Customer> GetAnimeWithCustomerId(int id)
        {
            if (id == null) throw new NullReferenceException();
            var animes = anime_customerRepository.GetAnimeWithCustomerId(id);
            return animes;
        }
        public void RemoveAnimeCustomer(Anime_CustomerInputModel model)
        {

            if (model.AnimeId == null || model.CustomerId == null) throw new NullReferenceException();
            var modelMap = mapper.Map<Anime_Customer>(model);
            anime_customerRepository.RemoveAnimeCustomer(modelMap);
            
        }
    }
}
