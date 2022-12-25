
using AnimesControl.Core.Entities;
using AnimesControl.Core.Interfaces.Repostories;
using AnimesControl.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimesControl.Infra.Repository
{
    public class Anime_CustomerRepository : IAnime_CustomerRepository
    {
        private readonly AnimeContext context;
        public Anime_CustomerRepository(AnimeContext _context)
        {
            context = _context;
        }
        public void AddAnime_Customer(Anime anime, Customer customer)
        {
            var model = context.Anime_Customer.AsNoTracking().Where(c => c.AnimeId == anime.Id && c.CustomerId == customer.Id).FirstOrDefault();
            if (model != null)
            {
                throw new ArgumentException();
            }
            var anime_customer = new Anime_Customer()
            {
                AnimeId = anime.Id,
                CustomerId = customer.Id
            };

           
           
            context.Anime_Customer.Add(anime_customer);
            context.SaveChanges();

        }
        public List<Anime_Customer> GetCustomerWithAnimeId(int id)
        {
           var animes = context.Anime_Customer.AsNoTracking().Where(a => a.AnimeId == id).ToList();
            
            return animes;
        }
        public List<Anime_Customer> GetAnimeWithCustomerId(int id)
        {
            var customers = context.Anime_Customer.AsNoTracking().Where(a => a.CustomerId == id).ToList();

            return customers;
        }
        public void RemoveAnimeCustomer(Anime_Customer model)
        {
            var anime_customer = context.Anime_Customer.AsNoTracking().Where(a => a.AnimeId == model.AnimeId && a.CustomerId == model.CustomerId).FirstOrDefault();
            if (anime_customer == null) throw new NullReferenceException();
            context.Anime_Customer.Remove(anime_customer);
            context.SaveChanges();
        }

    }
}
