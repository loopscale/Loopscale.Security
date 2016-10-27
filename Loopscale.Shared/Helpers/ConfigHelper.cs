using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loopscale.Shared.Helpers
{
    public class ConfigHelper
    {
        private static ConfigHelper instance;

        public static ConfigHelper _Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConfigHelper();
                }

                return instance;
            }
        }

        private ConfigHelper()
        {

        }

        public string AdminEmail
        {
            get
            {
                return ConfigurationManager.AppSettings["AdminEmail"] ?? "admin@Loopscale.com";
            }
        }

        public string SharedResourcePath
        {
            get
            {
                return ConfigurationManager.AppSettings["SharedResourcePath"].ToString();
            }
        }

        public string PdfFormPath
        {
            get
            {
                return ConfigurationManager.AppSettings["PdfFormPath"].ToString();
            }
        }

        public string ServerBaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["uiServerBaseUrl"].ToString();
            }
        }

        public string SchedulerIntervalInMins
        {
            get
            {
                return ConfigurationManager.AppSettings["SchedulerIntervalInMins"].ToString();
            }
        }
    }
}
