using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AnimesControl.Domain.Models
{
    public class Customer
    {
        public int Id {get;set;}
        public string Username { get; set; } 
        public string Password {get;set;} 
        public string Name { get; set; }
        public string Email {get;set;}
        public DateTime Birthdatt {get;set;}
        public List<AnimeDetail> ListAnimes {get;set;} 
        public DateTime LastLogin {get;set;} 
    }
}