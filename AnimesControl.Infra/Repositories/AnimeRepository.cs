using AnimesControl.Core.Interfaces.Repostories;
using AnimesControl.Core.Entities;
using AnimesControl.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata.Ecma335;

namespace AnimesControl.Infra.Repositories

{
    public class AnimeRepository : IAnimeRepository
    {
        private readonly AnimeContext context;

        public AnimeRepository(AnimeContext context)
        {
            this.context = context;
        }

        public bool AnimeExists(Guid id)
        {

            var exist = context.Anime.AsNoTracking().Any(a => a.Id.Equals(id));
            return exist;
        }

        public void DeleteAnime(Anime animeDetails)
        {

            var anime = context.Anime.Where(a => a.Id.Equals(animeDetails.Id)).FirstOrDefault();
            if (anime == null) throw new NullReferenceException();
            context.Entry(anime).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public async Task<List<Anime>> GetAnimes()
        {

            return await context.Anime.ToListAsync();
        }

        public async Task<Anime> GetByIdAnimeDetails(Guid id)
        {
            var anime = await context.Anime.Where(a => a.Id.Equals(id)).Include(a => a.Anime_Customer).FirstOrDefaultAsync();
            return anime != null ? anime : throw new NullReferenceException();    
        }

        public void PostAnime(Anime animeDetails)
        {
            context.Anime.Add(animeDetails);
            context.SaveChanges();
        }


        public void PutAnime(Anime animeDetails)
        {
            var anime = context.Anime.Where(a => a.Id.Equals(animeDetails.Id));
            context.Entry(anime).CurrentValues.SetValues(animeDetails);
            context.SaveChanges();
        }
    }
}