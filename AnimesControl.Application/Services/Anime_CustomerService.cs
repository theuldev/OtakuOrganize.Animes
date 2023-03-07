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
using AnimesControl.Application.Models.ViewModels;
using System.Data;

namespace AnimesControl.Application.Services
{
    public class Anime_CustomerService : IAnime_CustomerService
    {
        private readonly ICustomerService customerService;
        private readonly IAnimeService animeService;
        private readonly IAnime_CustomerRepository anime_customerRepository;
        private readonly IMapper mapper;
        public Anime_CustomerService(ICustomerService _customerService, IAnimeService _animeService, IAnime_CustomerRepository _anime_CustomerRepository, IMapper _mapper)
        {
            customerService = _customerService;
            animeService = _animeService;
            anime_customerRepository = _anime_CustomerRepository;
            mapper = _mapper;
        }

        public async Task AddAnimeCustomer(Anime_CustomerInputModel model)
        {
            var client = await customerService.GetByIdCustomer(model.CustomerId);
            var anime = await animeService.GetByIdAnimeDetails(model.AnimeId);
            if (anime.Id == null || client.Id == null) throw new NullReferenceException();

            var modelMap = mapper.Map<Anime_Customer>(model);

            anime_customerRepository.AddAnime_Customer(modelMap);
        }

        public async Task<List<Anime_CustomerViewModel>> GetCustomerWithAnimeId(int? id)
        {

            if (id == null) throw new ArgumentNullException();
            var clients = await anime_customerRepository.GetCustomersWithAnimeId(id);

            if (clients == null) throw new NullReferenceException();

            var clientsMap = mapper.Map<List<Anime_CustomerViewModel>>(clients);

            return clientsMap;
        }
        public async Task<List<Anime_CustomerViewModel>> GetAnimeWithCustomerId(int? id)
        {
            if (id == null) throw new ArgumentNullException();
            var animes = await anime_customerRepository.GetAnimesWithCustomerId(id);
            if (animes == null) throw new NullReferenceException();

            var animeMap = mapper.Map<List<Anime_CustomerViewModel>>(animes);
            return animeMap;
        }
        public void RemoveAnimeCustomer(Anime_CustomerInputModel anime)
        {

            if (anime.AnimeId == null || anime.CustomerId == null) throw new NullReferenceException();

            var animeMap = mapper.Map<Anime_Customer>(anime);
            anime_customerRepository.RemoveAnimeCustomer(animeMap);

        }
    }
}
