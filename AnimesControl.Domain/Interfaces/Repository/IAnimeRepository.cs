using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimesControl.Domain.Models;

namespace AnimesControl.Domain.Interfaces.Repository
{
    public interface IAnimeRepository
    {

        List<AnimeDetail> GetAnime();
        AnimeDetail GetByIdAnimeDetails(int id);
        void PostAnime(AnimeDetail animeDetails);
        void PutAnime(int id , AnimeDetail animeDetails);
        void DeleteAnime(AnimeDetail animeDetails);
        
    }
}