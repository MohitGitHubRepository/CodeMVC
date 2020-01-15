using System.Collections.Generic;
using MyShop.Core.Models;

namespace MyShop.Core.Contracts
{
    public interface IRepositroy<T> where T : BaseEntity
    {
        IEnumerable<T> Collection();
        void Commit();
        void Delete(T Item);
        void Edit(T item);
        T Find(string id);
        void Insert(T item);
    }
}