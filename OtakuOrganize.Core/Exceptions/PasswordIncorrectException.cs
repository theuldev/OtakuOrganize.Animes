using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Core.Exceptions
{
    public class PasswordIncorrectException : Exception
    {
        public PasswordIncorrectException() : base("Password is incorrect")
        {
            
        }
    }
}
