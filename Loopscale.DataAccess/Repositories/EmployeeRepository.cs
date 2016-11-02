using Loopscale.DataAccess.EFModel;
using Loopscale.DataAccess.Repositories.Interfaces;
using Loopscale.Shared.MasterEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loopscale.DataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly LS_SecurityEntities _db = new LS_SecurityEntities();

        public EmployeeRepository()
        {
            _db.Configuration.ProxyCreationEnabled = false;
        }


        public Profile AddEmployeeProfile(Profile profile)
        {
            _db.Profiles.Add(profile);

            if (_db.SaveChanges() > 0)
            {
                return profile;
            }
            return null;
        }

        public IEnumerable<Profile> GetEmployeeProfiles()
        {
            return _db.Profiles.Where(u => u.ProfileTypeId == (int)Enums.ProfileTypesEnum.Employee);
        }

        public Profile GetEmployeeProfile(long id)
        {
            return _db.Profiles.FirstOrDefault(u => u.ProfileTypeId == (int)Enums.ProfileTypesEnum.Employee && u.ProfileId == id);
        }
    }
}
