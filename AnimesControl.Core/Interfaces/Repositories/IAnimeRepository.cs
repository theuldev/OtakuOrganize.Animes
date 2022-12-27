using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimesControl.Core.Entities;

namespace AnimesControl.Core.Interfaces.Repostories
{
    public interface IAnimeRepository
    {
        List<Anime> GetAnimes();
        Anime GetByIdAnimeDetails(int? id);
        void PostAnime(Anime animeDetails);
        void PutAnime(Anime animeDetails);
        void DeleteAnime(Anime animeDetails);

    }
}