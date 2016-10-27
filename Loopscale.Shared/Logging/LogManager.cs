using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Loopscale.Shared.Logging
{
    public class LSLogManager
    {
        private ILogger _log = LogManager.GetCurrentClassLogger();

        private static LSLogManager _Instance;
        public static LSLogManager Instance
        {
            get
            {
                if(_Instance == null)
                {
                    _Instance = new LSLogManager();
                }
                return _Instance = new LSLogManager();
            }
        }

        private LSLogManager()
        {
            
        }

        public void LogInfo(string msg)
        {
            _log.Info(msg);
        }

        public void LogError(string msg)
        {
            _log.Error(msg);
        }

        public void LogError(string msg, Exception ex)
        {
           
            _log.Error(ex.Message, ex.StackTrace );
            if(ex.InnerException != null)
            {
                _log.Error("Inner Exception" + ex.InnerException);
                _log.Error("Call Stack : " + ex.StackTrace); 
            }
        }

        public void LogError(Exception ex)
        {
            _log.Error(ex, ex.StackTrace);
            if (ex.InnerException != null)
            {
                _log.Error("Inner Exception" + ex.InnerException);
                _log.Error("Call Stack : " + ex.InnerException.StackTrace);
            }
        }
    }
}
