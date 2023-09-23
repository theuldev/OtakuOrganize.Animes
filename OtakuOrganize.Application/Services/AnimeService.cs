using AutoMapper;
using OtakuOrganize.Application.Common.Interfaces.Services;
using OtakuOrganize.Application.Models.InputModels;
using OtakuOrganize.Application.Models.ViewModels;
using OtakuOrganize.Core.Entities;
using OtakuOrganize.Core.Exceptions;
using OtakuOrganize.Core.Interfaces.Repositories;
using OtakuOrganize.Infra.Caching;

namespace OtakuOrganize.Application.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly IAnimeRepository repository;
        private readonly IMapper mapper;
        private readonly ICachingService cachingService;

        public AnimeService(IAnimeRepository _repository, IMapper _mapper, ICachingService _cachingService)
        {
            mapper = _mapper;
            repository = _repository;
            cachingService = _cachingService;
        }

        public async Task DeleteAnime(Guid id)
        {
            await repository.DeleteAnime(id);
        }

        public async Task<IEnumerable<AnimeViewModel>> GetAnimes()
        {
            var animes = await repository.GetAnimes();
            if (animes.Count <= 0) throw new ArgumentNullException();

            var animeMap = mapper.Map<IEnumerable<AnimeViewModel>>(animes);

            return animeMap.OrderBy(sp => sp.Id);
        }

        public async Task<AnimeViewModel> GetByIdAnimeDetails(Guid id)
        {
            var anime = await repository.GetByIdAnimeDetails(id);
            var animeMap = mapper.Map<AnimeViewModel>(anime);
            return animeMap;

        }

        public async Task PostAnime(AnimeInputModel animeDetails)
        {
            if (animeDetails == null) throw new ArgumentNullException();

            var animeMap = mapper.Map<Anime>(animeDetails);
            await repository.PostAnime(animeMap);
        }

        public async Task PutAnime(Guid id, AnimeInputModel animeDetails)
        {

            if (!animeDetails.Id.Equals(id)) throw new CredentialsNotEqualsException();

            var exist = repository.AnimeExists(id);

            if (!exist) throw new NullReferenceException();
            var animeMap = mapper.Map<Anime>(animeDetails);
            await repository.PutAnime(animeMap);
        }
    }
}