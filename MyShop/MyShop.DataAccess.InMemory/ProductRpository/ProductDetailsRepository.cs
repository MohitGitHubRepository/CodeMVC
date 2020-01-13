using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory.ProductRpository
{
    public class ProductDetailsRepository
    {
        ObjectCache cache= MemoryCache.Default;
        
        List<Product> products;

        public ProductDetailsRepository()
        {
            products = cache["Products"] as List<Product>;
            if(products==null)
            {
                products = new List<Product>();
            }
        }

        public void Commit()
        {
            cache["Products"] = products;
        }
        public void Insert(Product product)
        {
            products.Add(product);
        }
        public void Edit(Product product)
        {
            var oldProduct = products.FirstOrDefault(a => a.Id == product.Id);
            if(oldProduct!=null)
            {
                oldProduct = product;
            }
        }

        public Product Find(string id)
        {
            return products.FirstOrDefault(a => a.Id == id);
        }
        public void Delete(Product product)
        {
            products.Remove(product);
        }
        public IEnumerable<Product> Collection()
        {
            return products.AsEnumerable();
        }
    }
}
