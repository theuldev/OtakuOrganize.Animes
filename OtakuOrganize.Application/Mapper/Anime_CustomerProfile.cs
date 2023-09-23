using OtakuOrganize.Application.Models.InputModels;
using OtakuOrganize.Application.Models.ViewModels;
using OtakuOrganize.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Application.Mapper
{
    public class Anime_CustomerProfile : Profile

    {
        public Anime_CustomerProfile()
        {
            CreateMap<Anime_CustomerInputModel, Anime_Customer>();
            CreateMap<Anime_Customer, Anime_CustomerViewModel>();

        }

    }
}
