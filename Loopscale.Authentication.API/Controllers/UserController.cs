using Beginnings.Services.ServiceHelpers;
using Loopscale.Authentication.API.ModelFactory;
using Loopscale.DataAccess.EFModel;
using Loopscale.DataAccess.Repositories;
using Loopscale.DataAccess.Repositories.Interfaces;
using Loopscale.Shared.Exceptions;
using Loopscale.Shared.Helpers;
using Loopscale.Shared.Logging;
using Loopscale.Shared.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace Loopscale.Authentication.API.Controllers
{
    [RoutePrefix("api/Users")]
    public class UserController : BaseController
    {
        private readonly IUserRepository _profileRepo = new UserRepository();   

        public UserController()
        {

        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IHttpActionResult GetAllUsers()
        {
            return Ok(_profileRepo.GetAllUsers());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetRole")]
        public string GetRole()
        {
            var identity = (ClaimsIdentity)User.Identity;

            if (identity.IsAuthenticated)
            {
                return identity.FindFirst(ClaimTypes.Role).Value;
            }

            return null;
        }

        [HttpPost]
        [Authorize]
        [Route("CreateProfile")]
        public IHttpActionResult CreateProfile([FromBody] ProfileModel profileModel)
        {
            var imageName = "";

            if (profileModel.Photo != null)
            {
                imageName = ImageHelper.SaveImageFromStream(profileModel.Photo);
            }

            var p = new Profile
            {
                RelationshipId = profileModel.RelationshipId,
                ProfileTypeId = profileModel.ProfileTypeId,
                //FamilyId = CommonHelper.GenerateUniqueNumberInt(),
                //FamilyId = (base.Role == "admin" ? profileModel.FamilyId : base.FamilyId),
                DOB = profileModel.DOB,
                Email = profileModel.Email,
                HomePhone = profileModel.HomePhone,
                Mobile = profileModel.Mobile,
                Gender = ((profileModel.Gender == "Male") ? 0 : ((profileModel.Gender == "Female") ? 1 : 2)),
                FirstName = profileModel.FirstName,
                LastName = profileModel.LastName,
                FamilyName = profileModel.FamilyName,
                HomeAddressLine1 = profileModel.HomeAddressLine1,
                HomeAddressLine2 = profileModel.HomeAddressLine2,
                City = profileModel.City,
                StateId = profileModel.StateId,
                Zip = profileModel.Zip,
                EmployeeName = profileModel.EmployerName,
                Occupation = profileModel.Occupation,
                OfficeAddressLine1 = profileModel.OfficeAddressLine1,
                OfficeAddressLine2 = profileModel.OfficeAddressLine2,
                OfficeCity = profileModel.OfficeCity,
                OfficeStateId = profileModel.OfficeStateId,
                OfficeZip = profileModel.OfficeZip,
                OfficePhone = profileModel.OfficePhone,
                OfficeEmail = profileModel.OfficeEmail,
                //Photo = profileModel.Photo,
                ImageId = imageName
            };

            if (profileModel.ProfileId > 0)
            {
                p.ProfileId = profileModel.ProfileId;
                _profileRepo.UpdateProfile(profileModel.ProfileId, p);

            }
            else
            {
                _profileRepo.AddProfile(p);
            }

            return Ok(new
            {
                isSuccess = true
            });
        }

        [HttpPost]
        [Route("RegisterNewProfile")]
        public IHttpActionResult RegisterNewProfile([FromBody] ProfileModel profileModel)
        {
            UserModel userModel = null;

            if (profileModel.AddNewUser)
            {
                try
                {
                    userModel = new UserModel
                    {
                        UserName = profileModel.Email,
                        Password = ConfigurationManager.AppSettings["TempApplicationPassword"].ToString(),
                        FirstName = profileModel.FirstName,
                        LastName = profileModel.LastName,
                        Email = profileModel.Email,
                        Phone = profileModel.HomePhone,
                        Mobile = profileModel.Mobile
                    };

                    var newUserRegistered = UserHelper.AddAspNetUser(AppUserManager, userModel);

                    if (newUserRegistered)
                    {
                        var currentUser = AppUserManager.FindByName(userModel.UserName);
                        userModel.Id = currentUser.Id;
                        userModel.Role = "Visitor";

                        UserHelper.AddRoleToUser(AppUserManager, userModel);

                        _profileRepo.ActivateProfile(currentUser.Id);

                        var imageName = "";

                        if (profileModel.Photo != null)
                        {
                            imageName = ImageHelper.SaveImageFromStream(profileModel.Photo);
                            profileModel.ImageId = imageName;
                        }

                        profileModel.AspNetUserId = currentUser.Id;

                        ProfileHelper.AddNewCustomerProfile(profileModel, _profileRepo);

                        //calling SendMail Asynchronously. If there is an error, code walks past and shows success to user.
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                        //EmailHelper.SendWelcomeEmail(AppUserManager, userModel);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed                   
                    }
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(CreateUserException))
                    {
                        if (userModel != null)
                        {
                            UserHelper.RemoveRoleFromUser(this.AppUserManager, userModel);
                            UserHelper.DeleteAspNetUser(this.AppUserManager, userModel);
                        }
                    }

                    throw ex;
                }
            }

            return Ok(new
            {
                isSuccess = true
            });
        }

        [HttpGet]
        [Authorize]
        [Route("GetAllUserProfiles")]
        public IHttpActionResult GetAllUserProfiles()
        {
            IEnumerable<Profile> profiles = null;
            if (base.Role == "admin")
            {
                profiles = _profileRepo.GetProfiles();
            }
            
            //var profiles = _profileRepo.GetAllProfilesRelatedToChild(HttpContext.Current.User.Identity.GetUserId());

            if (profiles.Any())
            {
                var profileModels = profiles.Select(ProfileModelFactory.MapToProfileModel);

                return Ok(profileModels);
            }
            return NotFound();
        }

        [HttpGet]
        [Authorize]
        [Route("GetProfileDetail")]
        public IHttpActionResult GetProfileDetail(Int64 profileId)
        {
            var profile = _profileRepo.GetProfileByProfileId(profileId);

            if (profile != null)
            {
                var profileModel = ProfileModelFactory.MapToProfileModel(profile);

                return Ok(profileModel);
            }

            return Ok();
        }

        [HttpGet]
        [Route("UniqueUserName/{userName}")]
        public async Task<bool> UniqueUserName(string userName)
        {
            var result = await AppUserManager.FindByNameAsync(userName);
            return result == null;
        }

        [HttpGet]
        [Route("GetProfileImageContent")]
        public IHttpActionResult GetProfileImageContent(string imageId)
        {
            var result = ImageHelper.GetBas364Image(imageId);
            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("UniqueEMail/{email}")]
        public async Task<bool> UniqueEMail(string email)
        {
            LSLogManager.Instance.LogInfo("TestLog");
            var user = await AppUserManager.FindByEmailAsync(email);
            return user == null;
        }

        //To Be used in Repo Validators
        private async Task<bool> IsValidModel(UserModel user)
        {
            var result = true;
            var userinfo = await AppUserManager.FindByEmailAsync(user.Email);

            if (userinfo != null)
            {
                result = false;
            }

            userinfo = await AppUserManager.FindByNameAsync(user.UserName);

            if (userinfo != null)
            {
                result = false;
            }

            return result;
        }

        [HttpGet]
        [Authorize]
        [Route("GetVisitorProfiles")]
        public async Task<IHttpActionResult> GetVisitorProfiles()
        {
            var profiles = _profileRepo.GetProfiles();

            if (profiles.Any())
            {

                var visitorRole = await AppRoleManager.FindByNameAsync(Shared.Helpers.Constants.RoleName_Visitor);
                var registrantRole = await AppRoleManager.FindByNameAsync(Shared.Helpers.Constants.RoleName_Registrant);
                var profileModels = new List<ProfileModel>();
                if (visitorRole != null)
                {

                    var visitorProfiles = _profileRepo.GetProfilesByRoleId(visitorRole.Id);
                    profileModels = visitorProfiles.Select(ProfileModelFactory.MapToProfileModel).ToList();
                    foreach (var item in profileModels)
                    {
                        //item.UserRole = Beginnings.Shared.Constants.RoleName_Visitor;
                    }

                    var regstrantProfiles = _profileRepo.GetProfilesByRoleId(registrantRole.Id);
                    if (regstrantProfiles != null)
                    {
                        var registrantModals = regstrantProfiles.Select(ProfileModelFactory.MapToProfileModel).ToList();
                        foreach (var item in registrantModals)
                        {
                            //item.UserRole = Beginnings.Shared.Constants.RoleName_Registrant;
                        }

                        if (registrantModals.Any())
                        {
                            profileModels.AddRange(registrantModals);
                        }
                    }
                }

                return Ok(profileModels);
            }
            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("UpdateVisitorToRegistrant")]
        public async Task<IHttpActionResult> UpdateVisitorToRegistrant(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var userinfo = await AppUserManager.FindByIdAsync(id);
                if (userinfo != null)
                {
                    await AppUserManager.RemoveFromRoleAsync(id, Shared.Helpers.Constants.RoleName_Visitor);
                    await AppUserManager.AddToRoleAsync(id, Shared.Helpers.Constants.RoleName_Registrant);
                }
            }
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("GetAllRegisteredProfiles")]
        public IHttpActionResult GetAllRegisteredProfiles([FromBody]string email)
        {

            var profiles = _profileRepo.GetAllRegisteredProfiles(email);

            if (profiles.Any())
            {
                var profileModels = profiles.Select(ProfileModelFactory.MapToProfileModel);

                return Ok(profileModels);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("SearchProfiles")]
        public IHttpActionResult SearchProfiles([FromBody] ProfileSearchFilter filter)
        {

            var profiles = _profileRepo.SearchProfiles(filter.Email, filter.FirstName, filter.LastName, filter.FamilyName);

            if (profiles.Any())
            {
                var profileModels = profiles.Select(ProfileModelFactory.MapToProfileModel);

                return Ok(profileModels);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("ActivateProfile")]
        public IHttpActionResult ActivateProfile([FromBody]long profileId)
        {
            try
            {
                var profile = _profileRepo.GetProfileByProfileId(profileId);
                if (profile != null && !string.IsNullOrEmpty(profile.AspNetUserId))
                {
                    if (_profileRepo.ActivateProfile(profile.AspNetUserId))
                    {
                        return Ok();
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                //LogManager.Instance.LogError(ex);
                throw;
            }
        }
    }

    public class ProfileSearchFilter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }
    }
}

