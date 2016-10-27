using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loopscale.Shared.Exceptions
{
    public class QueryStringPollutedException : Exception
    {
        public QueryStringPollutedException()
        {
        }

        public QueryStringPollutedException(string message)
            : base(message)
        {
        }
    }
}
