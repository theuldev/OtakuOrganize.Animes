using AnimesControl.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace AnimesControl.Infra.Context
{
    public class AnimeContext : DbContext
    {
    


        protected override void OnConfiguring(DbContextOptionsBuilder dbContext)
        {
            if (!dbContext.IsConfigured)
            {
                try
                {
                    dbContext.UseNpgsql("host=host.docker.internal;port=5432;database=animedatabase;username=mathemac;password=mathemac;");
                    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                    AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

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