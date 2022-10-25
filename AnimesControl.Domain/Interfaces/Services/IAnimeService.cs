using AnimesControl.Domain.Models;

namespace AnimesControl.Domain.Interfaces.Services
{
    public interface IAnimeService
    {
        List<AnimeDetail> GetAnime();
        AnimeDetail GetByIdAnimeDetails(int id);
        void PostAnime(AnimeDetail animeDetails);
        void PutAnime(int id , AnimeDetail animeDetails);
        void DeleteAnime(AnimeDetail animeDetails);
    }
} 