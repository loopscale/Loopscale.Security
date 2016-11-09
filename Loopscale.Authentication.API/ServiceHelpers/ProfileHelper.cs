using Loopscale.DataAccess.EFModel;
using Loopscale.DataAccess.Repositories.Interfaces;
using Loopscale.Authentication.API.Infrastructure;
using Loopscale.Shared.ViewModels;
using System;
using System.Security.Claims;
using System.Security.Principal;
using Loopscale.Shared.Helpers;

namespace Loopscale.Authentication.API.ServiceHelpers
{
    public static class ProfileHelper
    {
        public static void AddCustomerProfile(ApplicationUserManager userManager, UserModel userModel, IUserRepository profileRepo)
        {
            var profile = new Profile
            {
                RelationshipId = 1, //Self
                ProfileTypeId = 3, //Parent
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                HomePhone = userModel.Phone,
                Mobile = userModel.Mobile,
                Gender = 2, //Not-Specified
                AspNetUserId = userModel.Id,
                FamilyId = CommonHelper.GenerateUniqueNumberInt()
            };

            profileRepo.AddProfileOnUserRegistration(profile);
        }

        public static void AddNewCustomerProfile(ProfileModel profileModel, IUserRepository profileRepo)
        {
            profileRepo.AddProfile(new Profile
            {
                RelationshipId = profileModel.RelationshipId,
                ProfileTypeId = profileModel.ProfileTypeId,
                FamilyId = CommonHelper.GenerateUniqueNumberInt(),
                AspNetUserId = profileModel.AspNetUserId,
                Email = profileModel.Email,
                HomePhone = profileModel.HomePhone,
                Mobile = profileModel.Mobile,
                Gender = ((profileModel.Gender == "Male") ? 0 : ((profileModel.Gender == "Female") ? 1 : 2)),
                FamilyName = profileModel.FamilyName,
                FirstName = profileModel.FirstName,
                LastName = profileModel.LastName,
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
                ImageId = profileModel.ImageId
            });            
        }


        public static Int64 GetClaimsProfileId(IIdentity identity)
        {
            //This is how you get Logged-in user profile id. You canuse this id to fetch anything for this profile
            return Convert.ToInt64(((ClaimsIdentity)identity).FindFirst("ProfileId").Value);
        }
    }
}