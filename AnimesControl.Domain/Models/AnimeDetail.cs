using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimesControl.Domain.Models
{
    public class AnimeDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string Category { get; set; }
        public DateTime PostAt {get;set;}
        public DateTime ReleaseDate {get;set;}
        
    }
}