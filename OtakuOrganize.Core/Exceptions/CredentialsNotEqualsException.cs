using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OtakuOrganize.Core.Exceptions
{
    public class CredentialsNotEqualsException : Exception
    {
        public CredentialsNotEqualsException() : base("Credentials are not the same")
        {
            
        }
    }
}