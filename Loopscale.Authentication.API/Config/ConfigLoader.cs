//using Autofac;
//using Loopscale.DataAccess.Repositories.Interfaces;
//using Loopscale.Authentication.API.Config;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Web;

//namespace Loopscale.Authentication.API.Config
//{
//    public class ConfigLoader : IStartable
//    {
//        private IConfigRepository _configRepository;

//        public ConfigLoader(IConfigRepository configRepository)
//        {
//            _configRepository = configRepository;

//        }
//        public void Start()
//        {
//            ConfigProvider.Config = _configRepository.LoadAllConfig();
//        }
//    }
//}