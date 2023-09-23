using OtakuOrganize.Core.Entities;
using OtakuOrganize.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using OtakuOrganize.Core.Interfaces.Repositories;

namespace OtakuOrganize.Infra.Repositories

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

        public async Task DeleteAnime(Guid id)
        {
            var anime = GetByIdAnimeDetails(id);
            context.Remove(anime.Result);
            await context.SaveChangesAsync();
        }

        public async Task<List<Anime>> GetAnimes()
        {

            return await context.Anime.ToListAsync();
        }

        public async Task<Anime> GetByIdAnimeDetails(Guid id)
        {
            var anime = await context.Anime.AsNoTracking().SingleOrDefaultAsync(a => a.Id.Equals(id));
            return anime != null ? anime : throw new NullReferenceException();
        }

        public async Task PostAnime(Anime anime)
        {
            await context.Anime.AddAsync(anime);
            await context.SaveChangesAsync();
        }


        public async Task PutAnime(Anime model)
        {
            context.Update(model);
            await context.SaveChangesAsync();
        }
    }
}