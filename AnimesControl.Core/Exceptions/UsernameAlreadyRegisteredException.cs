using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimesControl.Core.Exceptions
{
    public class UsernameAlreadyRegisteredException : Exception
    {
        public UsernameAlreadyRegisteredException() : base("Username already registered")
        {

        }
    }
}
