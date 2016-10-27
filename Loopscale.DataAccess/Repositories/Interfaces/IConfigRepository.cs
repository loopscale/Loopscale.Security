using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loopscale.DataAccess.Repositories.Interfaces
{
    public interface IConfigRepository
    {
        Dictionary<string, string> LoadAllConfig();
    }
}
