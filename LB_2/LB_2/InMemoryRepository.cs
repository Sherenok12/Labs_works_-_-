using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB_2
{
    public class InMemoryRepository<T> : IRepository<T>
    {
        private readonly List<T> _items = new();
        private readonly Func<T, int> _getId;

        public InMemoryRepository(Func<T, int> getId) => _getId = getId;

        public void Add(T item)
        {
            if (_items.Any(x => _getId(x) == _getId(item)))
                throw new InvalidOperationException("Елемент з таким Id вже існує");
            _items.Add(item);
        }

        public T? GetById(int id) => _items.FirstOrDefault(x => _getId(x) == id);
        public IEnumerable<T> GetAll() => _items;

        public bool Remove(int id)
        {
            var item = GetById(id);
            return item != null && _items.Remove(item);
        }
    }
}
