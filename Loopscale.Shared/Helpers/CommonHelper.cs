using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loopscale.Shared.Helpers
{
    public static class CommonHelper
    {
        public static string GenerateUniqueNumberString()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public static long GenerateUniqueNumberInt()
        {
            return Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }
}
