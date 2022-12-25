using AnimesControl.Application.Models.InputModels;
using AnimesControl.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimesControl.Application.Common.Interfaces.Services
{
    public interface IAnime_CustomerService
    {
        public void AddAnimeCustomer(Anime_CustomerInputModel model);
        public List<Anime_Customer> GetCustomerWithAnimeId(int id);
        public List<Anime_Customer> GetAnimeWithCustomerId(int id);
        public void RemoveAnimeCustomer(Anime_CustomerInputModel anime_Customer);
    }
}
