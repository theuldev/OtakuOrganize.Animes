using AnimesControl.Application.Common.Interfaces.Services;
using AnimesControl.Core.Interfaces.Repostories;
using AnimesControl.Core.Entities;
using System.ComponentModel;
using AutoMapper;
using AnimesControl.Application.Models.InputModels;

namespace AnimesControl.Domain.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly IAnimeRepository repository;
        private readonly IMapper mapper;
        public AnimeService(IAnimeRepository repository , IMapper _mapper)
        {
            mapper = _mapper;
            this.repository = repository;
        }

        public void DeleteAnime(int id)
        {   
            var anime = repository.GetByIdAnimeDetails(id);
            if(anime == null) throw new NullReferenceException();
            repository.DeleteAnime(anime);
        }

        public IEnumerable<Anime> GetAnimes()
        {
            var animes = repository.GetAnimes();
            if (animes.Count <= 0) throw new NullReferenceException();

            return animes.OrderBy(sp => sp.Id);
        }

        public Anime GetByIdAnimeDetails(int? id)
        {
            if (id == null) throw new NullReferenceException();
                var anime = repository.GetByIdAnimeDetails(id);
                return anime;
           
        }

        public void PostAnime(AnimeInputModel animeDetails)
        { 
            if (animeDetails == null) throw new NullReferenceException();

                 var modelMap = mapper.Map<Anime>(animeDetails);
                 repository.PostAnime(modelMap);
   
            
        }

        public void PutAnime(int id, AnimeInputModel animeDetails)
        {
                var anime = GetByIdAnimeDetails(id);

                if (!anime.Equals(animeDetails)) return;

                var modelMap = mapper.Map<Anime>(animeDetails);

                repository.PutAnime(modelMap);   
        }
    }
}