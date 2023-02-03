using AnimesControl.Core.Entities;
using AnimesControl.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace AnimesControl.Infra.Context
{
    public class AnimeContext : DbContext
    {
        public AnimeContext(DbContextOptions<AnimeContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContext)
        {
            if (!dbContext.IsConfigured)
            {
                try
                {
                    dbContext.UseNpgsql("host=host.docker.internal;port=5432;database=animedatabase;username=mathemac;password=mathemac;");

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }


        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Anime>(e =>
            {
                e.ToTable("tb_animes");
                e.HasKey(e => e.Id);

            }

            );
            builder.Entity<Customer>(e =>
            {
                e.ToTable("tb_customers");
                e.HasKey(p => p.Id);


            });

            builder.Entity<Anime_Customer>(
                e => e.HasOne(e => e.Anime)
                .WithMany(e => e.Anime_Customer)
                .HasForeignKey(e => e.AnimeId)

                );
            builder.Entity<Anime_Customer>(
            e => e.HasOne(e => e.Customer)
            .WithMany(e => e.Animes_Customer)
            .HasForeignKey(e => e.CustomerId)

            );

        }
        public DbSet<Anime> Anime { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Anime_Customer> Anime_Customer { get; set; }

    }
}