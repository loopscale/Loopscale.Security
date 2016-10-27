using Loopscale.DataAccess.Repositories.Interfaces;
using Loopscale.DataAccess.EFModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Loopscale.Shared.MasterEnums;

namespace Loopscale.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LS_SecurityEntities _db = new LS_SecurityEntities();

        public UserRepository()
        {
            _db.Configuration.ProxyCreationEnabled = false;
        }

        public IEnumerable<Profile> GetProfiles()
        {
            return _db.Profiles.AsEnumerable();
        }

        public IEnumerable<AspNetUser> GetAllUsers()
        {
            return _db.AspNetUsers.AsEnumerable();
        }

        public Profile GetProfileByProfileId(long profileId)
        {
            var profile = _db.Profiles.FirstOrDefault(u => u.ProfileId == profileId);

            if (profile != null)
            {
                _db.Entry(profile).Reference(psc => psc.ProfileTypeMaster).Load();
                _db.Entry(profile).Reference(psc => psc.RelationshipMaster).Load();
            }

            return profile;
        }

        public Profile GetProfileByAspNetUserId(string aspNetUserId)
        {
            var profile = _db.Profiles.FirstOrDefault(u => u.AspNetUserId == aspNetUserId);

            if (profile != null)
            {
                return profile;
            }

            return null;
        }

        public Profile GetProfileByEmailId(string emailId)
        {
            var profile = _db.Profiles.FirstOrDefault(u => u.Email == emailId);

            if (profile != null)
            {
                return profile;
            }

            return null;
        }

        public Profile AddProfile(Profile profile)
        {
            _db.Profiles.Add(profile);

            if (_db.SaveChanges() > 0)
            {
                return profile;
            }

            return null;
        }

        public Profile AddProfileOnUserRegistration(Profile profile)
        {
            /*FAMILY ID IS A TIME FIELD WHICH HAS A POSSIBILITY OF BEING DUPLICATE. ADDING PROFILE-ID TO IT*/
            long? tempFamilyId = profile.FamilyId;
            profile.FamilyId = null;

            _db.Profiles.Add(profile);

            if (_db.SaveChanges() > 0)
            {
                //NOW ADD PROFILE-ID TO THE FAMILY-ID TO MAKE IT UNIQUE
                profile.FamilyId = Convert.ToInt64(Convert.ToString(tempFamilyId) + Convert.ToString(profile.ProfileId));
                _db.SaveChanges();
                return profile;
            }

            return null;
        }

        public bool UpdateProfile(long profileId, Profile updatedProfile)
        {
            var profile = _db.Profiles.FirstOrDefault(u => u.ProfileId == profileId);

            if (profile != null)
            {
                profile.RelationshipId = updatedProfile.RelationshipId;
                profile.ProfileTypeId = updatedProfile.ProfileTypeId;
                profile.DOB = updatedProfile.DOB;
                profile.Email = updatedProfile.Email;
                profile.HomePhone = updatedProfile.HomePhone;
                profile.Mobile = updatedProfile.Mobile;
                profile.Gender = updatedProfile.Gender;
                profile.FirstName = updatedProfile.FirstName;
                profile.LastName = updatedProfile.LastName;
                profile.FamilyName = updatedProfile.FamilyName;
                profile.HomeAddressLine1 = updatedProfile.HomeAddressLine1;
                profile.HomeAddressLine2 = updatedProfile.HomeAddressLine2;
                profile.City = updatedProfile.City;
                profile.StateId = updatedProfile.StateId;
                profile.Zip = updatedProfile.Zip;
                profile.EmployeeName = updatedProfile.EmployeeName;
                profile.Occupation = updatedProfile.Occupation;
                profile.OfficeAddressLine1 = updatedProfile.OfficeAddressLine1;
                profile.OfficeAddressLine2 = updatedProfile.OfficeAddressLine2;
                profile.OfficeCity = updatedProfile.OfficeCity;
                profile.OfficeStateId = updatedProfile.OfficeStateId;
                profile.OfficeZip = updatedProfile.OfficeZip;
                profile.OfficePhone = updatedProfile.OfficePhone;
                profile.OfficeEmail = updatedProfile.OfficeEmail;
                //Photo = profileModel.Photo,
                //profile.ImageId = imageName

                return _db.SaveChanges() > 0;
            }

            return false;
        }

        public IEnumerable<Profile> GetProfilesByRoleId(string roleId)
        {
            var role = _db.AspNetRoles.FirstOrDefault(r => r.Id == roleId);
            if (role != null)
            {
                var userIds = _db.AspNetUsers.Where(u => u.AspNetRoles.Any(r => r.Id == roleId) && u.EmailConfirmed == true).Select(s => s.Id);

                var profiles = _db.Profiles.Where(p => userIds.Contains(p.AspNetUserId));
                return profiles.AsEnumerable();
            }
            return null;
        }

        public IEnumerable<Profile> GetAllRegisteredProfiles(string email)
        {
            //return _db.Profiles.Where(p => p.AspNetUserId != null && p.ProfileTypeId == 3).AsEnumerable();
            return _db.Profiles.Where(p => p.Email.Contains(email)).Include("ProfileTypeMaster").Include("RelationshipMaster").Include(x => x.AspNetUser).AsEnumerable();
            //var matches = from m in db.Customers
            //where m.Name.Contains(key)
            //select m;
        }

        public IEnumerable<Profile> SearchProfiles(string email, string firstName, string lastName, string familyName)
        {
            var profiles = _db.Profiles.Include("ProfileTypeMaster").Include("RelationshipMaster").Include(x => x.AspNetUser);
            if (!string.IsNullOrEmpty(email))
            {
                profiles = profiles.Where(p => p.Email.ToUpper().Contains(email.ToUpper()));
            }
            if (!string.IsNullOrEmpty(firstName))
            {
                profiles = profiles.Where(p => p.FirstName.ToUpper().Contains(firstName.ToUpper()));
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                profiles = profiles.Where(p => p.LastName.ToUpper().Contains(lastName.ToUpper()));
            }
            if (!string.IsNullOrEmpty(familyName))
            {
                profiles = profiles.Where(p => p.FamilyName.ToUpper().Contains(familyName.ToUpper()));
            }
            return profiles.AsEnumerable();
        }

        public bool ActivateProfile(string userId)
        {
            var user = _db.AspNetUsers.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.EmailConfirmed = true;
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<KeyValuePair<long, string>> GetAllActiveProfiles()
        {
            var lst = _db.Profiles.Where(t => t.Status && t.ProfileTypeId == (int)Enums.ProfileTypesEnum.Child).ToList();
            var profiles = new List<KeyValuePair<long, string>>();

            if (lst != null && lst.Count > 0)
            {
                foreach (var item in lst)
                {
                    profiles.Add(new KeyValuePair<long, string>(item.ProfileId, item.FirstName + " " + item.LastName));
                }
            }

            return profiles;
        }

    }
}
