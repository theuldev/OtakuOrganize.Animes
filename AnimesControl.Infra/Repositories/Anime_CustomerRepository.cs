
using AnimesControl.Core.Entities;
using AnimesControl.Core.Interfaces.Repostories;
using AnimesControl.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AnimesControl.Infra.Repositories
{
    public class Anime_CustomerRepository : IAnime_CustomerRepository
    {
        private readonly AnimeContext context;
        public Anime_CustomerRepository(AnimeContext _context)
        {
            context = _context;
        }
        public void AddAnime_Customer(Anime_Customer anime_Customer)
        {
            var model = context.Anime_Customer.AsNoTracking().Where(c => c.AnimeId.Equals(anime_Customer.AnimeId) && c.CustomerId.Equals(anime_Customer.CustomerId)).FirstOrDefault();
            if (model == null) throw new ArgumentException();

            context.Anime_Customer.Add(anime_Customer);
            context.SaveChanges();

        }
        public async Task<List<Anime_Customer>> GetCustomersWithAnimeId(Guid? id)
        {
            return context.Anime_Customer.Where(a => a.AnimeId.Equals(id)).ToList();
        }
        public async Task<List<Anime_Customer>> GetAnimesWithCustomerId(Guid? id)
        {


            return context.Anime_Customer.AsNoTracking().Where(a => a.CustomerId.Equals(id)).ToList();
        }
        public void RemoveAnimeCustomer(Anime_Customer model)
        {
            var anime_customer = context.Anime_Customer.Where(a => a.AnimeId.Equals(model.AnimeId) && a.CustomerId.Equals(model.CustomerId)).FirstOrDefault();
            if (anime_customer == null) throw new NullReferenceException();
            context.Entry(anime_customer).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public async Task<bool> ExistAnimeAndCustomer(Guid animeId, Guid customerId)
        {
            var anime = await context.Anime.AnyAsync(a => a.Id.Equals(animeId));
            var customer = await context.Customers.AnyAsync(a => a.Id.Equals(customerId));

            return customer && anime ? true : false;

        }
    }
}
