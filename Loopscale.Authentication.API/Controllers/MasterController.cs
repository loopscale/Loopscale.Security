using Loopscale.DataAccess.Repositories;
using Loopscale.DataAccess.Repositories.Interfaces;
using Loopscale.Shared.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loopscale.Authentication.API.Controllers
{
    [RoutePrefix("api/masters")]

    public class MasterController : BaseController
    {
        private IGenericRepository _Repo = new GenericRepository();

        [HttpPost]
        [Route("GetAllStates")]
        public IHttpActionResult GetAllStates()
        {
            try
            {
                return Ok(_Repo.GetAllStates());

            }
            catch (Exception ex)
            {
                LSLogManager.Instance.LogError(ex);
                throw;
            }
        }

        [HttpPost]
        [Route("GetAllProfileTypes")]
        public IHttpActionResult GetAllProfileTypes()
        {
            try
            {
                return Ok(_Repo.GetAllProfileTypes());

            }
            catch (Exception ex)
            {
                LSLogManager.Instance.LogError(ex);
                throw;
            }
        }
    }
}
