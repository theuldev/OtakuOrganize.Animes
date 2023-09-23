using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Application.Models.InputModels
{
    public class Anime_CustomerInputModel
    {
        public Anime_CustomerInputModel(Guid _AnimeId, Guid _CustomerId )    
        {
            AnimeId = _AnimeId;
            CustomerId = _CustomerId;
        }
    
        public Guid AnimeId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
