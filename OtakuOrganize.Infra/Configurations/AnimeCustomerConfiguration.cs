using OtakuOrganize.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Infra.Configurations
{
    public class AnimeCustomerConfiguration : IEntityTypeConfiguration<Anime_Customer>
    {
        public void Configure(EntityTypeBuilder<Anime_Customer> builder)
        {

            builder.ToTable("tb_anime_customer");
            builder.HasOne(e => e.Anime)
               .WithMany(e => e.Anime_Customer)
               .HasForeignKey(e => e.AnimeId);
            builder.HasOne(e => e.Customer)
                .WithMany(e => e.Animes_Customer)
                .HasForeignKey(e => e.CustomerId);

        }
    }
}
