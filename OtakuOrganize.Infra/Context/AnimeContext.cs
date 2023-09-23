using OtakuOrganize.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace OtakuOrganize.Infra.Context
{
    public class AnimeContext : DbContext
    {
        private const string connectionString = "Server=DESKTOP-Q31UENU;Database=AnimeDatabase;Trusted_Connection=True;MultipleActiveResultSets=true;encrypt=false"; 

        protected override void OnConfiguring(DbContextOptionsBuilder dbContext)
        {
            if (!dbContext.IsConfigured)
            {
                try
                {
                    dbContext.UseSqlServer(connectionString);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }


        } 
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        }
        public DbSet<User> Users { get; set; }
        public DbSet<Anime> Anime { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Anime_Customer> Anime_Customer { get; set; }

    }
}