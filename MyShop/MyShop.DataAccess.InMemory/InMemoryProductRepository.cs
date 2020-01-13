using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryProductRepository<T> where T: BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> list;
        string className;

        public InMemoryProductRepository()
        {
            className = typeof(T).Name;
            list = cache[className] as List<T>;
            if(list==null)
            {
                list = new List<T>();
            }
        }
        public void Commit()
        {
            cache[className] = list;
        }
        public void Insert(T item)
        {
            list.Add(item);
        }
        public T Find(string id)
        {
            return list.FirstOrDefault(a => a.Id == id);
        }
        public void Edit(T item)
        {
            var olditem = list.FirstOrDefault(a => a.Id == item.Id);
            if (olditem != null)
            {
                olditem = item;
            }
        }
        
        public void Delete(T Item)
        {
            list.Remove(Item);
        }
        public IEnumerable<T> Collection()
        {
            return list.AsEnumerable();
        }
    }
}
