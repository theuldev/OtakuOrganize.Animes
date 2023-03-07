using AnimesControl.Application.Common.Interfaces.Services;
using AnimesControl.Core.Interfaces.Repostories;
using AnimesControl.Core.Entities;
using System.ComponentModel;
using AutoMapper;
using AnimesControl.Application.Models.InputModels;
using AnimesControl.Application.Models.ViewModels;
using AnimesControl.Infra.Caching;
using Newtonsoft.Json;
using AnimesControl.Core.Exceptions;

namespace AnimesControl.Application.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly IAnimeRepository repository;
        private readonly IMapper mapper;
        private readonly ICachingService cachingService;

        public AnimeService(IAnimeRepository _repository, IMapper _mapper,ICachingService _cachingService )
        {
            mapper = _mapper;
            repository = _repository;
            cachingService = _cachingService;
        }

        public async void DeleteAnime(int id)
        {
            var anime = await repository.GetByIdAnimeDetails(id);
            if (anime == null) throw new NullReferenceException();
            repository.DeleteAnime(anime);
        }

        public async Task<IEnumerable<AnimeViewModel>> GetAnimes()
        {
            var animes = await repository.GetAnimes();
            if (animes.Count <= 0) throw new ArgumentNullException();

            var animeMap = mapper.Map<IEnumerable<AnimeViewModel>>(animes);

            return animeMap.OrderBy(sp => sp.Id);
        }

        public async Task<AnimeViewModel> GetByIdAnimeDetails(int? id)
        {
            if (id == null) throw new ArgumentNullException();

            Anime anime;
            var cacheValue = await cachingService.GetAsync(id.ToString());
            if (!string.IsNullOrWhiteSpace(cacheValue))
            {
                anime = JsonConvert.DeserializeObject<Anime>(cacheValue);
            }
            else
            {
                anime = await repository.GetByIdAnimeDetails(id);
                await cachingService.SetAsync(id.ToString(),JsonConvert.SerializeObject(anime));

            }
           
            var animeMap = mapper.Map<AnimeViewModel>(anime);
            return animeMap;

        }

        public void PostAnime(AnimeInputModel animeDetails)
        {
            if (animeDetails == null) throw new ArgumentNullException();

            var animeMap = mapper.Map<Anime>(animeDetails);
            repository.PostAnime(animeMap);
        }

        public void PutAnime(int id, AnimeInputModel animeDetails)
        { 

            if (!animeDetails.Id.Equals(id)) throw new CredentialsNotEqualsException();

            var exist = repository.AnimeExists(id);

            if (!exist) throw new NullReferenceException();
            var animeMap = mapper.Map<Anime>(animeDetails);
            repository.PutAnime(animeMap);
        }
    }
}