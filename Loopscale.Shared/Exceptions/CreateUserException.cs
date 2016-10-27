using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loopscale.Shared.Exceptions
{
    public class CreateUserException : Exception
    {
        public CreateUserException()
        {
        }

        public CreateUserException(string message)
            : base(message)
        {
        }
    }
}
