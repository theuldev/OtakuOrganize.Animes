using AnimesControl.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimesControl.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public void Post(User user);
        public void Delete(User user);
    }
}
