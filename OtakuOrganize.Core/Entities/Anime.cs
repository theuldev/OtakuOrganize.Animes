using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OtakuOrganize.Core.Entities
{
    public class Anime
    { 

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Details { get; private set; }
        public int Category { get; private set; }
        public DateTime PostAt { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public List<Anime_Customer> Anime_Customer { get; private set; }



    }
}