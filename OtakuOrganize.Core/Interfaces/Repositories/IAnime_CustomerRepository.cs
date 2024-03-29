﻿using OtakuOrganize.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Core.Interfaces.Repositories
{
    public interface IAnime_CustomerRepository
    {
        void AddAnime_Customer(Anime_Customer anime_Customer);
        Task<List<Anime_Customer>> GetCustomersWithAnimeId(Guid? animeId);
        Task<List<Anime_Customer>> GetAnimesWithCustomerId(Guid? customerId);
        void RemoveAnimeCustomer(Anime_Customer anime_Customer);

        Task<bool> ExistAnimeAndCustomer(Guid animeId, Guid customerId);
    }
}
