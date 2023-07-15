using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimesControl.Core.Entities;

namespace AnimesControl.Core.Interfaces.Repostories
{
    public interface IAnimeRepository
    {
        Task<List<Anime>> GetAnimes();
        Task<Anime> GetByIdAnimeDetails(Guid id);
        void PostAnime(Anime animeDetails);
        void PutAnime(Anime animeDetails);
        void DeleteAnime(Anime animeDetails);
        bool AnimeExists(Guid id);

    }
}