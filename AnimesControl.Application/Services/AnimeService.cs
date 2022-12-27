using AnimesControl.Application.Common.Interfaces.Services;
using AnimesControl.Core.Interfaces.Repostories;
using AnimesControl.Core.Entities;
using System.ComponentModel;
using AutoMapper;
using AnimesControl.Application.Models.InputModels;
using AnimesControl.Application.Models.ViewModels;

namespace AnimesControl.Application.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly IAnimeRepository repository;
        private readonly IMapper mapper;
        public AnimeService(IAnimeRepository repository, IMapper _mapper)
        {
            mapper = _mapper;
            this.repository = repository;
        }

        public void DeleteAnime(int id)
        {
            var anime = repository.GetByIdAnimeDetails(id);
            if (anime == null) throw new NullReferenceException();
            repository.DeleteAnime(anime);
        }

        public IEnumerable<AnimeViewModel> GetAnimes()
        {
            var animes = repository.GetAnimes();
            if (animes.Count <= 0) throw new NullReferenceException();

            var animeMap = mapper.Map<List<AnimeViewModel>>(animes);

            return animeMap.OrderBy(sp => sp.Id);
        }

        public AnimeViewModel GetByIdAnimeDetails(int? id)
        {
            if (id == null) throw new NullReferenceException();
            var anime = repository.GetByIdAnimeDetails(id);
            var animeMap = mapper.Map<AnimeViewModel>(anime);
            return animeMap;

        }

        public void PostAnime(AnimeInputModel animeDetails)
        {
            if (animeDetails == null) throw new NullReferenceException();

            var animeMap = mapper.Map<Anime>(animeDetails);
            repository.PostAnime(animeMap);
        }

        public void PutAnime(int id, AnimeInputModel animeDetails)
        {
            var anime = GetByIdAnimeDetails(id);

            if (!anime.Equals(animeDetails)) return;

            var animeMap = mapper.Map<Anime>(animeDetails);

            repository.PutAnime(animeMap);
        }
    }
}