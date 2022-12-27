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

        public void AddAnimeCustomer(Anime_CustomerInputModel model)
        {
            if (model.AnimeId == null || model.CustomerId == null) throw new NullReferenceException();

            var client = customerService.GetByIdCustomer(model.CustomerId);
            var anime = animeService.GetByIdAnimeDetails(model.AnimeId);
            if (anime.Id == null || client.Id == null) throw new NullReferenceException();

            var modelMap = mapper.Map<Anime_Customer>(model);

            anime_customerRepository.AddAnime_Customer(modelMap);
        }

        public List<Anime_CustomerViewModel> GetCustomerWithAnimeId(int id)
        {

            if (id == null) throw new NullReferenceException();
            var clients = anime_customerRepository.GetCustomerWithAnimeId(id);

            if (clients == null) throw new NullReferenceException();

            var clientsMap = mapper.Map<List<Anime_CustomerViewModel>>(clients);

            return clientsMap;
        }
        public List<Anime_CustomerViewModel> GetAnimeWithCustomerId(int id)
        {
            List<Anime_CustomerViewModel> model = new List<Anime_CustomerViewModel>();
            if (id == null) throw new NullReferenceException();
            var animes = anime_customerRepository.GetAnimeWithCustomerId(id);

            var clientsMap = mapper.Map<List<Anime_CustomerViewModel>>(animes);
            return clientsMap;
        }
        public void RemoveAnimeCustomer(Anime_CustomerInputModel model)
        {

            if (model.AnimeId == null || model.CustomerId == null) throw new NullReferenceException();
            var modelMap = mapper.Map<Anime_Customer>(model);
            anime_customerRepository.RemoveAnimeCustomer(modelMap);

        }
    }
}
