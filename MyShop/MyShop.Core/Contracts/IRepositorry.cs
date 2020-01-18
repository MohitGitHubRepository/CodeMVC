using System.Collections.Generic;
using System.Linq;
using MyShop.Core.Models;

namespace MyShop.Core.Contracts
{
    public interface IRepositroy<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        void Commit();
        void Delete(T Item);
        void Edit(T item);
        T Find(string id);
        void Insert(T item);
    }
}