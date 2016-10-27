using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loopscale.DataAccess.Repositories.Interfaces
{
    interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        T GetById<I>(I Id);

        T Add(T obj);

        bool Delete(T obj);
    }
}
