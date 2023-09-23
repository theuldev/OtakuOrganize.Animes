using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Infra.Caching
{
    public class CachingService : ICachingService
    {
        private readonly IDistributedCache cache;
        private readonly DistributedCacheEntryOptions options;
        public CachingService(IDistributedCache cache)
        {
            this.cache = cache;
            options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(2600),
                SlidingExpiration = TimeSpan.FromSeconds(1000)
            };
        }

        public async Task<string> GetAsync(string key)
        {
            return await cache.GetStringAsync(key);
        }

        public async Task SetAsync(string key, string value)
        {
            await cache.SetStringAsync(key, value,options);
        }
    }
}
