using OtakuOrganize.Application.Models.InputModels;
using OtakuOrganize.Application.Models.ViewModels;
using OtakuOrganize.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Application.Common.Interfaces.Services
{
    public interface IAnime_CustomerService
    {
        public void AddAnimeCustomer(Anime_CustomerInputModel model);
        public Task<List<Anime_CustomerViewModel>> GetCustomerWithAnimeId(Guid? id);
        public Task<List<Anime_CustomerViewModel>> GetAnimeWithCustomerId(Guid? id);
        public void RemoveAnimeCustomer(Anime_CustomerInputModel anime_Customer);
    }
}
