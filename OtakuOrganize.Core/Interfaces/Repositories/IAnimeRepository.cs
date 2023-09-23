using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OtakuOrganize.Core.Entities;

namespace OtakuOrganize.Core.Interfaces.Repositories
{
    public interface IAnimeRepository
    {
        Task<List<Anime>> GetAnimes();
        Task<Anime> GetByIdAnimeDetails(Guid id);
        Task PostAnime(Anime animeDetails);
        Task PutAnime(Anime animeDetails);
        Task DeleteAnime(Guid id);
        bool AnimeExists(Guid id);

    }
}