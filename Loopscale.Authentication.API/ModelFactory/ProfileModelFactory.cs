using Loopscale.DataAccess.EFModel;
using Loopscale.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loopscale.Authentication.API.ModelFactory
{
    public class ProfileModelFactory
    {
        public static ProfileModel MapToProfileModel(Profile profile)
        {
            return new ProfileModel
            {
                ProfileId = profile.ProfileId,
                AspNetUserId = profile.AspNetUserId ?? "",
                DOB = profile.DOB,
                HomeAddressLine1 = profile.HomeAddressLine1,
                HomeAddressLine2 = profile.HomeAddressLine2,
                City = profile.City,
                Email = profile.Email,
                FirstName = profile.FirstName,
                Gender = (profile.Gender == 0 ? "Male" : ((profile.Gender == 1) ? "Female" : "Not Specified")),
                LastName = profile.LastName,
                FamilyName = profile.FamilyName,
                HomePhone = profile.HomePhone,
                Mobile = profile.Mobile,
                ImageId = profile.ImageId,
                RelationshipId = profile.RelationshipId,
                ProfileTypeId = profile.ProfileTypeId,
                StateId = profile.StateId.GetValueOrDefault(),
                Zip = profile.Zip,
                ProfileTypeName = (profile.ProfileTypeMaster != null) ? profile.ProfileTypeMaster.ProfileType : "",
                RelationshipName = (profile.RelationshipMaster != null) ? profile.RelationshipMaster.Name : "",
                EmployerName = profile.EmployeeName,
                Occupation = profile.Occupation,
                OfficeAddressLine1 = profile.OfficeAddressLine1,
                OfficeAddressLine2 = profile.OfficeAddressLine2,
                OfficeCity = profile.OfficeCity,
                OfficeEmail = profile.OfficeEmail,
                OfficePhone = profile.OfficePhone,
                OfficeStateId = profile.OfficeStateId,
                OfficeZip = profile.OfficeZip,
                IsEmailConfirmed = profile.AspNetUser != null ? profile.AspNetUser.EmailConfirmed : false,
            };
        }

        public static IList<ProfileModel> MapToProfileModelList(IEnumerable<Profile> profiles)
        {
            var modelList = new List<ProfileModel>();

            foreach (var p in profiles)
            {
                ProfileModel pm = new ProfileModel();
                pm.ProfileId = p.ProfileId;
                pm.FirstName = p.FirstName;
                pm.LastName = p.LastName;

                modelList.Add(pm);
            }

            return modelList;
        }

    }
}