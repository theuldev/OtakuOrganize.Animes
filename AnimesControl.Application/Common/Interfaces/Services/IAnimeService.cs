using AnimesControl.Application.Models.InputModels;
using AnimesControl.Core.Entities;

namespace AnimesControl.Application.Common.Interfaces.Services
{
    public interface IAnimeService
    {
        IEnumerable<Anime> GetAnimes();
        Anime GetByIdAnimeDetails(int? id );
        void PostAnime(AnimeInputModel animeDetails);
        void PutAnime(int id, AnimeInputModel animeDetails);
        void DeleteAnime(int id);
    }
}