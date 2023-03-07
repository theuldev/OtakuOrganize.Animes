using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimesControl.Application.Models.InputModels
{
    public class Anime_CustomerInputModel
    {
        public Anime_CustomerInputModel(int animeId, int customerId)
        {
            this.AnimeId = animeId;
            this.CustomerId = customerId;
        }
        public int AnimeId { get; set; }
        public int CustomerId { get; set; }
    }
}
