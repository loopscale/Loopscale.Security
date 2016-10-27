using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loopscale.DataAccess.Repositories.Interfaces
{
    public interface IGenericRepository
    {
        List<KeyValuePair<int, string>> GetAllStates();
        List<KeyValuePair<int, string>> GetAllProfileTypes(); 
    }
}
