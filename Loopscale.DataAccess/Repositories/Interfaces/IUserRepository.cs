using Loopscale.DataAccess.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loopscale.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<AspNetUser> GetAllUsers();
        IEnumerable<Profile> GetProfiles();
        Profile GetProfileByProfileId(long profileId);
        Profile GetProfileByAspNetUserId(string aspNetUserId);
        Profile GetProfileByEmailId(string emailId);
        Profile AddProfile(Profile profile);
        Profile AddProfileOnUserRegistration(Profile profile);
        bool UpdateProfile(long profileId, Profile profile);
        IEnumerable<Profile> GetProfilesByRoleId(string roleId);
        bool ActivateProfile(string userId);
        IEnumerable<Profile> GetAllRegisteredProfiles(string email);
        IEnumerable<Profile> SearchProfiles(string email, string firstName, string lastName, string familyName);
        List<KeyValuePair<long, string>> GetAllActiveProfiles();
     }
}
