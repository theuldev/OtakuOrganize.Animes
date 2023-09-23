using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Core.Entities
{
    public class Anime_Customer
    {
        public int Id { get; private set; }
        public Guid AnimeId { get; private set; }
        public Anime Anime { get; private set; }
        public Guid CustomerId { get; private set; }
        public Customer Customer { get; private set; }

    }
}
