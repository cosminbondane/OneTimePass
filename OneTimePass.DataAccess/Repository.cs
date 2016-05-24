using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTimePass.DataAccess
{
    public class Repository<T> : IRepository<T> where T : new()
    {
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public T Get(Func<T, bool> where)
        {
            throw new NotImplementedException();
        }

        public T Insert(T item)
        {
            throw new NotImplementedException();
        }

        public bool Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
