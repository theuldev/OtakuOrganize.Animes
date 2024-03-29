﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Infra.Caching
{
    public interface ICachingService
    {
        Task SetAsync(string key,string value); 
        Task<string> GetAsync(string key);
    }
}
