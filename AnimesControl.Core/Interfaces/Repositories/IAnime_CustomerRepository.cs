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
        void AddAnime_Customer(Anime_Customer anime_Customer);
        Task<List<Anime_Customer>> GetCustomersWithAnimeId(int? animeId);
        Task<List<Anime_Customer>> GetAnimesWithCustomerId(int? customerId);
        void RemoveAnimeCustomer(Anime_Customer anime_Customer);
    }
}
