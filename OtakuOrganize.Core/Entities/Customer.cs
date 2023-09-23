using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace OtakuOrganize.Core.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Phone { get; private set; }
        public int Gender { get; private set; }
        public DateTime Birthdate { get; private set; }
        public List<Anime_Customer> Animes_Customer { get; private set; }
        public User User { get; private set; }
        public Guid UserId { get; private set;}
    }
}