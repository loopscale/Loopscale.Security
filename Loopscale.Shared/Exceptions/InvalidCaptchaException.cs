using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loopscale.Shared.Exceptions
{
    public class InvalidCaptchaException: Exception
    {
        public InvalidCaptchaException()
        {
        }

        public InvalidCaptchaException(string message)
            : base(message)
        {
        }
    }
}
