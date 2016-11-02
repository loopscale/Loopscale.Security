using Loopscale.Authentication.API.ModelFactory;
using Loopscale.DataAccess.EFModel;
using Loopscale.DataAccess.Repositories;
using Loopscale.DataAccess.Repositories.Interfaces;
using Loopscale.Shared.MasterEnums;
using Loopscale.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loopscale.Authentication.API.Controllers
{
    [RoutePrefix("api/admin/employee")]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeRepository _employeeRepo = new EmployeeRepository();

        // GET: api/Appointment
        [HttpGet]
        [Route("GetAllEmployees")]
        public IHttpActionResult GetAllEmployees()
        {
            return Ok(ProfileModelFactory.MapToProfileModelList(_employeeRepo.GetEmployeeProfiles()));
        }

        // GET: api/Appointment
        [HttpPost]
        [Route("AddProfile")]
        [Authorize]
        public IHttpActionResult AddProfile([FromBody] ProfileModel profileModel)
        {
            var newEmployeeProfile = new Profile
            {
                FirstName = profileModel.FirstName,
                LastName = profileModel.LastName,
                Email = profileModel.Email,
                City = profileModel.City,
                Mobile = "000-000-0000",
                ProfileTypeId = (int)Enums.ProfileTypesEnum.Employee
            };
            _employeeRepo.AddEmployeeProfile(newEmployeeProfile);

            return Ok(newEmployeeProfile);
        }
    }

}
