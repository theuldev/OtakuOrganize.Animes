using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimesControl.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimesControl.Infra.Context
{
    public class AnimeContext : DbContext 
    {
        
         public AnimeContext(DbContextOptions<AnimeContext> options ) : base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
        public DbSet<AnimeDetail> animeDetails  {get;set;}
        public DbSet<Customer> customers {get;set;}
        
    }
}