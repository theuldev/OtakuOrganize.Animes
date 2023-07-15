using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimesControl.Application.Models.InputModels
{
    public class Anime_CustomerInputModel
    {
    
        public Guid AnimeId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
