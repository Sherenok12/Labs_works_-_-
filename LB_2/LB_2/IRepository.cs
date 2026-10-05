using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB_2
{
    public interface IRepository<T>
    {
        void Add(T item);
        T? GetById(int id);
        IEnumerable<T> GetAll();
        bool Remove(int id);
    }
}
