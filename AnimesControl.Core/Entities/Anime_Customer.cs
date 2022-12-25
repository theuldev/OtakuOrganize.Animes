using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimesControl.Core.Entities
{
    public class Anime_Customer
    {
        public Anime_Customer()
        {
        }

        public Anime_Customer(Anime anime,Customer customer)
        {
            Anime = anime;
            AnimeId = anime.Id;
            CustomerId = customer.Id;
            Customer = customer;

        }
        public int Id { get; set; }
        public int AnimeId { get; set; }
        public Anime Anime { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }  

    }
}
