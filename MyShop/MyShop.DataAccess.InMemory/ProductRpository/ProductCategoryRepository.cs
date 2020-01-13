using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory.ProductRpository
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;

        List<ProductCategory> products;

        public ProductCategoryRepository()
        {
            products = cache["ProductCategory"] as List<ProductCategory>;
            if (products == null)
            {
                products = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["ProductCategory"] = products;
        }
        public void Insert(ProductCategory product)
        {
            products.Add(product);
        }
        public void Edit(ProductCategory product)
        {
            var oldProduct = products.FirstOrDefault(a => a.Id == product.Id);
            if (oldProduct != null)
            {
                oldProduct = product;
            }
        }
        public ProductCategory Find(string id)
        {
            return products.FirstOrDefault(a => a.Id == id);
        }
        public void Delete(ProductCategory product)
        {
            products.Remove(product);
        }
        public IEnumerable<ProductCategory> Collection()
        {
            return products.AsEnumerable();
        }

    }
}
