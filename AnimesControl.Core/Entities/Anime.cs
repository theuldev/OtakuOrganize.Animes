using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnimesControl.Core.Entities
{
    public class Anime
    {
        public Anime(int id, string title, string details, int category, DateTime postAt)
        {
            Id = id;
            Title = title;
            Details = details;
            Category = category;
            PostAt = postAt;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public int Category { get; set; }
        public DateTime PostAt { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<Anime_Customer> Anime_Customer { get; set; }

      

    }
}