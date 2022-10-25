
using AnimesControl.Domain.Interfaces.Repository;
using AnimesControl.Domain.Models;

namespace AnimesControl.Infra.Repository

{
    public class AnimeRepository : IAnimeRepository
    {
        public void DeleteAnime(AnimeDetail animeDetails)
        {
            throw new NotImplementedException();
        }

        public List<AnimeDetail> GetAnime()
        {
            throw new NotImplementedException();
        }

        public AnimeDetail GetByIdAnimeDetails(int id)
        {
            throw new NotImplementedException();
        }

        public void PostAnime(AnimeDetail animeDetails)
        {
            throw new NotImplementedException();
        }

        public void PutAnime(int id, AnimeDetail animeDetails)
        {
            throw new NotImplementedException();
        }
    }
}