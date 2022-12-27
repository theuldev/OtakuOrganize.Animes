using AnimesControl.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimesControl.Core.Interfaces.Repostories
{
    public interface IAnime_CustomerRepository
    {
        public void AddAnime_Customer(Anime_Customer anime_Customer);
        public List<Anime_Customer> GetCustomerWithAnimeId(int animeId);
        public List<Anime_Customer> GetAnimeWithCustomerId(int customerId);
        public void RemoveAnimeCustomer(Anime_Customer anime_Customer);
    }
}
