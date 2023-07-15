using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimesControl.Core.Entities
{
    public class Anime_Customer
    {
    
        public int Id { get; set; }
        public Guid AnimeId { get; set; }
        public Anime Anime { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}
