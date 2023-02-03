using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimesControl.Core.Exceptions
{
    public class EmailAlreadyRegisteredException : Exception
    {
        public EmailAlreadyRegisteredException() : base("Email Already Registered")
        {

        }
    }
}
