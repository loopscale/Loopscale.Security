using Loopscale.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loopscale.Shared.Encryption
{
    public class Rot47Encryption
    {
        private const string PrivateKey = "lollipop";

        private static char Rot47(char chr)
        {
            if (chr == ' ') return ' ';
            int ascii = chr;
            ascii += 47;
            if (ascii > 126) ascii -= 94;
            if (ascii < 33) ascii += 94;
            return (char)ascii;
        }

        public static string EncodeRot47(string str)
        {
            str = str + PrivateKey;
            return Rot47(str);
        }

        private static string Rot47(string str)
        {
            string RetStr = "";
            foreach (char c in str.ToCharArray())
                RetStr += Rot47(c).ToString();

            return RetStr;
        }

        public static string DecodeRot47(string str)
        {
            string RetStr = Rot47(str);

            if (!RetStr.Contains(PrivateKey))
            {
                //log the exception
                throw new QueryStringPollutedException("QueryString hacked!!");
            }
            
            var actualString = RetStr.Substring(0, (RetStr.Length - PrivateKey.Length));

            return actualString;
        }
    }
}
