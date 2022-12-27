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

        public void DeleteAnime(Anime animeDetails)
        {
            
            context.Remove(animeDetails);
            context.SaveChanges();
        }

        public List<Anime> GetAnimes()
        {

            return context.Anime.ToList();
        }

        public Anime GetByIdAnimeDetails(int? id)
        {
            var anime = context.Anime.AsNoTracking().FirstOrDefault(e => e.Id == id);
            if (anime == null) throw new NullReferenceException();
            return anime;
        }

        public void PostAnime(Anime animeDetails)
        {
            context.Anime.Add(animeDetails);
            context.SaveChanges();
            
        }

        public void PutAnime(Anime animeDetails)
        {
        
            context.Entry(animeDetails).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}