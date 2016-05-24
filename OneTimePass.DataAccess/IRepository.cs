using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTimePass.DataAccess
{
    public interface IRepository<T> where T : new()
    {
        T Get(Func<T, bool> where);

        void Delete(string id);

        bool Update(T item);

        T Insert(T item);
    }
}
