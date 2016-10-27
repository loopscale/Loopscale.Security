using Loopscale.DataAccess.EFModel;
using Loopscale.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loopscale.DataAccess.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private readonly LS_SecurityEntities _db = new LS_SecurityEntities();

        public GenericRepository()
        {
        }
        public List<KeyValuePair<int, string>> GetAllProfileTypes()
        {
            var lst =
               _db.ProfileTypeMasters.Select(s => new { s.ProfileTypeMasterId, s.ProfileType }).AsEnumerable()
                   .Select(t => new KeyValuePair<int, string>(t.ProfileTypeMasterId, t.ProfileType))
                   .ToList();

            return lst;
        }

        public List<KeyValuePair<int, string>> GetAllStates()
        {
            var lst =
                           _db.StateMasters.Select(s => new { s.StateMasterId, s.ShortName }).AsEnumerable()
                               .Select(t => new KeyValuePair<int, string>(t.StateMasterId, t.ShortName))
                               .ToList();

            return lst;
        }
    }
}
