using OtakuOrganize.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    
namespace OtakuOrganize.Tests.Persisitence.ContextTest
{
    public static class DbContextTest
    {
        public static DbContextOptions<AnimeContext> CreateDbContextOptions()
        {
            var provider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
            var dbname = Guid.NewGuid().ToString();
            var settings = new DbContextOptionsBuilder<AnimeContext>().UseInMemoryDatabase(dbname).UseInternalServiceProvider(provider).Options;
            return settings;
        }
    }
}
