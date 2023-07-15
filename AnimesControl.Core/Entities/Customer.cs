using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace AnimesControl.Core.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public List<Anime_Customer> Animes_Customer { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set;}
    }
}