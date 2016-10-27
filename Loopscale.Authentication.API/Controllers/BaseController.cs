
using Loopscale.Authentication.API.Infrastructure;
using Loopscale.DataAccess.EFModel;
using Loopscale.DataAccess.Repositories.Interfaces;
using Loopscale.Shared.Helpers;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace Loopscale.Authentication.API.Controllers
{
    public class BaseController : ApiController
    {
        private ApplicationUserManager _AppUserManager = null;
        private ApplicationRoleManager _AppRoleManager = null;
        //private Dictionary<string, string> _config = null;

        protected ApplicationUserManager AppUserManager
        {
            get
            {
                return _AppUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        protected ApplicationRoleManager AppRoleManager
        {
            get
            {
                return _AppRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }
        //protected Dictionary<string, string> Config
        //{

        //    get
        //    {
        //        return _config ?? ConfigProvider.Config;
        //    }
        //}

        public string Role
        {
            get
            {
                var identity = (ClaimsIdentity)User.Identity;

                if (identity.IsAuthenticated)
                {
                    return identity.FindFirst(ClaimTypes.Role).Value.ToLower();
                }
                return "visitor";
            }
        }

        public bool IsAdminRole
        {
            get
            {
                var identity = (ClaimsIdentity)User.Identity;

                if (identity.IsAuthenticated)
                {
                    return (identity.FindFirst(ClaimTypes.Role).Value.ToLower() == "admin");
                }
                return false;
            }
        }

        protected Profile GetAdminProfile(IUserRepository repo)
        {
            var t = Task.Run(async () =>
            {
                return await AppRoleManager.FindByNameAsync(Constants.RoleName_Admin);
            });

            var adminRole = t.Result;
            return repo.GetProfilesByRoleId(adminRole.Id).FirstOrDefault();
        }
    }
}
