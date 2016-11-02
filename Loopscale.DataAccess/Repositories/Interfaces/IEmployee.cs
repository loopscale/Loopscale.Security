using Loopscale.DataAccess.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loopscale.DataAccess.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Profile AddEmployeeProfile(Profile profile);
        IEnumerable<Profile> GetEmployeeProfiles();
        Profile GetEmployeeProfile(long profileId);
    }
}
