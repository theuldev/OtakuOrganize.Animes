using AnimesControl.Application.Models.InputModels;
using AnimesControl.Application.Models.ViewModels;
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
        public Task<List<Anime_CustomerViewModel>> GetCustomerWithAnimeId(int id);
        public Task<List<Anime_CustomerViewModel>> GetAnimeWithCustomerId(int id);
        public void RemoveAnimeCustomer(Anime_CustomerInputModel anime_Customer);
    }
}
